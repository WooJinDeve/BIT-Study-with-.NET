using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0425_papago_client_.localhost;

namespace _0425_papago_client_
{
    public partial class Form1 : Form
    {
        public string InputLanguage { get;set;}
        public string OutPutLanguage { get; set; }

        private WbPapago papago = new WbPapago();


        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputLanguage = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutPutLanguage = comboBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" && comboBox2.Text == "")
                return;
            textBox2.Text = papago.OutputText(InputLanguage, OutPutLanguage, textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string temp = comboBox1.Text;
            comboBox1.Text = comboBox2.Text;
            comboBox2.Text = temp;

            temp = textBox1.Text;
            textBox1.Text = textBox2.Text;
            textBox2.Text = temp;
        }
    }
}
