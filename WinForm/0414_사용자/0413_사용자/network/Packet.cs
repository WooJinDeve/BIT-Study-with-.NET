using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_사용자
{
    static class Packet
    {
        private const string LOGIN = "Login";        //@검색할정보
        private const string NEWMEMBER = "NewMember";
        private const string IDTOMEMBER = "IdtoMember";
        private const string SELECTMEMBER = "SelectMember";
        private const string DELETEMEMBER = "DeleteMember";
        private const string UPDATEMEMBER = "UpdateMember";
        private const string LOGOUT = "Logout";


        

        public static string Login(string id, string pw)
        {
            string packet = string.Format("{0}@{1}#{2}", LOGIN, id, pw);
            return packet;
        }

        public static string NewMember(string id, string pw, string name, string phone, int age)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}#{5}", NEWMEMBER, id, pw, name, phone, age);
            return packet;
        }

        public static string IdtoMember(string id)
        {
            string packet = string.Format("{0}@{1}", IDTOMEMBER, id);
            return packet;
        }

        public static string SelectMember(string id)
        {
            string packet = string.Format("{0}@{1}", SELECTMEMBER, id);
            return packet;
        }

        public static string DeleteMember(string id)
        {
            string packet = string.Format("{0}@{1}", DELETEMEMBER, id);
            return packet;
        }

        public static string UpdateMember(string id, string phone, int age)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}", UPDATEMEMBER, id, phone, age);
            return packet;
        }

        public static string Logout(string id)
        {
            string packet = string.Format("{0}@{1}", LOGOUT, id);
            return packet;
        }
    }
}
