using System;

namespace DesignPatterns
{
    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private readonly string _filename;

        public RealImage(string filename)
        {
            _filename = filename;
            LoadFromDisk();
        }

        private void LoadFromDisk() => Console.WriteLine($"Loading image from disk: {_filename}");
        public void Display() => Console.WriteLine($"Displaying image: {_filename}");
    }

    public class ProxyImage : IImage
    {
        private RealImage? _realImage;
        private readonly string _filename;

        public ProxyImage(string filename) => _filename = filename;

        public void Display()
        {
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename);
            }
            _realImage.Display();
        }
    }

    public static class ProxyDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Proxy Pattern ---");
            IImage image = new ProxyImage("photo.png");
            
            Console.WriteLine("Image proxy created (not loaded yet).");
            Console.WriteLine("Calling Display() first time:");
            image.Display(); // Loads and displays
            
            Console.WriteLine("Calling Display() second time (should be cached):");
            image.Display(); // Displays from cache
        }
    }
}