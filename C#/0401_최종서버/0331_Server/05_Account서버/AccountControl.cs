using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Server._05_Account서버
{
    class AccountControl
    {
        private List<Account> accounts = new List<Account>();

        #region 싱글톤
        public static AccountControl Instance { get; private set; }
        static AccountControl()
        {
            Instance = new AccountControl();
        }
        private AccountControl() { }
        #endregion

        #region 네트웤 사용 필드
        private const int SERVER_PORT = 7000;
        private WbServer server = null;
        #endregion

        #region Application 호출 메시드
        public void Start()
        {
            server = new WbServer(SERVER_PORT); //소켓생성--> listen
            server.Start(LogMessage, RecvMessage); //ListenThread
        }

        public void Stop()
        {
            server.Dispose(); //ListenThread를 종료, 대기소켓close
            server = null;
        }
        #endregion

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})",
                        flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(Socket client, string msg)
        {
            //Console.WriteLine("수신 : " + msg);
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "MakeAccount":     OnMakeAccount(client, sp[1]); break;
                case "SelectAccount":   OnSelectAccount(client, sp[1]); break;
                case "InputAccount":    OnInputAccount(client, sp[1]); break;
                case "OutputAccount":   OnOutputAccount(client, sp[1]); break;
                case "DeleteAccount":   OnDeleteAccount(client, sp[1]); break;
                case "TransAccount":    OnTransAccount(client, sp[1]); break;
                case "PrintAll":        OnPrintAll(client, sp[1]); break;
            }
        }
        #endregion

        #region 메시지 처리 핸들러(MakeAccount)
        private void OnMakeAccount(Socket client, string msg)
        {
            int id;
            bool b = MakeAccount(msg, out id);
            string packet = Packet.MakeAccount(b, id);
            server.SendData(client, packet);
        }

        private bool MakeAccount(string msg, out int id)
        {
            string[] sp = msg.Split('#');
            int accid = int.Parse(sp[0]);
            string name = sp[1];
            int balance = int.Parse(sp[2]);

            id = accid;

            if (IdUniqCheck(accid) == false)
                return false;

            //저장
            accounts.Add(new Account(accid, name, balance));
            return true;
        }

        private bool IdUniqCheck(int id)
        {
            foreach (Account acc in accounts)
            {
                if (acc.Id == id)
                    return false;
            }
            return true;
        }

        #endregion

        #region 메시지 처리 핸들러(SelectAccount)
        private void OnSelectAccount(Socket client, string msg)
        {
            int id = 0;
            string name = "";
            int balance = 0;
            DateTime dt = DateTime.Now;

            bool b = SelectAccount(msg, ref id, ref name, ref balance, ref dt);
            string packet = Packet.SelectAccount(b, id, name, balance, dt);
            server.SendData(client, packet);
        }

        private bool SelectAccount(string msg, ref int id, ref string name, ref int balance, ref DateTime dt)
        {
            string[] sp = msg.Split('#');
            int accid = int.Parse(sp[0]);

            Account acc = IdToAccount(accid);
            if (acc == null)
                return false;

            id = acc.Id;
            name = acc.Name;
            balance = acc.Balance;
            dt = acc.Datetime;
            return true;
        }

        private Account IdToAccount(int id)
        {
            foreach (Account acc in accounts)
            {
                if (acc.Id == id)
                    return acc;
            }
            return null;
        }

        #endregion

        #region 메시지 처리 핸들러(InputAccount)
        private void OnInputAccount(Socket client, string msg)
        {
            int id = 0;
            int money = 0;
            int balance = 0;

            bool b = InputAccount(msg, ref id, ref money, ref balance);
            string packet = Packet.InputAccount(b, id, money, balance);
            server.SendData(client, packet);
        }

        private bool InputAccount(string msg, ref int id, ref int money, ref int balance)
        {
            string[] sp = msg.Split('#');
            int accid = int.Parse(sp[0]);
            int inputmoney = int.Parse(sp[1]);

            Account acc = IdToAccount(accid);
            if (acc == null)
                return false;

            if (acc.AddMoney(inputmoney) == false)
                return false;

            id = acc.Id;
            money = inputmoney;
            balance = acc.Balance;
            return true;

        }
        #endregion

        #region 메시지 처리 핸들러(InputAccount)
        private void OnOutputAccount(Socket client, string msg)
        {
            int id = 0;
            int money = 0;
            int balance = 0;

            bool b = OutputAccount(msg, ref id, ref money, ref balance);
            string packet = Packet.OutputAccount(b, id, money, balance);
            server.SendData(client, packet);
        }

        private bool OutputAccount(string msg, ref int id, ref int money, ref int balance)
        {
            string[] sp = msg.Split('#');
            int accid = int.Parse(sp[0]);
            int inputmoney = int.Parse(sp[1]);

            Account acc = IdToAccount(accid);
            if (acc == null)
                return false;

            if (acc.MinMoney(inputmoney) == false)
                return false;

            id = acc.Id;
            money = inputmoney;
            balance = acc.Balance;
            return true;

        }
        #endregion

        #region 메시지 처리 핸들러(DeleteAccount)
        private void OnDeleteAccount(Socket client, string msg)
        {
            int id;
            bool b = DeleteAccount(msg, out id);
            string packet = Packet.DeleteAccount(b, id);
            server.SendData(client, packet);
        }

        private bool DeleteAccount(string msg, out int id)
        {
            string[] sp = msg.Split('#');
            int accid = int.Parse(sp[0]);

            id = accid;

            Account acc = IdToAccount(accid);
            if (acc == null)
                return false;

            //삭제
            accounts.Remove(acc);

            return true;
        }
        #endregion

        #region 메시지 처리 핸들러(TransAccount)
        private void OnTransAccount(Socket client, string msg)
        {
            int money;
            bool b = TransAccount(msg, out money);
            string packet = Packet.TransAccount(b, money);
            server.SendData(client, packet);
        }

        private bool TransAccount(string msg, out int money)
        {
            string[] sp     = msg.Split('#');
            int fromid       = int.Parse(sp[0]);
            int targetid    = int.Parse(sp[1]);
            int transmoney  = int.Parse(sp[2]);

            money = transmoney;

            Account fromacc = IdToAccount(fromid);
            Account targetacc = IdToAccount(targetid);
            if (fromacc == null || targetacc == null)
                return false;

            if( fromacc.MinMoney(transmoney) == true)
            {
                targetacc.AddMoney(transmoney);
            }
            return true;
        }
        #endregion

        #region 메시지 처리 핸들러(PrintAll)
        private void OnPrintAll(Socket client, string msg)
        {
            List<Account> accounts = null;
            accounts = PrintAll(msg);
            string packet = Packet.PrintAll(accounts);
            server.SendData(client, packet);
        }

        private List<Account> PrintAll(string msg)
        {
            return accounts;
        }
        #endregion

    }
}
