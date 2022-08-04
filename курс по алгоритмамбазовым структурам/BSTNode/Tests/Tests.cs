using System.Collections.Generic;
using NUnit.Framework;
using AlgorithmsDataStructures2;
using System.Text.Json;

namespace Tests
{
    public class Tests
    {
        public static void AreEqualByJson(object expected, object actual)
        {
        }

        [Test]
        public void DeleteNodeByKeyHasNotChildrens()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.AddKeyValue(10, 10);
            bstreeObj.AddKeyValue(11, 11);
            bstreeObj.AddKeyValue(12, 12);
            bstreeObj.AddKeyValue(110, 110);
            bstreeObj.AddKeyValue(112, 112);
            bstreeObj.AddKeyValue(115, 115);
            bstreeObj.AddKeyValue(111, 111);
            bstreeObj.AddKeyValue(1, 1);
            bstreeObj.AddKeyValue(109, 109);
            bstreeObj.DeleteNodeByKey(1);

            BST<int> bstreeCompareObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeCompareObj.AddKeyValue(10, 10);
            bstreeCompareObj.AddKeyValue(11, 11);
            bstreeCompareObj.AddKeyValue(12, 12);
            bstreeCompareObj.AddKeyValue(110, 110);
            bstreeCompareObj.AddKeyValue(112, 112);
            bstreeCompareObj.AddKeyValue(115, 115);
            bstreeCompareObj.AddKeyValue(111, 111);
            bstreeCompareObj.AddKeyValue(109, 109);

            List<BSTNode<int>> bstreeObjList = new List<BSTNode<int>>();
            bstreeObjList = bstreeObj.GetAllNodes(bstreeObj.Root);

            List<BSTNode<int>> bstreeCompareObjList = new List<BSTNode<int>>();
            bstreeCompareObjList = bstreeCompareObj.GetAllNodes(bstreeCompareObj.Root);

            Assert.AreEqual(bstreeCompareObj.Count(), bstreeObj.Count());

            int index = 0;
            foreach (var element in bstreeCompareObjList)
            {
                Assert.AreEqual(element.NodeValue, bstreeObjList[index].NodeValue);
                Assert.AreEqual(element.NodeKey, bstreeObjList[index].NodeKey);
                index++;
            }
        }

        [Test]
        public void DeleteNodeByKeyHasTwoChildrens()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.AddKeyValue(10, 10);
            bstreeObj.AddKeyValue(11, 11);
            bstreeObj.AddKeyValue(12, 12);
            bstreeObj.AddKeyValue(110, 110);
            bstreeObj.AddKeyValue(112, 112);
            bstreeObj.AddKeyValue(115, 115);
            bstreeObj.AddKeyValue(111, 111);
            bstreeObj.AddKeyValue(1, 1);
            bstreeObj.AddKeyValue(109, 109);
            bstreeObj.DeleteNodeByKey(112);

            BST<int> bstreeCompareObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeCompareObj.AddKeyValue(10, 10);
            bstreeCompareObj.AddKeyValue(11, 11);
            bstreeCompareObj.AddKeyValue(12, 12);
            bstreeCompareObj.AddKeyValue(110, 110);
            bstreeCompareObj.AddKeyValue(115, 115);
            bstreeCompareObj.AddKeyValue(111, 111);
            bstreeCompareObj.AddKeyValue(1, 1);
            bstreeCompareObj.AddKeyValue(109, 109);

            List<BSTNode<int>> bstreeObjList = new List<BSTNode<int>>();
            bstreeObjList = bstreeObj.GetAllNodes(bstreeObj.Root);

            List<BSTNode<int>> bstreeCompareObjList = new List<BSTNode<int>>();
            bstreeCompareObjList = bstreeCompareObj.GetAllNodes(bstreeCompareObj.Root);

            Assert.AreEqual(bstreeCompareObj.Count(), bstreeObj.Count());

