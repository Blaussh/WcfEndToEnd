using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace GeoLib.WindowsHost
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;
        }


        
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {


            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {


            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }
    }
}
