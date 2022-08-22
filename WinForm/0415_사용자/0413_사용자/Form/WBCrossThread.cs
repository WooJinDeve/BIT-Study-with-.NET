using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_사용자
{
    static class WBCrossThread
    {  

        public static void LogIn_FormShow(LoginForm form, bool b)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    if (b)
                        form.Show();
                    else
                        form.Hide();
                }));
            }
            else
            {
                if (b)
                    form.Show();
                else
                    form.Hide();
            }
        }

        public static void MainForm_SelectPrint(MainForm form, Member member)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    form.textBox1.Text = member.Id;
                    form.textBox2.Text = member.Pw;
                    form.textBox3.Text = member.Name;
                    form.textBox4.Text = member.Phone;
                    form.textBox5.Text = member.Age.ToString();
                    form.textBox6.Text = member.DateTime.ToShortDateString();
                }));
            }
            else
            {
            }
        }
    }
}
