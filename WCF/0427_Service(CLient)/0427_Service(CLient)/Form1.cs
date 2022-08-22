using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0427_Service_CLient_.ServiceReference1;

namespace _0427_Service_CLient_
{
    public partial class Form1 : Form, ICalCallback
    {
        private CalClient cal = null;

        public Form1()
        {
            InitializeComponent();

            InstanceContext site = new InstanceContext(this);
            cal = new CalClient(site);
        }

        #region ICalCallback
        public IAsyncResult BeginResult(float result, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndResult(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void Result([MessageParameter(Name = "result")] float result1)
        {
            textBox3.Text = result1.ToString();
        }
        #endregion



        private void button1_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(textBox1.Text);
            int num2 = int.Parse(textBox2.Text);

            try
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "+": cal.Add(num1,num2); break;
                    case "-": cal.Sub(num1, num2); break;
                    case "*": cal.Mul(num1, num2); break;
                    case "/": cal.Div(num1, num2); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(textBox1.Text);
            int num2 = int.Parse(textBox2.Text);

            try
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "+": cal.BeginAdd(num1, num2, null, null); break;
                    case "-": cal.BeginSub(num1, num2, null, null); break;
                    case "*": cal.BeginMul(num1, num2, null, null); break;
                    case "/": cal.BeginDiv(num1, num2, Callback, null); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //비동기 통지
        private void Callback(IAsyncResult result)
        {
            //var asyncResult = (System.Runtime.Remoting.Messaging.AsyncResult)result;
            //var testDelegate = (Dele)asyncResult.AsyncDelegate;
            //testDelegate.EndInvoke(result);
        }

    }
}
