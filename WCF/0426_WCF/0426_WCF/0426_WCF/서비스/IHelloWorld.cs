using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0426_WCF
{
    [ServiceContract] // 서비스계약
    interface IHelloWorld
    {
        [OperationContract] // 메시지 계약
        string SayHello();
    }
}
