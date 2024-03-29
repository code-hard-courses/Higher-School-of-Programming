## 46. Императивное программирование

**46.1. Мутабельные переменные**

Изменяемые переменные задаются с помощью ключевых слов let mutable.

```
let mutable x = 1 // x = 1
```

В дальнейшем значение переменной можно изменить (с учётом её типа) с помощью оператора присваивания <-.

```
x <- 7 // x = 7
```

Формально оператор <- выполняет некоторое вычисление, возвращая значение () типа unit.

В отличие от других операторов чистого функционального программирования оператор присваивания имеет побочный эффект: изменяется значение переменной, причём в результате вычисления <- это никак не отображается.

**46.2. Последовательная композиция**

Оператор последовательной композиции ; комбинирует два выражения в одно.

Выражение

```
exp1 ; exp2
```

вычисляется так: сначала вычисляется выражение exp1 и его результат теряется; затем вычисляется выражение exp2, и его значение становится результатом композиции.

```
let x = 2
let y = x + 2 ; x - 2 // y = 0
```

Если первое выражение возвращает результат, тип которого отличен от unit, то компилятор выдаст предупреждение, которое можно отключить функцией ignore().

```
let y = ignore(x + 2); x - 2
```

Разделение выражений новыми строками эквивалентно применению оператора композиции.

```
exp1
exp2
```

**46.3. Мутабельные поля записей**

В записях допускается создавать мутабельные поля, перед которыми указывается ключевое слово mutable.

```
type Cat = { name : string; age : int; sex: char; mutable food : int }

let eat (cat: Cat) = 
    cat.food <- cat.food + 10
    cat.food

let barsik = { name = "Barsik"; age = 5; sex = 'M'; food = 0 }
eat barsik // barsik.food = 10
```

**46.4. Ссылки**

Запись -- это значение, поэтому присвоить записи новую запись невозможно, допустимо лишь изменять её мутабельные поля.

В F# имеется тип ref, который упрощает работу с записями, состоящими из единственного мутабельного поля contents.

Оператор := имитирует императивное присваивание, заменяя в мутабельном поле contents левого аргумента его значение на правый аргумент.

Оператор ! извлекает содержимое поля contents.

```
let x = ref [1; 2; 3] // x : int list ref = {contents = [1;2;3];}
x := [3; 2; 1] // x : int list ref = {contents = [3;2;1];}
! x // [3; 2; 1]
```

**[[ предыдущее ]](https://skillsmart.ru/fp/fsh/f57f33c802.html)**

**[[ далее ]](https://skillsmart.ru/fp/fsh/d337138cc4.html)**
