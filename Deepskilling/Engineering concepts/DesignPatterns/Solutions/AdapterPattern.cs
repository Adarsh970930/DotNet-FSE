using System;

namespace DesignPatterns
{
    // Target Interface
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    // Adaptee 1
    public class PayPalGateway
    {
        public void SendPayment(double amount) => Console.WriteLine($"Processing ${amount} via PayPal.");
    }

    // Adaptee 2
    public class StripeGateway
    {
        public void MakePayment(double amount) => Console.WriteLine($"Processing ${amount} via Stripe.");
    }

    // Adapter 1
    public class PayPalAdapter : IPaymentProcessor
    {
        private readonly PayPalGateway _payPalGateway = new();
        public void ProcessPayment(double amount) => _payPalGateway.SendPayment(amount);
    }

    // Adapter 2
    public class StripeAdapter : IPaymentProcessor
    {
        private readonly StripeGateway _stripeGateway = new();
        public void ProcessPayment(double amount) => _stripeGateway.MakePayment(amount);
    }

    public static class AdapterDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Adapter Pattern ---");
            IPaymentProcessor payPalProcessor = new PayPalAdapter();
            payPalProcessor.ProcessPayment(150.00);

            IPaymentProcessor stripeProcessor = new StripeAdapter();
            stripeProcessor.ProcessPayment(250.00);
        }
    }
}