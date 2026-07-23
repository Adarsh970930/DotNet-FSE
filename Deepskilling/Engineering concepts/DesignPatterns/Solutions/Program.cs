using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n============================================");
                Console.WriteLine("Design Patterns and Principles Solution Menu");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Singleton Pattern (Logger)");
                Console.WriteLine("2. Factory Method Pattern (Documents)");
                Console.WriteLine("3. Builder Pattern (Computer Constructor)");
                Console.WriteLine("4. Adapter Pattern (Payment Gateways)");
                Console.WriteLine("5. Decorator Pattern (Notifiers)");
                Console.WriteLine("6. Proxy Pattern (Image Cache)");
                Console.WriteLine("7. Observer Pattern (Stock Market)");
                Console.WriteLine("8. Strategy Pattern (Checkout Strategy)");
                Console.WriteLine("9. Command Pattern (Light Control)");
                Console.WriteLine("10. MVC Pattern (Student View Controller)");
                Console.WriteLine("11. Dependency Injection (Customer Service)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": SingletonDemo.Run(); break;
                    case "2": FactoryMethodDemo.Run(); break;
                    case "3": BuilderDemo.Run(); break;
                    case "4": AdapterDemo.Run(); break;
                    case "5": DecoratorDemo.Run(); break;
                    case "6": ProxyDemo.Run(); break;
                    case "7": ObserverDemo.Run(); break;
                    case "8": StrategyDemo.Run(); break;
                    case "9": CommandDemo.Run(); break;
                    case "10": MvcDemo.Run(); break;
                    case "11": DependencyInjectionDemo.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }
    }
}