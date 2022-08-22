using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace _0428_REST_TEST_client_
{
    public partial class Form1 : Form
    {
        string url = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = textBox1.Text;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = textBox2.Text.Length;

            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(textBox2.Text);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    textBox3.Text = response;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "http://localhost:50226/Service1.svc/postdata";
        }
    }
}
