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

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                myThread = new Thread(Func);
                myThread.Start();
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
                ListViewPrintAll(dataGridView1);
                Thread.Sleep(1000);
            }
        }

        public void ListViewPrintAll(DataGridView dataGridView)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new MethodInvoker(ListViewPrintAll));
            }
            else
            {
                // tb.Text = contents;
            }
        }

       
        private void ListViewPrintAll()
        {
            DataTable table = con.GetMemberListAll();
            dataGridView1.DataSource = table;
        }
    }
}
