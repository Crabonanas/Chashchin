using CalculatorChashchin;
using CalculatorChashchin;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorChashchin.WPF
{
    public partial class MainWindow : Window
    {
        private readonly Calculator _calculator = new Calculator();

        private double _firstOperand;
        private string _pendingOperator;
        private bool _isNewEntry = true;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var digit = ((Button)sender).Content.ToString();
            if (_isNewEntry || Display.Text == "0")
            {
                Display.Text = digit;
                _isNewEntry = false;
            }
            else
            {
                Display.Text += digit;
            }
        }
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewEntry)
            {
                Display.Text = "0,";
                _isNewEntry = false;
            }
            else if (!Display.Text.Contains(','))
            {
                Display.Text += ",";
            }
        }
        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out var value))
            {
                value = -value;
                Display.Text = value.ToString(CultureInfo.CurrentCulture);
            }
        }
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var op = ((Button)sender).Tag.ToString();

            if (!double.TryParse(Display.Text, out var current))
                return;

            if (_pendingOperator != null && !_isNewEntry)
            {
                TryCompute(current, out var result);
                Display.Text = result.ToString(CultureInfo.CurrentCulture);
                _firstOperand = result;
            }
            else
            {
                _firstOperand = current;
            }

            _pendingOperator = op;
            _isNewEntry = true;
        }
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (_pendingOperator == null) return;
            if (!double.TryParse(Display.Text, out var second)) return;

            TryCompute(second, out var result);
            Display.Text = result.ToString(CultureInfo.CurrentCulture);
            _pendingOperator = null;
            _isNewEntry = true;
        }
        private void TryCompute(double second, out double result)
        {
            result = 0;
            try
            {
                result = _calculator.Calculate(_firstOperand, second, _pendingOperator);
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ClearAll();
                result = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                result = 0;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e) => ClearAll();
        private void ClearAll()
        {
            Display.Text = "0";
            _firstOperand = 0;
            _pendingOperator = null;
            _isNewEntry = true;
        }
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewEntry || Display.Text.Length <= 1)
            {
                Display.Text = "0";
                _isNewEntry = true;
                return;
            }
            Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
        }
    }
}