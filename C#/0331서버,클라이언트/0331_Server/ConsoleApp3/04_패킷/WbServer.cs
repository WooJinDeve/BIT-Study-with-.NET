using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3._04_패킷
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
        public WbServer(int port)
        {
            CreateListenSocket(port);
        }

        #region 소켓 기본 코드
        public void CreateListenSocket(int port)
        {
            //1 소켓생성
            server = new Socket(AddressFamily.InterNetwork,
                                              SocketType.Stream, ProtocolType.Tcp);
            //2, 주소할당
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
            server.Bind(ipep);

            //3. 망 연결
            server.Listen(20);
        }
        #endregion

        #region 쓰레드 생성(Accept)
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
                    //접속 대기
                    Socket client = server.Accept();

                    //접속 메시지 출력
                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                    string temp = string.Format("클라이언트 연결 - {0}:{1}", ip.Address, ip.Port);
                    LogMessage(LogFlag.CONNECT, temp);

                    //소켓 저장
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
            byte[] data = null;

            try
            {
                while (true)
                {
                    //client.Receive(data, data.Length, SocketFlags.None);
                    if (ReceiveData(client ,ref data) == false)
                        throw new Exception("");
                    
                    
                    RecvMessage(client, Encoding.Default.GetString(data).Trim('\0'));
                }
            }
            catch (Exception)
            {
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                string temp = string.Format("클라이언트 연결 해제 - {0}:{1}", ip.Address, ip.Port);
                LogMessage(LogFlag.DISCONNECT, temp);

                sockets.Remove(client);
                client.Close();
            }
        }
        private bool ReceiveData(Socket client, ref byte[] data)
        {
            try
            {
                int total = 0;
                int size = 0;
                int left_data = 0;
                int recv_data = 0;

                // 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = client.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    recv_data = client.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public void SendData(Socket sock, string msg)
        {
            byte[] data = Encoding.Default.GetBytes(msg);
            try
            {
                int total = 0;
                int size = data.Length;
                int left_data = size;
                int send_data = 0;

                // 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = sock.Send(data_size);

                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = sock.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendAllData(Socket sock, string msg, int size, bool b)
        {

            if (b == true)
            {
                foreach (Socket socket in sockets)
                {
                    SendData(socket,msg);
                }
            }
            else
            {
                foreach (Socket socket in sockets)
                {
                    if (socket != sock)
                        SendData(socket, msg);
                }
            }
        }
        #endregion


        public void Dispose()
        {
            LogMessage(LogFlag.START, "서버 종료......");

            //tr.Abort();
            tr.Interrupt();
            server.Close();
        }
    }
}
