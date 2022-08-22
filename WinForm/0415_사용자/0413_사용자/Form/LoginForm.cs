using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_사용자
{
    public partial class LoginForm : Form
    {
        #region 프로퍼티
        public string Id
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string Pw
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        #endregion


        private WbControl con = WbControl.Instance;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                con.Init();
                con.LoginForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region Login
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id == string.Empty || Pw == String.Empty)
                {
                    Id = Pw = "";
                    throw new Exception("ID나 PW를 입력하세요");
                }

                con.Login(Id, Pw);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
       
        }

        public void Login(string name)
        {

                Action login = () =>
                {
                    WBCrossThread.LogIn_FormShow(this, false);
                    MainForm mainform = new MainForm();
                    mainform.MemberName = name;
                   
                    mainform.MemberId = Id;
                
                    mainform.ShowDialog();
                    WBCrossThread.LogIn_FormShow(this, true);
                };
                Task tasklogin = new Task(login);
                tasklogin.Start();
        
        }
        #endregion

        #region NewMember
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                NewMemberForm form = new NewMemberForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string id, pw, name, phone;
                    int age;
                    form.GetMemberData(out id, out pw, out name, out phone, out age);
                    con.NewMember(id, pw, name, phone, age);
                }
                this.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("회원가입 실패");
            }
        }

        public void NewMemeber(bool b)
        {
            if (b)
                MessageBox.Show("회원가입 성공");
            else
                MessageBox.Show("회원가입 성공");
        }
        #endregion
    }
}
