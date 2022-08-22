using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_Winform
{
    //개발자가 코딩하는 영역
    internal partial class TestNewWinForm : Form
    {
        public TestNewWinForm()
        {
            initData();
        }

        private void Program_MouseMove(object sender, MouseEventArgs e)
        {
            string str = string.Format("{0}, {1}", e.X, e.Y);
            this.Text = str;
        }

        #region 폼 핸들러
        private void Program_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("종료하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Program_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("윈도우가 Closed 됩니다.");
        }

        private void Program_Load(object sender, EventArgs e)
        {
            Console.WriteLine("윈도우가 Load 됩니다.");
        }

        #endregion

    }
}
