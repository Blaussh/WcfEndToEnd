﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using GeoLib.Contracts;

namespace GeoLib.Proxies
{
    public class GeoClient : DuplexClientBase<IGeoService>, IGeoService
    {
        public GeoClient(InstanceContext instanceContext)
            : base(instanceContext)
        {
        }

        public GeoClient(InstanceContext instanceContext, string endpointName)
            : base(instanceContext, endpointName)
        {
        }

        public GeoClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
            : base(instanceContext, binding, address)
        {
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            return Channel.GetZipInfo(zip);
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            return Channel.GetStates(primaryOnly);
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            return Channel.GetZips(state);
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            return Channel.GetZips(zip, range);
        }

        public void UpdateZipCity(IEnumerable<ZipCityData> zipCityData)
        {
            Channel.UpdateZipCity(zipCityData);
        }

        public void OneWayExample()
        {
            Channel.OneWayExample();
        }
    }
}
