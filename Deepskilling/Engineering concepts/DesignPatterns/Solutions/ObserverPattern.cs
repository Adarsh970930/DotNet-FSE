using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public interface IStockObserver
    {
        void Update(string stockName, double price);
    }

    public interface IStockMarket
    {
        void RegisterObserver(IStockObserver observer);
        void RemoveObserver(IStockObserver observer);
        void NotifyObservers();
    }

    public class StockMarket : IStockMarket
    {
        private readonly List<IStockObserver> _observers = new();
        private string _stockName = "";
        private double _price;

        public void RegisterObserver(IStockObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IStockObserver observer) => _observers.Remove(observer);

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_stockName, _price);
            }
        }

        public void SetStockPrice(string stockName, double price)
        {
            _stockName = stockName;
            _price = price;
            NotifyObservers();
        }
    }

    public class MobileAppObserver : IStockObserver
    {
        private readonly string _appName;
        public MobileAppObserver(string appName) => _appName = appName;
        public void Update(string stockName, double price) => 
            Console.WriteLine($"[{_appName}] Alert: {stockName} price changed to ${price}");
    }

    public static class ObserverDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Observer Pattern ---");
            StockMarket market = new();
            MobileAppObserver robinhood = new("Robinhood");
            MobileAppObserver etrade = new("E-Trade");

            market.RegisterObserver(robinhood);
            market.RegisterObserver(etrade);

            market.SetStockPrice("MSFT", 420.50);
            market.SetStockPrice("GOOGL", 175.25);
        }
    }
}