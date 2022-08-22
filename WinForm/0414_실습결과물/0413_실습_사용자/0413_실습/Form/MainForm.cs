using System;
using System.Windows.Forms;

namespace _0413_실습
{
    public partial class MainForm : Form
    {
        public string MemberName  { get; set; }
        public string MemberID      { get;set; }

        private WbControl con = WbControl.Instance;

        //검색 모달리스 
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
                string id = selectForm.MemberName;

                Member mem = con.SelectMember(id);

                textBox1.Text = mem.Id;
                textBox2.Text = mem.Pw;
                textBox3.Text = mem.Name;
                textBox4.Text = mem.Phone;
                textBox5.Text = mem.Age.ToString();
                textBox6.Text = mem.DateTime.ToShortDateString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //삭제
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string delid = textBox1.Text;
                con.DeleteMember(delid);
                MessageBox.Show("삭제되었습니다.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //수정
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string id       = textBox1.Text;
                string phone    = textBox4.Text;
                int age         = int.Parse(textBox5.Text);

                con.UpdateMember(id, phone, age);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //로그아웃
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.LogOut(MemberID);

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }
    }
}
