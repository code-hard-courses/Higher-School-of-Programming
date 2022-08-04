пример1
=======
public static final String GRANT_TYPE_GUEST = "guest";

Параметр "guest" используется на протяжении всего приложения, но он неизменен по условию задачи и параметризовать его нет смысла.

пример2
=======
public X509Certificate getCertificate() throws Exception {
Resource resource = new ClassPathResource("VUser1.cer");
CertificateFactory cf = CertificateFactory.getInstance("X.509");
return (X509Certificate) cf.generateCertificate(resource.getInputStream());
}

Сертификат хранится в файле и считывается при прогонке локальных тестов.
Его удобно хранить в файле и легко корректировать при необходимости.
Он не влияет на работу приложения, поэтому его выгодно оставить в качестве внешнего ресурса.

пример3
=======
logback.xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
    <springProperty scope="context" name="springAppName" source="spring.application.name"/>
    <property name="LOG_FILE" value="${BUILD_FOLDER:-log}/microservices-${springAppName}.log"/>

    <include resource="org/springframework/boot/logging/logback/base.xml"/>
    <property name="FLUENTD_HOST" value="${FLUENTD_HOST:-${DOCKER_HOST:-localhost}}"/>
    <property name="FLUENTD_PORT" value="${FLUENTD_PORT:-24224}"/>
    <appender name="FLUENT" class="ch.qos.logback.more.appenders.DataFluentAppender">
        <tag>dab</tag>
        <label>normal</label>
        <remoteHost>${FLUENTD_HOST}</remoteHost>
        <port>${FLUENTD_PORT}</port>
        <!--<maxQueueSize>20</maxQueueSize>-->
    </appender>

    <appender name="FILE-PROFILE-LOAD-ANALYSIS" class="ch.qos.logback.core.rolling.RollingFileAppender">
        <file>log/load-analysis.log</file>

        <rollingPolicy class="ch.qos.logback.core.rolling.SizeAndTimeBasedRollingPolicy">
            <fileNamePattern>log/archived/load-analysis.%d{yyyy-MM-dd}.%i.log.gz</fileNamePattern>
            <!-- each archived file, size max 10MB -->
            <maxFileSize>10MB</maxFileSize>
            <!-- total size of all archive files, if total size > 20GB, it will delete old archived file -->
            <totalSizeCap>20GB</totalSizeCap>
            <!-- 60 days to keep -->
            <maxHistory>60</maxHistory>
        </rollingPolicy>

        <encoder>
            <pattern>%d %p %c{1.} [%t] %m%n</pattern>
        </encoder>
    </appender>

    <logger name="ru.vtb.dbo.user.load.analysis" level="debug" additivity="false">
        <appender-ref ref="FILE-PROFILE-LOAD-ANALYSIS"/>
    </logger>

    <logger name="fluentd" level="debug" additivity="false">
        <appender-ref ref="CONSOLE"/>
        <appender-ref ref="FILE"/>
        <appender-ref ref="FLUENT"/>
    </logger>
</configuration>

Данный файл настроек позволяет динамически настраивать конфигурацию логирования в приложении.
Параметры6 укзаанные в данном файле варируются от стенда к стенду, конфигурация на ПСИ отличается от параметров прода.
Таким образом, в данном случае динамическое связывание является наиболее выгодным.
