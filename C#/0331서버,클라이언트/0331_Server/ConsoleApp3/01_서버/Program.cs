using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. 소켓생성
            Socket server = new Socket(AddressFamily.InterNetwork,
                                                      SocketType.Stream, ProtocolType.Tcp);
            //2, 주소할당
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 7000);
            server.Bind(ipep);

            //3. 망 연결
            server.Listen(20);
            //------------------------------------------------------
     
            Console.WriteLine("서버 시작... 클라이언트 접속 대기중...");

            //4. 접속 대기
            Socket client = server.Accept();  // 클라이언트 접속 대기
                                              // 접속한 클라이언트 아이피 주소와 접속 포트번호 출력

            IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("클라이언트 연결 - {0}:{1}", ip.Address, ip.Port);

            //1) 전송..(byte[])
            byte[] data = Encoding.Default.GetBytes("환영합니다.*^^*");
            client.Send(data, data.Length, SocketFlags.None); // 문자열 전송

            //2) 수신
            data = new byte[1024];
            if (client.Receive(data, data.Length, SocketFlags.None) != 0)   // 수신한 문자열이 있으면 화면에 출력
                Console.WriteLine("수신 메시지: " + Encoding.Default.GetString(data));
            else
                Console.WriteLine("수신 데이터 없음...");

            //소켓 연결 종료
            client.Close();         //  소켓 연결 끊기
            server.Close();
        }
    }
}
