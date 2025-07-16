using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    public void TestPriorityQueue_Empty()
    {
        PriorityQueue queue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestMethod]
    public void TestPriorityQueue_SingleItem()
    {
        PriorityQueue queue = new PriorityQueue();
        queue.Enqueue("Task1", 1);
        Assert.AreEqual("Task1", queue.Dequeue());
        Assert.AreEqual(0, queue.Length);
    }

    [TestMethod]
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