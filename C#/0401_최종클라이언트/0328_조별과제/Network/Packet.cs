using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{
    static class Packet
    {
        private const string MAKEACCOUNT    = "MakeAccount";
        private const string SELECTACCOUNT  = "SelectAccount";
        private const string PRINTALL       = "PrintAll";
        private const string INPUTACCOUNT   = "InputAccount";
        private const string OUTPUTACCOUNT  = "OutputAccount";
        private const string DELETEACCOUNT  = "DeleteAccount";
        private const string TRANSACCOUNT   = "TransAccount";
        
        public static string MakeAccount(int id, string name, int balance)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}",
                                            MAKEACCOUNT, id, name, balance);
            return packet;
        }

        public static string SelectAccount(int id)
        {
            string packet = string.Format("{0}@{1}",
                                            SELECTACCOUNT, id);
            return packet;
        }

        public static string PrintAll()
        {
            string packet = string.Format("{0}@",
                                            PRINTALL);
            return packet;
        }

        public static string InputAccount(int id, int balance)
        {
            string packet = string.Format("{0}@{1}#{2}",
                                            INPUTACCOUNT, id, balance);
            return packet;
        }

        public static string OutputAccount(int id, int balance)
        {
            string packet = string.Format("{0}@{1}#{2}",
                                            OUTPUTACCOUNT, id, balance);
            return packet;
        }

        public static string DeleteAccount(int id)
        {
            string packet = string.Format("{0}@{1}",
                                            DELETEACCOUNT, id);
            return packet;
        }

        public static string TransAccount(int fromid, int toid, int money)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}",
                                            TRANSACCOUNT, fromid, toid, money);
            return packet;
        }        
    }
}
