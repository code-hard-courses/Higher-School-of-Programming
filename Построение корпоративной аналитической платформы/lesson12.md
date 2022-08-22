# Практика

Сегодня вы преобразуете и загрузите данные из промежуточного хранилища на базе Managed Service for Kafka в витрину данных нашего аналитического хранилища.

## **Шаг 1**

Настроим кластер ClickHouse для интеграции с Apache Kafka. В [консоли управления Yandex Cloud](https://console.cloud.yandex.ru/) выберите  **Managed Service for ClickHouse** . В правой верхней части страницы нажмите  **Изменить кластер** . Откроется страница с параметрами кластера ClickHouse.

В нижней части страницы в блоке **Настройки СУБД** нажмите кнопку  **Настроить** . В открывшемся окне укажите параметры **Kafka:**

* **Sasl mechanism** *SCRAM-SHA-*
* **Sasl** **password:** в нашем случае *pass@word1*
* **Sasl username: в** нашем случае *inventory*
* **Security protocol** *SASL_SSL*

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/1789.png)

*Настройка параметров Kafka*

На данном этапе в таблице хранится избыточная иерархическая структура JSON с большой вложенностью. Для загрузки в таблицы ClickHouse её необходимо упростить.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/24312.png)

*Настройка параметров Kafka*

## **Шаг 2**

Для этого в настройках коннектора Debezium включим преобразование записи Single Message Transformation (SMT). В результате запись будет содержать только собственно данные, что позволит передать ее в ClickHouse с использованием выражения JSONEachRow ([https://debezium.io/documentation/reference/configuration/event-flattening.html](https://debezium.io/documentation/reference/configuration/event-flattening.html)).

1. Чтобы включить преобразование, измените настройки коннектора Debezium. Для этого добавьте в файл **register-connectors.http** следующие строки:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">"transforms" : "unwrap",
"transforms.unwrap.type" : "io.debezium.transforms.ExtractNewRecordState", </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

2. Выполните REST-запрос к сервису для применения настроек. Подробнее о работе с коннектором смотрите в [модуле](https://practicum.yandex.ru/trainer/ycloud-corp/lesson/fd9970d5-ec75-47be-bad2-42cad8b18901/)

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/1332.png)

*Применение настроек после REST-запроса*

3. Добавьте несколько записей в таблицу Products для проверки. Подключитесь к Microsoft SQL Server и выполните следующий SQL-запрос:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">INSERT INTO products(name,description,weight)
VALUES ('car battery','12V car battery',8.1);
INSERT INTO products(name,description,weight)
VALUES ('12-pack drill bits','12-pack of drill bits with sizes ranging from #40 to #3',0.8);
INSERT INTO products(name,description,weight)
VALUES ('hammer','12oz carpenter''s hammer',0.75);
INSERT INTO products(name,description,weight)
VALUES ('bike 12','Small 2-wheel scooter',3.14); </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

4. Убедитесь, что в кластере Kafka в топиках появились сообщения с преобразованной структурой:

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/4181.png)

*Содержимое топиков кластера Kafka*

5. Подключитесь к ClickHouse, используя DBeaver, и создайте таблицу ClickHouse для чтения топика Kafka. В редакторе введите запрос на создание таблицы  **kafka_store_data** :

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">CREATE TABLE kafka_store_data  (
       store_id  UInt32,
       store_name String,
       store_address Nullable(String),
       store_location Nullable(String),
       description Nullable(String)
) ENGINE = Kafka SETTINGS kafka_broker_list = '<kafka-broker-fqdn>:9091',
                kafka_topic_list = 'inventory.dbo.store_data',
                kafka_group_name = 'inventory-consumer-group',
                kafka_format = 'JSONEachRow'; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

В запросе замените *<kafka-broker-fqdn>* на хост кластера Kafka.

6. Создайте основную таблицу **ch_store_data** на кластере ClickHouse для репликации изменений. Для этого в редакторе введите запрос на создание таблицы:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">CREATE TABLE ch_store_data  ON CLUSTER  '{cluster}'(
        store_id  UInt32,
        store_name String,
        store_address Nullable(String),
        store_location Nullable(String),
        description Nullable(String)
) ENGINE = ReplicatedMergeTree('/clickhouse/tables/{shard}/ch_store_data', '{replica}')
ORDER BY (store_id); </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Если не используете распределенные таблицы, то задействуйте движок MergeTree без фразы ON CLUSTER.

7. Чтобы посмотреть содержимое созданной таблицы, выполните запрос:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">SELECT * FROM ch_store_data; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

8. Создайте материализованное представление, выполнив следующий запрос:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">CREATE MATERIALIZED VIEW materialized_store_data TO ch_store_data
               AS SELECT  store_id, store_name, store_address, store_location, description
FROM kafka_store_data; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

## **Шаг 3**

Для демонстрации сценария с построением отчетов в DataLens нам понадобится таблица, содержащая информацию о точках отгрузки, включая их географические координаты и адреса для отображения на карте. Создадим эту таблицу, включим для нее возможность CDC и заполним таблицу путем импорта данных из CSV-файла.

1. Подключитесь к Microsoft SQL Server, используя DBeaver, и выполните следующий скрипт:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">CREATE TABLE store_data (
       store_id bigint NULL,
       store_name nvarchar(11) NULL,
       store_address nvarchar(105) NULL,
       store_location varchar(27) NULL,
       description nvarchar(257) NULL
);

EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'store_data', @role_name = NULL, @supports_net_changes = 0; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollable__scrollbar scrollable__scrollbar_type_horizontal"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

В результате вы создадите таблицу store_data и включите CDC.

2. Заполните таблицу, импортировав данные из файла примера  *store_data.csv* : вызовите контекстное меню правой кнопкой мыши и выберите **Import** **Data.**
   []()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/5210.png)

   *Выбор импорта данных*
3. В открывшемся окне укажите источник CSV и целевую базу данных **Inventory.dbo.store_data.**

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/61234.png)

*Выбор импорта данных*

* В поле **Source** **Container** выберите файл *store_data.csv* с данными для импорта
* Укажите в параметрах импорта: **Column** **delimeter —**
* Нажмите **Next**

4. В открывшемся окне нажмите **Preview** **data** для просмотра импортируемой информации.

[]()![image](https://pictures.s3.yandex.net/resources/791_1656313821.png)

*Просмотр данных из импортируемого файла*

5. На следующем шаге проверьте настройки импорта и нажмите **Start** для запуска операции.

[]()![image](https://pictures.s3.yandex.net/resources/8_7_1656313856.png)

*Запуск импорта данных*

6. Убедитесь, что в Kafka был создан топик для таблицы store_data с данными JSON, соответствующие записям в таблицах.

[]()![image](https://pictures.s3.yandex.net/resources/9_6_1656313893.png)

*Успешно созданный топик в Kafka с JSON-данными*

7. Данные успешно переместились в витрину данных в ClickHouse. Для их просмотра выполните запрос:

<pre class="code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code">SELECT * FROM ch_store_data; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

[]()![image](https://pictures.s3.yandex.net/resources/10_5_1656313929.png)

*Данные успешно переместились в витрину ClickHouse*
