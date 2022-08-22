using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Client._03_패킷
{
    class Program
    {
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 8000;

        private WbClient client = new WbClient();

        public void Run()
        {
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return;

            while (true)
            {
                string msg = Console.ReadLine();
                if (msg == null)
                    break;

                client.SendData(msg);
            }
        }

        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})", flag, msg, DateTime.Now);
        }

        private void RecvMessage(string msg)
        {
            Console.WriteLine("수신 : {0} ({1})", msg, DateTime.Now);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
