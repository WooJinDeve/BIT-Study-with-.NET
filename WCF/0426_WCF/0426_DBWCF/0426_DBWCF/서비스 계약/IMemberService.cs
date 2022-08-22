using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0426_DBWCF
{
    [ServiceContract]
    interface IMemberService
    {
        [OperationContract]
        bool InsertMember(string name, string phone);

        [OperationContract]
        Member SelectMember(string name);

        [OperationContract]
        List<Member> SelectAllMember();

    }
}
