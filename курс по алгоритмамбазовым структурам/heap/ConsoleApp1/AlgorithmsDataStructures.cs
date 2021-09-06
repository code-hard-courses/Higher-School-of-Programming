using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {
	
        public int [] HeapArray; // хранит неотрицательные числа-ключи
        public int count;
		
        public Heap() { HeapArray = null; }
		
        public void MakeHeap(int[] a, int depth)
        {
            // создаём массив кучи HeapArray из заданного
            // размер массива выбираем на основе глубины depth
            // ...
            int size = (int) Math.Pow(2, depth + 1) - 1;
            HeapArray = new int[size];
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
		
        public int GetMax()
        {
            
            if (HeapArray == null || count == 0) return -1; // если куча пуста
            int maxKey = HeapArray[0];
            HeapArray[0] = HeapArray[count - 1];
            HeapArray[count - 1] = 0;
            count--;
            Sifting(0); // перестроение дерева

            return maxKey;
        }

        public bool Add(int key)
        {
            if(count == HeapArray.Length) return false; // если куча вся заполнена

            int index = count;                           // индекс первого свободного слота
            HeapArray[index] = key;
            int parentIndex = (index - 1) / 2;              // индекс родительского узла

            while (index > 0 && parentIndex >= 0)
            {
                if (HeapArray[index] > HeapArray[parentIndex])
                {
                    int temp = HeapArray[index];
                    HeapArray[index] = HeapArray[parentIndex];
                    HeapArray[parentIndex] = temp;
                }

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
            count++;

            return true;
        }
        
        private void Sifting(int index)
        {
            while (true)
            {
                int maxIndex = index; // индекс для потомка
                int left = index * 2 + 1;
                int right = index * 2 + 2;

                // определяем потомка с ключом больше, чем ключ родителя 
                if (left < count && HeapArray[left] > HeapArray[maxIndex]) maxIndex = left;
                if (right < count && HeapArray[right] > HeapArray[maxIndex]) maxIndex = right;
                if (maxIndex == index) break;

                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[maxIndex];
                HeapArray[maxIndex] = temp;

                index = maxIndex;
            }
        }
		
    }
}