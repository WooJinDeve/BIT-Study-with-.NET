using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0412_Winform
{
    //디자이너가 자동으로 생성해 주는 영역
    internal partial class TestNewWinForm
    {
        //초기화 코드(속성초기화, 이벤트 처리)
        public void initData()
        {
            this.Text = "타이틀처리";
            this.SetBounds(10, 10, 500, 500);

            this.Load += Program_Load;
            this.FormClosed += Program_FormClosed;
            this.FormClosing += Program_FormClosing;
            this.MouseMove += Program_MouseMove;

            this.Show();
        }
    }
}
