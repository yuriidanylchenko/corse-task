﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace course_task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            WindowReg WindowReg = new WindowReg();
            WindowReg.Show();
        }

        private void buttonLog_Click(object sender, RoutedEventArgs e)
        {
            WindowLog WindowLog = new WindowLog();
            WindowLog.Show(); 
        }
    }
}
