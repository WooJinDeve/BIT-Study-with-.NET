using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_사용자
{
    class Member
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

        public Member(bool b, string id, string pw, string name, string phone, int age, DateTime datetime)
        {
            IsLogin = b;
            Id = id;
            Pw = pw;
            Name = name;
            Phone = phone;
            Age = age;
            DateTime = datetime;
        }

    }
}
