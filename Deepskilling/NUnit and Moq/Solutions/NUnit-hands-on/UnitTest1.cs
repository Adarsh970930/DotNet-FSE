using NUnit.Framework;
using CalcLibrary; // Use your calculator code

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calc;

        [SetUp]
        public void Init()
        {
            calc = new Calculator();
        }

        [TestCase(2, 3, 5)]
        [TestCase(10, 5, 15)]
        [TestCase(-1, 1, 0)]
        public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
        {
            int result = calc.Add(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}