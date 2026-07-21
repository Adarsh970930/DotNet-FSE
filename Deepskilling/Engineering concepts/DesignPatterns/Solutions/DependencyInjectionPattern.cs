using System;

namespace DesignPatterns
{
    public interface ICustomerRepository
    {
        string GetCustomerName(int id);
    }

    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public string GetCustomerName(int id)
        {
            // Simulating database lookup
            return id == 101 ? "Alice Cooper" : "Unknown Customer";
        }
    }

    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        // Constructor Injection
        public CustomerService(ICustomerRepository repository) => _repository = repository;

        public void DisplayCustomerName(int id)
        {
            string name = _repository.GetCustomerName(id);
            Console.WriteLine($"Customer name for ID {id} is: {name}");
        }
    }

    public static class DependencyInjectionDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Dependency Injection Pattern ---");
            // Injecting implementation into service
            ICustomerRepository repo = new CustomerRepositoryImpl();
            CustomerService service = new(repo);

            service.DisplayCustomerName(101);
            service.DisplayCustomerName(202);
        }
    }
}