using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{    
    class AccountControl
    {
        #region 싱글톤
        public static AccountControl Instance { get; private set; }
        static AccountControl()
        {
            Instance = new AccountControl();
        }
        private AccountControl() { }
        #endregion

        #region 네트웤 사용 필드
        private const string SERVER_IP = "127.0.0.1";//"192.168.0.5"
        private const int SERVER_PORT = 7000;

        private WBClient client = new WBClient();
        #endregion

        public bool Init()
        {
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return false;
            return true;
        }

        #region 기능 시작 메서드(Input->Packet->WbClient)
        public void PrintAll()
        {
            string packet = Packet.PrintAll();
            client.SendData(packet);
        }

        public void MakeAccount()
        {
            int id; 
            string name;
            int balance;

            Input.MakeAccount(out id, out name, out balance);
            string packet = Packet.MakeAccount(id, name, balance);
            client.SendData(packet);
        }

        public void SelectAccount()
        {
            int id;

            Input.SelectAccount(out id);
            string packet = Packet.SelectAccount(id);
            client.SendData(packet);
        }

        public void InputAccount()
        {
            int id;
            int money;

            Input.InputAccount(out id, out money);
            string packet = Packet.InputAccount(id, money);
            client.SendData(packet);
        }

        public void OutputAccount()
        {
            int id;
            int money;

            Input.OutputAccount(out id, out money);
            string packet = Packet.OutputAccount(id, money);
            client.SendData(packet);
        }

        public void TransAccount()
        {
            int fromid;
            int toid;
            int money;

            Input.TransAccount(out fromid, out toid, out money);
            string packet = Packet.TransAccount(fromid, toid, money);
            client.SendData(packet);
        }

        public void DeleteAccount()
        {
            int id;
 
            Input.DeleteAccount(out id);
            string packet = Packet.DeleteAccount(id);
            client.SendData(packet);
        }
        #endregion

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})", flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(string msg)
        {
            //Console.WriteLine("수신 : " + msg);
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "MakeAccount":     OnMakeAccount(sp[1]); break;
                case "SelectAccount":   OnSelectAccount(sp[1]); break;
                case "InputAccount":    OnInputAccount(sp[1]); break;
                case "OutputAccount":   OnOutputAccount(sp[1]); break;
                case "DeleteAccount":   OnDeleteAccount(sp[1]); break;
                case "TransAccount":    OnTransAccount(sp[1]); break;
                case "PrintAll":        OnPrintAll(sp[1]); break;
            }
        }
        #endregion

        #region 네트웤 응답 처리 메서드

        //MAKEACCOUNT(bool, 계좌번호)
        private void OnMakeAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int accid = int.Parse(sp[1]);

            if(b == true)
            {
                Console.WriteLine("{0} 계좌 개설 성공", accid);
            }
            else
            {
                Console.WriteLine("{0} 계좌 개설 실패", accid);
            }            
        }

        //SELECT(bool, 계좌번호, 이름, 잔액정보, 일시)
        private void OnSelectAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int accid = int.Parse(sp[1]);
            string name = sp[2];
            int money = int.Parse(sp[3]);
            DateTime dt = DateTime.Parse(sp[4]);

            if (b == true)
            {
                Console.WriteLine("계좌번호 : " + accid);
                Console.WriteLine("이름 : " + name);
                Console.WriteLine("잔액 : " + money);
                Console.WriteLine("개설일시 : " + dt);
            }
            else
            {
                Console.WriteLine("{0} 없는 계좌", accid);
            }
        }

        //Input(bool, 계좌번호, 입금액, 잔액)
        private void OnInputAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int accid = int.Parse(sp[1]);
            int money = int.Parse(sp[2]);
            int balance = int.Parse(sp[3]);

            if (b == true)
            {
                Console.WriteLine("{0}계좌에 {1}원이 입금되었습니다.",
                    accid, money);
                Console.WriteLine("입금후 잔액 {0}원 입니다.", balance);
            }
            else
            {
                Console.WriteLine("{0} 없는 계좌", accid);
            }
        }

        //Output(bool, 계좌번호, 출금액, 잔액)
        private void OnOutputAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int accid = int.Parse(sp[1]);
            int money = int.Parse(sp[2]);
            int balance = int.Parse(sp[3]);

            if (b == true)
            {
                Console.WriteLine("{0}계좌에 {1}원이 출금되었습니다.",
                    accid, money);
                Console.WriteLine("출금후 잔액 {0}원 입니다.", balance);
            }
            else
            {
                Console.WriteLine("{0} 출금 오류", accid);
            }
        }

        //delete(bool, 계좌번호)
        private void OnDeleteAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int accid = int.Parse(sp[1]);

            if (b == true)
            {
                Console.WriteLine("{0} 계좌 삭제 성공", accid);
            }
            else
            {
                Console.WriteLine("{0} 계좌 삭제 실패", accid);
            }
        }

        //trans(bool, 이체금액)
        private void OnTransAccount(string msg)
        {
            string[] sp = msg.Split('#');
            bool b = bool.Parse(sp[0]);
            int money = int.Parse(sp[1]);

            if (b == true)
            {
                Console.WriteLine("{0}원 계좌 이체 성공", money);
            }
            else
            {
                Console.WriteLine("계좌 이체 실패");
            }
        }

        //PrintAll(Accounts)
        //msg ..$..$..#..$..$..#
        private void OnPrintAll(string msg)
        {
            string[] sp = msg.Split('#');
            for(int i=0; i< sp.Length-1; i++)
            {
                string aa = sp[i];
                string[] acc = aa.Split('$');

                int accid = int.Parse(acc[0]);
                string name = acc[1];
                int balance = int.Parse(acc[2]);
                DateTime dt = DateTime.Parse(acc[3]);

                Console.WriteLine("{0} {1} {2} {3}", accid, name, balance, dt);
            }
        }

        #endregion 

    }
}
