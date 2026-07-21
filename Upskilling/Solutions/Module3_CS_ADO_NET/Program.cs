using System;
using System.Threading.Tasks;

namespace Module3_CS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n============================================");
                Console.WriteLine("C# and ADO.NET Upskilling Solutions Menu");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Exercise 1: Hello World Setup");
                Console.WriteLine("2. Exercise 2: Value vs Reference Types");
                Console.WriteLine("3. Exercise 3: Primary Constructors (C# 12)");
                Console.WriteLine("4. Exercise 4: Type Inference (var and new)");
                Console.WriteLine("5. Exercise 5: Grade switch logic");
                Console.WriteLine("6. Exercise 6: Loop through arrays");
                Console.WriteLine("7. Exercise 7: Method Overloading");
                Console.WriteLine("8. Exercise 8: ref, out, in Parameters");
                Console.WriteLine("9. Exercise 9: Local Functions Factorial");
                Console.WriteLine("10. Exercise 10: OOP Basics and Car constructors");
                Console.WriteLine("11. Exercise 11: Access Modifiers demo");
                Console.WriteLine("12. Exercise 12: Price Properties Validation");
                Console.WriteLine("13. Exercise 13: Immutable Records (C# 9/12)");
                Console.WriteLine("14. Exercise 14: Method Overriding (Shapes)");
                Console.WriteLine("15. Exercise 15: Interfaces vs Abstract Classes");
                Console.WriteLine("16. Exercise 16: Safe Null Coalescing");
                Console.WriteLine("17. Exercise 17: Null-Conditional Chaining");
                Console.WriteLine("18. Exercise 18: required Modifier (C# 12)");
                Console.WriteLine("19. Exercise 19: Collections: Lists & Dicts");
                Console.WriteLine("20. Exercise 20: LINQ select & project");
                Console.WriteLine("21. Exercise 21: Pattern matching object processing");
                Console.WriteLine("22. Exercise 22: Tuples Create and Deconstruct");
                Console.WriteLine("23. Exercise 23: Async file upload simulator");
                Console.WriteLine("24. Exercise 24: JSON Serialization");
                Console.WriteLine("25. Exercise 25: Streams (MemoryStream)");
                Console.WriteLine("26. Exercise 26: Multi-threading Counter locks");
                Console.WriteLine("27. Exercise 27: Deadlocks & Monitor.TryEnter");
                Console.WriteLine("28. Exercise 28: Logging to Trace");
                Console.WriteLine("29. Exercise 29: XSS Prevention (Sanitisation)");
                Console.WriteLine("30. Exercise 30: ADO.NET CRUD demonstration code");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": Exercise1.Run(); break;
                    case "2": Exercise2.Run(); break;
                    case "3": Exercise3.Run(); break;
                    case "4": Exercise4.Run(); break;
                    case "5": Exercise5.Run(); break;
                    case "6": Exercise6.Run(); break;
                    case "7": Exercise7.Run(); break;
                    case "8": Exercise8.Run(); break;
                    case "9": Exercise9.Run(); break;
                    case "10": Exercise10.Run(); break;
                    case "11": Exercise11.Run(); break;
                    case "12": Exercise12.Run(); break;
                    case "13": Exercise13.Run(); break;
                    case "14": Exercise14.Run(); break;
                    case "15": Exercise15.Run(); break;
                    case "16": Exercise16.Run(); break;
                    case "17": Exercise17.Run(); break;
                    case "18": Exercise18.Run(); break;
                    case "19": Exercise19.Run(); break;
                    case "20": Exercise20.Run(); break;
                    case "21": Exercise21.Run(); break;
                    case "22": Exercise22.Run(); break;
                    case "23": await Exercise23.Run(); break;
                    case "24": Exercise24.Run(); break;
                    case "25": Exercise25.Run(); break;
                    case "26": Exercise26.Run(); break;
                    case "27": Exercise27.Run(); break;
                    case "28": Exercise28.Run(); break;
                    case "29": Exercise29.Run(); break;
                    case "30": Exercise30.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }
    }
}