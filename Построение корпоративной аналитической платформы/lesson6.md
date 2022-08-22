# Практика по синхронизации данных из MySQL

Из описания сценария вы узнаете, как обеспечить периодическую доставку изменений из внешней базы данных в облако при помощи Data Transfer. Для синхронизации данных в вашем облаке нужно создать промежуточное хранилище данных — Managed Service for MySQL, в которое будут реплицироваться таблицы. Данные синхронизируются практически в режиме реального времени.

Для управления облачными ресурсами через командную строку установите и настройте интерфейс командной строки (CLI). Как это сделать, описано [здесь](https://cloud.yandex.ru/docs/cli/operations/install-cli).

📌 Обратите внимание, что в примерах даны команды для использования в Linux. Для улучшения читаемости используется символ переноса строки `\`.

Если вы используете командную строку ОС Windows, вводите команду в одну строку без символа переноса строки. Также можно воспользоваться командной строкой, [установив подсистему Windows для Linux (WSL)](https://docs.microsoft.com/ru-ru/windows/wsl/install).

## **Шаг 1**

Создадим виртуальный диск из образа с предварительно настроенной платформой интернет-магазина magento. Для этого откройте любую командную строку, например Git Bash, и введите команду:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ yc compute disk create \ 
--zone ru-central1-a \
--name web-store-lab-dataplatform \
--source-image-id fd8afehd5a5asg351p3n \
--folder-id <your-yc-folder-id> </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Замените <your-yc-folder-id> на идентификатор облачного каталога, в котором вы планируете разворачивать инфраструктуру. Консоль может переадресовать вас на [консоль управления Yandex Cloud](https://console.cloud.yandex.ru/) и предложить пройти аутентификацию. Дождитесь отработки команды. В случае успешного выполнения вы получите похожее сообщение:

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/%D0%A0%D0%B8%D1%81%D1%83%D0%BD%D0%BE%D0%BA%201.jpg)

*Сообщение об успешном выполнении аутентификации*

## **Шаг 2**

Теперь создадим виртуальную машину. Введите в консоли команду:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ yc compute instance create \
--name magento \
--zone ru-central1-a \
--network-interface subnet-name=default-ru-central1-a,nat-ip-version=ipv4 \
--hostname ya-sample-store \
--use-boot-disk disk-name=web-store-lab-dataplatform \
--folder-id <your-yc-folder-id> \
--ssh-key ~/.ssh/id_rsa.pub </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Чтобы создать виртуальную машину интернет-магазина и подключиться к ней, потребуется пара SSH-ключей. Если у вас ее нет, [создайте](https://cloud.yandex.ru/docs/compute/operations/vm-connect/ssh#creating-ssh-keys). После отработки команды вы увидите похожее сообщение:

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/243.png)

*Сообщение об успешном создании виртуальной машины*

Перейдите в консоль управления Yandex Cloud. В разделе Compute Cloud вы увидите созданную ВМ magento.

Если в вашем облаке включена [функциональность «Группы безопасности»,](https://cloud.yandex.ru/docs/vpc/operations/security-group-create)  то для передачи данных по сети нам нужно настроить параметры группы безопасности. Для этого перейдите в параметры ВМ, выберите раздел Сеть и нажмите ссылку с именем группы безопасности. Добавьте разрешение на входящий трафик с портов 80 и 443, а также с порта MySQL 3306. Для исходящего трафика можно разрешить весь диапазон портов.

📖 На момент написания курса функция «Группы безопасности» находится в Preview и включается по запросу. Если в вашем облаке эта функция не включена, пропустите шаг с настройкой групп безопасности.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/3321.png)

*Настройки параметров группы безопасности*

Подключитесь к ВМ по ssh:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">$ ssh yc-user@<ip-address-vm> </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**<ip-address-vm>** замените на публичный IP-адрес ВМ. В случае успешного подключения, вы увидите приветственное сообщение Linux:

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/417.png)

*Сообщение об успешном подключении*

## **Шаг 3**

Проверим подключение к сайту с интернет-магазином. Для того, чтобы можно было подключаться по прямой ссылке [http://ya-sample-store.local](http://ya-sample-store.local/), от имени администратора откройте файл hosts (C:\Windows\System32\drivers\etc\hosts для Windows и /etc/hosts для Linux) и добавьте строку:

**<ip-address-vm> ya-sample-store.local**

Первый раз сайт может загружаться довольно долго.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/519.png)

*Главная страница интернет-магазина*

Схему данных интернет-магазина можно посмотреть при помощи [DBeaver](https://dbeaver.io/download/).

При подключении нужно указывать:

* **Server** **Host:** <публичный_IP-адрес_виртуальной_машины>
* **Port:** 3306
* **Имя** **базы** **данных:** *ya_sample_store*
* **Имя пользователя:** *magento-svc*
* **Пароль:** *m@gent0*

## **Шаг 4**

Для реплицирования таблиц с информацией о заказах интернет-магазина создадим кластер Managed Service for MySQL:

1. В консоли управления выберите каталог, в котором нужно создать кластер БД.
2. Выберите сервис Managed Service for MySQL и нажмите **Создать кластер.**
3. Задайте имя кластера *ya-sample-cloud-mysql.*
4. Выберите класс хоста *s2.small (4 cores vCPU, 16 ГБ).*

В блоке **Размер хранилища** выберите:

* **Тип хранилища:** *network-ssd*
* **Объем:** *32 ГБ*

В блоке **База данных** укажите:

* **Имя базы данных:** *magento-cloud*
* **Имя пользователя:** *yc-user*
* **Пароль:** *12345678*

В блоке **Сетевые настройки** выберите облачную сеть для размещения кластера и группы безопасности для сетевого трафика кластера.

В блоке **Хосты** выберите параметры хостов БД, создаваемых вместе с кластером:

* **Зона доступности:** *ru-central1-a*
* **Подсеть:** *default-ru-central1-a*

Нажмите кнопку **Создать кластер**

[]()![image](https://pictures.s3.yandex.net/resources/611_1655982597.png)

*Созданный кластер Managed Service for MySQL*

Подробнее о создании кластера см. раздел [Как начать работать с Managed Service for MySQL](https://cloud.yandex.ru/docs/managed-mysql/quickstart#cluster-create.md).

## **Шаг 5**

Чтобы синхронизировать информацию о заказах из БД MySQL интернет-сайта с промежуточным хранилищем данных, которое находится в облаке, настроим Data Transfer:

1. В консоли управления выберите каталог, в котором нужно создать конфигурацию для подключения.
2. Выберите сервис Data Transfer и нажмите  **Создать эндпойнт** .

Определите параметры источника данных — виртуальной машины интернет-магазина с запущенным на ней экземпляром MySQL:

* **Имя:** *magento-source*
* Выберите из списка **тип БД:** MySQL
* **IP хоста:** <публичный_IP-адрес_виртуальной_машины>
* **Имя** **базы** **данных:** *ya_sample_store*
* **Имя пользователя:** *magento-svc*
* **Пароль:** *m@gent0*
* В **белом списке** укажите префиксы таблиц, которые подлежат репликации, например,  *sales_* *
* Нажмите кнопку **Создать**

Определите параметры приемника данных — управляемой базы данных Managed Service for MySQL, которая находится в облаке:

* **Имя:** *magento-report-dest*
* **База** **данных:** Managed Service for MySQL
* Выберите из списка **идентификатор кластера:** *ya-sample-cloud-mysql*
* **Имя** **базы** **данных:** *magento-cloud*
* **Имя пользователя** репликации: *yc-user*
* **Пароль:** *12345678*
* В строке **Отключение проверки констрейнтов** поставьте галочку. Если порядок передачи данных будет нарушен, сообщения об ошибках выдаваться не будут.
* Нажмите кнопку  **Создать** .

[]()![image](https://pictures.s3.yandex.net/resources/78_1655982853.png)

*Созданные кластеры источника и приемника данных*

Теперь создадим трансфер. Выберите в меню раздел Трансферы и нажмите кнопку  **Создать трансфер** . Далее определим параметры трансфера:

* **Имя:** *sales-order-sync*
* В блоке **Источник** выберите эндпойнт *magento-source.*
* В блоке **Приемник** выберите эндпойнт *magento-report-dest.*
* В блоке **Тип трансфера** выберите *Копировать и реплицировать.*
* Нажмите кнопку **Создать.**
  []()![image](https://pictures.s3.yandex.net/resources/86_1655982908.png)

  *Созданный трансфер*

Нажмите на три точки в строке с описанием трансфера и выберите  **Активировать** .

Будет выполнена первоначальная синхронизация схем данных и другой информации, а в дальнейшем данные будут автоматически синхронизироваться при появлении изменений в базе данных источника. Статус синхронизации и сообщения об ошибках можно найти в разделе Логи.

## **Шаг 6**

Проверим, что схемы базы данных появились в промежуточном хранилище:

1. Перейдите в раздел SQL стейджингового хранилища ya-sample-cloud-mysql.
2. Введите **имя пользователя** *yc-user* и **пароль** *12345678.*
3. Выберите БД *magento-cloud.*
4. Нажмите **Подключиться.**

В окне появится схема БД интернет-магазина.

[]()![image](https://pictures.s3.yandex.net/resources/95_1655982937.png)

*Схема базы данных интернет-магазина*

Проследите за переносом изменений в Yandex Cloud:

Создайте заказ на сайте интернет-магазина [http://ya-sample-store.local](http://ya-sample-store.local/).

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/104.png)

*Раздел заказа в интернет-магазине*

Выполните запрос к БД в облаке:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">SELECT so.*, soi.* FROM sales_order_grid so
INNER JOIN sales_order_item soi ON so.entity_id = soi.order_id
ORDER BY entity_id DESC
LIMIT 5 </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

[]()![image](https://pictures.s3.yandex.net/resources/113_1655982973.png)

*Данные заказа появились в базе*
