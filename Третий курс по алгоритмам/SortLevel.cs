using System;
using System.Collections.Generic;
using System.Linq;

namespace SortSpace
{
    public static class SortLevel
    {
        public static void SelectionSortStep(int[] array, int i)
        {
            if (i < array.Length - 1)
            {
                int minPosition = MinElementPosition(array, i);
                if (array[minPosition] < array[i]) Exchange(array, i, minPosition);
            }
        }

        public static int MinElementPosition(int[] array, int i)
        {
            int minPosition = i;

            for (int current = i; current < array.Length; current++)
            {
                if (array[minPosition] > array[current])
                {
                    minPosition = current;
                }
            }

            return minPosition;
        }

        public static bool Exchange(int[] array, int positionOld, int positionNew)
        {
            if (positionOld == positionNew) return true;
            int oldElement = array[positionOld];
            array[positionOld] = array[positionNew];
            array[positionNew] = oldElement;
            return true;
        }


        public static bool BubbleSortStep(int[] array)
        {
            for (int currentFirst = 0; currentFirst < array.Length; currentFirst++)
            {
                bool flagExchange = true;

                int currentValue = array[currentFirst];
                int currentPosition = currentFirst;

                for (int currentSecond = currentFirst + 1; currentSecond < array.Length; currentSecond++)
                {
                    if (currentValue > array[currentSecond])
                    {
                        Exchange(array, currentSecond, currentPosition);
                        currentPosition = currentSecond;
                        flagExchange = false;
                    }
                }

                return flagExchange;
            }


            return true;
        }

        public static void InsertionSortStep(int[] array, int step, int i)
        {
            if ((step < 0) || (i < 0))
            {
                return;
            }

            for (int indexFirst = i; indexFirst < array.Length; indexFirst += step)
            {
                int currentValue = array[indexFirst];
                int currentPosition = indexFirst;

                for (int indexSecond = i; indexSecond < indexFirst; indexSecond += step)
                {
                    if (currentValue < array[indexSecond])
                    {
                        currentValue = array[indexSecond];
                        Exchange(array, indexSecond, currentPosition);
                    }
                }
            }
        }

        public static List<int> KnuthSequence(int arraySize)
        {
            List<int> list = new List<int>();
            int firstElementOfKnutSequences = KnuthNextStep(1, arraySize);
            for (;
                firstElementOfKnutSequences >= 1;
                firstElementOfKnutSequences = CalculateKnuthPrestep(firstElementOfKnutSequences))
                list.Add(firstElementOfKnutSequences);
            return list;
        }


        public static int CalculateKnuthPrestep(int t)
        {
            return (t - 1) / 3;
        }

        public static int CalculateKnuthStep(int t)
        {
            return 3 * t + 1;
        }

        public static int KnuthNextStep(int t, int s)
        {
            if (CalculateKnuthStep(t) > s) return t;
            t = CalculateKnuthStep(t);
            return KnuthNextStep(t, s);
        }

        public static int ArrayChunk(int[] array)
        {
            while (true)
            {
                int N = array[array.Length / 2];
                int n_i = array.Length / 2;
                int i1 = 0;
                int i2 = array.Length - 1;

                while (true)
                {
                    while (array[i1] < N)
                    {
                        ++i1;
                    }

                    while (array[i2] > N)
                    {
                        --i2;
                    }

                    if (i1 == i2 - 1 && array[i1] > array[i2])
                    {
                        int temp = array[i1];
                        array[i1] = array[i2];
                        array[i2] = temp;
                        break;
                    }

                    if (i1 == i2 || (i1 == i2 - 1 && array[i1] < array[i2]))
                    {
                        return n_i;
                    }

                    int temp1 = array[i1];
                    array[i1] = array[i2];
                    array[i2] = temp1;

                    if (array[i1] == N)
                        n_i = i1;
                    if (array[i2] == N)
                        n_i = i2;
                }
            }
        }

