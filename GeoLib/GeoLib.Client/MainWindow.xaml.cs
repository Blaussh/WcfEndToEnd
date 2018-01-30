using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using GeoLib.Proxies;
using GeoLib.Contracts;
using System.Threading;

namespace GeoLib.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "")
            {
                GeoClient proxy = new GeoClient();

                try
                {
                    ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                    if (data != null)
                    {
                        lblCity.Content = data.City;
                        lblState.Content = data.State;
                    }

                    proxy.Close();
                }
                catch (FaultException ex)
                {
                    MessageBox.Show("Fault Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                GeoClient proxy = new GeoClient();

                IEnumerable<ZipCodeData> data = proxy.GetZips(txtState.Text);
                if (data != null)
                    lstZips.ItemsSource = data;

                proxy.Close();
            }
        }

        private void btnUpdateBatch_Click(object sender, RoutedEventArgs e)
        {
            List<ZipCityData> cityZipList = new List<ZipCityData>()
            {
            	new ZipCityData() { ZipCode = "07035", City = "Bedrock" },
                new ZipCityData() { ZipCode = "33033", City = "End of the World" },
                new ZipCityData() { ZipCode = "90210", City = "Alderan" },
                new ZipCityData() { ZipCode = "07094", City = "Storybrooke" }
            };

            lstUpdates.Items.Clear();

            try
            {
                GeoClient proxy = new GeoClient();

                proxy.UpdateZipCity(cityZipList);

                proxy.Close();

                MessageBox.Show("Updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnPutBack_Click(object sender, RoutedEventArgs e)
        {
            List<ZipCityData> cityZipList = new List<ZipCityData>()
            {
            	new ZipCityData() { ZipCode = "07035", City = "Lincoln Park" },
                new ZipCityData() { ZipCode = "33033", City = "Homestead" },
                new ZipCityData() { ZipCode = "90210", City = "Beverly Hills" },
                new ZipCityData() { ZipCode = "07094", City = "Secaucus" }
            };

            lstUpdates.Items.Clear();

            Thread thread = new Thread(() =>
            {
                try
                {
                    GeoClient proxy = new GeoClient();
                    proxy.Open();

                    proxy.UpdateZipCity(cityZipList);

                    proxy.Close();

                    MessageBox.Show("Updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            });

            thread.Start();
        }

        private void btnOneWay_Click(object sender, RoutedEventArgs e)
        {
            GeoClient proxy = new GeoClient();

            proxy.OneWayExample();
            MessageBox.Show("Oneway Example called. Back at client.");

            proxy.Close();

            MessageBox.Show("Proxy is closed");
        }
    }
}
