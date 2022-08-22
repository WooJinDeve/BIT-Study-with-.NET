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
            con.MainForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = MemberName + "님이 로그인하셨습니다.";
        }


        #region 검색
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

        private void OnApply(object sender, EventArgs eventArgs)
        {
            try
            {
                string id = selectForm.MemberName;
                con.SelectMember(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void SelectMember_Ack(Member member)
        {
            WBCrossThread.MainForm_SelectPrint(this, member);      
        }

        #endregion

        #region Delete
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string delid = textBox1.Text;
                con.DeleteMember(delid);        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Delete(bool b)
        {
            if(b)
                MessageBox.Show("회원 삭제 성공");
            else
                MessageBox.Show("회원 삭제 실패");
        }
        #endregion


        #region Update
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
        public void Update(bool b)
        {
            if (b)
                MessageBox.Show("회원 수정 성공");
            else
                MessageBox.Show("회원 수정 실패");
        }

        #endregion

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
