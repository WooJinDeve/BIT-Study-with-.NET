using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0414_Server
{

    internal class Member
    {
        public bool IsLogin { get; set; }
        public string Id { get; private set; }
        public string Pw { get; set; }
        public string Name { get; private set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public DateTime DateTime { get; private set; }

        public Member(string id, string pw, string name, string phone, int age)
        {
            IsLogin = false;
            Id = id;
            Pw = pw;
            Name = name;
            Phone = phone;
            Age = age;
            DateTime = DateTime.Now;
        }
        public Member(bool islogin, string id, string pw, string name, string phone, int age, DateTime datatime)
        {
            IsLogin = islogin;
            Id = id;
            Pw = pw;
            Name = name;
            Phone = phone;
            Age = age;
            DateTime = datatime;
        }


    }
}
