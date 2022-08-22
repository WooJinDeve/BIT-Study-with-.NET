using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _0425_실습
{
    public class Member
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Member() { }

        public Member(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public override string ToString()
        {
            return "이름 : " + Name + " / " + "전화번호 : " + Phone;
        }
    }
}