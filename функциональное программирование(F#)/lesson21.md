## 43. Отображения (Maps)

**43.1. Отображение**

Отображение (Map) -- это отображение множества ключей на множество значений.

Отображение содержит набор пар ключ-значение, и позволяет быстро извлекать значения по ключу.

Типы ключа и значения полиморфические.

**43.2. Стандартные функции для отображений**

Функция Map.ofList преобразует список пар ключ-значение в отображение.

```
let map1 = Map.ofList [(128,"oksana"); (32,"oleg")]
```

Функция Map.toList преобразует отображение в список.

```
Map.toList map1 // [(128,"oksana"); (32,"oleg")]
```

Функция Map.add добавляет пару ключ-значение в отображение.

```
let map1 = Map.ofList [(128,"oksana"); (32,"oleg")]
let map2 = Map.add 256 "olga" map1 // [(128,"oksana"); (32,"oleg")б (256,"olga")]
```

Если ключ уже существовал, то старое значение по этому ключу будет перезаписано новым.

Функция Map.find находит в отображении значение по ключу.

```
Map.find 128 map1 // "oksana"
```

Если ключа в отображение нету, возникнет исключение KeyNotFoundException.

Безопасная версия Map.tryFind ищет в отображении значение по ключу, возвращая значение типа option.

```
Map.tryFind 128 map1 // Some("oksana")
Map.tryFind 64 map1 // None
```

Функция Map.containsKey проверяет, имеется ли в отображении ключ.

```
Map.containsKey 64 map1 // false
```

Функция Map.remove удаляет из отображения ключ вместе со значением.

```
Map.remove 32 map1  // map [(128,"oksana")]
```

Функция Map.count возвращает количество пар ключ-значение в отображении.

```
Map.count map1 // 2
```

Отображение поддерживает также функции exists, forall, map, fold и foldBack, работающие аналогично как и для списков и множеств, но применяются они только к значениям.

```
Map.map (fun f v -> string(v)+"!") map1 // map [(128,"oksana!"); (32,"oleg!")] 
```

**43.3. Задание**

Реализуйте стандартную функцию Map.tryFind самостоятельно в виде функции try_find.

Шаблон для отправки на сервер:

```
// 43.3
let try_find key m = ...
```

**[[ предыдущее ]](https://skillsmart.ru/fp/fsh/z9742195d8.html)**

---

**Ответы на задания 42**

```
let allSubsets n k = 
     let rec subsets xs= 
         match xs with 
           | [] -> [[]] 
           | head::tail -> List.fold (fun head1 tail1 -> 
                (head::tail1)::tail1::head1) [] (subsets tail) 
     Set.filter (fun xs -> Set.count xs = k) (Set.ofList (List.foldBack (fun head tail ->
        (Set.ofList head) :: tail) (subsets [1..n]) [] ))
```
