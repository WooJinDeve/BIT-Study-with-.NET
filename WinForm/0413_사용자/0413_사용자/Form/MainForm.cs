using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_사용자
{
    public partial class MainForm : Form
    {
        public string MemberName { get; set; }
        public string MemberId { get; set; }

        private WbControl con = WbControl.Instance;

        //검색 모달리스 객체 생성
        private SelectForm selectForm = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = MemberName + "님이 로그인하셨습니다.";
        }

        //검색 

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectForm == null || selectForm.IsDisposed)
            {
                selectForm = new SelectForm();
                selectForm.Owner = this;
                selectForm.Apply += new EventHandler(OnApply);
                selectForm.Show();
            }
        }
        private void OnApply(object sender, EventArgs e)
        {
            try
            {
                string Id = selectForm.MemberName;

                Member mem = con.SelectMember(Id);

                textBox1.Text = mem.Id;
                textBox2.Text = mem.Pw;
                textBox3.Text = mem.Name;
                textBox4.Text = mem.Phone;
                textBox5.Text = mem.Age.ToString();
                textBox6.Text = mem.DateTime.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string delid = textBox1.Text;
                con.DeleteMember(delid);

                string msg = string.Format("[ID : {0}] 회원 정보 삭제", delid);
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string id = textBox1.Text;
                string phone = textBox4.Text;
                int age = int.Parse(textBox5.Text);
                con.UpdateMember(id, phone, age);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                con.Logout(MemberId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
