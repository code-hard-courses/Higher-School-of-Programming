## Топологии репликации с несколькими ведущими узлами

**Топология репликации (replication topology)** описывает пути, по которым операции записи распространяются с одного узла на другой. В случае двух ведущих узлов существует только одна разумная топология: ведущий узел 1 должен отправлять информацию обо всех своих операциях записи ведущему узлу 2, и наоборот.

Наиболее общий вариант топологии в случае более чем двух ведущих узлов --  **каждый с каждым** , когда каждый ведущий узел отправляет информацию об операциях записи всем остальным ведущим узлам.

Более ограниченный вариант (например, в MySQL по умолчанию) --  **топология типа "кольцо"** , при которой каждый узел получает информацию об операциях записи от ровно одного "соседнего" (например, ближайшего физически) узла, и передаёт её (+ информацию о своих собственных операциях записи) ровно одному следующему узлу.

Еще одна популярная  **топология "звезда"** : один узел, назначенный корневым, пересылает информацию об операциях записи всем остальным узлам. Эта топология обобщается до дерева.

## Топологии репликации с несколькими ведущими узлами

Проблема 42. В топологиях типа "кольцо" и "звезда" операция записи должна пройти через несколько узлов, прежде чем попадёт во все реплики, и потенциально возможны бесконечные циклы репликации.

Решение. Всем узлам присваиваются уникальные идентификаторы, а каждая операция записи в журнале репликации оснащается идентификаторами всех узлов, через которые она прошла.

## Топологии репликации с несколькими ведущими узлами

Проблема 43. В случае топологий типа "кольцо" и "звезда" отказ даже одного узла может полностью прервать всю репликацию вплоть до момента его восстановления.

Решение. Использовать топологию "каждый с каждым".

## Топологии репликации с несколькими ведущими узлами

Проблема 44. В топологиях типа "каждый с каждым" одни сетевые ссылки могут оказаться быстрее других (например, из за перегруженности сети), в результате одни сообщения репликации станут существенно обгонять другие, и начнут возникать эффекты "скачков во времени".

Решение. Стратегически -- вообще отказаться от ведущих узлов (как? рассматриваем далее).


### Топологии репликации с несколькими ведущими узлами (задания)

======= 35. Наиболее общий вариант топологии репликации -- это ...

[1] каждый с каждым

[] "кольцо"

[] "звезда"

======= 36. В какой топологии операция записи проходит только через один узел?

[1] каждый с каждым

[] "кольцо"

[] "звезда"

======= 37. В какой топологии отказ любого узла может полностью прервать всю репликацию (выберите неверный ответ)?

[1] каждый с каждым

[] "кольцо"

[] "звезда"

======= 38. В какой топологии одни сетевые сообщения могут приходить быстрее других?

[1] каждый с каждым

[] "кольцо"

[] "звезда"
