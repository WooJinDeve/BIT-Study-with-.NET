using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Server._04_패킷경계
{
    internal class Program
    {
        private WbServer server = new WbServer(7000);

        public void Run()
        {
            server.Start(LogMessage, RecvMessage);

            Console.ReadLine();
            server.Dispose();
        }

        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})",
                flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(Socket client, string msg)
        {
            Console.WriteLine("수신 : " + msg);

            //송신
            //server.SendData(client, msg);
            server.SendAllData(client, msg,true);
        }

        public static void Main()
        {
            Program p = new Program();
            p.Run();
        }
    }
}
