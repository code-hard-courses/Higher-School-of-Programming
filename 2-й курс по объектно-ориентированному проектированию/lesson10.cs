//В C# некоторые классы (например String) и все структуры (например DateTime, Decimal) являются запечатанными (sealed).
// Это значит что их нельзя модифицировать через наследование и они являются полностью закрытыми. При этом все классы и структуры
// наследуют классу System.Object, который является верхушкой иерархии классов и структур.
// Так сделано, потому что структуры - это объекты-значения и в них запрещён полиморфизм из-за проблем именуемых slicing.
// Некоторые классы же запечатаны, для того, чтобы гарантировать эффективность работы и избежать ошибок, которые могут быть
// превнесены извне.
// С другой стороны есть класс Exception, от которого можно наследовать и создать собственные исключения. Это очень удобно,
// т.к. позволяет кастомизировать проблемы именно для вашего кода.
