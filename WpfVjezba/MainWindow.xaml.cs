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
        char[] operators = { '+', '-', '*', '/' };

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
            TextBoxCalc.Text += b.Content.ToString()[1];
            Keyboard.Focus(TextBoxCalc);
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            Clear_Text();
        }

        private void Clear_Text()
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
            // Check for multiplication and division
            int operator_index = expression.IndexOfAny(new char[] {'+', '-'}, 1);

            if (operator_index == -1)
            {
                operator_index = expression.IndexOfAny(new char[] { '*', '/' }, 1);
            }

            if (operator_index == expression.Length - 1)
            {
                MessageBox.Show("You can't have an operator at the end of an expression.");
                return -1;
            }

            if (expression[0] == '*' || expression[0] == '/')
            {
                MessageBox.Show("You can't have an operator at the beginning of an expression.");
                TextBoxCalc.Text = "";
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
                    return Math.Abs(op1 - op2).ToString();
                case '*':
                    return (op1 * op2).ToString();
                case '/':
                    return (op1 / op2).ToString();
                default:
                    return "Error!";
            }
        }
    }
}
