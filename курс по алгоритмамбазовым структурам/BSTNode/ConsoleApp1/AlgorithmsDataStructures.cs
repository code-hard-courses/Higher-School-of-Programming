using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    // промежуточный результат поиска
    public class BSTFind<T>
    {
        // null если не найден узел, и в дереве только один корень
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind()
        {
            Node = null;
        }

        public BSTFind(BSTNode<T> node, bool nodeHasKey, bool toLeft)
        {
            Node = node;
            NodeHasKey = nodeHasKey;
            ToLeft = toLeft;
        }
    }

    public class BST<T>
    {
        public BSTNode<T> Root; // корень дерева, или null

        private int count;

        public BST(BSTNode<T> node)
        {
            Root = node;
            count = Root == null ? 0 : 1;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            // ищем в дереве узел и сопутствующую информацию по ключу
            BSTFind<T> bstFindRes = new BSTFind<T>();
            BSTNode<T> tempBstNode = Root;
            if (tempBstNode == null) return bstFindRes;

            while (key != tempBstNode.NodeKey) {
                if (key < tempBstNode.NodeKey) {
                    if (tempBstNode.LeftChild != null) {
                        tempBstNode = tempBstNode.LeftChild;
                    } else {
                        bstFindRes.ToLeft = true;
                        break;
                    }
                } else {
                    if (tempBstNode.RightChild != null) {
                        tempBstNode = tempBstNode.RightChild;
                    } else {
                        bstFindRes.ToLeft = false;
                        break;
                    }
                }
            }

            bstFindRes.Node = tempBstNode;
            bstFindRes.NodeHasKey = tempBstNode.NodeKey == key;
            return bstFindRes;
 
        }

        private bool Add(BSTNode<T> node, int key, T value)
        {
            int result = key.CompareTo(node.NodeKey);
            if (result < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new BSTNode<T>(key, value, node);
                    count++;
                    return true;
                }
                else
                {
                    Add(node.LeftChild, key, value);
                }
            }
            else if (result > 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new BSTNode<T>(key, value, node);
                    count++;
                    return true;
                }
                else
                {
                    Add(node.RightChild, key, value);
                }
            }

            return false;
        }

        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            BSTFind<T> find = FindNodeByKey(key);
            if (find.NodeHasKey) return false;
            
            BSTNode<T> bstNewNode = new BSTNode<T>(key, val, find.Node);
            

            if (Root == null)
            {
                Root = bstNewNode;
                count++;
                return true;
            }
            else
            {

                if (find.ToLeft) find.Node.LeftChild = bstNewNode;
                else find.Node.RightChild = bstNewNode;
            }

            count++;
            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальное/минимальное в поддереве
            BSTNode<T> current = FromNode;

            if (FromNode == null) return null;

            if (FindMax)
            {
                while (current.RightChild != null)
                {
                    current = current.RightChild;
                }
            }
            else
            {
                while (current.LeftChild != null)
                {
                    current = current.LeftChild;
                }
            }
            
            return current;
        }

        public bool DeleteNodeByKey(int key)
        {
            BSTFind<T> found = FindNodeByKey(key);

            //Ничего не нашли - вышли.
            if (!found.NodeHasKey)
            {
                return false;
            }

            BSTNode<T> nodeDel = found.Node;
            BSTNode<T> parentNodeDel = nodeDel.Parent;
            
            bool isLeftChild = true;
            
            if (parentNodeDel != null)
            {
                isLeftChild = parentNodeDel.LeftChild == nodeDel;
            }

            // ищем узел которым надо заменить
            BSTNode<T> nodeForReplace = nodeDel;
            if (nodeForReplace.RightChild != null) nodeForReplace = nodeForReplace.RightChild;
            while (nodeForReplace.LeftChild != null) nodeForReplace = nodeForReplace.LeftChild;

            // если удаляемый узел лист или один, удаляем его
            if (nodeDel == nodeForReplace)
            {
                if (parentNodeDel != null)
                {
                    if (!isLeftChild) parentNodeDel.RightChild = null;
                    else parentNodeDel.LeftChild = null;
                }
                else
                {
                    Root = null;
                }
                
                count--;
                return true;
            }

            // если удаляемый узел и заменяющий узел не один и тот же, изменяем ...

            // ... ссылку на заменяющий
            BSTNode<T> parentNodeForReplace = nodeForReplace.Parent;
            if (parentNodeForReplace != null)
            {
                if (parentNodeForReplace.LeftChild == nodeForReplace) parentNodeForReplace.LeftChild = null;
                else parentNodeForReplace.RightChild = null;
            }

            // ... ссылку на родителя
            nodeForReplace.Parent = parentNodeDel;

            // ... у родителя ссылку на удаляемый узел
            if (parentNodeDel != null)
            {
                if (!isLeftChild) parentNodeDel.RightChild = nodeForReplace;
                else parentNodeDel.LeftChild = nodeForReplace;
            }
            else
            {
                Root = nodeForReplace;
            }

            // ... ссылки на детей
            BSTNode<T> nodeDelLeftChild = nodeDel.LeftChild;
            BSTNode<T> nodeDelRightChild = nodeDel.RightChild;
            nodeForReplace.LeftChild = nodeDelLeftChild;
            nodeForReplace.RightChild = nodeDelRightChild;

            // ... у детей ссылку на родителя
            if (nodeDelLeftChild != null) nodeDelLeftChild.Parent = nodeForReplace;
            if (nodeDelRightChild != null) nodeDelRightChild.Parent = nodeForReplace;

            count--;
            return true;
        }

        public int Count()
        {
            return count; // количество узлов в дереве
        }

        public List<BSTNode<T>> GetAllNodes(BSTNode<T> Root)
        {
            List<BSTNode<T>> Nodes = new List<BSTNode<T>>(); // all nodes
            Nodes.Add(Root);

            if (Root.LeftChild != null)
                Nodes.AddRange(GetAllNodes(Root.LeftChild));

            if (Root.RightChild != null)
                Nodes.AddRange(GetAllNodes(Root.RightChild));

            return Nodes;
        }

        
        public List<BSTNode<T>> WideAllNodes()
        {
            List<BSTNode<T>> Nodes = new List<BSTNode<T>>();
            if (Root == null) return Nodes;
            
            BSTNode<T> node = Root;
            
            List<BSTNode<T>> listTemp = new List<BSTNode<T>>();
            listTemp.Add(node);

            while (listTemp.Count!=0)
            {
                node = listTemp[listTemp.Count - 1];
                Nodes.Add(node);
                listTemp.RemoveAt(listTemp.Count - 1);

                if (node.LeftChild != null)
                {                
                    listTemp.Insert(0, node.LeftChild);
                }
                if (node.RightChild != null)
                {
                    listTemp.Insert(0, node.RightChild);
                }
            }
            
            return Nodes;
        }
        
        // метод-обёртка для вызова рекурсивного метода обхода в глубину
        public List<BSTNode<T>> DeepAllNodes(int orderType)
        {
            return RecursiveDeep(Root, orderType);
        }

        // вспомогательный метод обхода в глубину 
        private List<BSTNode<T>> RecursiveDeep(BSTNode<T> FromNode, int orderType)
        {
            BSTNode<T> node = FromNode;
            List<BSTNode<T>> nodes = new List<BSTNode<T>>();

            if (node != null)
            {
                switch (orderType)
                {
                    case 0: // in-order
                        nodes.AddRange(RecursiveDeep(node.LeftChild, orderType));
                        nodes.Add(node);
                        nodes.AddRange(RecursiveDeep(node.RightChild, orderType));
                        break;
                    case 1: // post-order
                        nodes.AddRange(RecursiveDeep(node.LeftChild, orderType));
                        nodes.AddRange(RecursiveDeep(node.RightChild, orderType));
                        nodes.Add(node);
                        break;
                    case 2: // pre-order
                        nodes.Add(node);
                        nodes.AddRange(RecursiveDeep(node.LeftChild, orderType));
                        nodes.AddRange(RecursiveDeep(node.RightChild, orderType));
                        break;
                    default: return null;
                }
            }

            return nodes;
        }
      

    }
}