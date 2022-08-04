using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            if (FindNodesByValue(ParentNode.NodeValue) != null)
            {
                SimpleTreeNode<T> targetNode = FindNodesByValue(ParentNode.NodeValue)[0]; 

                if (targetNode.Children == null)
                    targetNode.Children = new List<SimpleTreeNode<T>> { NewChild };
                else
                    targetNode.Children.Add(NewChild);

                NewChild.Parent = targetNode;
            }
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            List<SimpleTreeNode<T>> targetList = FindNodesByValue(NodeToDelete.NodeValue);

            if (targetList != null)
            {
                var targetNode = targetList.Find(delegate (SimpleTreeNode<T> node) { return node == NodeToDelete; });
                if (targetNode != null && targetNode != Root)
                {
                    targetNode.Parent.Children.Remove(targetNode);
                    if (targetNode.Children != null)
                    {
                        foreach (var child in targetNode.Children)
                        {
                            child.Parent = targetNode.Parent;
                        }
                        targetNode.Parent.Children.AddRange(targetNode.Children);
                    }

                    targetNode = null;
                }
            }
        }

        public List<SimpleTreeNode<T>> GetAllNodes() { return Recursive(Root); }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            return Recursive(Root, val, true).Count == 0 ? null : Recursive(Root, val, true);
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            List<SimpleTreeNode<T>> targetList = FindNodesByValue(OriginalNode.NodeValue);
            List<SimpleTreeNode<T>> newParentList = FindNodesByValue(NewParent.NodeValue);

            if (targetList != null && newParentList != null)
            {
                var targetNode = targetList.Find(delegate (SimpleTreeNode<T> node) { return node == OriginalNode; });
                var newParentNode = newParentList.Find(delegate (SimpleTreeNode<T> node) { return node == NewParent; });

                if (targetNode != null && targetNode != Root && newParentNode != null)
                {
                    OriginalNode.Parent.Children.Remove(OriginalNode);
                    OriginalNode.Parent = NewParent;
                    AddChild(NewParent, OriginalNode);
                }
            }
        }

        public int Count() { return GetAllNodes().Count; }

        public int LeafCount()
        {
            return Recursive(Root).FindAll(delegate (SimpleTreeNode<T> node) { return node.Children == null || node.Children.Count == 0; }).Count;
        }

        public List<int> EvenTrees()
        {
            if (Count() % 2 == 1) return new List<int>(); // при нечетном количестве возвращаем пустой список
            // список листов
            List<SimpleTreeNode<T>> leafs = Recursive(Root).FindAll(
                delegate (SimpleTreeNode<T> node) { return node.Children == null || node.Children.Count == 0; });

            List<int> edges = new List<int>(); // создаем список связей
            SimpleTreeNode<T> previousLeafParrent = null; // используется для исключения повторного обхода от листьев с общим родителем 
            foreach (var leaf in leafs)
            {
                if (previousLeafParrent != leaf.Parent) // пропуск обхода от листов - соседей 
                {
                    SimpleTreeNode<T> node = leaf.Parent; // проход от каждого листа
                    while (node != Root)
                    {
                        int nodesCount = Recursive(node).Count; // считать количество узлов узла
                        if (nodesCount % 2 == 0) // при чётном числе узлов сохраняем связь узла и его родителя в список
                        {
                            if (typeof(T) == typeof(int))
                            {
                                // проверка, содержится ли такая связь в списке
                                if (!edges.Contains(Convert.ToInt32(node.Parent.NodeValue)) ||
                                    !edges.Contains(Convert.ToInt32(node.NodeValue)))
                                {
                                    edges.Add(Convert.ToInt32(node.Parent.NodeValue));
                                    edges.Add(Convert.ToInt32(node.NodeValue));
                                }
                            }
                        }

                        node = node.Parent; // переход к корню
                    }
                }

                previousLeafParrent = leaf.Parent;
            }

            return edges;
        }

        private List<SimpleTreeNode<T>> Recursive(SimpleTreeNode<T> targetNode, T val = default(T), bool isFind = false)
        {
            SimpleTreeNode<T> node = targetNode;
            var result = new List<SimpleTreeNode<T>> { node };

            if (isFind)
            {
                result = new List<SimpleTreeNode<T>>();
                if (node.NodeValue.Equals(val))
                    result.Add(node);
            }

            for (int i = 0; node.Children != null && i < node.Children.Count; i++)
            {
                if (isFind)
                    result.AddRange(Recursive(node.Children[i], val, true));
                else
                    result.AddRange(Recursive(node.Children[i]));
            }

            return result;
        }

    }

}