## Задержки репликации

Проблема 21. Любой highload-проект подразумевает достаточно высокую масштабируемость (обработка такого количества запросов, которое не под силу одной машине) и низкое время отклика (для чего реплики желательно размещать в географической близости от пользователей). При этом очень часто подавляющий объём запросов приходится на чтение, и ведущий узел ими перегружается.

Решение. Репликация с ведущим узлом требует, чтобы все операции записи проходили через один узел, но запросы только на чтение могут поступать на любой узел. Поэтому вполне возможно распределить запросы на чтение между всеми ведомыми узлами, что позволит физически ближайшим к пользователям репликам быстро обслуживать такие запросы.

Такая  **архитектура, масштабируемая по чтению (read-scaling architecture)** , позволяет повышать выдачу запросов на чтение просто добавлением новых ведомых узлов.

## Задержки репликации

Проблема 22. Если попытаться синхронно реплицировать базу на все ведомые узлы, то отказ одного узла или перебой в сети сделает всю систему недоступной для операций записи. А чем больше узлов, тем вероятнее отказ одного из них.

Решение. Архитектура, масштабируемая по чтению, реально работает только при асинхронной репликации.

## Задержки репликации

Проблема 23. Приложение, читающее данные с асинхронного ведомого узла, может получать устаревшую информацию, если такой узел запаздывает. Это приводит к очевидной несогласованности: при одновременном выполнении одного и того же запроса на ведущем и ведомом узлах результаты могут оказаться различными. Такая несогласованность (точнее,  **конечная во времени согласованность, eventual consistency** ) лишь временное состояние: если прекратить запись в базу и подождать, ведомые узлы постепенно согласуют данные с ведущим. При штатной эксплуатации подобная **задержка репликации (replication lag)** обычно составляет доли секунды, однако если система работает на пределе, или возникли проблемы с сетью, она легко может вырасти до нескольких секунд или даже минут.

Решение. В базовом варианте реализуется как минимум так называемая  **согласованность "чтение после записи" (read after-write consistency)** , или  **чтение своих записей (read-your writes)** . Она гарантирует, что например перезагрузив страницу, пользователь точно увидит внесенные им же изменения. Относительно других пользователей такая репликация ничего не обещает: внесённые ими изменения могут еще некоторое время не быть видны.

Данные, которые пользователь мог изменить, читаются с ведущего узла, а все остальные с одного из ведомых. Для этого нужно как-то маркировать информацию, которую нужно брать с ведущего узла. Это прикладной момент: например, информация из профиля пользователя в соцсети обычно доступна для редактирования только ему самому, поэтому вводится простое правило: всегда читать собственный профиль с ведущего узла, а профили других пользователей -- с ведомых узлов.

### Задержки репликации (задания)

======= 20. Подавляющий объём запросов обычно приходится на чтение, и ведущий узел ими перегружается

[ ] можно распределить запросы на чтение между всеми узлами

[1] можно распределить запросы на чтение между всеми ведомыми узлами

[ ] можно распределить запросы на чтение между всеми ведущими узлами

======= 21. Архитектура, масштабируемая по чтению, реально работает ...

[ ] всегда

[ ] только при синхронной репликации

[ 1] только при асинхронной репликации

======= 22. Запаздываающий ведомый узел может выдавать устаревшую информацию

[ ] применяем согласованность "чтение до записи"

[ 1] применяем согласованность "чтение после записи"

[ ] применяем согласованность, конечную по времени
