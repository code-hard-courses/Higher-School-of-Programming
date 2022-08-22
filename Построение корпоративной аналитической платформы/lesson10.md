# Практика

Из описания сценария вы узнаете, как организовать кластер Managed Service for ClickHouse с гибридным хранилищем, которое позволит управлять жизненным циклом данных.

## **Шаг 1**

Создадим кластер ClickHouse. В [консоли управления Yandex Cloud](https://console.cloud.yandex.ru/) выберите **Managed** **Service** **for**  **ClickHouse** . В правой верхней части страницы нажмите  **Создать кластер** . Откроется страница с параметрами кластера ClickHouse:

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/16919.png)

*Создание кластера Managed Sevice for ClickHouse*

1. В разделе **Базовые параметры** задайте имя кластера (в нашем примере  *inventory)* .
2. В разделе **База данных** задайте имя БД и имя пользователя как  *inventory* . Также задайте пароль для пользователя.
3. В разделе **Хосты** добавьте новый хост. Укажите зону доступности  *ru-central1-a* . Если Вы планируете подключаться к кластеру через интернет, нажмите на иконку редактирования хоста и включите настройку «Публичный доступ». Нажмите **Сохранить** . Подробнее о создании кластера см. раздел документации «[Создание ClickHouse-кластера](https://cloud.yandex.ru/docs/managed-clickhouse/operations/cluster-create)».
4. В разделе «Дополнительные настройки» включите опции:
   * **Гибридное хранилище,**
   * **Доступ из DataLens,**
   * **Доступ из консоли управления,**
   * **Доступ из Метрики и AppMetrica,**
   * **Доступ из Serverless.**

Опции позволяют интегрировать ClickHouse с другими сервисами облака.

5. Нажмите  **Создать кластер** .

Для обеспечения отказоустойчивости вы можете создать кластер из нескольких хостов. При этом дополнительные хосты будут учитываться в расчёте стоимости и квоте на облачные ресурсы. Обратите внимание, что в зависимости от типа кластера может меняться синтаксис запросов DDL (конструкция ON CLUSTER).

На создание кластера может уйти некоторое время. Когда кластер будет создан, его статус изменится на  **Alive** :

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/2432%201.png)

*Успешное создание кластера: появился статус Alive*

После создания кластера откройте DBeaver. В левом верхнем углу нажмите значок, чтобы создать соединение с источником данных. В открывшемся окне выберите тип **All** и нажмите на  **ClickHouse** :

