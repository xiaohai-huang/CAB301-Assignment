using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    using System;
    using System.Collections.Generic;
    class BinaryTree<E> where E : IComparable<E>
    {
        public Node<E> root;
        public int Size
        {
            get;
            private set;
        }

        public int Height
        {
            get
            {
                return GetHeight(this.root);
            }
        }

        public BinaryTree(E initialItem)
        {
            root = new Node<E>(initialItem);
            Size = 1;
        }
        public BinaryTree()
        {
            Size = 0;
        }
        public BinaryTree(Node<E> root)
        {
            this.root = root;
            Size = GetNumNodes(root);
        }

        public void Add(E item)
        {
            if (root == null)
            {
                root = new Node<E>(item);
            }
            Add(item, root);
            Size++;
        }
        private void Add(E item, Node<E> node)
        {
            // item to add is greater than the node
            // goes to the right
            if (item.CompareTo(node.data) > 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node<E>(item);
                    return;
                }
                else
                {
                    Add(item, node.RightChild);
                }
            }
            // item is smaller than or equal to the node
            // goes to the left
            else
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node<E>(item);
                    return;
                }
                else
                {
                    Add(item, node.LeftChild);
                }
            }
        }

        public bool Search(E item)
        {
            Node<E> current = root;
            while (current != null)
            {
                // if the item to search is greater than the current node
                // goes to the right
                if (item.CompareTo(current.data) > 0)
                {
                    current = current.RightChild;
                }
                // the item to search is smaller than the current node goes to the left
                else if (item.CompareTo(current.data) < 0)
                {
                    current = current.LeftChild;
                }
                else // the item is equal to the current node
                {
                    return true;
                }
            }
            return false;
        }

        // there are three cases to consider:
        // 1. the node to be deleted is a leaf
        // 2. the node to be deleted has only one child 
        // 3. the node to be deleted has both left and right children
        /// <summary>
        /// Delete the item from the binary tree
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        /// <returns>true if the deletion is successful, otherwise false</returns>
        public bool Delete(E item)
        {
            // find the item
            Node<E> nodeToDelete = root;
            Node<E> parent = null;
            while (root != null)
            {
                parent = nodeToDelete;
                if (item.CompareTo(nodeToDelete.data) == 0)
                {
                    break;
                }
                else if (item.CompareTo(nodeToDelete.data) < 0)
                {
                    nodeToDelete = nodeToDelete.LeftChild;
                }
                else if (item.CompareTo(nodeToDelete.data) > 0)
                {
                    nodeToDelete = nodeToDelete.RightChild;
                }
            }

            // if the item does not exist in the tree
            if (nodeToDelete == null) return false;

            // case 1: the node to delete is a leaf
            if (nodeToDelete.LeftChild == null && nodeToDelete.RightChild == null)
            {
                if (nodeToDelete == root)
                {
                    root = null;
                }
                else
                {
                    if (parent.LeftChild == nodeToDelete)
                    {
                        parent.LeftChild = null;
                    }
                    else if (parent.RightChild == nodeToDelete)
                    {
                        parent.RightChild = null;
                    }
                }
            }
            // case 2 : the node to delete has only one child
            else if (nodeToDelete.LeftChild == null || nodeToDelete.RightChild == null)
            {
                Node<E> successor;
                // in this case, the node to delete only has one child
                if (nodeToDelete.LeftChild != null)
                {
                    successor = nodeToDelete.LeftChild;
                }
                else
                {
                    successor = nodeToDelete.RightChild;
                }
               
                if (nodeToDelete == root)
                {
                    root = nodeToDelete;
                }
                // connect, the parent of the node to delete, to the successor
                else
                {
                    if (parent.LeftChild == nodeToDelete)
                    {
                        parent.LeftChild = successor;
                    }
                    else
                    {
                        parent.RightChild = successor;
                    }
                }
            }
            // case 3: the node to delete has two children
            else if (nodeToDelete.LeftChild != null && nodeToDelete.RightChild != null)
            {
                // find the biggest node in the left subtree
                // a special case: the right subtree of ptr.LChild is empty
                if (nodeToDelete.LeftChild.RightChild == null)
                {
                    nodeToDelete.data = nodeToDelete.LeftChild.data;
                    nodeToDelete.LeftChild = nodeToDelete.LeftChild.LeftChild;
                }
                else
                {
                    Node<E> parentOfBiggest = null;
                    Node<E> biggest = nodeToDelete.LeftChild;

                    while (biggest != null)
                    {
                        parentOfBiggest = biggest;
                        biggest = biggest.RightChild;
                    }

                    nodeToDelete.data = biggest.data;
                    parentOfBiggest.RightChild = biggest.LeftChild;
                }
            }

            Size--;
            return true;
        }

        /// <summary>
        /// Recursively gets the height of the given node
        /// </summary>
        /// <param name="root">input node</param>
        /// <returns>height of the given node</returns>
        private static int GetHeight(Node<E> root)
        {
            if (root == null)
            {
                return 0;
            }
            int leftHeight = GetHeight(root.LeftChild);
            int rightHeight = GetHeight(root.RightChild);

            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public static int GetHeightIterative(Node<E> root)
        {
            // level order traversal
            Queue<Node<E>> queue = new Queue<Node<E>>();
            int height = 0;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int numNodesAtCurrentLevel = queue.Count;

                // level by level
                while (numNodesAtCurrentLevel > 0)
                {
                    // visit all nodes at the current level
                    Node<E> node = queue.Dequeue();

                    // adds left and right child of the node visted to the queue
                    // these children will be visted at the iteration
                    if (node.LeftChild != null)
                    {
                        queue.Enqueue(node.LeftChild);
                    }
                    if (node.RightChild != null)
                    {
                        queue.Enqueue(node.RightChild);
                    }
                    numNodesAtCurrentLevel--;
                }
                height++;
            }
            return height;
        }

        /// <summary>
        /// Incldues the input node itself - BFS
        /// </summary>
        /// <param name="root">input node</param>
        /// <returns>The number of nodes associated with the input node.</returns>
        public static int GetNumNodes(Node<E> root)
        {
            if (root == null)
            {
                return 0;
            }
            // use level order traversal - breadth first traversal
            int numNodes = 0;
            Queue<Node<E>> queue = new Queue<Node<E>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {

                int numNodesAtCurrentLevel = queue.Count;

                while (numNodesAtCurrentLevel > 0)
                {
                    Node<E> node = queue.Dequeue();
                    if (node.LeftChild != null)
                    {
                        queue.Enqueue(node.LeftChild);
                    }
                    if (node.RightChild != null)
                    {
                        queue.Enqueue(node.RightChild);
                    }
                    numNodesAtCurrentLevel--;
                    numNodes++;
                }
            }


            return numNodes;
        }

        /// <summary>
        /// Incldues the input node itself - DFS
        /// </summary>
        /// <param name="root">input node</param>
        /// <returns>The number of nodes associated with the input node.</returns>
        public static int GetNumNodesDFS(Node<E> root)
        {
            if (root == null)
            {
                return 0;
            }
            Console.WriteLine(root.data);
            return GetNumNodesDFS(root.LeftChild) + GetNumNodesDFS(root.RightChild);
            // Stack<Node<E>> stack = new Stack<Node<E>>();

        }

        public static void PreOrderTraversal(Node<E> root, Action<E> visit)
        {
            if (root == null)
            {
                return;
            }
            visit(root.data);
            PreOrderTraversal(root.LeftChild, visit);
            PreOrderTraversal(root.RightChild, visit);
        }

        public static void InOrderTraversal(Node<E> root, Action<E> visit)
        {
            if (root == null)
            {
                return;
            }
            InOrderTraversal(root.LeftChild, visit);
            visit(root.data);
            InOrderTraversal(root.RightChild, visit);
        }

        public static void PostOrderTraversal(Node<E> root, Action<E> visit)
        {
            if (root == null)
            {
                return;
            }
            PostOrderTraversal(root.LeftChild, visit);
            PostOrderTraversal(root.RightChild, visit);
            visit(root.data);
        }
    }
    class Node<E> where E : IComparable<E>
    {
        public E data;
        public Node<E> LeftChild { get; set; }
        public Node<E> RightChild { get; set; }

        public Node(E data)
        {
            this.data = data;
        }

    }
}
