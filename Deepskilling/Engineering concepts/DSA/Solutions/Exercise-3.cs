using System;

class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public double TotalPrice { get; set; }

    public Order(int orderId, string customerName, double totalPrice)
    {
        OrderId = orderId;
        CustomerName = customerName;
        TotalPrice = totalPrice;
    }

    public void Display()
    {
        Console.WriteLine(OrderId + " " + CustomerName + " " + TotalPrice);
    }
}

class Program
{
    static void BubbleSort(Order[] orders)
    {
        int n = orders.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (orders[j].TotalPrice > orders[j + 1].TotalPrice)
                {
                    Order temp = orders[j];
                    orders[j] = orders[j + 1];
                    orders[j + 1] = temp;
                }
            }
        }
    }

    static int Partition(Order[] orders, int low, int high)
    {
        double pivot = orders[high].TotalPrice;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (orders[j].TotalPrice < pivot)
            {
                i++;

                Order temp = orders[i];
                orders[i] = orders[j];
                orders[j] = temp;
            }
        }

        Order swap = orders[i + 1];
        orders[i + 1] = orders[high];
        orders[high] = swap;

        return i + 1;
    }

    static void QuickSort(Order[] orders, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(orders, low, high);

            QuickSort(orders, low, pivotIndex - 1);
            QuickSort(orders, pivotIndex + 1, high);
        }
    }

    static void DisplayOrders(Order[] orders)
    {
        foreach (Order order in orders)
        {
            order.Display();
        }

        Console.WriteLine();
    }

    static void Main()
    {
        Order[] orders1 =
        {
            new Order(101, "Rahul", 4500),
            new Order(102, "Aman", 1200),
            new Order(103, "Priya", 7800),
            new Order(104, "Neha", 3000)
        };

        Order[] orders2 =
        {
            new Order(101, "Rahul", 4500),
            new Order(102, "Aman", 1200),
            new Order(103, "Priya", 7800),
            new Order(104, "Neha", 3000)
        };

        Console.WriteLine("Bubble Sort:");
        BubbleSort(orders1);
        DisplayOrders(orders1);

        Console.WriteLine("Quick Sort:");
        QuickSort(orders2, 0, orders2.Length - 1);
        DisplayOrders(orders2);
    }
}