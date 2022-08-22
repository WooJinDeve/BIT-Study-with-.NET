using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. 소켓 생성
            Socket server = new Socket(AddressFamily.InterNetwork,
                                     SocketType.Stream, ProtocolType.Tcp);
            //2. 연결 요청
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
            server.Connect(ipep); 

            Console.WriteLine("서버에 접속...");  // 만약 서버 접속이 실패하면 예외 발생
            IPEndPoint ip = (IPEndPoint)server.LocalEndPoint;
            Console.WriteLine("Local 연결 - {0}:{1}", ip.Address, ip.Port);

            //(1) 수신
            byte[] data = new byte[1024];
            server.Receive(data);
            Console.WriteLine("수신 데이터: " + Encoding.Default.GetString(data));

            //(2) 전송
            server.Send(Encoding.Default.GetBytes("데이터 전송..."));

            //(3) 종료
            server.Close();
        }
    }
}
