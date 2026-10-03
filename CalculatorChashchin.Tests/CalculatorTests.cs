using CalculatorChashchin;
using CalculatorChashchin;
using System;
using Xunit;

namespace CalculatorChashchin.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calc = new Calculator();

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(-2, -3, -5)]
        [InlineData(0, 0, 0)]
        [InlineData(2.5, 2.5, 5)]
        public void Add_ReturnsExpected(double a, double b, double expected)
        {
            Assert.Equal(expected, _calc.Add(a, b), 10);
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(3, 5, -2)]
        [InlineData(0, 0, 0)]
        public void Subtract_ReturnsExpected(double a, double b, double expected)
        {
            Assert.Equal(expected, _calc.Subtract(a, b), 10);
        }

        [Theory]
        [InlineData(3, 4, 12)]
        [InlineData(-3, 4, -12)]
        [InlineData(0, 100, 0)]
        public void Multiply_ReturnsExpected(double a, double b, double expected)
        {
            Assert.Equal(expected, _calc.Multiply(a, b), 10);
        }

        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(1, 4, 0.25)]
        [InlineData(-10, 2, -5)]
        public void Divide_ReturnsExpected(double a, double b, double expected)
        {
            Assert.Equal(expected, _calc.Divide(a, b), 10);
        }

        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calc.Divide(5, 0));
        }

        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(9, 0.5, 3)]
        [InlineData(5, 0, 1)]
        [InlineData(2, -2, 0.25)]
        public void Power_ReturnsExpected(double a, double b, double expected)
        {
            Assert.Equal(expected, _calc.Power(a, b), 10);
        }

        [Theory]
        [InlineData(2, 3, "+", 5)]
        [InlineData(10, 4, "-", 6)]
        [InlineData(6, 7, "*", 42)]
        [InlineData(20, 5, "/", 4)]
        [InlineData(2, 10, "^", 1024)]
        public void Calculate_ValidOperator_ReturnsExpected(double a, double b, string op, double expected)
        {
            Assert.Equal(expected, _calc.Calculate(a, b, op), 10);
        }

        [Fact]
        public void Calculate_UnknownOperator_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calc.Calculate(1, 1, "%"));
        }
    }
}