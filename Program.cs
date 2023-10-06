using System;

public class Program
{
    public static void Main(string[] args)
    {
        AVLTree avlTree = new AVLTree();
        int[] nodeArray = {
            17, 6, 5, 20, 19, 18, 11, 14, 12, 13, 2, 4, 10
        };

        foreach (int node in nodeArray)
        {
            avlTree.Insert(node);
        }

        avlTree.Visualize();

        Console.Write("In-order Traversal: ");
        avlTree.InOrderTraversal();
    }
}

