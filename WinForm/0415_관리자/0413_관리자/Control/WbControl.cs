using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_관리자
{
    class WbControl
    {
        #region 싱글톤 패턴
        private WbControl() { }
        public static WbControl Instance { get; private set; }
        static WbControl()
        {
            Instance = new WbControl();
        }
        #endregion

        #region 네트웤 사용 필드
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 7000;

        private WBClient client = new WBClient();
        #endregion

        private MainForm form = null;

        #region Form Load 시 서버 연결
        public void Init(MainForm mainform)
        {
            form = mainform; 
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return;
        }
        #endregion

   
        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            string temp = string.Format("[{0}] : {1} ({2})", flag, msg, DateTime.Now.ToString());
            if (flag == LogFlag.CONNECT || flag == LogFlag.DISCONNECT)
                form.Text = temp;
        }

        private void RecvMessage(string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "GetMemberAllList": OnGetMemberAllList_Ack(sp[1]); break;
        
            }
        }
        #endregion

        #region 회원전체정보 요청 - 응답
        public void GetMemberListAll()
        {
            string packet = Packet.GetMemberAllList();
            client.SendData(packet);
        }

        public void OnGetMemberAllList_Ack(string msg)
        {
            string[] sp = msg.Split('$');
            form.ListViewPrintAll(sp);
        }
        #endregion
    }
}
