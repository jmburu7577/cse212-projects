using System;
using System.Collections.Generic;

public class PriorityQueue
{
    private List<(string Item, int Priority)> queue = new List<(string, int)>();

    public void Enqueue(string item, int priority)
    {
        queue.Add((item, priority));
    }

    public string Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        // Find the item with the lowest priority number (highest priority)
        int minPriority = queue[0].Priority;
        int minIndex = 0;
        for (int i = 1; i < queue.Count; i++)
        {
            if (queue[i].Priority < minPriority)
            {
                minPriority = queue[i].Priority;
                minIndex = i;
            }
            else if (queue[i].Priority == minPriority)
            {
                // For same priority, maintain FIFO by keeping the earliest item
                // (i.e., don't update minIndex unless priority is strictly lower)
            }
        }

        string result = queue[minIndex].Item;
        queue.RemoveAt(minIndex);
        return result;
    }

    public int Length
    {
        get { return queue.Count; }
    }
}
