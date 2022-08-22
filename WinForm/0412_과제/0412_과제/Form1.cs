using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_과제
{
    public partial class Form1 : Form
    {
        FileControl con = new FileControl();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.LoadFileInfo();
            con.ListBoxPrint(listBox1);           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.TextBoxPrint(textBox1, listBox1.SelectedItem.ToString());
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            con.ImagePrint(e.Graphics, listBox1.Text);
        }
    }
}
