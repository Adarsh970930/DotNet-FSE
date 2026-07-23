using System;

namespace DesignPatterns
{
    public interface INotifier
    {
        void Send(string message);
    }

    public class EmailNotifier : INotifier
    {
        public void Send(string message) => Console.WriteLine($"Sending Email: {message}");
    }

    public abstract class NotifierDecorator : INotifier
    {
        protected readonly INotifier _notifier;
        protected NotifierDecorator(INotifier notifier) => _notifier = notifier;
        public virtual void Send(string message) => _notifier.Send(message);
    }

    public class SMSNotifierDecorator : NotifierDecorator
    {
        public SMSNotifierDecorator(INotifier notifier) : base(notifier) {}
        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Sending SMS: {message}");
        }
    }

    public class SlackNotifierDecorator : NotifierDecorator
    {
        public SlackNotifierDecorator(INotifier notifier) : base(notifier) {}
        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Sending Slack notification: {message}");
        }
    }

    public static class DecoratorDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Decorator Pattern ---");
            INotifier baseEmail = new EmailNotifier();
            INotifier fullyDecorated = new SlackNotifierDecorator(new SMSNotifierDecorator(baseEmail));

            fullyDecorated.Send("Critical system update required.");
        }
    }
}