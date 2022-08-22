using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _0428_REST_DB_Client_
{
    public partial class Form1 : Form
    {
        string url = "";
        string method;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = comboBox1.Text;
            method = textBox1.Text;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            if (comboBox1.SelectedIndex != 0)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textBox2.Text);

                request.Method = method;
                request.ContentType = "application/json; charset=UTF-8;";
                request.ContentLength = bytes.Length;
                
                Stream webStream = request.GetRequestStream();


                using (StreamWriter requestWriter = new StreamWriter(webStream))
                {                    
                    requestWriter.Write(textBox2.Text);
                        
                }
            }
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    if (comboBox1.SelectedIndex == 0)
                    {
                        ListViewPrint(response);
                        return;
                    }
                    textBox3.Text = response;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: textBox1.Text = "GET"; break;
                case 1: textBox1.Text = "POST"; break;
                case 2: textBox1.Text = "PUT"; break;
                case 3: textBox1.Text = "DELETE"; break;
            }
        }

        private void ListViewPrint(string msg)
        {
            listView1.Items.Clear();

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(msg);
            XmlNode xn = xd["ArrayOfMember"];
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(xn.ChildNodes[i]["Name"].InnerText);
                item.SubItems.Add(xn.ChildNodes[i]["Age"].InnerText);
                item.SubItems.Add(xn.ChildNodes[i]["Phone"].InnerText);
                listView1.Items.Add(item);
            }
        }
    }
}
