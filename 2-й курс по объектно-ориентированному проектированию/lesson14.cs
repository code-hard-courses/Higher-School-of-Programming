using System;
using System.Collections.Generic;

namespace lesson14
{
    using System;
    using System.Collections.Generic;

    namespace main
    {
        public abstract class GeneralClass : System.Object
        {
            public abstract void Add(GeneralClass first);
        }

        public class Vector<T> where T : GeneralClass
        {
            public List<T> List { get; set; }

            public int Length => List.Count;

            public void Add(Vector<T> first)
            {
                if (this.Length != first.Length) return;

                for (int i = 0; i < Length; i++)
                {
                    List[i].Add(first.List[i]);
                }
            }
        }

    }
}