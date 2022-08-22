using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _0414_Server
{
    class Control
    {
        #region 싱글톤
        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();
        }
        private Control() { }
        #endregion

        #region 네트웤 사용 필드
        private const int SERVER_PORT = 7000;
        private WbServer server = null;
        #endregion

        #region 시작과 종료시점(데이터 베이스 연결/소켓 및 종료 처리)

        public bool Init()
        {
            if (db.Open() == false)
                return false;

            server = new WbServer(SERVER_PORT); //소켓생성--> listen
            server.Start(LogMessage, RecvMessage); //ListenThread
            return true;
        }

        public void Exit()
        {
            server.Dispose(); //ListenThread를 종료, 대기소켓close
            server = null;

            db.Close();
        }
        #endregion 

        private MemberDatabase db = new MemberDatabase();

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})",
                        flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(Socket client, string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                //[관리자]
                case "GetMemberAllList": OnGetMemberAllList(client); break;
                //[사용지]
                case "Login": OnLogin(client, sp[1]);  break;
                case "NewMember": OnNewMember(client, sp[1]);  break;
                case "IdtoMember": OnIdtoMember(client, sp[1]);  break;
                case "SelectMember": OnSelectMember(client, sp[1]);  break;
                case "DeleteMember": OnDeleteMember(client, sp[1]); break;
                case "UpdateMember": OnUpdateMember(client, sp[1]);  break;
                case "Logout": OnLogout(client, sp[1]); break;

            }
        }
        #endregion

        #region 수신처리를 위한 핸들러함수
        //[관리자]
        private void OnGetMemberAllList(Socket client)
        {
            try
            {
                List<Member> members = db.GetMemberAllList();
                string packet = Packet.GetMemberAllList_ACK(true, members);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 로그인 응답
        public void OnLogin(Socket client, string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string name = db.Login(sp[0], sp[1]);
                string packet = Packet.Login_ACK(true, name);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.Login_ACK(false, null);
                server.SendData(client, packet);
            }
          
        }

        public void OnNewMember(Socket client, string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                db.NewMember(new Member(sp[0], sp[1], sp[2], sp[3], int.Parse(sp[4])));
                string packet = Packet.NewMember_ASK(true);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.NewMember_ASK(false);
                server.SendData(client, packet);
            }
           

        }

        public void OnIdtoMember(Socket client, string msg)
        {
            try
            {
                Member mem = db.IdtoMember(msg);
                string packet = Packet.IdtoMember_ASK(true);
                server.SendData(client, packet);
            }
            catch (Exception)
            {                
                string packet = Packet.IdtoMember_ASK(false);
                server.SendData(client, packet);
            }            
        }
        public void OnSelectMember(Socket client, string msg)
        {
            try
            {
                Member mem = db.IdtoMember(msg);
                string packet = Packet.SelectMember_ASK(true,mem);
                server.SendData(client, packet);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.SelectMember_ASK(false);
                server.SendData(client, packet);
            }
        }

        public void OnDeleteMember(Socket client, string msg)
        {
            try
            {
                db.DeleteMember(msg);
                string packet = Packet.DeleteMember_ASK(true);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.DeleteMember_ASK(false);
                server.SendData(client, packet);
            }
        }

        //수정
        public void OnUpdateMember(Socket client, string msg)
        {
            try
            {
                string[] update = msg.Split('#'); 
                db.UpdateMember(update[0],update[1],int.Parse(update[2]));
                string packet = Packet.UpdateMember_ASK(true);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.UpdateMember_ASK(false);
                server.SendData(client, packet);
            }
        }

        public void OnLogout(Socket client, string msg)
        {
            try
            {
                 db.Logout(msg);
                string packet = Packet.Logout_ASK(true);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.Logout_ASK(false);
                server.SendData(client, packet);
            }
        }
        #endregion
    }
}
