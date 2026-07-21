using System;

namespace DesignPatterns
{
    public class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();

        private Logger()
        {
            Console.WriteLine("Logger instance created.");
        }

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG - {DateTime.Now}]: {message}");
        }
    }

    public static class SingletonDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Singleton Pattern ---");
            Logger logger1 = Logger.GetInstance();
            Logger logger2 = Logger.GetInstance();

            logger1.Log("Logging from variable 1.");
            logger2.Log("Logging from variable 2.");

            if (ReferenceEquals(logger1, logger2))
            {
                Console.WriteLine("SUCCESS: Both variables refer to the exact same instance.");
            }
            else
            {
                Console.WriteLine("FAILURE: Different instances exist.");
            }
        }
    }
}