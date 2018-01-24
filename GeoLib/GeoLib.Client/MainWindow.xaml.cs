using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows;
using GeoLib.Client.Contracts;
using GeoLib.Contracts;
using GeoLib.Proxies;

namespace GeoLib.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _proxy = new GeoClient("tcpEP");
            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        private GeoClient _proxy = null;

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            //Using configuration for EndPoints
            if (txtZipCode.Text != "")
            {
                GeoClient proxy = new GeoClient("tcpEP");
                ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                if (data != null)
                {
                    lblCity.Content = data.City;
                    lblState.Content = data.State;
                }
                proxy.Close();
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            //NoConfig
            if (txtState.Text != null)
            {
                //Use programaticaly proxy creation
                //
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();
                //GeoClient proxy = new GeoClient(binding, address);

                //Use Configurably proxy creation
                //GeoClient proxy = new GeoClient("tcpEP");
                IEnumerable<ZipCodeData> data = _proxy.GetZips(txtState.Text);
                if (data != null)
                {
                    lstZips.ItemsSource = data;
                }
                //proxy.Close();
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8010/MessageService");
            Binding binding = new NetTcpBinding();

            //ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>(binding, address);
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMsg(txtMessage.Text);
            factory.Close();
        }
    }
}
