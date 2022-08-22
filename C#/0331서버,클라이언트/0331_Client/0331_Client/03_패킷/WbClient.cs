using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0331_Client._03_패킷
{
    enum LogFlag { NONE, CONNECT, DISCONNECT, ERROR };

    delegate void LogMessage(LogFlag flag, string msg);
    delegate void RecvMessage(string msg);

    class WbClient
    {
        private LogMessage LogMessage = null;
        private RecvMessage RecvMessage = null;

        private Socket sock = null;
        private const int BUFF_SIZE = 1024;
        public WbClient()
        {
            CreateSocket();
        }

        #region 소켓 생성 및 연결
        private void CreateSocket()
        {
            sock = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Start(string server_ip, int server_port, LogMessage log, RecvMessage rmsg)
        {
            LogMessage = log;
            RecvMessage = rmsg;

            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(server_ip), server_port);
                sock.Connect(ipep);

                LogMessage(LogFlag.CONNECT, "서버에 접속...");

                Thread t = new Thread(WorkThread);
                t.IsBackground = true;
                t.Start();

                return true;
            }
            catch (Exception)
            {
                LogMessage(LogFlag.ERROR, "연결 오류....");
                return false;
            }
        }
        #endregion

        private bool ReceiveData(ref byte[] data)
        {
            try
            {
                int total = 0;
                int size = 0;
                int left_data = 0;
                int recv_data = 0;

                // 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = this.sock.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    recv_data = this.sock.Receive(data, total, left_data, 0);
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


        public void SendData(string msg)
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
                send_data = this.sock.Send(data_size);

                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = this.sock.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Recv만 처리
        private void WorkThread()
        {
            byte[] data = null;
            try
            {
                while (true)
                {

                    if (ReceiveData(ref data) == false)
                        throw new Exception("");
                    //sock.Receive(data, data.Length, SocketFlags.None);
                    RecvMessage(Encoding.Default.GetString(data).Trim('\0'));
                }
            }
            catch (Exception)
            {
                IPEndPoint ip = (IPEndPoint)sock.RemoteEndPoint;
                LogMessage(LogFlag.DISCONNECT, "연결해제");

                sock.Close();
            }
        }
    }
}


