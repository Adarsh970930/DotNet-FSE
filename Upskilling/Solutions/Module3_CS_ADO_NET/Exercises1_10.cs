using System;

namespace Module3_CS
{
    // Exercise 1: Hello World
    public static class Exercise1
    {
        public static void Run()
        {
            Console.WriteLine("Hello, World! Welcome to C# Development Environment.");
        }
    }

    // Exercise 2: Value vs Reference Types
    public class CustomClass
    {
        public int Value { get; set; }
    }

    public static class Exercise2
    {
        public static void ModifyVal(int x) { x = 100; }
        public static void ModifyRef(CustomClass obj) { obj.Value = 200; }

        public static void Run()
        {
            int num = 10;
            CustomClass c = new CustomClass { Value = 20 };

            Console.WriteLine($"Before: num = {num}, obj.Value = {c.Value}");
            ModifyVal(num);
            ModifyRef(c);
            Console.WriteLine($"After: num = {num} (unchanged), obj.Value = {c.Value} (changed)");
        }
    }

    // Exercise 3: Primary Constructors in C# 12
    public class Person(string firstName, string lastName, int age)
    {
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public int Age { get; } = age;

        public void DisplayInfo() => Console.WriteLine($"Person: {FirstName} {LastName}, Age: {Age}");
    }

    public static class Exercise3
    {
        public static void Run()
        {
            Person p = new Person("John", "Doe", 30);
            p.DisplayInfo();
        }
    }

    // Exercise 4: Type Inference with var and new()
    public static class Exercise4
    {
        public static void Run()
        {
            var number = 42; // inferred as int
            var text = "C# 12 is awesome"; // inferred as string
            CustomClass myObj = new(); // target-typed new() expression

            Console.WriteLine($"number type: {number.GetType()}, val: {number}");
            Console.WriteLine($"text type: {text.GetType()}, val: {text}");
            Console.WriteLine($"myObj type: {myObj.GetType()}");
        }
    }

    // Exercise 5: Conditional Logic for Grade Calculation
    public static class Exercise5
    {
        public static void Run()
        {
            Console.Write("Enter your score (0-100): ");
            if (int.TryParse(Console.ReadLine(), out int score))
            {
                // Switch with pattern matching
                string grade = score switch
                {
                    >= 90 and <= 100 => "A",
                    >= 80 and < 90 => "B",
                    >= 70 and < 80 => "C",
                    >= 60 and < 70 => "D",
                    >= 0 and < 60 => "F",
                    _ => "Invalid Score"
                };
                Console.WriteLine($"Grade: {grade}");
            }
            else
            {
                Console.WriteLine("Invalid Input.");
            }
        }
    }

    // Exercise 6: Loop Through an Array with Different Loops
    public static class Exercise6
    {
        public static void Run()
        {
            int[] nums = { 10, 20, 30, 42, 50 };

            Console.Write("For loop: ");
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 42) continue; // Skip 42
                Console.Write(nums[i] + " ");
            }

            Console.Write("\nForeach: ");
            foreach (var n in nums)
            {
                Console.Write(n + " ");
            }

            Console.Write("\nWhile: ");
            int idx = 0;
            while (idx < nums.Length)
            {
                Console.Write(nums[idx] + " ");
                idx++;
            }
            Console.WriteLine();
        }
    }

    // Exercise 7: Method Overloading
    public static class Exercise7
    {
        public static int CalculateTotal(int a, int b) => a + b;
        public static double CalculateTotal(double a, double b, double c) => a + b + c;

        public static void Run()
        {
            Console.WriteLine($"Two ints sum: {CalculateTotal(5, 10)}");
            Console.WriteLine($"Three doubles sum: {CalculateTotal(5.5, 10.2, 4.3)}");
        }
    }

    // Exercise 8: ref, out, and in Parameters
    public static class Exercise8
    {
        public static void Square(ref int x) { x = x * x; }
        public static void Initialize(out int x) { x = 42; }
        public static void DisplayReadOnly(in int x)
        {
            // x = x + 1; // Error: compile time constant
            Console.WriteLine($"Read-only in param value: {x}");
        }

        public static void Run()
        {
            int r = 5;
            Console.WriteLine($"Before ref: {r}");
            Square(ref r);
            Console.WriteLine($"After ref: {r}");

            int o;
            Initialize(out o);
            Console.WriteLine($"After out initialization: {o}");

            int i = 100;
            DisplayReadOnly(in i);
        }
    }

    // Exercise 9: Local Functions
    public static class Exercise9
    {
        public static long CalculateFactorial(int n)
        {
            return FactorialLocal(n);

            // Local Function
            long FactorialLocal(int val)
            {
                if (val <= 1) return 1;
                return val * FactorialLocal(val - 1);
            }
        }

        public static void Run()
        {
            Console.WriteLine($"Factorial of 5 is: {CalculateFactorial(5)}");
        }
    }

    // Exercise 10: OOP Basics with Constructors
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car()
        {
            Make = "Unknown";
            Model = "Unknown";
            Year = 2000;
        }

        public Car(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public void DisplayDetails() => Console.WriteLine($"Car: {Year} {Make} {Model}");
    }

    public static class Exercise10
    {
        public static void Run()
        {
            Car car1 = new();
            Car car2 = new("Tesla", "Model S", 2024);
            car1.DisplayDetails();
            car2.DisplayDetails();
        }
    }
}