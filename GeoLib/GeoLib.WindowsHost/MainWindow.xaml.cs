using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GeoLib.Services;
using GeoLib.WindowsHost.Contracts;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId + " | Process " + Process.GetCurrentProcess().Id.ToString() + ")";
            _syncContext = SynchronizationContext.Current;
        }


        private ServiceHost _hostGeoManager = null;
        private ServiceHost _hostMessageManager = null;

        private SynchronizationContext _syncContext = null;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostMessageManager = new ServiceHost(typeof(MessageManager));

            _hostGeoManager.Open();
            _hostMessageManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager.Close();
            _hostMessageManager.Close();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            SendOrPostCallback callback = new SendOrPostCallback(arg =>
            {
                lblMessage.Content = message + Environment.NewLine + " (marshalled from thread " + threadId + " to thread " +
                                     Thread.CurrentThread.ManagedThreadId.ToString() +
                                     " | Process " + Process.GetCurrentProcess().Id.ToString() + ")";
            });

            _syncContext.Send(callback, null);
        }

        private async void btnInProc_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");

                IMessageService proxy = factory.CreateChannel();

                proxy.ShowMessage(DateTime.Now.ToLongTimeString() + " from in-process call.");
                factory.Close();
            });
        }
    }
}
