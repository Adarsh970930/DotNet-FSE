using System;
using System.Collections.Generic;

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Product(int productId, string productName, int quantity, double price)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString()
    {
        return $"ID: {ProductId}, Name: {ProductName}, Quantity: {Quantity}, Price: {Price}";
    }
}

class Inventory
{
    private Dictionary<int, Product> products = new Dictionary<int, Product>();

    public void AddProduct(Product product)
    {
        if (!products.ContainsKey(product.ProductId))
        {
            products.Add(product.ProductId, product);
            Console.WriteLine("Product added.");
        }
        else
        {
            Console.WriteLine("Product ID already exists.");
        }
    }

    public void UpdateProduct(int productId, string name, int quantity, double price)
    {
        if (products.ContainsKey(productId))
        {
            products[productId].ProductName = name;
            products[productId].Quantity = quantity;
            products[productId].Price = price;
            Console.WriteLine("Product updated.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void DeleteProduct(int productId)
    {
        if (products.Remove(productId))
        {
            Console.WriteLine("Product deleted.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void DisplayProducts()
    {
        foreach (var product in products.Values)
        {
            Console.WriteLine(product);
        }
    }
}

class Program
{
    static void Main()
    {
        Inventory inventory = new Inventory();

        inventory.AddProduct(new Product(101, "Laptop", 10, 50000));
        inventory.AddProduct(new Product(102, "Mouse", 40, 500));

        inventory.DisplayProducts();

        inventory.UpdateProduct(101, "Gaming Laptop", 8, 65000);

        inventory.DisplayProducts();

        inventory.DeleteProduct(102);

        inventory.DisplayProducts();
    }
}