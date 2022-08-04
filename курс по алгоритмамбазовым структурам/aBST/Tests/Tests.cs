using NUnit.Framework;
using AlgorithmsDataStructures2;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void FindIfOnlyRoot()
        {
            aBST testTree = new aBST(3);
            int index = testTree.AddKey(4);
            int? index2 = testTree.FindKeyIndex(2);
            int expectedIndex = 0;
            int? expectedIndex2 = -1;


            Assert.IsTrue(testTree.Tree.Length == 15);
            Assert.IsTrue(testTree.Tree[0] == 4);
            Assert.IsTrue(testTree.Tree[1] == null);
            Assert.AreEqual(expectedIndex, index);
            Assert.AreEqual(expectedIndex2, index2);
        }
    }
}