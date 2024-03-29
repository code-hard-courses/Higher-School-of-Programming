## 4. Удобство сопровождения

На практике существенная доля (причём часто более 50%) стоимости программной системы приходится не на начальную разработку, а на **сопровождение** системы (непосредственная эксплуатация, исправление багов, расширение под новые сценарии использования, адаптация к новым платформам и т. д.).

Удобство сопровождения обычно рассматривается в контексте трёх принципов:

- **удобство эксплуатации** : насколько легко и просто обслуживающему персоналу обеспечивать устойчивую работу системы;
- **простота для разработчиков** : насколько легко новым инженерам и программистам разобраться в устройстве системы и приступить к её улучшению и расширению. Это достигается прежде всего через максимально возможное её упрощение;
- **возможность развития** (продолжение предыдущего пункта): насколько легко и быстро действующим инженерам и программистам удастся в будущем вносить в систему новые изменения и улучшения, адаптировать её при возникновении непредвиденных сценариев использования, принципиально новых требований и т. д. Этот принцип известен также как расширяемость (extensibility), модифицируемость (modifiability), пластичность (plasticity).

Один из лучших подходов для достижения максимальной простоты -- это  **абстракция** . Хорошая абстракция позволяет скрыть большую (или даже всю) часть подробностей реализации за простым, аккуратным и понятным интерфейсом. Хорошую абстракцию можно успешно и многократно применять в самых разных проектах. Например, в ООП такая сильная абстракция -- это абстрактный тип данных, который мы проходили на соответствующих курсах по ОО-проектированию.

Однако найти и формализовать хорошую абстракцию сложно. Хотя в области highload-систем реализовано множество проектов и имеется много эффективных алгоритмов и локальных инженерных шаблонов, однако не известно хорошей формальной методики, позволяющей создавать из них сильные абстракции, которые как минимум сдерживали бы рост сложности проекта. Поэтому на курсе мы отслеживаем хорошие абстракции, выделяющие части большой системы в чётко ограниченные компоненты, допускающие повторное использование.

**Надежность** обеспечивает правильную работу системы даже в случае сбоев в аппаратном обеспечении (чаще всего случайные и невзаимосвязанные) и в софте (тут наоборот ошибки обычно носят системный характер и слабо контролируемы), и вследствие человеческих ошибок (люди неминуемо допускают ошибки с некоторой периодичностью).

**Масштабируемость** предлагает ряд стратегий обеспечения хорошей производительности даже в случае значительного роста нагрузки. Для этого необходимы прежде всего способы количественного описания нагрузки и производительности.

**Удобство сопровождения** , по существу, подразумевает облегчение жизни разработчикам и техническому персоналу. Помочь в этом могут хорошие абстракции, а также хорошо организованный мониторинг состояния системы и эффективные способы управления им.

К сожалению, не существует лёгкого пути для обеспечения надежности, масштабируемости и удобства сопровождения программ. Однако в программной инженерии выявлены успешные паттерны и методики, которые снова и снова встречаются в самых разных видах приложений.

## Удобство сопровождения (задания)

======= 11. Какой процент всех усилий в проекте приходится обычно на исправление и отладку багов на всех его этапах?

[ ] 0-20%

[ ] 21-50%

[1 ] 51-80%

[ ] 81-100%

======= 12. Относится ли к удобству сопровождения удобство работы с системой для пользователей (удобство UI)?

[ ] да

[ 1] нет

======= 13. Относится ли к удобству сопровождения выявление узких мест в системе?

[1 ] да

[ ] нет
