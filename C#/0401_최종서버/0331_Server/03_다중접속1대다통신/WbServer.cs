using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _0331_Server._03_다중접속1대다통신
{
    enum LogFlag { NONE, START, STOP, CONNECT, DISCONNECT, ERROR };

    delegate void LogMessage(LogFlag flag, string msg);
    delegate void RecvMessage(Socket s, string msg);

    internal class WbServer : IDisposable
    {
        private LogMessage LogMessage = null;
        private RecvMessage RecvMessage = null;

        private Socket server = null;
        List<Socket> sockets = new List<Socket>();

        private Thread tr = null;

        private const int BUFF_SIZE = 1024;

        public WbServer(int port)
        {
            CreateListenSocket(port);
        }

        #region 대기소켓 생성 및 클라이언트 접속 대기
        private void CreateListenSocket(int port)
        {
            //1. 소켓 생성
            server = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp);

            //2. 주소 할당
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            server.Bind(ipep);

            //3. 망 연결
            server.Listen(20);
        }

        public void Start(LogMessage log, RecvMessage rmsg)
        {
            LogMessage = log;
            RecvMessage = rmsg;

            tr = new Thread(ListenThread);
            //tr.IsBackground = true;
            tr.Start();
        }

        private void ListenThread()
        {
            LogMessage(LogFlag.START, "서버 동작......");

            try
            {
                while (true)
                {
                    //4. 접속 대기
                    Socket client = server.Accept();

                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                    string temp =
                        string.Format("클라이언트 연결 - {0}:{1}", ip.Address, ip.Port);
                    LogMessage(LogFlag.CONNECT, temp);

                    sockets.Add(client);

                    Thread t = new Thread(WorkThread);
                    t.IsBackground = true;
                    t.Start(client);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion 

        #region 통신 관련 코드

        //Recv만 처리
        private void WorkThread(object obj)
        {
            Socket client = (Socket)obj;
            byte[] data;

            try
            {
                while (true)
                {
                    data = new byte[BUFF_SIZE];
                    client.Receive(data, data.Length, SocketFlags.None);
                    RecvMessage(client, Encoding.Default.GetString(data).Trim('\0'));
                }
            }
            catch (Exception)
            {
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                string temp =
                    string.Format("클라이언트 연결 해제 - {0}:{1}", ip.Address, ip.Port);
                LogMessage(LogFlag.DISCONNECT, temp);

                sockets.Remove(client);
                client.Close();
            }
        }

        public int SendData(Socket sock, string msg, int size)
        {
            int ret = sock.Send(Encoding.Default.GetBytes(msg));
            return ret;
        }

        public void SendAllData(Socket sock, string msg, int size, bool b)
        {
            if (b == true)
            {
                foreach (Socket socket in sockets) //자신을 포함
                {
                    SendData(socket, msg, size);
                }
            }
            else
            {
                foreach (Socket socket in sockets)
                {
                    if( socket != sock)  //자신을 제외
                        SendData(socket, msg, size);
                }
            }
        }

        #endregion

        public void Dispose()
        {
            LogMessage(LogFlag.STOP, "서버 종료......");
            //tr.Abort();
            tr.Interrupt();
            server.Close();
        }
    }
}
