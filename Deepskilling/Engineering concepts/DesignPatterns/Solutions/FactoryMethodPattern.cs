using System;

namespace DesignPatterns
{
    public interface IDocument
    {
        void Open();
    }

    public class PdfDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening PDF Document.");
    }

    public class WordDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening Word Document.");
    }

    public abstract class DocumentFactory
    {
        public abstract IDocument CreateDocument();
    }

    public class PdfFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new PdfDocument();
    }

    public class WordFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new WordDocument();
    }

    public static class FactoryMethodDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Factory Method Pattern ---");
            DocumentFactory pdfFactory = new PdfFactory();
            IDocument pdf = pdfFactory.CreateDocument();
            pdf.Open();

            DocumentFactory wordFactory = new WordFactory();
            IDocument word = wordFactory.CreateDocument();
            word.Open();
        }
    }
}