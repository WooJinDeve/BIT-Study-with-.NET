using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_사용자
{
    public partial class SelectForm : Form
    {
        public string MemberName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public SelectForm()
        {
            InitializeComponent();
        }

        public event EventHandler Apply = null;

        private void button1_Click(object sender, EventArgs e)
        {
            WbControl.Instance.selectChange(MemberName);
        }
    }
}
