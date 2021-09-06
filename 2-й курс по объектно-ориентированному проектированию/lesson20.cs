// 4) Наследования вариаций
// Абстрактный класс Vehicle и его реализации: Automobile, Bike, Plane etc, переопределяющие абстрактный метод базового класса Drive.
public abstract class Vehicle
{
    public abstract void Drive();
}

public class Automobile : Vehicle
{
    public override void Drive()
    {
        System.Console.WriteLine("I drive on a road by 4 wheels");
    }
}

public class Bike : Vehicle
{
    public override void Drive()
    {
        System.Console.WriteLine("I drive on a road by 2 wheels");
    }
}

// 5) Наследование с конкретизацией (reification inheritance)
// Класс TABLE, описывающий таблицы в общем виде и его наследники SEQUENTIAL_TABLE и HASH_TABLE. В свою очередь SEQUENTIAL_TABLE имеет классы наследники: ARRAYED_TABLE, LINKED_TABLE, FILE_TABLE.
class TABLE {}
class SEQUENTIAL_TABLE: TABLE {}
class HASH_TABLE: TABLE {}

// 6) Структурное наследование (structure inheritance)
// Класс INTEGER наследуется от класса NUMERIC и от интерфейса COMPARABLE. От NUMERIC он получает арифметические свойства, а от COMPARABLE сигнатуры операций сравнения.
interface COMPARABLE
{
    int Compare<T>(T first, T second);
}