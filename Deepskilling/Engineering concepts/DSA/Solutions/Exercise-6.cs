using System;

class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(int bookId, string title, string author)
    {
        BookId = bookId;
        Title = title;
        Author = author;
    }

    public void Display()
    {
        Console.WriteLine(BookId + " " + Title + " " + Author);
    }
}

class Program
{
    static Book LinearSearch(Book[] books, string title)
    {
        foreach (Book book in books)
        {
            if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return book;
            }
        }

        return null;
    }

    static Book BinarySearch(Book[] books, string title)
    {
        int left = 0;
        int right = books.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            int result = string.Compare(
                books[mid].Title,
                title,
                StringComparison.OrdinalIgnoreCase
            );

            if (result == 0)
            {
                return books[mid];
            }

            if (result < 0)
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
        Book[] books =
        {
            new Book(101, "Java Programming", "James"),
            new Book(102, "Data Structures", "Mark"),
            new Book(103, "Operating Systems", "Andrew"),
            new Book(104, "Computer Networks", "Forouzan")
        };

        Book[] sortedBooks =
        {
            new Book(104, "Computer Networks", "Forouzan"),
            new Book(102, "Data Structures", "Mark"),
            new Book(101, "Java Programming", "James"),
            new Book(103, "Operating Systems", "Andrew")
        };

        Book book1 = LinearSearch(books, "Data Structures");

        if (book1 != null)
        {
            Console.WriteLine("Linear Search Result:");
            book1.Display();
        }

        Book book2 = BinarySearch(sortedBooks, "Data Structures");

        if (book2 != null)
        {
            Console.WriteLine("\nBinary Search Result:");
            book2.Display();
        }
    }
}