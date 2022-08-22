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
            Thread thread = new Thread(ThreadFun);
            thread.IsBackground = true;
            thread.Start();
        }

        public void ThreadFun(object obj)
        {
            while(true)
            {
                ListVIewPrintAll(listView1, true, "필요한 정보");
                Thread.Sleep(2000);
            }
        }

        public void ListVIewPrintAll(ListView listview, bool g, string str)
        {
            if (listview.InvokeRequired)
            {
                listview.Invoke(new MethodInvoker(delegate ()
                {
                    ListVIewPrintAll(listview, str);
                }));
            }
            else
            {
               // tb.Text = contents;
            }
        }

        private void ListVIewPrintAll(ListView listview, string str1)
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
            con.Dispose();
        }

    }
}
