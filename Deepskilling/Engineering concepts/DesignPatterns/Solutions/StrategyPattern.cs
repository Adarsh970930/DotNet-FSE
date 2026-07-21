using System;

namespace DesignPatterns
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private readonly string _cardNum;
        public CreditCardPayment(string cardNum) => _cardNum = cardNum;
        public void Pay(double amount) => Console.WriteLine($"Paid ${amount} using Credit Card ({_cardNum})");
    }

    public class PaypalPayment : IPaymentStrategy
    {
        private readonly string _email;
        public PaypalPayment(string email) => _email = email;
        public void Pay(double amount) => Console.WriteLine($"Paid ${amount} using PayPal ({_email})");
    }

    public class ShoppingCart
    {
        private IPaymentStrategy? _strategy;
        public void SetPaymentStrategy(IPaymentStrategy strategy) => _strategy = strategy;

        public void Checkout(double amount)
        {
            if (_strategy == null)
            {
                Console.WriteLine("No payment strategy selected.");
            }
            else
            {
                _strategy.Pay(amount);
            }
        }
    }

    public static class StrategyDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Strategy Pattern ---");
            ShoppingCart cart = new();

            cart.SetPaymentStrategy(new CreditCardPayment("1234-5678-9876-5432"));
            cart.Checkout(100.50);

            cart.SetPaymentStrategy(new PaypalPayment("user@example.com"));
            cart.Checkout(45.99);
        }
    }
}