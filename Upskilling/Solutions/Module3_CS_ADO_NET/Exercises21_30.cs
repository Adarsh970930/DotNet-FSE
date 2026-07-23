using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Module3_CS
{
    // Exercise 21: Pattern Matching with is and switch
    public static class Exercise21
    {
        public static void ProcessObject(object obj)
        {
            if (obj is int val)
            {
                Console.WriteLine($"Object is an integer: {val}");
            }
            else if (obj is string str)
            {
                Console.WriteLine($"Object is a string of length {str.Length}: {str}");
            }
            else
            {
                Console.WriteLine("Unknown object type.");
            }
        }

        public static void Run()
        {
            ProcessObject(42);
            ProcessObject("C# Programming");
            ProcessObject(3.14);
        }
    }

    // Exercise 22: Create and Deconstruct Tuples
    public static class Exercise22
    {
        public static (int code, string message) GetResponse()
        {
            return (200, "Success");
        }

        public static void Run()
        {
            var (code, msg) = GetResponse(); // Deconstruction
            Console.WriteLine($"Response code: {code}, Message: {msg}");
        }
    }

    // Exercise 23: Simulate Async File Upload with Exception Handling
    public static class Exercise23
    {
        public static async Task<string> UploadFileAsync()
        {
            Console.WriteLine("File upload started... waiting 3 seconds.");
            await Task.Delay(3000); // Simulate network delay
            return "File Upload Completed Successfully.";
        }

        public static async Task Run()
        {
            try
            {
                string result = await UploadFileAsync();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload Error: {ex.Message}");
            }
        }
    }

    // Exercise 24: Serialize and Deserialize JSON Files
    public class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
    }

    public static class Exercise24
    {
        public static void Run()
        {
            User u = new() { Name = "Keshav", Age = 25, Email = "keshav@fse.com" };
            string json = JsonSerializer.Serialize(u);
            Console.WriteLine("Serialized JSON:\n" + json);

            User? deserializedUser = JsonSerializer.Deserialize<User>(json);
            Console.WriteLine($"Deserialized Name: {deserializedUser?.Name}");
        }
    }

    // Exercise 25: FileStream and MemoryStream
    public static class Exercise25
    {
        public static void Run()
        {
            // Writing and reading using MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes("Data in MemoryStream");
                ms.Write(data, 0, data.Length);
                Console.WriteLine($"Bytes written to memory: {ms.Length}");

                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    Console.WriteLine("Reading memory content: " + sr.ReadToEnd());
                }
            }
        }
    }

    // Exercise 26: Race Conditions with Multi-threading
    public static class Exercise26
    {
        private static int _counter = 0;
        private static readonly object _lock = new();

        public static void Run()
        {
            _counter = 0;
            Thread t1 = new Thread(Increment);
            Thread t2 = new Thread(Increment);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"Final Counter Value (with lock): {_counter}");
        }

        private static void Increment()
        {
            for (int i = 0; i < 5000; i++)
            {
                lock (_lock)
                {
                    _counter++;
                }
            }
        }
    }

    // Exercise 27: Simulate and Resolve a Deadlock
    public static class Exercise27
    {
        private static readonly object _lockA = new();
        private static readonly object _lockB = new();

        public static void Run()
        {
            Thread t1 = new Thread(RunThread1);
            Thread t2 = new Thread(RunThread2);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        private static void RunThread1()
        {
            if (Monitor.TryEnter(_lockA, 1000))
            {
                try
                {
                    Console.WriteLine("Thread 1 acquired lockA");
                    Thread.Sleep(100);
                    if (Monitor.TryEnter(_lockB, 1000))
                    {
                        try
                        {
                            Console.WriteLine("Thread 1 acquired lockB");
                        }
                        finally
                        {
                            Monitor.Exit(_lockB);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Thread 1 failed to acquire lockB. Timeout avoided deadlock.");
                    }
                }
                finally
                {
                    Monitor.Exit(_lockA);
                }
            }
        }

        private static void RunThread2()
        {
            if (Monitor.TryEnter(_lockB, 1000))
            {
                try
                {
                    Console.WriteLine("Thread 2 acquired lockB");
                    Thread.Sleep(100);
                    if (Monitor.TryEnter(_lockA, 1000))
                    {
                        try
                        {
                            Console.WriteLine("Thread 2 acquired lockA");
                        }
                        finally
                        {
                            Monitor.Exit(_lockA);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Thread 2 failed to acquire lockA. Timeout avoided deadlock.");
                    }
                }
                finally
                {
                    Monitor.Exit(_lockB);
                }
            }
        }
    }

    // Exercise 28: Log with System.Diagnostics.Trace
    public static class Exercise28
    {
        public static void Run()
        {
            System.Diagnostics.Trace.Listeners.Clear();
            
            // Console listener
            System.Diagnostics.TextWriterTraceListener consoleListener = new(Console.Out);
            System.Diagnostics.Trace.Listeners.Add(consoleListener);

            // Log entries
            System.Diagnostics.Trace.WriteLine("Logging to trace listeners.");
            System.Diagnostics.Trace.Flush();
            Console.WriteLine("Trace line printed successfully.");
        }
    }

    // Exercise 29: Sanitize Input and Prevent XSS
    public static class Exercise29
    {
        public static void Run()
        {
            string userInput = "<script>alert('hack')</script>";
            string sanitized = System.Net.WebUtility.HtmlEncode(userInput);

            Console.WriteLine("Original User Input: " + userInput);
            Console.WriteLine("Sanitized Output: " + sanitized);
        }
    }

    // Exercise 30: CRUD Operations using ADO.NET
    public static class Exercise30
    {
        private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=EmployeeDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void Run()
        {
            Console.WriteLine("ADO.NET CRUD Operations Demonstration:");
            Console.WriteLine("SQL Server local instance connection string is configured.");
            Console.WriteLine("Ensure MS SQL Express service is running locally to execute live operations.");
            Console.WriteLine("Demonstrating template connection and query syntax:");
            
            Console.WriteLine(@"
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string insertQuery = ""INSERT INTO Employees (Name, Role) VALUES (@name, @role)"";
        SqlCommand cmd = new SqlCommand(insertQuery, conn);
        cmd.Parameters.AddWithValue(""@name"", ""Jane Doe"");
        cmd.Parameters.AddWithValue(""@role"", ""FSE Developer"");
        // conn.Open();
        // cmd.ExecuteNonQuery();
    }");
        }
    }
}