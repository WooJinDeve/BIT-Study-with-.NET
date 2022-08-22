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


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                con.Init(this);
                con.GetMemberListAll();
                timer1.Start();
      
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     


        public void ListViewPrintAll(string[] strarr)
        {
            listView1.Items.Clear();
            foreach (string str in strarr)
            {
                string[] data = str.Split('#');            
                ListViewItem item = new ListViewItem(data);
                listView1.Items.Add(item);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            con.GetMemberListAll();
        }
    }
}
