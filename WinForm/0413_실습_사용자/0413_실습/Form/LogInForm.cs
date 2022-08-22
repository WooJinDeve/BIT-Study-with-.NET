using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_실습
{
    public partial class LogInForm : Form
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

        public LogInForm()
        {
            InitializeComponent();
        }

        //로그인
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id == String.Empty || Pw == String.Empty)
                    throw new Exception("ID나 PW를 입력하세요");

                string name = con.LogIn(Id, Pw);

                //로그인 성공!
                this.Hide();
                MainForm form = new MainForm();
                form.MemberName = name;
                form.MemberID = Id; 
                form.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            finally
            {
                Id = Pw = "";
            }
        }

        //회원가입
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
                    MessageBox.Show("회원가입 성공");
                }
                this.Show();
            }
            catch(Exception)
            {
                MessageBox.Show("회원가입실패");
            }
        }
    }
}
