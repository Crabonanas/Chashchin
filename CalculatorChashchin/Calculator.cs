using System;

namespace CalculatorChashchin
{
    public class Calculator
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль недопустимо.");
            return a / b;
        }
        public double Power(double baseValue, double exponent)
        {
            return Math.Pow(baseValue, exponent);
        }
        public double Calculate(double a, double b, string op)
        {
            return op switch
            {
                "+" => Add(a, b),
                "-" => Subtract(a, b),
                "*" => Multiply(a, b),
                "/" => Divide(a, b),
                "^" => Power(a, b),
                _ => throw new ArgumentException($"Неизвестный оператор: {op}")
            };
        }
    }
}