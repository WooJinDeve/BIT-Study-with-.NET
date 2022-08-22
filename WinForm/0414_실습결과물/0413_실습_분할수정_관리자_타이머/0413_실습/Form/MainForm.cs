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
        private WbControl con = WbControl.Instance;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
             ListVIewPrintAll();
            timer1.Start();
        }

        private void ListVIewPrintAll()
        {
            listView1.Items.Clear();
            List<Member> members = con.GetMemberAllList();
            foreach (Member m in members)
            {
                string[] str = new string[] {
                    m.IsLogin.ToString(),m.Id, m.Pw,m.Name,m.Phone,m.DateTime.ToShortDateString() 
                };                
                ListViewItem item = new ListViewItem(str);
                listView1.Items.Add(item);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
            con.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ListVIewPrintAll();
        }
    }
}
