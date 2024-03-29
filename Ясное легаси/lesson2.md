### В систему надо внести изменения (добавить новую фичу или изменить существующую)

1. Проблема. Обычно заранее неизвестно, какое именно поведение системы с обоих точек зрения (добавление новой фичи или изменение существующей) будет подвергнуто риску быть затронутым побочными эффектами при внесении изменений.

Решение. Ищем максимально точные и конкретные ответы на вопросы

1. Какие именно изменения нам нужно сделать?
2. Как мы узнаем, что сделали их правильно?
3. Как мы узнаем, что ничего не поломали в системе?
4. До какой степени мы можем вносить изменения, чтобы не появился риск побочных эффектов?

### В систему надо внести изменения (добавить новую фичу или изменить существующую)

2. Проблема. Если избегать создания новых классов и функций, то придётся укрупнять существующие классы и усложнять их понимание.

Решение. Никогда так не делать, никаких "приходится укрупнять", никто вас к этому не принуждает. Всегда надо соблюдать принцип единственной ответственности SRP -- ключевой принцип при разбирательстве с легаси-кодом и в разрыве зависимостей.

### В систему надо внести изменения (добавить новую фичу или изменить существующую)

3. Проблема. Вы тщательно планируете изменения, сперва убедившись, что хорошо понимаете изменяемый код. По завершении вы запускаете систему, чтобы убедиться в работоспособности изменений, и тщательно проверяете, не сломалось ли что-нибудь. Обязательная составляющая данного способа -- выискивание возможных проблем и тщательная проверка. Фактически, вы вносите изменения наудачу, и просто надеетесь, что они сделаны правильно, поэтому требуется дополнительное время, чтобы в этом убедиться.
   Этот подход похож на "работу с большой осторожностью" (а при радикальных изменениях соблюдается особая осторожность) -- вроде бы очень профессиональный. Но что толку, если хирург делает операцию очень осторожно и аккуратно, но при этом квалификация его недостаточна, и он особо не понимает, что вообще делает.

Решение. Создаём своеобразный покров, который накидываем на код, чтобы исключить последствия неудачных изменений, способных отрицательно повлиять на программу. Покров в данном случае означает  **покрытие кода тестами** . Имея в своем распоряжении хороший набор тестов, окружающих каждый важный фрагмент кода, мы можем вносить изменения и сразу обнаруживать их воздействие на код. В этом случае мы тоже соблюдаем осторожность, но, получая ответную реакцию от проверок, можем вносить изменения куда более качественно и осознанно.


### В систему надо внести изменения (задания)

======= 4. Заранее неизвестно, насколько рискованно изменение поведения системы. Какой вопрос для выяснения этого лишний?

[ ] Как мы узнаем, что сделали изменения правильно?

[ ] Как мы узнаем, что ничего не поломали в системе?

[ 1] Где именно нам надо внести изменения?

[ ] До какой степени мы можем вносить изменения без риска?

[ ] Какие именно изменения нам нужно сделать?

======= 5. Принцип единственной ответственности SRP нужен прежде всего для ...

[ 1] упрощения понимания структуры классов и методов

[ ] уменьшения размера классов

[ ] уменьшения размера функций/методов

======= 6. Чем прежде всего плохо осторожное изменение кода?

[ ] внесение изменений наудачу требует дополнительного времени для проверок

[1 ] внесение изменений наудачу происходит без понимания, а что вообще делается

[ ] внесение изменений наудачу подразумевает последующее выискивание проблем в коде

[ ] внесение изменений наудачу требует очень тщательных проверок
