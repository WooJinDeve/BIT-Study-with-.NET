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
            ListViewPrintAll();
        }

        private void ListViewPrintAll()
        {
            listView1.Items.Clear();
            List<Member> members = con.GetMemberListAll();
            foreach(Member m in members)
            {
                string[] str = new string[] { m.IsLogin.ToString(), m.Id, m.Pw, m.Name, m.Phone, m.Age.ToString() ,m.DateTime.ToShortDateString() };
                ListViewItem item = new ListViewItem(str);
                listView1.Items.Add(item);
            }
        }

        //리스트뷰 아이템 변경
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                //Text = "선택된 항목이 없습니다.";
            }
            else
            {
                ListViewItem item = listView1.SelectedItems[0];
                textBox1.Text = item.SubItems[1].Text;
                textBox2.Text = item.SubItems[2].Text;
                textBox3.Text = item.SubItems[3].Text;
                textBox4.Text = item.SubItems[4].Text;
                textBox5.Text = item.SubItems[5].Text;
                textBox6.Text = item.SubItems[6].Text;
            }

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
                ListViewPrintAll();
            }
            catch(Exception ex)
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
                ListViewPrintAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //로그아웃
        private void button4_Click(object sender, EventArgs e)
        {
            
             this.Close();          
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
