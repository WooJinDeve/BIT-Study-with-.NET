using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._03_다중접속1대다통신
{
    class Program
    {
        private WbServer server = new WbServer(8000);

        public void Run()
        {
            server.Start(LogMessage, RecvMessage);

            Console.ReadLine();
            server.Dispose();
        }

        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})", flag, msg, DateTime.Now);
        }

        private void RecvMessage(Socket client, string msg)
        {
            Console.WriteLine("수신 : {0} ({1})", msg, DateTime.Now);

            //송신
            server.SendAllData(client, msg, msg.Length,true);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