[]()![image](https://pictures.s3.yandex.net/resources/Group_1222_1655984978.png)

*Выбор ClickHouse в общем списке*

Нажмите  **Далее** . Откроется окно с параметрами соединения:

[]()![image](https://pictures.s3.yandex.net/resources/4_17_1655985017.png)

*Параметры соединения в ClickHouse*

Укажите параметры:

* **Хост** кластера
* **Порт: 8443** (если вы настроили публичный доступ для нужного хоста) **8123** (если к хосту нет публичного доступа. Для подключения с таких виртуальных машин необязательно использовать SSL-соединение)
* **БД/Схема** — название БД. В нашем случае *inventory*
* **Пользователь** . В нашем случае *inventory*
* **Пароль** пользователя

Если вы настроили публичный доступ для нужного хоста, дополнительно нужно указать параметры SSL-соединения.  Для этого перейдите на вкладку **Driver properties** и задайте параметры:

* **SSL:** *true*
* **sslmode:** *strict*
* **sslrootcert:** путь к файлу на диске с CA сертификатом Yandex. Скачать файл сертификата можно по ссылке: [https://storage.yandexcloud.net/cloud-certs/CA.pem](https://storage.yandexcloud.net/cloud-certs/CA.pem)

Подробнее о подключении к кластеру см. раздел «[Подключение к базе данных в кластере ClickHouse](https://cloud.yandex.ru/docs/managed-clickhouse/operations/connect)» документации Yandex Cloud.

[]()![image](https://code.s3.yandex.net/Free%20courses/YCloud_corp/Group%201223.png)

*Настройка параметров SSL-соединения*

Нажмите  **Готово** , чтобы сохранить соединение. Во вкладке **Базы данных** появится БД с именем inventory:

[]()![image](https://pictures.s3.yandex.net/resources/6_11_1655985161.png)

*База данных inventory отобразилась в MySQL*

## **Шаг 2**

Теперь можно создать связь с таблицей во внешней БД MySQL. Конфигурирование интеграционных таблиц осуществляется с помощью запросов CREATE TABLE или ALTER TABLE. Настроенная интеграция выглядит как обычная таблица, но запросы к ней передаются во внешнюю систему.

Нажмите на БД inventory. Чтобы открыть редактор SQL, на панели сверху выберите **SQL**  **→ Новый редактор SQL** .

[]()![image](https://pictures.s3.yandex.net/resources/7_8_1655985396.png)

*Редактор SQL в панели DBeaver*

В редакторе введите запрос на создание таблицы  **mysql_sales_order_grid** :

<pre class="jsx code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code jsx">CREATE TABLE mysql_sales_order_grid  (
`entity_id` UInt32,
`status` Nullable(String),
`store_id` Nullable(UInt16),
`store_name` Nullable(String),
`customer_id` Nullable(UInt32),
`grand_total` Nullable(Float32),
`order_currency_code` String,
`created_at` DateTime,
`updated_at` DateTime,
`billing_address` Nullable(String),
`shipping_address` Nullable(String),   
`shipping_location` Nullable(String),
`shipping_information` Nullable(String),
`customer_email` Nullable(String),
`customer_group` Nullable(String)

)  ENGINE = MySQL('<my-yandex-cloud-cluster>.mdb.yandexcloud.net:3306', '<mysql-database-name>', 'sales_order_grid', '<mysql-user>', '<mysql-user-pwd>'); </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollable__scrollbar scrollable__scrollbar_type_horizontal"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

В запросе замените:

* **<my-yandex-cloud-cluster>** на хост кластера
* **<mysql-database-name>** на имя БД
* **<mysql-user>** на имя пользователя
* **<mysql-user-pwd>** на пароль пользователя

Чтобы отправить запрос, выделите его в окне редактирования и нажмите значок:

[]()![image](https://pictures.s3.yandex.net/resources/Group_1222_1_1655985668.png)

*Выделение запроса в окне редактирования*

Чтобы посмотреть содержимое созданной страницы, отправьте запрос:

<pre class="xml code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code xml">select * from mysql_sales_order_grid; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

[]()![image](https://pictures.s3.yandex.net/resources/9_5_1655985723.png)

*Исполнение запроса на создание таблицы mysql_sales_order_grid*

Далее отправьте запрос на создание таблицы  **sales_order** :

<pre class="jsx code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code jsx">CREATE TABLE sales_order (
`entity_id` UInt32,
`status` Nullable(String),
`store_id` Nullable(UInt16),
`store_name` Nullable(String),
`customer_id` Nullable(UInt32),
`grand_total` Nullable(Float32),
`order_currency_code` String,
`created_at` DateTime,
`updated_at` DateTime,
`billing_address` Nullable(String),
`shipping_address` Nullable(String),   
`shipping_location` Nullable(String),
`shipping_information` Nullable(String),
`customer_email` Nullable(String),
`customer_group` Nullable(String)

) ENGINE = ReplacingMergeTree
  PARTITION BY toYYYYMM(`created_at`)
  ORDER BY (`entity_id`)
  TTL `created_at` + INTERVAL 1 MONTH TO DISK 'object_storage'; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

**ReplacingMergeTree** позволяет исключить дубликаты записей.

**TTL** (Time to Live) указывает, что через месяц после создания записи она будет перенесена в Yandex Object Storage в системный раздел S3.

Обратите внимание, что в примере приведен синтаксис запроса для случая, когда БД ClickHouse развернута на одном узле. Если вы развернули кластер ClickHouse, то для создания таблицы на всех репликах кластера нужно использовать синтаксис распределенного DDL (подробнее см. [https://clickhouse.com/docs/en/sql-reference/distributed-ddl/](https://clickhouse.com/docs/en/sql-reference/distributed-ddl/))

<pre class="jsx code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code jsx">CREATE TABLE sales_order **ON CLUSTER  `{cluster}`**(
`entity_id` UInt32,
`status` Nullable(String),
`store_id` Nullable(UInt16),
`store_name` Nullable(String),
`customer_id` Nullable(UInt32),
`grand_total` Nullable(Float32),
`order_currency_code` String,
`created_at` DateTime,
`updated_at` DateTime,
`billing_address` Nullable(String),
`shipping_address` Nullable(String),   
`shipping_location` Nullable(String),
`shipping_information` Nullable(String),
`customer_email` Nullable(String),
`customer_group` Nullable(String)

) ENGINE = ReplacingMergeTree(**'/clickhouse/tables/{shard}/sales_order', '{replica}'**) --
  PARTITION BY toYYYYMM(`created_at`)
  ORDER BY (`entity_id`)
  TTL `created_at` + INTERVAL 1 MONTH TO DISK 'object_storage'; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Выполните запрос, который копирует данные из таблицы mysql_sales_order_grid в таблицу sales_order.

<pre class="xml code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code xml">INSERT INTO inventory.sales_order (entity_id, status, store_id, store_name, customer_id, grand_total, order_currency_code, created_at, updated_at,
billing_address, shipping_address, shipping_location, shipping_information, customer_email, customer_group)
SELECT  entity_id, status, store_id, store_name, customer_id, grand_total, order_currency_code, created_at, updated_at,
billing_address, shipping_address, shipping_location, shipping_information, customer_email, customer_group
FROM mysql_sales_order_grid
WHERE entity_id > (SELECT MAX(entity_id) from sales_order); </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollable__scrollbar scrollable__scrollbar_type_horizontal"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Чтобы посмотреть содержимое страницы sales_order, отправьте запрос:

<pre class="xml code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code xml">select * from sales_order so; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

Чтобы оценить объем данных, который находится на сетевых дисках и в Object Storage, отправьте еще один запрос:

<pre class="xml code-block code-block_theme_light"><div class="code-block__tools"><span class="code-block__clipboard">Скопировать код</span><span class="code-block__lang">JSX</span></div><div class="scrollable-default scrollable scrollable_theme_light code-block__scrollable prisma prisma_theme_light"><div></div><div class="scrollable__content-wrapper"><div class="scrollbar-remover scrollable__content-container"><div class="scrollable__content"><div class="code-block__code-wrapper"><code class="code-block__code xml">SELECT * FROM system.disks; </code></div><div></div></div></div></div><section class="scrollbar-default scrollbar scrollbar_vertical scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_vertical" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section><section class="scrollbar-default scrollbar scrollbar_horizontal scrollbar_hidden scrollable__scrollbar scrollable__scrollbar_type_horizontal" size="1"><div class="scrollbar__control-container"><div class="scrollbar__control"><div class="scrollbar__control-line"></div></div></div></section></div></pre>

В ответе вы должны получить информацию об используемом пространстве кластера ClickHouse с гибридным хранилищем:

[]()![image](https://pictures.s3.yandex.net/resources/10_4_1655985767.png)

*Кластер ClickHouse с данными гибридного хранилища*

Мы реализовали гибридное хранилище: часто используемые данные хранятся в кластере Managed Service for ClickHouse, а архивные данные — в Yandex Object Storage.
