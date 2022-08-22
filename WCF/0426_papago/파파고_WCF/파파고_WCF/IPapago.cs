using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace 파파고_WCF
{
    [ServiceContract]
    interface IPapago
    {
        [OperationContract]
        string papago(string source, string target, string text);
        [OperationContract]
        string RetMssage(string msg);
    }
}
