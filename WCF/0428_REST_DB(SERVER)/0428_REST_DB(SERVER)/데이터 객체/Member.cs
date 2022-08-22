using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _0428_REST_DB_SERVER_
{
    [DataContract]
    public class Member
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Phone { get; set; }

        public Member() { }

        public Member(string name, int age, string phone)
        {
            Name = name;
            Age = age;
            Phone = phone;
        }
    }
}