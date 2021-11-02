## 42. Множества

**42.1. Множество**

Множество -- это неупорядоченный набор значений одного типа, среди которых нету одинаковых.

Множество в F# задаётся ключевым словом set, за которым следует список значений (порядок значений в нём не играет роли).

Множества можно сравнивать -- это происходит с автоматическим учётом лексической упорядоченности.

```
set [1; 2; 3; 4; 5] = set [5; 4; 3; 2; 1] // true
set [1; 2] = set [1; 2; 2] // true
set [2; 1] < set [1; 3] // true
```

**42.2. Стандартные функции над множествами**

Функция Set.ofList преобразует список в множество.

```
Set.ofList [1; 2; 3] = set [1; 2; 3] // true
```

Функция Set.toList преобразует множество в список.

```
Set.toList (set [1; 2; 3]) = [1; 2; 3] // true
```

Функция Set.add добавляет элемент в множество.

```
Set.add 4 (set [1; 2; 3]) = set [1; 2; 3; 4] // true
```

Функция Set.remove удаляет элемент из множества.

```
Set.remove 4 (set [4; 1; 2; 3]) = set [1; 2; 3] // true
```

Функция Set.contains возвращает true, если элемент имеется в множестве.

```
Set.contains 4 (set [4; 1; 2; 3]) // true
```

Функция Set.isSubset возвращает true, если первый параметр -- подмножество второго параметра.

```
Set.isSubset (set [1; 2; 3]) (set [1; 2; 5]) // false
Set.isSubset (set [1; 2]) (set [1; 2; 5]) // true
```

Функции Set.minElement и Set.maxElement возвращают, соответственно, минимальный и максимальный элементы множества.

```
Set.maxElement (set [1; 2; 3]) = 3 // true
Set.minElement (set [1; 2; 3]) = 1 // true
```

Функция Set.count возвращает количество элементов в множестве.

```
Set.count (set [1; 2; 5]) = 3 // true
```

Set.empty -- это стандартная константа, пустое множество.

```
Set.count Set.empty = 0 // true
```

Функция Set.union выполняет объединение двух множеств.

```
Set.union (set [1; 2]) (set [3; 4]) = set [1; 2; 3; 4]
```

Функция Set.intersect находит пересечение двух множеств.

```
Set.intersect (set [1; 2; 3]) (set [3; 4; 5]) = set [3]
```

Функция Set.difference "вычитает" из первого множества второе.

```
Set.difference (set [1; 2; 3]) (set [3; 4; 5]) = set [1; 2]
```

Множество Set поддерживает также функции filter, exists, forall, map, fold и foldBack, работающие аналогично как и для списков List.

```
Set.map (fun x -> x*x) (set [3; 1; 2]) = set [1; 4; 9]
```

**42.3. Задание**

Напишите функцию allSubsets, получающую целочисленные параметры n и k, и выдающую множество всех подмножеств множества {1, 2, ..., n}, в которых ровно k элементов (0 < k < n).

Шаблон для отправки на сервер:

```
// 42.3
let rec allSubsets n k = ...
```

**[[ предыдущее ]](https://skillsmart.ru/fp/fsh/k8d3b08138.html)**

---

**Ответы на задания 41**

```
let list_filter f xs = 
   let func i acc = if (f i) then (i :: acc) else acc  
   List.foldBack func xs [] 
  
let sum (p, xs) = 
   let func acc i = if (p i) then (acc + i) else acc 
   List.fold func 0 xs 
  
let revrev = fun list -> 
   List.fold (fun head tail -> (List.rev tail) :: head) [] list
```
