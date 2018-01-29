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
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Calculator";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (isLastEquals && !(b.Content.ToString().Contains("+") || b.Content.ToString().Contains("-") ||
                b.Content.ToString().Contains("/") || b.Content.ToString().Contains("*")) )
            {
                Clear_Textbox();
            }

            isLastEquals = false;
            TextBoxCalc.Text += b.Content.ToString()[1];
            Keyboard.Focus(TextBoxCalc);
        }

        private void Clear_Textbox()
        {
            TextBoxCalc.Text = "";
            Keyboard.Focus(TextBoxCalc);
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            Clear_Textbox();
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            Equals_Result();
        }

        private void Equals_Result()
        {
            char operator_sign = ReturnOperatorSign();

            if (operator_sign == '!')
            {
                MessageBox.Show("There's been an error!");
                TextBoxCalc.Text = "";
                return;
            }

            TextBoxCalc.Text = Result_Equals(operator_sign);
            isLastEquals = true;
        }

        private char ReturnOperatorSign()
        {
            if (TextBoxCalc.Text.Contains("+"))
            {
                return '+';
            }

            else if (TextBoxCalc.Text.Contains("-"))
            {
                return '-';
            }

            else if (TextBoxCalc.Text.Contains("*"))
            {
                return '*';
            }

            else if (TextBoxCalc.Text.Contains("/"))
            {
                return '/';
            }

            else
            {
                return '!';
            }
        }

        private string Result_Equals(char operator_sign)
        {
            string[] operands = TextBoxCalc.Text.Split(operator_sign);

            double op1 = Convert.ToDouble(operands[0]);
            double op2 = Convert.ToDouble(operands[1]);

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
    }
}
