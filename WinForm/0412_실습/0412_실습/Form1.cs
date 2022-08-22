using System;
using System.Drawing;
using System.Windows.Forms;

namespace _0412_실습
{
    public partial class Form1 : Form
    {
        ShapeControl con = new ShapeControl();

        #region 깜박임 방지
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region [기능 1] 현재 정보
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            con.LeftPrint(e.Graphics);
        }

        // 메뉴 핸들러(도형선택)
        private void ShapeMenuItem_Click(object sender, EventArgs e)
        {
            if ((ToolStripMenuItem)sender == 사각형RToolStripMenuItem)
                con.Curshape.Type = ShapeType.RECT;
            else if ((ToolStripMenuItem)sender == 타원EToolStripMenuItem)
                con.Curshape.Type = ShapeType.ELLIPSE;
            else if ((ToolStripMenuItem)sender == 삼각형TToolStripMenuItem)
                con.Curshape.Type = ShapeType.TRIANGLE;
            panel1.Invalidate();
        }

        // 키보드 (픽셀 / RGB)
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //con.Curshape.PenSize = (con.Curshape.PenSize + 1) % 5;
            if (e.KeyCode == Keys.Up)
            {
                if (con.Curshape.PenSize == 5)
                    con.Curshape.PenSize--;
                con.Curshape.PenSize++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (con.Curshape.PenSize == 0)
                    con.Curshape.PenSize++;
                //con.Curshape.Pensize = 6;
                con.Curshape.PenSize--;

            }
            else if (e.KeyCode == Keys.R)
            {
                con.Curshape.BrushColor = Color.Red;
            }
            else if (e.KeyCode == Keys.G)
            {
                con.Curshape.BrushColor = Color.Green;
            }
            else if (e.KeyCode == Keys.B)
            {
                con.Curshape.BrushColor = Color.Blue;
            }
            panel1.Invalidate();
        }

        // 마우스 ( 좌표 )
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            con.Curshape.ShapePoint = e.Location;
            panel1.Invalidate();
        }
        #endregion

        #region [기능 2] 저장 및 도형 출력
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                con.DeleteShape();
            else
                con.SaveShape(e.Location);

            con.TitlePrint(this);
            panel2.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            con.ShapePrintAll(e.Graphics);
        }

        #endregion

        #region 저장 + 불러오기 + 종료
        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Save();
        }
        private void 불러오기LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Load();
            panel1.Invalidate();
            panel2.Invalidate();
        }

        private void 프로그램종료XToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
