using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfVjezba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isLastEquals = false;
        readonly char[] operators = { '+', '-', '*', '/' }; 

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Calculator";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (isLastEquals && b.Content.ToString().IndexOfAny(operators) != 1)
            {
                TextBoxCalc.Text = "";
            }

            isLastEquals = false;

            if (CheckExprValidity(TextBoxCalc.Text, b.Content.ToString()[1]))
            {
                TextBoxCalc.Text += b.Content.ToString()[1];
            }

            else
            {
                MessageBox.Show("Invalid expression.");
            }

            Keyboard.Focus(TextBoxCalc);
        }

        private bool CheckExprValidity(string expression, char symbol)
        {
            if (expression.Length != 0 && !Char.IsDigit(symbol))
            {
                // 1. case: if the + and - are at the end of the string
                // 2. case: if the operator put in is already at the end of the string (to cover repeating operators)
                if ((operators[0] == expression[expression.Length - 1] || operators[1] == expression[expression.Length - 1])
                    || symbol == expression[expression.Length - 1])
                {
                    return false;
                }

                // 3. case: if there are incompatible operators next to each other
                else if ((expression[expression.Length - 1] == operators[2] || expression[expression.Length - 1] == operators[3])
                    && symbol == operators[2] || symbol == operators[3])
                {
                    return false;
                }
            }

            // 4. case: if the * and / are to be put at the beginning
            else if (symbol == operators[2] || symbol == operators[3])
            {
                return false;
            }

            return true;
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            TextBoxCalc.Text = "";
            Keyboard.Focus(TextBoxCalc);
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            Equals_Result();
        }

        private void Equals_Result()
        {
            int operator_index = ReturnOperatorIndex(TextBoxCalc.Text);

            if (operator_index == -1)
            {
                return;
            }

            TextBoxCalc.Text = Result_Equals(TextBoxCalc.Text);
            isLastEquals = true;
        }

        private int ReturnOperatorIndex(string expression)
        {
            int operator_index = expression.LastIndexOfAny(new char[] {'+', '-'});

            if (operator_index == 0)
            {
                operator_index = -1;
            }

            if (operator_index == -1 || operators.Contains(expression[operator_index - 1]))
            {
                operator_index = expression.LastIndexOfAny(new char[] { '*', '/' });
            }

            if (operator_index == expression.Length - 1)
            {
                MessageBox.Show("You can't have an operator at the end of an expression.");
                return -1;
            }

            return operator_index;
        }

        private string Result_Equals(string expression)
        {
            int operator_index = ReturnOperatorIndex(expression);
            char operator_sign = expression[operator_index];

            string operand1 = expression.Substring(0, operator_index);
            string operand2 = expression.Substring(operator_index + 1, expression.Length - operator_index - 1);

            if (ReturnOperatorIndex(operand1) != -1)
            {
                operand1 = Result_Equals(operand1);
            }

            if (ReturnOperatorIndex(operand2) != -1)
            {
                if (operator_sign == '-')
                    operand2 = "-" + operand2;

                operand2 = Result_Equals(operand2);
            }

            double op1 = Convert.ToDouble(operand1);
            double op2 = Convert.ToDouble(operand2);

            switch(operator_sign)
            {
                case '+':
                    return (op1 + op2).ToString();
                case '-':
                    return (op1 - op2).ToString();
                case '*':
                    return (op1 * op2).ToString();
                case '/':
                    return (op1 / op2).ToString();
                default:
                    return "Error!";
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            TextBoxCalc.Text = TextBoxCalc.Text.Remove(TextBoxCalc.Text.Length - 1);
        }
    }
}
