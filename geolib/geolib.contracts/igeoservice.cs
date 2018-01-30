using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        ZipCodeData GetZipInfo(string zip);

        [OperationContract]
        IEnumerable<string> GetStates(bool primaryOnly);

        [OperationContract(Name = "GetZipsByState")]
        IEnumerable<ZipCodeData> GetZips(string state);

        [OperationContract(Name = "GetZipsForRange")]
        IEnumerable<ZipCodeData> GetZips(string zip, int range);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateZipCity(IEnumerable<ZipCityData> zipCityData);

        [OperationContract(IsOneWay = true)]
        void OneWayExample();
    }

    [ServiceContract]
    public interface IUpdateZipCallback
    {
        [OperationContract(IsOneWay = true)]
        void ZipUpdated(ZipCityData zipCityData);
    }
}
