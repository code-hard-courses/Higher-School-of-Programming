// Множественное наследование классов в C# не поддерживается
namespace Lesson11
{
    public abstract class General : System.Object
    {
    }

    public class Any : General
    {
    }

    public sealed class None : Any
    {
    }
}