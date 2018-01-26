using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager),
                new Uri("http://localhost:8080"), 
                new Uri("net.tcp://localhost:8009"));

            ServiceMetadataBehavior behavior = hostGeoManager.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (behavior == null)
            {
                behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                hostGeoManager.Description.Behaviors.Add(behavior);
            }

            hostGeoManager.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexTcpBinding(), "MEX");
            //string address = "net.tcp://localhost:8009/GeoService";
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof(IGeoService);

            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            #region Add Behavior programatically

            // Add Behavior programatically
            //ServiceDebugBehavior behavior =
            //    hostGeoManager.Description.Behaviors.Find<ServiceDebugBehavior>();

            //if (behavior == null)
            //{
            //    behavior = new ServiceDebugBehavior();
            //    behavior.IncludeExceptionDetailInFaults = true;
            //    hostGeoManager.Description.Behaviors.Add(behavior);
            //}
            //else
            //{
            //    behavior.IncludeExceptionDetailInFaults = true;
            //}
            // End Add Behavior programatically 
            #endregion

            hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();
            hostGeoManager.Close();

        }
    }
}
