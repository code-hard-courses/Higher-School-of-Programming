using System;
using System.Collections.Generic;
using System.Linq;

namespace SortSpace
{
    public class KSort
    {
        public string[] items;
        public string patternFirstElement = "abcdefgh";

        public KSort()
        {
            int count = 800;
            items = new string[count];
        }

        private bool CheckStringFormat(string s)
        {
            if (s == null || s.Length != 3) return false;

            if (!patternFirstElement.Contains(s[0].ToString()))
            {
                return false;
            }

            if (!Char.IsDigit(s[1]) || !Char.IsDigit(s[2]))
            {
                return false;
            }

            return true;
        }


        public int Index(string s)
        {
            if (!CheckStringFormat(s))
            {
                return -1;
            }

            return patternFirstElement.IndexOf(s[0]) * 100 + Convert.ToInt32(s[1].ToString()) * 10 + Convert.ToInt32(s[2].ToString());
        }

        public bool Add(string s)
        {
            int i = Index(s);
            if (i == -1)
                return false;

            items[i] = s;

            return true;
        }
    }
}