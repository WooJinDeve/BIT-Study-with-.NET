using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_Form1
{
    public partial class Color_Formcom : Form
    {
        public Color_Formcom()
        {
            InitializeComponent();


            //COMBOX초기화
            Array arr = System.Enum.GetValues(typeof(KnownColor));
            foreach(KnownColor c in arr)
            {
                comboBox1.Items.Add(c.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = comboBox1.Text;
            this.Text = comboBox1.SelectedItem.ToString();

            pictureBox1.BackColor = Color.FromName(this.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //pictureBox1.BackColor = Color.FromName(comboBox1.Text);

            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            colorDlg.ShowHelp = true;

            if (colorDlg.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDlg.Color;
        }
    }
}
