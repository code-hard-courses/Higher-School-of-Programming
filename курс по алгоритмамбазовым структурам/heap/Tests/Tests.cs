using System;
using NUnit.Framework;
using AlgorithmsDataStructures2;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void FirstTest()
        {
            int[] array1 = new int[] {4,5,6,7,8,9,11};
            Heap heapObj = new Heap();
            heapObj.MakeHeap(array1,2);
            int maxKey = heapObj.GetMax();
            Assert.AreEqual(11, maxKey);
        }
    }
}