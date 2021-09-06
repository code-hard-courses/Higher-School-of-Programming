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
            int[] array1 = new int[] {1, 9, 4, 3, 6, 11, 2};
            BalancedBST bsTree = new BalancedBST();
            bsTree.GenerateTree(array1);
            Assert.IsTrue(bsTree.IsBalanced(bsTree.Root));
        }
    }
}