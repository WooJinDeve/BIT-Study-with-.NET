using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0331_Client
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
            catch(Exception ex)
            {
                LogMessage(LogFlag.ERROR, "연결 오류....");
                return false;
            }
        }
        #endregion

        public int SendData(string msg, int size)
        {
            byte[] data = Encoding.Default.GetBytes(msg);

            int ret = sock.Send(Encoding.Default.GetBytes(msg));
            return ret;
        }

        //Recv만 처리
        private void WorkThread()
        {
            byte[] data;
            try
            {
                while (true)
                {
                    data = new byte[BUFF_SIZE];
                    sock.Receive(data, data.Length, SocketFlags.None);
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