            int index = 0;
            foreach (var element in bstreeCompareObjList)
            {
                Assert.AreEqual(element.NodeValue, bstreeObjList[index].NodeValue);
                Assert.AreEqual(element.NodeKey, bstreeObjList[index].NodeKey);
                index++;
            }
        }


        [Test]
        public void DeleteNodeByKeyHasOneChildren()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.AddKeyValue(10, 10);
            bstreeObj.AddKeyValue(11, 11);
            bstreeObj.AddKeyValue(12, 12);
            bstreeObj.AddKeyValue(110, 110);
            bstreeObj.AddKeyValue(112, 112);
            bstreeObj.AddKeyValue(115, 115);
            bstreeObj.AddKeyValue(111, 111);
            bstreeObj.AddKeyValue(1, 1);
            bstreeObj.AddKeyValue(109, 109);
            bstreeObj.DeleteNodeByKey(111);

            BST<int> bstreeCompareObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeCompareObj.AddKeyValue(10, 10);
            bstreeCompareObj.AddKeyValue(11, 11);
            bstreeCompareObj.AddKeyValue(12, 12);
            bstreeCompareObj.AddKeyValue(110, 110);
            bstreeCompareObj.AddKeyValue(112, 112);
            bstreeCompareObj.AddKeyValue(115, 115);
            bstreeCompareObj.AddKeyValue(1, 1);
            bstreeCompareObj.AddKeyValue(109, 109);

            List<BSTNode<int>> bstreeObjList = new List<BSTNode<int>>();
            bstreeObjList = bstreeObj.GetAllNodes(bstreeObj.Root);

            List<BSTNode<int>> bstreeCompareObjList = new List<BSTNode<int>>();
            bstreeCompareObjList = bstreeCompareObj.GetAllNodes(bstreeCompareObj.Root);

            Assert.AreEqual(bstreeCompareObj.Count(), bstreeObj.Count());

            int index = 0;
            foreach (var element in bstreeCompareObjList)
            {
                Assert.AreEqual(element.NodeValue, bstreeObjList[index].NodeValue);
                Assert.AreEqual(element.NodeKey, bstreeObjList[index].NodeKey);
                index++;
            }
        }


        [Test]
        public void DeleteNodeByKeyRoot()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.DeleteNodeByKey(100);
            Assert.AreEqual(null, bstreeObj.Root);
        }

        [Test]
        public void FinMinMaxRoot()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.AddKeyValue(10, 10);
            bstreeObj.AddKeyValue(11, 11);
            bstreeObj.AddKeyValue(12, 12);
            bstreeObj.AddKeyValue(110, 110);
            bstreeObj.AddKeyValue(112, 112);
            bstreeObj.AddKeyValue(115, 115);
            bstreeObj.AddKeyValue(111, 111);
            bstreeObj.AddKeyValue(1, 1);
            bstreeObj.AddKeyValue(109, 109);
            Assert.AreEqual(115, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(1, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);

            BSTFind<int> foundNodeTest1 = bstreeObj.FindNodeByKey(110);
            Assert.AreEqual(115, bstreeObj.FinMinMax(foundNodeTest1.Node, true).NodeKey);
            Assert.AreEqual(109, bstreeObj.FinMinMax(foundNodeTest1.Node, false).NodeKey);

            BSTFind<int> foundNodeTest2 = bstreeObj.FindNodeByKey(1);
            Assert.AreEqual(1, bstreeObj.FinMinMax(foundNodeTest2.Node, true).NodeKey);
            Assert.AreEqual(1, bstreeObj.FinMinMax(foundNodeTest2.Node, false).NodeKey);


            BSTFind<int> foundNodeTest3 = bstreeObj.FindNodeByKey(115);
            Assert.AreEqual(115, bstreeObj.FinMinMax(foundNodeTest3.Node, true).NodeKey);
            Assert.AreEqual(115, bstreeObj.FinMinMax(foundNodeTest3.Node, false).NodeKey);
        }

        [Test]
        public void FinMinMaxRootFirstStepDeleteRoot()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            Assert.AreEqual(100, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(100, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);
        }


        [Test]
        public void FinMinMaxRootDelete()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 'f', null));
            bstreeObj.DeleteNodeByKey(100);
            Assert.AreEqual(null, bstreeObj.FinMinMax(bstreeObj.Root, true));
            Assert.AreEqual(null, bstreeObj.FinMinMax(bstreeObj.Root, false));

            bstreeObj.AddKeyValue(111, 'f');
            Assert.AreEqual(111, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(111, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);

            bstreeObj.AddKeyValue(10, 'f');
            Assert.AreEqual(111, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(10, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);

            bstreeObj.AddKeyValue(222, 'f');
            Assert.AreEqual(222, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(10, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);


            bstreeObj.AddKeyValue(8, 'f');
            Assert.AreEqual(222, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(8, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);


            bstreeObj.AddKeyValue(333, 'f');
            Assert.AreEqual(333, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(8, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);

            bstreeObj.AddKeyValue(4, 'f');
            Assert.AreEqual(333, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(4, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);


            bstreeObj.AddKeyValue(555, 'f');
            Assert.AreEqual(555, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(4, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);

            bstreeObj.AddKeyValue(1, 'f');
            Assert.AreEqual(555, bstreeObj.FinMinMax(bstreeObj.Root, true).NodeKey);
            Assert.AreEqual(1, bstreeObj.FinMinMax(bstreeObj.Root, false).NodeKey);
        }

        [Test]
        public void FinMinMaxRootDeleteSecondTest()
        {
            BST<int> tree = new BST<int>(new BSTNode<int>(8,0, null));
            tree.DeleteNodeByKey(8);
            tree.AddKeyValue(8,0);
            tree.AddKeyValue(4,4);
            tree.AddKeyValue(12,12);
            tree.AddKeyValue(2,2);
            tree.AddKeyValue(6,6);
            tree.AddKeyValue(10,10);
            tree.AddKeyValue(14,14);
            tree.AddKeyValue(1,1);
            tree.AddKeyValue(3,3);
            tree.AddKeyValue(5,5);
            tree.AddKeyValue(7,7);
            tree.AddKeyValue(9,9);
            tree.AddKeyValue(11,11);
            tree.AddKeyValue(13,13);
            tree.AddKeyValue(15,15);
            
            Assert.AreEqual(9, tree.FinMinMax(tree.Root.RightChild, false).NodeKey);
            Assert.AreEqual(15, tree.FinMinMax(tree.Root.RightChild, true).NodeKey);
            Assert.AreEqual(1, tree.FinMinMax(tree.Root.LeftChild, false).NodeKey);
            Assert.AreEqual(7, tree.FinMinMax(tree.Root.LeftChild, true).NodeKey);
        }
        
        [Test]
        public void WideAllNodesTest()
        {
            // BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            // bstreeObj.AddKeyValue(10, 10);
            // bstreeObj.AddKeyValue(11, 11);
            // bstreeObj.AddKeyValue(12, 12);
            // bstreeObj.AddKeyValue(110, 110);
            // bstreeObj.AddKeyValue(112, 112);
            // bstreeObj.AddKeyValue(115, 115);
            // bstreeObj.AddKeyValue(111, 111);
            // bstreeObj.AddKeyValue(1, 1);
            // bstreeObj.AddKeyValue(109, 109);
            // bstreeObj.WideAllNodes();
        }
        
        [Test]
        public void DeepAllNodesTest()
        {
            BST<int> bstreeObj = new BST<int>(new BSTNode<int>(100, 100, null));
            bstreeObj.AddKeyValue(10, 10);
            bstreeObj.AddKeyValue(11, 11);
            bstreeObj.AddKeyValue(12, 12);
            bstreeObj.AddKeyValue(110, 110);
            bstreeObj.AddKeyValue(112, 112);
            bstreeObj.AddKeyValue(115, 115);
            bstreeObj.AddKeyValue(111, 111);
            bstreeObj.AddKeyValue(1, 1);
            bstreeObj.AddKeyValue(109, 109);
            List<BSTNode<int>> result1 =  bstreeObj.DeepAllNodes(0);
            List<BSTNode<int>> result2 = bstreeObj.DeepAllNodes(1);
            List<BSTNode<int>> result3 = bstreeObj.DeepAllNodes(2);
        }

    }
}