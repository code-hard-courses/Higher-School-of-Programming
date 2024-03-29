## Повторное выполнение транзакций

Важная отличительная особенность транзакций -- возможность их прерывания и безопасного повторного выполнения в случае возникновения ошибки. На этом принципе и построены базы данных в модели ACID: при возникновении риска нарушения гарантий атомарности, изоляции или сохраняемости СУБД скорее полностью отменит транзакцию, нежели оставит её незавершенной.

Проблема 16. Хранилища данных, использующие репликацию без ведущего узла, работают скорее по принципу "лучшее из возможного": база данных делает всё, что может, и при столкновении с ошибкой не откатывает уже выполненные действия, поэтому восстановление после ошибок перекладывается на приложение.

Например, популярные фреймворки объектно-реляционного отображения (ORM) вроде Django не повторяют попытку выполнения прерванных транзакций: ошибка в них обычно приводит к "всплытию" исключения по стеку, так что все введенные пользователем данные теряются, а сам он получает сообщение об ошибке. Это поразительно, ведь весь смысл прерывания транзакций как раз в обеспечении возможности безопасного их повторения.

Решение. Применять механизм повторного выполнения транзакций в модели ACID по возможности всегда.

## Повторное выполнение транзакций

Проблема 17. Вероятны ситуации, когда транзакция была выполнена успешно, но произошёл сбой сети при передаче клиенту подтверждения её успешной фиксации. Вследствие этого клиент полагает, что она завершилась неудачей, и повторяет её второй раз. Это может привести как к нарушению логически целостных данных (появлению двойников), так и к дополнительной перегрузке самой СУБД, когда повтор транзакций только усугубляет проблему.

Решение. Для страховки от таких случаев приходится предусматривать дополнительный  **механизм дедупликации на уровне приложения** .

Можно ограничивать количество повторов транзакций (с экспоненциальной отсрочкой отправки), контролировать перегрузку БД и ошибки перегруженности обрабатывать особым образом.

## Повторное выполнение транзакций

Проблема 18. Некоторая транзакция может отменяться СУБД всегда -- например, при нарушении ограничений целостности.

Решение. Попытка повтора выполнения при постоянной ошибке бессмысленна. Имеет смысл повторно выполнять транзакции только для временных ошибок -- происходящих, например, из-за взаимной блокировки, нарушения изоляции, сбоев сети и т. п.

## Повторное выполнение транзакций

Проблема 19. У транзакций могут быть побочные действия за пределами базы данных, которые могут выполняться без возможности отката даже в случае её прерывания. Например, вряд ли имеет смысл повторять отправку электронной почты при каждой попытке повтора прерванной транзакции.

Решение. Если нужны гарантии, что сразу несколько различных систем, а не только одна СУБД, будут только все вместе либо фиксировать изменения, либо прерывать транзакцию, может оказаться полезна двухфазная фиксация транзакции (см. далее).

### Транзакции (задания)

======= 11. Для гарантий функциональной безопасности в реляционных СУБД ...

[ ] достаточно однообъектных транзакций

[1 ] нужны многообъектные транзакции

[ ] нужна консистентность

======= 12. Транзакции в NoSQL-системах ...

[ ] аналогичны транзакциями в реляционных СУБД

[ ] никак не помогают c денормализованными данными

[ 1] полезны для предотвращения рассогласования данных

======= 13. Проблема с корректным обновлением вторичных индексов хорошо решается ...

[ ] атомарностью операций

[ ] специальной внутренней механикой СУБД

[ 1] транзакциями, где индексы это обычные объекты БД
