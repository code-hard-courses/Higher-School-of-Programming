### Проблемы оптимизации

Имеется определённая сложность при обновлении страниц на месте: при одновременном доступе нескольких потоков к B-дереву необходима аккуратность в управлении конкурентным доступом, в противном случае поток может просмотреть дерево в несогласованном состоянии (эти проблемы мы изучали на первом курсе по парадигмам программирования).

Механизм корректного доступа обычно реализуется путём защиты структур данных дерева с помощью **защёлок (latch)** -- облегчённого варианта блокировок. Журналированный подход в этом смысле проще, поскольку слияние происходит в фоновом режиме, без создания помех входящим запросам, с периодической атомарной заменой старых сегментов новыми.

Более того, для отдельных операций необходимо перезаписать несколько различных страниц. Например, при разбиении страницы из-за её переполнения вследствие вставки необходимо записать две новые страницы, а также перезаписать их родительскую страницу для обновления ссылок на две дочерние страницы. Это опасная операция: ведь в случае фатального сбоя базы данных в тот момент, когда записана только часть страниц, индекс окажется повреждён. Например, может возникнуть так называемая  **бесхозная (orphan) страница** , у которой нет ни одного родителя.

Чтобы обеспечить отказоустойчивость БД, реализации B-деревьев обычно включают дополнительную структуру данных на диске:  **журнал упреждающей записи (write-ahead log, WAL)** , также именуемый  **журналом повтора (redo log)** . Он представляет собой файл, предназначенный только для добавления, в который все модификации B-деревьев записываются ещё до того, как применятся к самим страницам дерева. Когда база возвращается в норму после сбоя, этот журнал используется для восстановления и приведения B-дерева в согласованное состояние.

### Проблемы оптимизации - 2

Некоторые СУБД применяют также схему копирования при записи. Изменённая страница записывается в другое место с созданием в дереве новых версий родительских страниц, указывающих на это новое местоположение. Такой подход полезен и для управления конкурентным доступом.

В целях оптимизации иногда экономят место на страницах, сохраняя не весь ключ, а только его сокращенный вариант.

В дерево также могут добавляться дополнительные указатели. Например, каждая страница-лист может ссылаться на страницы своего уровня слева и справа, что позволяет упорядоченно просматривать ключи без возврата к родительским страницам.

Существуют также такие варианты B-деревьев, как **фрактальные деревья**  *(но у них нет ничего общего с фракталами :)* , которые заимствуют некоторые идеи у журналированных индексов, чтобы снизить количество необходимых переходов к позициям на диске.


## Проблемы оптимизации (задания)

======= 51. При одновременном доступе к БД нескольких потоков могут возникнуть проблемы с ...

[1 ] B-деревом

[ ] журналированием

[ ] LSM-деревом

======= 52. При фатальном сбое СУБД может возникнуть

[ 1] бесхозная страница в дереве

[ ] бесхозный сегмент в журнале

======= 53. Журнал упреждающей записи нужен для обеспечения надёжности

[ 1] B-дерева

[ ] журналирования

[ ] LSM-дерева
