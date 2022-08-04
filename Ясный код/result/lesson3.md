6.1. Разберите свой код, и сделайте пять примеров, где можно более наглядно учесть в именах переменных уровни абстракции.
- 1
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L163
  current - currentNode (CurrentNode)
- 2
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L187
  nodeDel - nodeForDelete (NodeForDelete)
- 3
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L163
  current - currentNode (CurrentNode)
- 4
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L233
  parentNodeDel - parentNodeForDelete (ParentNodeForDelete)
- 5
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/HashTable/ConsoleApp1/Program.cs#L16
  colCounter1 - collisionsCount (CollisionsCount)

6.2. Приведите четыре примера, где вы в качестве имён переменных использовали или могли бы использовать технические термины из информатики.
- https://github.com/olegnizamov/finish_education/blob/master/C%23/AlgorithmsDataStructures/HashTable/ConsoleApp1/Program.cs
  HashTable table = new HashTable(23, 5); - table изменить на hashTableObj, чтобы понимать из контекста что это за сущность
- 2
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/SortSpace/HeapSort.cs#L7
-       public class Heap
  {

        public int [] HeapArray; // хранит неотрицательные числа-ключи
        public int count;

- 3

- 4
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/SortSpace/HeapSort.cs#L86
  public class HeapSort
  {
  public Heap HeapObject;

        public HeapSort(int[] array)
        {
            int depth = (int)Math.Log(array.Length, 2);
            HeapObject = new Heap();
            HeapObject.MakeHeap(array, depth);
        }
6.3. Придумайте или найдите в своём коде три примера, когда имена переменных даны с учётом контекста (функции, метода, класса).
- 1
  https://github.com/olegnizamov/0a1/blob/b44b6f31175ca6da1de8f5a9d2f5529bc6abea95/HashTable.cs#L35
  class HashTable<T>
  {
  //хранимые значения
  private int length; //изначальная длина таблицы
  private int length_new; //текущая длина таблицы
  private T[] space; //пространство для хранения
- 2
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/SortSpace/SortLevel.cs#L343
  public class BinarySearch
  {
  public int Left;
  public int Right;
  public int[] Array;
- 3
  https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/SortSpace/KSort.cs#L7
  public class KSort
  {
  public string[] items;

6.4. Найдите пять имён переменных в своём коде, длины которых не укладываются в 8-20 символов, и исправьте, чтобы они укладывались в данный диапазон.
- https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L163
  current - currentNode (CurrentNode)
- https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L187
  nodeDel - nodeForDelete (NodeForDelete)
- https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/Queue/ConsoleApp1/AlgorithmsDataStructures.cs#L12
  items - itemsAtQueue
- https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/HashTable/ConsoleApp1/Program.cs#L16
  putSlot - slotIndex (SlotIndex)
- https://github.com/olegnizamov/finish_education/blob/f837dca0e4157258ab2cff3014e699e5862183bf/C%23/AlgorithmsDataStructures/BSTNode/ConsoleApp1/AlgorithmsDataStructures.cs#L138
  count - countNodeAtTree (CountNodeAtTree)
