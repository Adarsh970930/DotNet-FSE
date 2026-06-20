using System;

class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Employee(int employeeId, string name, string position, double salary)
    {
        EmployeeId = employeeId;
        Name = name;
        Position = position;
        Salary = salary;
    }

    public void Display()
    {
        Console.WriteLine(EmployeeId + " " + Name + " " + Position + " " + Salary);
    }
}

class EmployeeManagement
{
    private Employee[] employees;
    private int count;

    public EmployeeManagement(int size)
    {
        employees = new Employee[size];
        count = 0;
    }

    public void AddEmployee(Employee employee)
    {
        if (count < employees.Length)
        {
            employees[count] = employee;
            count++;
        }
        else
        {
            Console.WriteLine("Array is full.");
        }
    }

    public Employee SearchEmployee(int employeeId)
    {
        for (int i = 0; i < count; i++)
        {
            if (employees[i].EmployeeId == employeeId)
            {
                return employees[i];
            }
        }

        return null;
    }

    public void TraverseEmployees()
    {
        for (int i = 0; i < count; i++)
        {
            employees[i].Display();
        }
    }

    public void DeleteEmployee(int employeeId)
    {
        int index = -1;

        for (int i = 0; i < count; i++)
        {
            if (employees[i].EmployeeId == employeeId)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        for (int i = index; i < count - 1; i++)
        {
            employees[i] = employees[i + 1];
        }

        employees[count - 1] = null;
        count--;
    }
}

class Program
{
    static void Main()
    {
        EmployeeManagement system = new EmployeeManagement(10);

        system.AddEmployee(new Employee(101, "Rahul", "Developer", 50000));
        system.AddEmployee(new Employee(102, "Priya", "Tester", 45000));
        system.AddEmployee(new Employee(103, "Aman", "Manager", 70000));

        Console.WriteLine("Employees:");
        system.TraverseEmployees();

        Employee employee = system.SearchEmployee(102);

        if (employee != null)
        {
            Console.WriteLine("\nEmployee Found:");
            employee.Display();
        }

        system.DeleteEmployee(102);

        Console.WriteLine("\nAfter Deletion:");
        system.TraverseEmployees();
    }
}