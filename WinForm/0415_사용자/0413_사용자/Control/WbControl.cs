using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_사용자
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

        private WBClient client = new WBClient();


        #region 네트웤 사용 필드
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 7000;

        #endregion

        LoginForm loginform = null;
        MainForm mainform = null;
        NewMemberForm newmemberform = null;


        #region Form Load 시 서버 연결
        public void Init()
        {
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return;
        }
        #endregion

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            string temp = string.Format("[{0}] : {1} ({2})", flag, msg, DateTime.Now.ToString());            
        }

        private void RecvMessage(string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "Login": OnLogin(sp[1]); break;
                case "NewMember": OnNewMember(sp[1]); break;
                case "IdtoMember": OnIdtoMember(sp[1]); break;
                case "SelectMember": OnSelectMember(sp[1]); break;
                case "DeleteMember": OnDeleteMember(sp[1]); break;
                case "UpdateMember": OnUpdateMember(sp[1]); break;
                case "Logout": OnLogout(sp[1]); break;
                //case "IdtoMember": 
            }
        }
        #endregion

        #region Form 초기화 코드
        public void LoginForm(LoginForm form)
        {
            loginform = form;
        }

        public void MainForm(MainForm form)
        {
            mainform = form;
        }
        public void NewMemberForm(NewMemberForm form)
        {
            newmemberform = form;
        }
        #endregion


        #region 로그인

        public void Login(string id, string pw)
        {
            string packet = Packet.Login(id, pw);
            client.SendData(packet);

        }

        public void OnLogin(string msg)
        {
            try
            {
                string[] str = msg.Split('#');
                if (bool.Parse(str[0]) == false)
                    throw new Exception("로그인 실패");

                loginform.Login(str[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 회원가입
        public void NewMember(string id, string pw, string name, string phone, int age)
        {
            string packet = Packet.NewMember(id, pw, name, phone, age);
            client.SendData(packet);
        }

        public void OnNewMember(string msg)
        {
            loginform.NewMemeber(bool.Parse(msg));
        }

        //아이디 중복체크
        public void IdtoMember(string id)
        {
            string packet = Packet.IdtoMember(id);
            client.SendData(packet);
        }

        public void OnIdtoMember(string msg)
        {
            newmemberform.OnIdtoMember(bool.Parse(msg));
        }
        #endregion

        #region 검색
        public void SelectMember(string id)
        {
            string packet = Packet.SelectMember(id);
            client.SendData(packet);
        }

        //검색
        public void OnSelectMember(string msg)
        {
            try
            {
                string[] sp1 = msg.Split('$');
                if (bool.Parse(sp1[0]) == false)
                    throw new Exception("해당 아이디는 존재하지 않습니다.");

                string[] sp2 = sp1[1].Split('#');
                mainform.SelectMember_Ack(new Member(bool.Parse(sp2[0]), sp2[1], sp2[2], sp2[3], sp2[4], int.Parse(sp2[5]), DateTime.Parse(sp2[6])));
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region 삭제
        public void DeleteMember(string delid)
        {
            string packet = Packet.DeleteMember(delid);
            client.SendData(packet);
        }

        public void OnDeleteMember(string msg)
        {
            mainform.Delete(bool.Parse(msg));
        }
        #endregion

        #region 수정
        public void UpdateMember(string id, string phone, int age)
        {
            string packet = Packet.UpdateMember(id,phone, age);
            client.SendData(packet);
        }
        public void OnUpdateMember(string msg)
        {
            mainform.Update(bool.Parse(msg));
               
        }
        #endregion

        #region 로그아웃
        public void Logout(string id)
        {
            string packet = Packet.Logout(id);
            client.SendData(packet);
        }


        public void OnLogout(string msg)
        {
            if (bool.Parse(msg) == false)
                MessageBox.Show("로그아웃 실패");
        }
        #endregion



  
    }
}
