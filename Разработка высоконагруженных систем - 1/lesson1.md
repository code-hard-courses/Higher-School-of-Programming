## Часть 1. Введение в HighLoad

**Highload (высоконагруженные приложения)** -- это универсальный термин для приложений, систем, архитектур, устойчиво и надёжно поддерживающих работу при некоей высокой нагрузке.

Что будем понимать под  **высокой нагрузкой** ? Ведь для одной системы нагрузка в 38 попугаев очень высока, а для другой почти не заметна.

Высокая нагрузка -- это не конкретный количественный критерий, а  **класс инженерных ситуаций, когда проект сталкивается с реальными сложностями в процессе своего роста** .

Типичные вопросы, которые возникают, когда разрабатывается highload-система:

- как обеспечить полноту и корректность данных?
- что делать при внутренних ошибках системы?
- как минимизировать лаг для всех клиентов в случае отключения некоторых подсистем?
- как обеспечить масштабирование под растущую нагрузку?

*По материалам учебника
**"Designing Data-Intensive Applications: The Big Ideas Behind Reliable, Scalable, and Maintainable Systems"**
автор  **Martin Kleppmann** .*

На практике необходимо различать как минимум два класса highload-систем (и сам термин highload, кстати, инженеры не очень уважают, считая слишком попсовым):

1) **Приложения, высоконагруженные данными (data-intensive applications, DIA)** -- класс ситуаций, когда проблемой становится объём, качество или сложность данных, обрабатываемых системой.
2) **Приложения, высоконагруженные вычислениями (compute-intensive, CIA)** -- класс ситуаций, когда проблемой становится нагрузка на процессоры (их ресурса не хватает).

В дальнейшем под системой будем понимать высоконагруженную, highload-систему.


## 1. Введение в HighLoad (задания)

======= 1. Относится ли задача "как правильно спроектировать хороший API для highload-системы" к теме данного курса?

[x] да

[] нет

======= 2. Относится ли проблема с нехваткой оперативной памяти к теме высоконагруженных вычислений?

[x] да

[] нет

======= 3. Относится ли проблема с высокой скоростью изменения обрабатываемых данных к теме приложений, высоконагруженных данными?

[x] да

[ ] нет
