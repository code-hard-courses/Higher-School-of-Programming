# Практика по синхронизации данных из MS SQL Server

Из описания сценария вы узнаете, как обеспечить передачу данных о складских остатках из внешней базы SQL Server в облако при помощи Debezium CDC. Аналогичный подход будет работать для Oracle, DB2 и других БД, поддерживаемых Debezium. Для того, чтобы извлечь информацию из БД SQL Server, мы должны разрешить CDC сначала на уровне базы данных, а потом и для всех нужных таблиц. Изменения будут передаваться в Apache Kafka в облаке — Managed Service for Kafka.

## **Шаг 1**

Создайте виртуальную машину по [инструкции](https://cloud.yandex.ru/docs/compute/operations/vm-create/create-linux-vm).

Если в вашем облаке включена [функциональность «Группы безопасности»,](https://cloud.yandex.ru/docs/vpc/operations/security-group-create)  то для передачи данных по сети нужно настроить параметры группы безопасности. Для этого перейдите в параметры ВМ, выберите раздел **Сеть** и нажмите ссылку с именем группы безопасности. Добавьте разрешение на входящий трафик с портов 80, 443, 9091, 8083, а также с порта SQL Server 1433.

📖 На момент написания курса функция «Группы безопасности» находится в Preview и включается по запросу. Если в вашем облаке эта функция не включена, пропустите шаг с настройкой групп безопасности.

## **Шаг 2**

Создадим кластер Kafka:

1. В консоли управления выберите каталог, в котором нужно создать кластер БД.
2. Выберите сервис Managed Service for Kafka и нажмите **Создать кластер.**
3. Задайте **Имя кластера** *inventory-cluster.*
4. Поставьте галочку  **Управление топиками через API** . Она включает режим unmanaged-topics, который позволяет использовать API внешних приложений для создания данных в кластере Apache Kafka.
5. Включите **Публичный доступ** для обращения к кластеру вне облака.
6. В разделе **Сетевые настройки** укажите только одну зону доступности, потому что в рамках данной лабораторной работы не требуется отказоустойчивая конфигурация.
7. Остальные параметры оставьте по умолчанию.
8. Нажмите кнопку  **Создать кластер** .

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/169.png)

*Созданный кластер Kafka*

## **Шаг 3**

Создадим пользователя кластера Managed Service for Kafka с правами администратора, воспользовавшись интерфейсом командной строки (CLI). Инструкцию по установке и настройке CLI см. [здесь](https://cloud.yandex.ru/docs/cli/operations/install-cli).

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ yc managed-kafka user create inventory --cluster-name inventory-cluster --folder-id <your-yc-folder-id> --password=pass@word1 --permission topic="*",role=ACCESS_ROLE_ADMIN </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollable__scrollbar scrollable__scrollbar_type_horizontal"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**<your-yc-folder-id>** замените на идентификатор своего облачного каталога.

Данные будут передаваться в Apache Kafka в формате *.json. В данном случае стейджинговое хранилище выступает в роли платформы массивной передачи сообщений. Сначала передаются снапшоты, а потом и изменения.

Чтобы для каждой таблицы автоматически создавался собственный топик, установите флаг **auto-create-topics-enable:**

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ yc managed-kafka cluster update --id <cluster_id> --auto-create-topics-enable </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**<cluster_id>** замените на идентификатор кластера.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/2431.png)

*Сообщение об успешном создании кластера Managed Service for Kafka*

## **Шаг 4**

Настроим Debezium, воспользовавшись преднастроенным контейнером. Фактически это стандартный контейнер, в котором дополнительно установлен корневой сертификат Yandex Cloud и создано хранилище ключей **client.truststore.jks** с паролем *pass@word1**.*** В него также добавлен корневой сертификат Yandex Cloud, чтобы упростить задачу аутентификации в процессе выполнения работы.

1. Подключитесь к ВМ по SSH:
   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ ssh yc-user@<ip-address-vm> </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**<ip-address-vm>** замените на публичный IP-адрес ВМ.

2. Установите Git:

   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ sudo apt install git </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>
3. Клонируйте репозиторий с исходным кодом:

   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ git clone https://github.com/MaxKhlupnov/yc-cdc-datamart </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>
4. Перейдите в каталог **~/yc-cdc-datamart/debezium-cdc/&**
5. Установите Docker:

   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ sudo apt install docker.io </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>
6. Запустите контейнер:

   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ docker build -t yc-connect -f ./yc-connect/Dockerfile yc-connect/. </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>
7. Добавьте текущего пользователя в группу докеров:

   <pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ sudo gpasswd -a $USER docker
   $ newgrp docker </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

   []()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/332.png)

   *Успешно запущенный контейнер*
   Информация для подключения к Apache Kafka и к SQL Server находится в файлере  **~/yc-cdc-datamart/docker-compose.yaml** . Укажите в строке BOOTSTRAP_SERVERS адрес хоста своего кластера Kafka:
   []()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/4171.png)

   *Содержимое docker-compose*
