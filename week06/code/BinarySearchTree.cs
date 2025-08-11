using System;
using System.Collections;
using System.Collections.Generic;

// Binary Search Tree Implementation
public class BinarySearchTree : IEnumerable<int>
{
    private Node? _root;

    // Insert a new value 
    public void Insert(int value)
    {
        if (_root == null)
            _root = new Node(value);
        else
            _root.Insert(value);
    }
    public bool Contains(int value)
    {
        return _root?.Contains(value) ?? false;
    }
    public IEnumerator<int> GetEnumerator()
    {
        var values = new List<int>();
        TraverseForward(_root, values);
        foreach (var val in values)
            yield return val;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node != null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }


    public IEnumerable<int> Reverse()
    {
        var values = new List<int>();
        TraverseBackward(_root, values);
        foreach (var val in values)
            yield return val;
    }

    private void TraverseBackward(Node? node, List<int> values)
    {
        if (node != null)
        {
            TraverseBackward(node.Right, values);
            values.Add(node.Data);
            TraverseBackward(node.Left, values);
        }
    }

    public int GetHeight()
    {
        return _root?.GetHeight() ?? 0;
    }

    public override string ToString()
    {
        return "<Bst>{" + string.Join(", ", this) + "}";
    }

    private class Node
    {
        public int Data;
        public Node? Left;
        public Node? Right;

        public Node(int data)
        {
            Data = data;
        }

        public void Insert(int value)
        {
            if (value == Data)
            {

                return;
            }
            else if (value < Data)
            {
                if (Left == null)
                    Left = new Node(value);
                else
                    Left.Insert(value);
            }
            else
            {
                if (Right == null)
                    Right = new Node(value);
                else
                    Right.Insert(value);
            }
        }

        public bool Contains(int value)
        {
            if (value == Data)
                return true;
            else if (value < Data)
                return Left?.Contains(value) ?? false;
            else
                return Right?.Contains(value) ?? false;
        }

        public int GetHeight()
        {
            int leftHeight = Left?.GetHeight() ?? 0;
            int rightHeight = Right?.GetHeight() ?? 0;
            return 1 + Math.Max(leftHeight, rightHeight);
        }
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable<int> array)
    {
        return "<IEnumerable>{" + string.Join(", ", array) + "}";
    }
}