        public static int ArrayChunk(int[] array, int left, int right)
        {
            while (true)
            {
                int N = array[(left + right) / 2];
                int n_i = (left + right) / 2;
                int i1 = left;
                int i2 = right;

                while (true)
                {
                    while (array[i1] < N)
                    {
                        ++i1;
                    }

                    while (array[i2] > N)
                    {
                        --i2;
                    }

                    if (i1 == i2 - 1 && array[i1] > array[i2])
                    {
                        int temp = array[i1];
                        array[i1] = array[i2];
                        array[i2] = temp;
                        break;
                    }

                    if (i1 == i2 || (i1 == i2 - 1 && array[i1] < array[i2]))
                    {
                        return n_i;
                    }

                    int temp1 = array[i1];
                    array[i1] = array[i2];
                    array[i2] = temp1;

                    if (array[i1] == N)
                        n_i = i1;
                    if (array[i2] == N)
                        n_i = i2;
                }
            }
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int N = ArrayChunk(array, left, right);
                QuickSort(array, left, N - 1);
                QuickSort(array, N + 1, right);
            }
        }

        public static void QuickSortTailOptimization(int[] array, int left, int right)
        {
            int start = left;
            int end = right;


            if (left < right)
            {
                int division = ArrayChunk(array, left, right);
                if (left < end)
                {
                    QuickSortTailOptimization(array, start, division - 1);
                }

                if (right > start)
                {
                    QuickSortTailOptimization(array, division + 1, end);
                }
            }
        }

        public static List<int> KthOrderStatisticsStep(int[] array, int left, int right, int k)
        {
            int division = ArrayChunk(array, left, right);

            if (division < k)
            {
                left = division + 1;
            }
            else if (division > k)
            {
                right = division - 1;
            }

            return new List<int> {left, right};
        }

        public static List<int> MergeSort(List<int> array)
        {
            if (array.Count <= 1) return array;

            int middle = array.Count / 2;

            List<int> leftPart = array.GetRange(0, middle);
            List<int> rightPart = array.GetRange(middle, array.Count - middle);

            leftPart = MergeSort(leftPart);
            rightPart = MergeSort(rightPart);


            List<int> result = new List<int>();

            while (leftPart.Count > 0 || rightPart.Count > 0)
            {
                if (leftPart.Count == 0)
                {
                    rightPart.ForEach(item => result.Add(item));
                    break;
                }

                if (rightPart.Count == 0)
                {
                    leftPart.ForEach(item => result.Add(item));
                    break;
                }

                if (leftPart.First() > rightPart.First())
                {
                    result.Add(rightPart.First());
                    rightPart.RemoveAt(0);
                }
                else
                {
                    result.Add(leftPart.First());
                    leftPart.RemoveAt(0);
                }
            }

            return result;
        }

        public static bool GallopingSearch(int[] array, int N)
        {
            if (array.Length == 0) return false;

            int i = 1;
            int index = 0;
            
            while (array[index] <= N && index < array.Length - 1)
            {
                if (array[index] == N)
                {
                    return true;
                }
                
                if (array[index] < N)
                {
                    i++;
                    index = Convert.ToInt32(Math.Pow(2, i) - 2);
                    
                    if (index >= array.Length - 1)
                        index = array.Length - 1;
                    
                }
                
            }

            BinarySearch bs = new BinarySearch(array);
            bs.Right = index;
            bs.Left = Convert.ToInt32(Math.Pow(2, i - 1)) - 2 + 1;

            while (bs.GetResult() == 0)
            {
                bs.Step(N);
            }

            return bs.GetResult() == 1;
        }
    }
    
    public class BinarySearch
    {
        public int Left;
        public int Right;
        public int[] Array;
        private int flagFindElement;

        public BinarySearch(int[] array)
        {
            Left = 0;
            Right = array.Length - 1;
            Array = array;
            flagFindElement = 0;
        }

        public void Step(int N)
        {
            if (flagFindElement != 0) return;

            int middle = (Left + Right) / 2;


            if (Array[middle] == N)
            {
                flagFindElement = 1;
                return;
            }

            if (N > Array[middle])
            {
                if (Left < Array.Length-1)
                {
                    Left = middle + 1;
                }
            }
            else
            {
                if (Right > 0)
                {
                    Right = middle - 1;
                }
            }

            middle = (Left + Right) / 2;

            if ((Right - Left <= 1) && Array[Right] != N && Array[Left] != N)
            {
                flagFindElement = -1;
                return;
            }

            if (Array[middle] == N)
            {
                flagFindElement = 1;
                return;
            }
        }

        public int GetResult()
        {
            return flagFindElement;
        }
    }
    
}