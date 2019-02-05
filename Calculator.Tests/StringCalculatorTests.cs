using System;
using Calculator;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_NullString_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new StringCalculator().Add(null));
        }

        [TestCase("", ExpectedResult = 0)]
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("1,2", ExpectedResult = 3)]
        [TestCase("1,2,3", ExpectedResult = 6)]
        [TestCase("1\n2,3", ExpectedResult = 6)]
        [TestCase("//;\n1;2", ExpectedResult = 3)]
        [TestCase("//[***]\n1***2***3", ExpectedResult = 6)]
        [TestCase("//[*][%]\n1*2%3", ExpectedResult = 6)]
        [TestCase("//[**][%]\n1**2%3", ExpectedResult = 6)]
        [TestCase("//[**][%][,]\n1**2%3,10", ExpectedResult = 16)]
        public int Add_ValidData_ValidResult(string numbers)
        {
            return new StringCalculator().Add(numbers);
        }

        [Test]
        public void Add_InvalidData_Throws()
        {
            Assert.Throws<FormatException>(() => new StringCalculator().Add("1,\n"));
        }

        [Test]
        public void Add_NegativeNumber_Throws()
        {
            Assert.Throws<ArgumentException>(() => new StringCalculator().Add("-1,2"));
        }

        [Test]
        public void Add_NegativeNumbers_Throws()
        {
            Assert.Throws<ArgumentException>(() => new StringCalculator().Add("-1,2,-3,-4"));
        }

        [TestCase("1,1001", ExpectedResult = 1)]
        [TestCase("1,2000,2,1003", ExpectedResult = 3)]
        public int Add_NumbersGreaterThan1000_IgnoringThem(string numbers)
        {
            return new StringCalculator().Add(numbers);
        }
    }
}
