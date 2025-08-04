using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// A priority queue where **lower** numbers represent higher priority.
/// Items are dequeued based on highest priority first (i.e., smallest number).
/// In case of ties, items are dequeued in the order they were enqueued (FIFO).
/// </summary>
public class PriorityQueue
{
    // Named tuple for clarity
    private List<(string Item, int Priority)> queue = new List<(string Item, int Priority)>();

    /// <summary>
    /// Adds an item to the queue with a given priority.
    /// </summary>
    public void Enqueue(string item, int priority)
    {
        queue.Add((Item: item, Priority: priority));
    }

    /// <summary>
    /// Removes and returns the item with the highest priority (lowest number).
    /// If multiple items share the same priority, the one enqueued first is returned.
    /// Throws InvalidOperationException if the queue is empty.
    /// </summary>
    public string Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        int highestPriority = queue[0].Priority;
        int targetIndex = 0;

        for (int i = 1; i < queue.Count; i++)
        {
            if (queue[i].Priority < highestPriority) // 🛠 corrected comparison: lower is higher priority
            {
                highestPriority = queue[i].Priority;
                targetIndex = i;
            }
            // Tie: do nothing — keeps FIFO by choosing earlier index
        }

        string result = queue[targetIndex].Item;
        queue.RemoveAt(targetIndex);
        return result;
    }

    /// <summary>
    /// Returns the number of items currently in the queue.
    /// </summary>
    public int Length => queue.Count;

    /// <summary>
    /// Returns a string representation of the queue in the format:
    /// [Item1 (Pri:X), Item2 (Pri:Y), ...]
    /// </summary>
    public override string ToString()
    {
        if (queue.Count == 0)
        {
            return "[]";
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("[");

        for (int i = 0; i < queue.Count; i++)
        {
            sb.Append($"{queue[i].Item} (Pri:{queue[i].Priority})");

            if (i < queue.Count - 1)
            {
                sb.Append(", ");
            }
        }

        sb.Append("]");
        return sb.ToString();
    }
}
