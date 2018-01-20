using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.WindowsHost.Contracts
{
    [ServiceContract(Namespace = "http://www.blaussh.com/Shai/WcfEndToEnd")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
