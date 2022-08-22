using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace _0426_DBWCF
{
    [DataContract]
    public class Member
    {
        [DataMember(Order = 1, IsRequired = true, Name ="MemberName")]
        public string Name { get; set; }
        [DataMember(Order = 2, IsRequired = true, Name = "MemberPhone")]
        public string Phone { get; set; }

        public Member() { }

        public Member(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}
