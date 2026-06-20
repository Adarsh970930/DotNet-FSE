using System;

class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }

    public Product(int productId, string productName, string category)
    {
        ProductId = productId;
        ProductName = productName;
        Category = category;
    }
}

class Program
{
    static Product LinearSearch(Product[] products, int id)
    {
        foreach (Product product in products)
        {
            if (product.ProductId == id)
            {
                return product;
            }
        }

        return null;
    }

    static Product BinarySearch(Product[] products, int id)
    {
        int left = 0;
        int right = products.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (products[mid].ProductId == id)
            {
                return products[mid];
            }

            if (products[mid].ProductId < id)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return null;
    }

    static void Main()
    {
        Product[] products =
        {
            new Product(105, "Keyboard", "Electronics"),
            new Product(101, "Laptop", "Electronics"),
            new Product(103, "Mouse", "Electronics"),
            new Product(102, "Shoes", "Fashion"),
            new Product(104, "Watch", "Accessories")
        };

        Product[] sortedProducts =
        {
            new Product(101, "Laptop", "Electronics"),
            new Product(102, "Shoes", "Fashion"),
            new Product(103, "Mouse", "Electronics"),
            new Product(104, "Watch", "Accessories"),
            new Product(105, "Keyboard", "Electronics")
        };

        Product result1 = LinearSearch(products, 103);

        if (result1 != null)
        {
            Console.WriteLine("Linear Search Found: " + result1.ProductName);
        }

        Product result2 = BinarySearch(sortedProducts, 103);

        if (result2 != null)
        {
            Console.WriteLine("Binary Search Found: " + result2.ProductName);
        }
    }
}