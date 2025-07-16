using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from an empty priority queue.
    // Expected Result: Throws InvalidOperationException.
    // Defect(s) Found: None.
    public void TestPriorityQueue_Empty()
    {
        PriorityQueue queue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue one item with priority 1, dequeue it, and check length.
    // Expected Result: Dequeues "Task1", length is 0.
    // Defect(s) Found: Missing Length property (fixed).
    public void TestPriorityQueue_SingleItem()
    {
        PriorityQueue queue = new PriorityQueue();
        queue.Enqueue("Task1", 1);
        Assert.AreEqual("Task1", queue.Dequeue());
        Assert.AreEqual(0, queue.Length);
    }

    [TestMethod]
    // Scenario: Enqueue three items with same priority (1) and dequeue in order.
    // Expected Result: Dequeues in FIFO order: Task1, Task2, Task3.
    // Defect(s) Found: None.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        PriorityQueue queue = new PriorityQueue();
        queue.Enqueue("Task1", 1);
        queue.Enqueue("Task2", 1);
        queue.Enqueue("Task3", 1);

        Assert.AreEqual("Task1", queue.Dequeue());
        Assert.AreEqual("Task2", queue.Dequeue());
        Assert.AreEqual("Task3", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue three items with different priorities (3, 1, 2) and dequeue.
    // Expected Result: Dequeues in priority order: HighPriority (1), MediumPriority (2), LowPriority (3).
    // Defect(s) Found: None.
    public void TestPriorityQueue_DifferentPriorities()
    {
        PriorityQueue queue = new PriorityQueue();
        queue.Enqueue("LowPriority", 3);
        queue.Enqueue("HighPriority", 1);
        queue.Enqueue("MediumPriority", 2);

        Assert.AreEqual("HighPriority", queue.Dequeue());
        Assert.AreEqual("MediumPriority", queue.Dequeue());
        Assert.AreEqual("LowPriority", queue.Dequeue());
    }
}