using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{    
    class Control
    {
        #region 싱글톤
        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();
        }
        private Control() { }
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

        public void SearchBook()
        {
            string msg = WbLib.getString("도서 검색 명");

            string packet = Packet.SearchBook(msg);
            client.SendData(packet);
        }

        public void SelectAllBookName()
        {
            string packet = Packet.SelectAllBookName();
            client.SendData(packet);
        }

        public void SelectBookName()
        {
            string msg = WbLib.getString("도서명 검색");
            string packet = Packet.SelectBookName(msg);
            client.SendData(packet);
        }

        public void SelectAllBook()
        {
            string packet = Packet.SelectAllBook();
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
                case "SearchBook":           OnSearchBook(sp[1]); break;
                case "SelectAllBookName":    OnSelectAllBookName(sp[1]); break;
                case "SelectBookName":       OnSelectBookName(sp[1]); break;
                case "SelectAllBook":         OnSelectAllBook(sp[1]); break;
            }
        }
        #endregion

        #region 네트웤 응답 처리 메서드

        //MAKEACCOUNT(bool, 계좌번호)
        private void OnSearchBook(string msg)
        {
            bool b = bool.Parse(msg);

            if(b == true)
            {
                Console.WriteLine("검색 성공");
            }
            else
            {
                Console.WriteLine("검색 실패");
            }            
        }

        private void OnSelectAllBookName(string msg)
        {
            string[] f1 = msg.Split('#');
            bool b = bool.Parse(f1[0]);

            if (b == true)
            {
                for(int i=1; i<f1.Length-1; i++)
                {
                    Console.WriteLine("[{0}] {1}", i, f1[i]);
                }
            }
            else
            {
                Console.WriteLine("검색 실패");
            }
        }

        private void OnSelectBookName(string msg)
        {
            string[] f1 = msg.Split('#');
            bool b = bool.Parse(f1[0]);

            if(b == true)
            {
                Console.WriteLine("[Title] " + f1[2]);
                Console.WriteLine("[Author] " + f1[3]);
                Console.WriteLine("[Image] " + f1[4]);
                Console.WriteLine("[Price] " + f1[5]);
                Console.WriteLine("[Publisher] " + f1[6]);
                Console.WriteLine("[Description] " + f1[7]);
            }
            else
            {
                Console.WriteLine("검색 실패");
            }
        }

        private void OnSelectAllBook(string msg)
        {
            string [] f1 = msg.Split('$');
            bool b = bool.Parse(f1[0]);
            if (b == true)
            {
                Console.WriteLine(f1[0]);
                for (int i = 1; i < f1.Length - 1; i++)
                {
                    string[] f2 = f1[i].Split('#');
                    Console.WriteLine("[Title] " + f2[0]);
                    Console.WriteLine("[Author] " + f2[1]);
                    Console.WriteLine("[Image] " + f2[2]);
                    Console.WriteLine("[Price] " + f2[3]);
                    Console.WriteLine("[Publisher] " + f2[4]);
                    Console.WriteLine("[Description] " + f2[5]);
                    Console.WriteLine();
                }
            }


        }
        #endregion

    }
}
