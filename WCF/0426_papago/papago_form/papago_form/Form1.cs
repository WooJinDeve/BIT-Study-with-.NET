
using papago_form.ServiceReference1;
using System;
using System.Windows.Forms;

namespace papago_form
{
    public partial class Form1 : Form
    {
        private PapagoClient ppg = new PapagoClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string source = comboBox1.SelectedIndex.ToString();
            string target = comboBox2.SelectedIndex.ToString();

            string source1 = Len(source);
            string target1 = Len(target);

            if(source1==target1)
            {
                MessageBox.Show("번역할언어가 동일합니다. ");
                return;

            }

            textBox2.Text = ppg.papago(source1, target1, text);
        }


           string Len(string msg)

            {
                string leng = string.Empty;
            switch (msg)
            {
                case "0": leng = "ko"; break;
                case "1": leng = "en"; break;
                case "2": leng = "ja"; break;
                case "3": leng = "zh-CN"; break;
                case "4": leng = "es"; break;
            }
            return leng;





                
                   
            }

        
    }
}
