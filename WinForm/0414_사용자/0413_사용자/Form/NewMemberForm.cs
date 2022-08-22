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
    public partial class NewMemberForm : Form
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

        public string Phone
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }

        public string MemberName
        {
            get { return textBox3.Text; }
            set { textBox4.Text = value; }
        }

        public int Age
        {
            get { return int.Parse(comboBox1.Text); } // -> 콤보박스에 숫자로 입력시 해당 값 오류  : comboBox1.SeletedItem.toString();
            set { comboBox1.SelectedIndex = value; }
        }
        #endregion

        public bool isCheck = false;
        private bool isPwCheck = false;

        public NewMemberForm()
        {
            InitializeComponent();
        }

        public void GetMemberData(out string id, out string pw, out string name, out string phone, out int age)
        {
            id = Id;
            pw = Pw;
            name = MemberName;
            phone = Phone;
            age = Age;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isCheck == false)
            {
                MessageBox.Show("ID중복 여부를 확인하세요.");
                return;
            }
            if (isPwCheck == false)
            {
                MessageBox.Show("패스워드가 일치하지 않습니다.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
                WbControl.Instance.IdtoMember(Id, this);

                //    if (mem.Id == Id)
                //    {
                //        MessageBox.Show("중복된 ID 입니다.");
                //        textBox1.Focus();
                //    }             
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("사용할 수 있는 ID 입니다.");
                //    isCheck = true;
                //}
            //}
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("패스워드가 일치하지 않습니다.");
                return;
            }
            isPwCheck = true;
        }
    }
}
