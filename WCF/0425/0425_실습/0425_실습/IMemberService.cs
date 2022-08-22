using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0425_실습
{
    interface IMemberService
    {
        bool InsertMember(string name, string phone);
        Member SelectMember(string name);

        List<Member> SelectAllMember();

    }
}
