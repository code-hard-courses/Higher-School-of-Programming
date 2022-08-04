// 7) Наследование реализации (implementation inheritance)
public class ArrayedStack<T>: System.Collections.Generic.Stack<T>
{
    private T[] _array;
    private int _count;
    private int _size;

    public ArrayedStack()
    {
        _array = new T[_size = 10];
        _count = 0;
    }

    public ArrayedStack(int size)
    {
        _array = new T[_size = size];
        _count = 0;
    }

    public T Top() =>  _array[_count - 1];

    public bool IsEmpty() => _count == 0;

    public bool IsFull() => _count == _size;
}

// 8) Льготное наследование (facility inheritance)
public class MyException : System.Exception
{
    public MyException() { }
    public MyException(string message) : base(message) { }
    public MyException(string message, System.Exception inner) : base(message, inner) { }
    protected MyException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}