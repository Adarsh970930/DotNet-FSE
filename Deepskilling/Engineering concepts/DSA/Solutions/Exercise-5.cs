using System;

class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string Status { get; set; }

    public Task(int taskId, string taskName, string status)
    {
        TaskId = taskId;
        TaskName = taskName;
        Status = status;
    }
}

class Node
{
    public Task Data;
    public Node Next;

    public Node(Task task)
    {
        Data = task;
        Next = null;
    }
}

class TaskList
{
    private Node head;

    public void AddTask(Task task)
    {
        Node newNode = new Node(task);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Node current = head;

        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode;
    }

    public Task SearchTask(int taskId)
    {
        Node current = head;

        while (current != null)
        {
            if (current.Data.TaskId == taskId)
            {
                return current.Data;
            }

            current = current.Next;
        }

        return null;
    }

    public void TraverseTasks()
    {
        Node current = head;

        while (current != null)
        {
            Console.WriteLine(current.Data.TaskId + " " +
                              current.Data.TaskName + " " +
                              current.Data.Status);

            current = current.Next;
        }
    }

    public void DeleteTask(int taskId)
    {
        if (head == null)
        {
            return;
        }

        if (head.Data.TaskId == taskId)
        {
            head = head.Next;
            return;
        }

        Node current = head;

        while (current.Next != null &&
               current.Next.Data.TaskId != taskId)
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            current.Next = current.Next.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        TaskList tasks = new TaskList();

        tasks.AddTask(new Task(1, "Design UI", "Pending"));
        tasks.AddTask(new Task(2, "Develop Backend", "In Progress"));
        tasks.AddTask(new Task(3, "Testing", "Pending"));

        Console.WriteLine("Tasks:");
        tasks.TraverseTasks();

        Task task = tasks.SearchTask(2);

        if (task != null)
        {
            Console.WriteLine("\nTask Found:");
            Console.WriteLine(task.TaskId + " " +
                              task.TaskName + " " +
                              task.Status);
        }

        tasks.DeleteTask(2);

        Console.WriteLine("\nAfter Deletion:");
        tasks.TraverseTasks();
    }
}