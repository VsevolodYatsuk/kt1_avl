using System;

public class AVLNode
{
    public int key;
    public AVLNode left;
    public AVLNode right;
    public int height;

    public AVLNode(int key)
    {
        this.key = key;
        this.left = null;
        this.right = null;
        this.height = 1;
    }
}

public class AVLTree
{
    public AVLNode root;

    public AVLTree()
    {
        this.root = null;
    }

    public void Insert(int key)
    {
        root = InsertHelper(root, key);
    }

    private AVLNode InsertHelper(AVLNode root, int key)
    {
        if (root == null)
        {
            return new AVLNode(key);
        }

        if (key < root.key)
        {
            root.left = InsertHelper(root.left, key);
        }
        else if (key > root.key)
        {
            root.right = InsertHelper(root.right, key);
        }

        root.height = 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (key < root.left.key)
            {
                return RightRotate(root);
            }
            else
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (key > root.right.key)
            {
                return LeftRotate(root);
            }
            else
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    public AVLNode Delete(AVLNode root, int key)
    {
        if (root == null)
        {
            return root;
        }
        else if (key < root.key)
        {
            root.left = Delete(root.left, key);
        }
        else if (key > root.key)
        {
            root.right = Delete(root.right, key);
        }
        else
        {
            if (root.left == null)
            {
                AVLNode tempNode = root.right;
                root = null;
                return tempNode;
            }
            else if (root.right == null)
            {
                AVLNode tempNode = root.left;
                root = null;
                return tempNode;
            }
            AVLNode temp = GetMinValueNode(root.right);
            root.key = temp.key;
            root.right = Delete(root.right, temp.key);
        }

        if (root == null)
        {
            return root;
        }

        root.height = 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (GetBalance(root.left) >= 0)
            {
                return RightRotate(root);
            }
            else
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (GetBalance(root.right) <= 0)
            {
                return LeftRotate(root);
            }
            else
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    public int GetHeight(AVLNode root)
    {
        if (root == null)
        {
            return 0;
        }
        return root.height;
    }

    public int GetBalance(AVLNode root)
    {
        if (root == null)
        {
            return 0;
        }
        return GetHeight(root.left) - GetHeight(root.right);
    }

    public AVLNode LeftRotate(AVLNode z)
    {
        AVLNode y = z.right;
        AVLNode T2 = y.left;

        y.left = z;
        z.right = T2;

        z.height = 1 + Math.Max(GetHeight(z.left), GetHeight(z.right));
        y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));

        return y;
    }

    public AVLNode RightRotate(AVLNode z)
    {
        AVLNode y = z.left;
        AVLNode T3 = y.right;

        y.right = z;
        z.left = T3;

        z.height = 1 + Math.Max(GetHeight(z.left), GetHeight(z.right));
        y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));

        return y;
    }

    public AVLNode GetMinValueNode(AVLNode root)
    {
        if (root == null || root.left == null)
        {
            return root;
        }
        return GetMinValueNode(root.left);
    }

    public void Visualize()
    {
        _VisualizeHelper(this.root, "", true);
    }

    private void _VisualizeHelper(AVLNode node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            return;
        }

        string nodeStr = node.key.ToString();
        string line = prefix + (isLeft ? "├── " : "└── ");
        Console.WriteLine(line + nodeStr);

        string childPrefix = prefix + (isLeft ? "│   " : "    ");
        _VisualizeHelper(node.left, childPrefix, true);
        _VisualizeHelper(node.right, childPrefix, false);
    }

    public void InOrderTraversal()
    {
        InOrderTraversalHelper(root);
        Console.WriteLine();
    }

    private void InOrderTraversalHelper(AVLNode node)
    {
        if (node != null)
        {
            InOrderTraversalHelper(node.left);
            Console.Write(node.key + " ");
            InOrderTraversalHelper(node.right);
        }
    }

    public void PreOrderTraversal()
    {
        PreOrderTraversalHelper(root);
        Console.WriteLine();
    }

    private void PreOrderTraversalHelper(AVLNode node)
    {
        if (node != null)
        {
            Console.Write(node.key + " ");
            PreOrderTraversalHelper(node.left);
            PreOrderTraversalHelper(node.right);
        }
    }
}
