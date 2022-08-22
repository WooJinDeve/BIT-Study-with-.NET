using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_관리자
{
    public partial class MainForm : Form
    {
        private WbControl con = WbControl.Instance;

        public MainForm()
        {
            InitializeComponent();
        }

        static Timer myTimer = new Timer();

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                myTimer.Tick += new EventHandler(TimerEventProcessor);
                myTimer.Interval = 5000;
                myTimer.Start();
                ListViewPrintAll();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            ListViewPrintAll();
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