8. Установите docker-compose:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ sudo apt install docker-compose </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

9. Запустите команду:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ docker-compose up </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

В результате развернутся два контейнера: в одном из них Kafka, в другом — SQL Server. Таким образом мы эмулируем распределенную инфраструктуру.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/5191.png)

*Эмуляция двух контейнеров с Kafka и SQL Server*

Как только запускаются модули Debezium, они автоматически подключаются к Managed Apache Kafka с использованием тех характеристик, которые мы указали при настройке, и создают системные топики, которые необходимы для передачи изменений — offset, config и status. Эти топики показывают смещение между чтением и текущим положением временного указателя, конфигурацию и информацию об ошибках.

**Обратите внимание,** что при разрыве SSH-сессии выполнение контейнеров завершится и стенд перестанет работать. Чтобы выполнение контейнеров продолжилось после завершение SSH-сессии, используйте следующий синтаксис команды:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ docker-compose up -d </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**Флаг -d** запускает docker compose в фоновом режиме.

Если нужно получить доступ к логам контейнеров, используйте команду  **docker logs <имя_контейнера>** .

## **Шаг 5**

Следующим шагом нам необходимо настроить получение информации из SQL Server.

Для отображения схемы БД воспользуемся [SQL Server management studio (SSMS)](https://docs.microsoft.com/ru-ru/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15). **Альтернативно можно использовать dBeaver.**

В параметрах соединения укажите:

* **Адрес виртуальной машины** с контейнером Debezium
* **Тип аутентификации:** *SQL* *Server* *Authentication*
* **Имя пользователя:** *sa*
* **Пароль:** *Password!*

Используем скрипт **~/yc-cdc-datamart/debezium-cdc/SQL/inventory-mssql.sql,** чтобы сначала включить CDC, а затем заполнить данными нашу базу.

Используем скрипт **~/yc-cdc-datamart/debezium-cdc/SQL/starting-agent.sql** для включения SQL Server Agent: он создаст задания, которые будут обеспечивать синхронизацию. Убедитесь, что SQL Server Agent запущен, а задания для формирования таблицы изменений и для обеспечения очистки успешно создались. Подробнее см. в [документации Microsoft](https://docs.microsoft.com/en-us/sql/relational-databases/track-changes/about-change-data-capture-sql-server?view=sql-server-ver15#agent-jobs).

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/6119.png)

*Успешно запущенный SQL Agent Server*

Чтобы наблюдать за процессом передачи сообщений и создания топиков в графическом интерфейсе Kafka, установите инструмент [Conduktor](https://www.conduktor.io/).

Альтернативно можете воспользоваться утилитой командной строки *kafkacat.* Процесс ее подключения и использования подробно описан в [документации](https://cloud.yandex.ru/docs/managed-kafka/operations/connect#bash).

1. Создайте подключение к кластеру.
2. Сохраните файл сертификата  **client.truststore.jks** , находящийся в репозитории, на локальный компьютер и пропишите путь к нему.
3. В строке **bootstrap-servers** пропишите адрес хоста своего кластера Kafka.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/781.png)

*Настройка Kafka Cluster*

1. Сохраните параметры и откройте созданное подключение. Проследите, что автоматически создались системные топики (offsets, configs и statuses), о которых говорилось выше.

## **Шаг 6**

Запустим процесс синхронизации, для чего настроим клиента Debezium, выполнив REST-запрос к сервису. Для этого можно воспользоваться [VS Code](https://code.visualstudio.com/) с установленным plugin [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) или командой curl.

Отредактируйте параметры запроса в файле, укажите идентификатор кластера Kafka, FQDN или IP-адрес ВМ, на которой запущен контейнеры с SQL Server.

**Если используете VS Code** , примените шаблон из файла  **~/yc-cdc-datamart/debezium-cdc/SQL/register-connectors.http** , а затем выполните запрос нажатием Send Request.

**Если используете curl,** примените шаблон из файла  **~/yc-cdc-datamart/debezium-cdc/SQL/register-connector.json** , а затем выполните запрос:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ curl -i -X POST -H "Accept:application/json" -H  "Content-Type:application/json" http://localhost:8083/connectors/ -d @register-connector.json </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollable__scrollbar scrollable__scrollbar_type_horizontal"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Убедитесь, что:

* в кластере создались топики для каждой таблицы БД MS SQL Server, для которой был настроен CDC;
* в топиках таблиц появились JSON, соответствующие записям в таблицах.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/861.png)

*Запущенная синхронизация с торговыми остатками через Debezium*
