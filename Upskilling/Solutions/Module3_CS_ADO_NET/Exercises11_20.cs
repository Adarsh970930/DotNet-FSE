using System;
using System.Collections.Generic;
using System.Linq;

namespace Module3_CS
{
    // Exercise 11: Access Modifiers
    public class BaseClass
    {
        public string PublicField = "Public Access";
        private string PrivateField = "Private Access";
        protected string ProtectedField = "Protected Access";

        public void ShowPrivate() => Console.WriteLine(PrivateField);
    }

    public class DerivedClass : BaseClass
    {
        public void ShowProtected()
        {
            // Accessible here because of inheritance
            Console.WriteLine(ProtectedField);
        }
    }

    public static class Exercise11
    {
        public static void Run()
        {
            BaseClass bc = new();
            Console.WriteLine(bc.PublicField);
            // bc.PrivateField; // Compile error
            // bc.ProtectedField; // Compile error
            
            DerivedClass dc = new();
            dc.ShowProtected();
        }
    }

    // Exercise 12: Auto-Properties and Backing Fields
    public class Product
    {
        private decimal _price;
        public string Name { get; set; } = "Generic Product";

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                _price = value;
            }
        }
    }

    public static class Exercise12
    {
        public static void Run()
        {
            Product p = new() { Name = "Laptop", Price = 999.99m };
            Console.WriteLine($"Product: {p.Name}, Price: {p.Price}");

            try
            {
                p.Price = -100;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation Success: {ex.Message}");
            }
        }
    }

    // Exercise 13: Records with init Properties
    public record Employee
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public string Position { get; init; } = "";
    }

    public static class Exercise13
    {
        public static void Run()
        {
            Employee emp1 = new() { Id = 101, Name = "Alice", Position = "Dev" };
            // emp1.Name = "Bob"; // Error: init-only property

            // Mutating using 'with' expression (creates a clone with modifications)
            Employee emp2 = emp1 with { Position = "Senior Dev" };

            Console.WriteLine($"emp1 (original): {emp1}");
            Console.WriteLine($"emp2 (modified clone): {emp2}");
        }
    }

    // Exercise 14: Inheritance and Method Overriding
    public class Shape
    {
        public virtual void Draw() => Console.WriteLine("Drawing a Shape.");
    }

    public class Circle : Shape
    {
        public override void Draw() => Console.WriteLine("Drawing a Circle.");
    }

    public class Rectangle : Shape
    {
        public override void Draw() => Console.WriteLine("Drawing a Rectangle.");
    }

    public static class Exercise14
    {
        public static void Run()
        {
            Shape s1 = new Circle();
            Shape s2 = new Rectangle();
            s1.Draw();
            s2.Draw();
        }
    }

    // Exercise 15: Abstract Classes and Interfaces
    public interface IDrivable
    {
        void Start();
    }

    public abstract class Vehicle
    {
        public abstract void Drive();
    }

    public class SportsCar : Vehicle, IDrivable
    {
        public void Start() => Console.WriteLine("SportsCar engine started.");
        public override void Drive() => Console.WriteLine("SportsCar is racing.");
    }

    public static class Exercise15
    {
        public static void Run()
        {
            SportsCar car = new();
            car.Start();
            car.Drive();
        }
    }

    // Exercise 16: Handle Null References Safely
    public class PersonDetails
    {
        public string? Email { get; set; }
    }

    public static class Exercise16
    {
        public static void Run()
        {
            PersonDetails? p = null;

            // Null-conditional operator (?.)
            string? email = p?.Email;
            Console.WriteLine($"Email: {email ?? "No Email Provided (Null Coalescing)"}");
        }
    }

    // Exercise 17: Null-Conditional Chaining in a Contact App
    public class Contact
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public static class Exercise17
    {
        public static void Run()
        {
            Contact? contact = null;
            Console.WriteLine($"Contact Name: {contact?.Name ?? "N/A"}");
        }
    }

    // Exercise 18: Use the required Modifier in C# 12
    public class Student
    {
        public required int StudentId { get; set; }
        public required string Name { get; set; }
    }

    public static class Exercise18
    {
        public static void Run()
        {
            // Student s = new Student(); // Compile error: StudentId and Name must be initialized
            Student s = new Student { StudentId = 2026, Name = "Keshav" };
            Console.WriteLine($"Student ID: {s.StudentId}, Name: {s.Name}");
        }
    }

    // Exercise 19: Work with Lists and Dictionaries
    public static class Exercise19
    {
        public static void Run()
        {
            List<string> list = new() { "Apple", "Banana", "Cherry" };
            Dictionary<int, string> dict = new()
            {
                { 1, "One" },
                { 2, "Two" }
            };

            Console.WriteLine("List items:");
            foreach (var item in list) Console.WriteLine("- " + item);

            Console.WriteLine("Dict items:");
            foreach (var kvp in dict) Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    // Exercise 20: Use LINQ for Filtering and Projection
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = "";
        public decimal TotalAmount { get; set; }
    }

    public static class Exercise20
    {
        public static void Run()
        {
            List<Order> orders = new()
            {
                new() { OrderId = 1, CustomerName = "Alice", TotalAmount = 150m },
                new() { OrderId = 2, CustomerName = "Bob", TotalAmount = 45m },
                new() { OrderId = 3, CustomerName = "Charlie", TotalAmount = 300m }
            };

            // LINQ query
            var highValueOrders = from o in orders
                                  where o.TotalAmount > 100m
                                  select new { o.OrderId, o.CustomerName };

            Console.WriteLine("High value orders (> 100):");
            foreach (var o in highValueOrders)
            {
                Console.WriteLine($"ID: {o.OrderId}, Customer: {o.CustomerName}");
            }
        }
    }
}