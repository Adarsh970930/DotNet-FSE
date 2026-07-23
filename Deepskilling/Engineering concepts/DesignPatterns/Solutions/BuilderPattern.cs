using System;

namespace DesignPatterns
{
    public class Computer
    {
        public string CPU { get; }
        public string RAM { get; }
        public string Storage { get; }
        public string GPU { get; }

        private Computer(Builder builder)
        {
            CPU = builder.CPU;
            RAM = builder.RAM;
            Storage = builder.Storage;
            GPU = builder.GPU;
        }

        public override string ToString()
        {
            return $"Computer Config -> CPU: {CPU}, RAM: {RAM}, Storage: {Storage}, GPU: {GPU}";
        }

        public class Builder
        {
            public string CPU { get; set; } = "Intel Core i3";
            public string RAM { get; set; } = "8GB";
            public string Storage { get; set; } = "256GB SSD";
            public string GPU { get; set; } = "Integrated Graphics";

            public Builder SetCPU(string cpu) { CPU = cpu; return this; }
            public Builder SetRAM(string ram) { RAM = ram; return this; }
            public Builder SetStorage(string storage) { Storage = storage; return this; }
            public Builder SetGPU(string gpu) { GPU = gpu; return this; }

            public Computer Build() => new Computer(this);
        }
    }

    public static class BuilderDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Builder Pattern ---");
            Computer gamingPc = new Computer.Builder()
                .SetCPU("Intel Core i9")
                .SetRAM("32GB")
                .SetStorage("2TB NVMe SSD")
                .SetGPU("NVIDIA RTX 4090")
                .Build();

            Computer officePc = new Computer.Builder()
                .SetCPU("Intel Core i5")
                .SetRAM("16GB")
                .Build();

            Console.WriteLine(gamingPc);
            Console.WriteLine(officePc);
        }
    }
}