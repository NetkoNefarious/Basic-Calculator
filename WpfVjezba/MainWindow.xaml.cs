﻿using Microsoft.Win32;
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
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Calculator";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            TextBoxCalc.Text += b.Content.ToString()[1];
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            TextBoxCalc.Text = "";
        }

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            char operator_sign = ReturnOperatorSign();

            if (operator_sign == '!')
            {
                MessageBox.Show("There's been an error!");
                TextBoxCalc.Text = "";
                return;
            }

            TextBoxCalc.Text = Result_Equals(operator_sign);            
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
