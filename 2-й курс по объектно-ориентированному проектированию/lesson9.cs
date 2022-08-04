using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lesson9
{
    public abstract class General : System.Object
    {
        //-- копирование объекта (копирование содержимого одного объекта в другой существующий, включая DeepCopy -- глубокое рекурсивное дублирование, подразумевающее также копирование содержимого объектов, вложенных в копируемый объект через его поля, атрибуты);
        public abstract General Copy();

        // -- клонирование объекта (создание нового объекта и глубокое копирование в него исходного объекта);
        public abstract General Clone();

        //-- сравнение объектов (включая глубокий вариант);
        public abstract bool Equals(General other);

        //-- сериализация/десериализация (перевод в формат, подходящий для удобного ввода-вывода, как правило, в строковый тип, и восстановление из него);
        public abstract string Serialize();

        public abstract General Deserialize(string obj);

        // -- печать (наглядное представление содержимого объекта в текстовом формате);
        public abstract string toString();

        //-- проверка типа (является ли тип текущего объекта указанным типом);
        public abstract bool IsGeneralType();

        //-- получение реального типа объекта (непосредственного класса, экземпляром которого он был создан).
        public abstract Type RealTypeOf();
    }

    public class Any : General
    {
        public override General Clone()
        {
            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (General) formatter.Deserialize(stream);
            }
        }

        public override General Copy()
        {
            return (General) this.MemberwiseClone();
        }

        public override string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public override General Deserialize(string jsonString)
        {
            return JsonSerializer.Deserialize<General>(jsonString);
        }

        public override bool Equals(General other)
        {
            //Check for null and compare run-time types.
            if ((other == null) || !this.GetType().Equals(other.GetType()))
            {
                return false;
            }

            return true;
        }

        public override bool IsGeneralType()
        {
            return this is General;
        }

        public override Type RealTypeOf()
        {
            return this.GetType();
        }

        public override string toString()
        {
            return this.ToString();
        }
    }
}