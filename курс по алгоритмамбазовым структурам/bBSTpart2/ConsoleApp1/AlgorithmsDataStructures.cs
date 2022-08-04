using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey; // ключ узла
        public BSTNode Parent; // родитель или null для корня
        public BSTNode LeftChild; // левый потомок
        public BSTNode RightChild; // правый потомок	
        public int Level; // глубина узла

        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }


    public class BalancedBST
    {
        public BSTNode Root; // корень дерева
        public BalancedBST()
        {
            Root = null;
        }

        public void GenerateTree(int[] a)
        {
            Array.Sort(a);
            Generate(null, a);
        }

        public BSTNode Generate(BSTNode parent, int[] array)
        {

            if (array.Length == 0)
                return null;

            int center = array.Length / 2;

            BSTNode node = new BSTNode(array[array.Length / 2], parent);

            if (Root == null)
            {
                Root = node;
                Root.Level = 1;
            }

            if (node.Parent != null)
                node.Level = node.Parent.Level + 1;

            int[] left = new int[center];
            int[] right = new int[array.Length - left.Length - 1];
            Array.ConstrainedCopy(array, 0, left, 0, left.Length);
            Array.ConstrainedCopy(array, center + 1, right, 0, right.Length);

            node.LeftChild = Generate(node, left);
            node.RightChild = Generate(node, right);

            return node;
        }


        public bool IsBalanced(BSTNode root_node)
        {
            if (root_node != null)
            {
                int maxLeftLevel = root_node.Level;
                int maxRightLevel = root_node.Level;
                int diff;

                if (root_node.LeftChild != null)
                {
                    List<BSTNode> AllChildrenLeft = GetAllChildren(root_node.LeftChild);
                    foreach (BSTNode item in AllChildrenLeft)
                    {
                        if (item.Level > maxLeftLevel)
                            maxLeftLevel = item.Level;
                    }
                }

                if (root_node.RightChild != null)
                {
                    List<BSTNode> AllChildrenRight = GetAllChildren(root_node.RightChild);
                    foreach (BSTNode item in AllChildrenRight)
                    {
                        if (item.Level > maxRightLevel)
                            maxRightLevel = item.Level;
                    }
                }

                diff = Math.Abs(maxLeftLevel - maxRightLevel);

                if (diff <= 1)
                    return true;
            }

            return false; // сбалансировано ли дерево с корнем root_node
        }

        public List<BSTNode> GetAllChildren (BSTNode node)
        {
            List<BSTNode> AllChildren = new List<BSTNode>();
            BSTNode Node = node;

            if (Node != null)
            {
                if (Node.LeftChild != null)
                {
                    AllChildren.Add(Node.LeftChild);
                    AllChildren.AddRange(GetAllChildren(Node.LeftChild));
                }
                    
                if (Node.RightChild != null)
                {
                    AllChildren.Add(Node.RightChild);
                    AllChildren.AddRange(GetAllChildren(Node.RightChild));
                }

                return AllChildren;
            }
            else
                return null;
        }
    }
}