using System;
using System.ServiceModel;
using System.Windows;
using _0427_ChatClient.ServiceReference1;

namespace _0427_ChatClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback
    {
        public string NickName
        {
            get { return nickname.Text; }
            set { nickname.Text = value; }
        }

        public string Message
        {
            get { return msgbox.Text; }
            set { msgbox.Text = value; }
        }

        private ChatClient chat = null;

        public MainWindow()
        {
            InitializeComponent();

            InstanceContext site = new InstanceContext(this);
            chat = new ChatClient(site);
        }

        #region IChatCallback
        public void Receive(string nickname, string message)
        {
            string msgtemp = string.Format("[{0}] {1}", nickname, message);
            chatlist.Items.Add(msgtemp);
        }

        public void UserEnter(string nickname)
        {
            string msgtemp = string.Format("{0}님이 로그인하셨습니다.", nickname);
            chatlist.Items.Add(msgtemp);
        }


        #endregion

        public void UserLeave(string nickname)
        {
            string msgtemp = string.Format("{0}님이 로그아웃하셨습니다.", nickname);
            chatlist.Items.Add(msgtemp);
        }

        #region 버튼 핸들러
        private void Join_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //서버 접속
                if (chat.Join(NickName) == false)
                    UserEnter("로그인 실패");                
            }
            catch (Exception ex)
            {
                UserEnter(string.Format("접속 오류 :{0}", ex.Message));
            }
        }

        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chat.Leave(NickName);
                UserLeave(NickName);
            }
            catch (Exception ex)
            {
                UserEnter(string.Format("나가기 오류 :{0}", ex.Message));
            }
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chat.Say(NickName, Message);
                msgbox.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("로그인을 먼저하세요");
            }

        }
    }
    #endregion
}
