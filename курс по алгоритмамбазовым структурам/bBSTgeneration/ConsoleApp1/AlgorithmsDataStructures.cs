using System;

namespace AlgorithmsDataStructures2
{
    public static class BalancedBST
    {
        public static int[] arrayBST;

        public static int[] GenerateBBSTArray(int[] a)
        {
            arrayBST = new int[a.Length];
            Array.Sort(a);  // сортируем исходный массив по возрастанию
            Add(a, 0);      // формируем массив со структурой сбалансированного дерева

            return arrayBST;
        }

        private static void Add(int[] arr, int index)
        {
            int middle = arr.Length / 2;        // центральный индекс массива
            arrayBST[index] = arr[middle];      // центральный элемент - корень дерева 

            if (arr.Length == 1) return;        // выход из рекурсии

            int[] left = new int[middle];       // левый подмассив
            int[] right = new int[middle];     // правый подмассив

            for (int i = 0; i < middle; i++)
            {
                left[i] = arr[i];               // вычисление левой части массива
                right[i] = arr[middle + i + 1]; // вычисление правой части массива
            }

            Add(left, 2 * index + 1);           // определяем левого наследника родительского узла
            Add(right, 2 * index + 2);          // определяем правого наследника родительского узла 

        }
    }
}