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

namespace _0413_관리자
{
    public partial class MainForm : Form
    {
        private WbControl con = WbControl.Instance;

        //쓰레드 
        Thread myThread = null;

        public MainForm()
        {
            InitializeComponent();
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                myThread = new Thread(Func);
                myThread.Start();
            }
            catch (Exception)
            { }

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                // 쓰레드 시작
                //CheckForIllegalCrossThreadCalls = false;
                //myThread = new Thread(Func);
                //myThread.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Func()
        {
            while (this.IsDisposed == false)
            {
                ListViewPrintAll(listView1);
                Thread.Sleep(1000);
            }
        }

        public void ListViewPrintAll(ListView listview)
        {
            if (listview.InvokeRequired)
            {
                listview.Invoke(new MethodInvoker(ListViewPrintAll));
            }
            else
            {
                // tb.Text = contents;
            }
        }


        private void ListViewPrintAll()
        {
            listView1.Items.Clear();
            string[] strarr = con.GetMemberListAll();
            foreach (string str in strarr)
            {
                string[] data = str.Split('#');            
                ListViewItem item = new ListViewItem(data);
                listView1.Items.Add(item);
            }
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            con.Close();
        }
    }
}
