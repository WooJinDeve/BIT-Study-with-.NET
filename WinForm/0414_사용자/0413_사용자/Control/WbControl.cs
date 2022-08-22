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

        public Member mem { get; set; }

        #region 네트웤 사용 필드
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 7000;

        #endregion


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

        LoginForm loginform = null;
        MainForm mainform = null;
        NewMemberForm newmemberform = null;
        SelectForm selectform = null;


        #region 패킷 생성
        public void Login(string id, string pw, LoginForm form)
        {
            string packet = Packet.Login(id, pw);
            client.SendData(packet);
            loginform = form;
        }

        public void NewMember(string id, string pw, string name, string phone, int age)
        {
            string packet = Packet.NewMember(id, pw, name, phone, age);
            client.SendData(packet);
        }

        public void IdtoMember(string id, NewMemberForm form)
        {
            string packet = Packet.IdtoMember(id);
            client.SendData(packet);
            newmemberform = form;
        }

        public void SelectMember(string id)
        {
            string packet = Packet.SelectMember(id);
            client.SendData(packet);
        }

        public void DeleteMember(string delid)
        {
            string packet = Packet.DeleteMember(delid);
            client.SendData(packet);
            //doc.DeleteMember(delid);
        }

        public void UpdateMember(string id, string phone, int age)
        {
            string packet = Packet.UpdateMember(id,phone, age);
            client.SendData(packet);
        }

        public void Logout(string id)
        {
            string packet = Packet.Logout(id);
            client.SendData(packet);
        }
        #endregion

        public void selectChange(string id)
        {
            try
            {
                Thread.Sleep(1000);
                mainform.textBox1.Text = mem.Id;
                mainform.textBox2.Text = mem.Pw;
                mainform.textBox3.Text = mem.Name;
                mainform.textBox4.Text = mem.Phone;
                mainform.textBox5.Text = mem.Age.ToString();
                mainform.textBox6.Text = mem.DateTime.ToShortDateString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        #region 기능
        //로그인
        public void OnLogin(string msg)
        {
            try
            {
                string[] str = msg.Split('#');
                if (bool.Parse(str[0]) == false)
                    throw new Exception("로그인 실패");

                //로그인 성공
               loginform.Hide();
                mainform = new MainForm();
                mainform.MemberName = str[1];
                mainform.MemberId = loginform.Id;
                mainform.ShowDialog();
                loginform.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                loginform.Id = loginform.Pw = "";
            }
        }

        //insert member
        public void OnNewMember(string msg)
        {
            if (bool.Parse(msg) == false)
                MessageBox.Show("회원가입 실패");
            MessageBox.Show("회원가입 성공");
        }


        //
        public void OnIdtoMember(string msg)
        {
            try
            {                       
                if (bool.Parse(msg) == true)
                {
                    MessageBox.Show("중복된 ID 입니다.");
                }
                else
                {
                    MessageBox.Show("사용할 수 있는 ID 입니다.");
                    newmemberform.isCheck = true;
                }
            }            
            catch (Exception)
            {
              
            }
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
                mem = new Member(sp2[0], sp2[1], sp2[2], sp2[3], int.Parse(sp2[4]));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //삭제
        public void OnDeleteMember(string msg)
        {
            if (bool.Parse(msg) == false)
                MessageBox.Show("회원 삭제 실패");
            MessageBox.Show("회원 삭제 성공");
        }

        //수정
        public void OnUpdateMember(string msg)
        {
            if (bool.Parse(msg) == false)
                MessageBox.Show("회원 수정 실패");
            MessageBox.Show("회원 수정 성공");
        }

        public void OnLogout(string msg)
        {
            if (bool.Parse(msg) == false)
                MessageBox.Show("로그아웃 실패");
        }
        #endregion
    }
}
