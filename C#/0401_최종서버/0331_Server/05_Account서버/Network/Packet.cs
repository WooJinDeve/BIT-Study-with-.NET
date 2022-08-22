using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Server._05_Account서버
{ 
    class Packet
    {
        private const string MAKEACCOUNT_ACK    = "MakeAccount";
        private const string SELECTACCOUNT_ACK  = "SelectAccount";
        private const string PRINTALL_ACK       = "PrintAll";
        private const string INPUTACCOUNT_ACK   = "InputAccount";
        private const string OUTPUTACCOUNT_ACK  = "OutputAccount";
        private const string DELETEACCOUNT_ACK  = "DeleteAccount";
        private const string TRANSACCOUNT_ACK   = "TransAccount";

        //MAKEACCOUNT(bool, 계좌번호)
        public static string MakeAccount(bool b, int id)
        {
            string packet = string.Format("{0}@{1}#{2}",
                                            MAKEACCOUNT_ACK, b, id);
            return packet;
        }

        //SELECT(bool, 계좌번호, 이름, 잔액정보, 일시)
        public static string SelectAccount(bool b, int id, string name, int balance, DateTime dt)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}#{5}",
                                            SELECTACCOUNT_ACK, b, id,name, balance, dt);
            return packet;
        }

        //Input(bool, 계좌번호, 입금액, 잔액)
        public static string InputAccount(bool b, int id, int money, int balance)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}",
                                            INPUTACCOUNT_ACK, b, id, money, balance);
            return packet;
        }

        //Output(bool, 계좌번호, 출금액, 잔액)
        public static string OutputAccount(bool b, int id, int money, int balance)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}",
                                            OUTPUTACCOUNT_ACK, b, id, money, balance);
            return packet;
        }

        //delete(bool, 계좌번호)
        public static string DeleteAccount(bool b, int id)
        {
            string packet = string.Format("{0}@{1}#{2}",
                                            DELETEACCOUNT_ACK, b, id);
            return packet;
        }

        //trans(bool, 이체금액)
        public static string TransAccount(bool b, int money)
        {
            string packet = string.Format("{0}@{1}#{2}",
                                            TRANSACCOUNT_ACK, b, money);
            return packet;
        }

        //PrintAll(Accounts)
        public static string PrintAll(List<Account> accounts )
        {
            string msg = "";
            foreach(Account acc in accounts)
            {
                msg += acc.Id + "$" + acc.Name + "$" + acc.Balance + "$" + acc.Datetime + "#";
            }
            string packet = string.Format("{0}@{1}",
                                            PRINTALL_ACK, msg);
            return packet;
        }
    }
}
