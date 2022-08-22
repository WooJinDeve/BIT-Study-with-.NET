using System;
using System.ServiceModel;
using System.Windows;
using _0427_1대1_Client_.ServiceReference1;

namespace _0427_1대1_Client_
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback
    {
        int idx;
        private IChat chat;


        public MainWindow()
        {
            InitializeComponent();
            InstanceContext site = new InstanceContext(this);
            chat = new ChatClient(site);

        }

        #region IChatCallback 인터페이스 함수 생성

        #region ? 
        public IAsyncResult BeginReceive(int idx, string message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserEnter(int idx, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceive(IAsyncResult result)
        {
            
        }

        public void EndUserEnter(IAsyncResult result)
        {
            
        }
        #endregion


        public void Receive(int idx, string message)
        {
            string msgtemp = string.Format("{0}", message);
            chatlist.Items.Add(msgtemp);
        }


        public void UserEnter(int idx)
        {
            string msgtemp = string.Format("{0}님이 로그인하셨습니다.", idx);
            chatlist.Items.Add(msgtemp);
        }
        #endregion

        #region Connect 
        private void Connect()
        {
            try
            {
                idx = int.Parse(seatbox.Text);

                //서버 접속
                if (chat.Join(idx))
                {
                    MessageBox.Show("접속 성공");
                }
                else
                    MessageBox.Show("접속 실패");



                btnJoin.Content = "로그아웃";
                string login = string.Format("{0}님이 로그인하셨습니다.", seatbox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("접속 오류 :{0}", ex.Message);
            }
        }

        private void DisConnect()
        {
            try
            {
                chat.Leave(idx);

                btnJoin.Content = "로그인";

                string logout = string.Format("{0}님이 로그아웃하셨습니다.", seatbox.Text);
                chatlist.Items.Add(logout);
            }
            catch (Exception ex)
            {
                MessageBox.Show("나가기 오류 :{0}", ex.Message);
            }
        }
        #endregion



        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnJoin.Content == "로그인")
                this.Connect();
            else 
                this.DisConnect();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (btnJoin.Content.Equals("로그아웃"))
            {
                string msg = msgbox.Text;

                string temp = string.Format("[{0}]", msg);

                chat.Say(idx, msg);

                msgbox.Clear();
            }
            else
                MessageBox.Show("로그인을 하시오");

        }
    }
}
