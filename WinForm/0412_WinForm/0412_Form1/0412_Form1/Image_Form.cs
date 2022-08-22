using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_Form1
{
    public partial class Image_Form : Form
    {
        private Bitmap      originalbmp = null;      // 원본 이미지(왼쪽패널)
        private Bitmap      smallbmp;                // 오른쪽에 출력될 이미지
        private Rectangle   region;                  // 마우스가 가리키는 영역(왼쪽패널)
        private bool        bmousedown = false;      // 마우스 버튼이 눌렸는지 유무(왼쪽패널)
        private int         ratio;                   // 이미지 확대/축소 비율

        public Image_Form()
        {
            InitializeComponent();

            this.smallbmp = new Bitmap(this.panel1.Width,this.panel1.Height);
            this.ratio = 0;
        }

        #region 메뉴 control
        private void 파일열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = @"C:\Users\user\Desktop\0412_Form1\0412_Form1\bin\Debug";
            fd.Filter = "Image Files | *.JPG;*.GIF;*.PNG;*.TIF;*.BMP;*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.originalbmp = new Bitmap(fd.FileName);
                region = new Rectangle(this.panel2.Width / 2 - 100, this.panel2.Height / 2 - 100, 100, 100);
                this.panel2.Invalidate();               
            }

        }

        private void 프로그램종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            this.Close();
        }
        #endregion

        #region 왼쪽 패널
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if(this.originalbmp != null)
{
                Graphics g = e.Graphics;
                g.FillRectangle(Brushes.White, this.panel2.ClientRectangle);
                Rectangle rect = new Rectangle(0, 0, this.panel2.Width, this.originalbmp.Height);

                g.DrawImage(this.originalbmp, rect);

                // 노란색 점선 그리기
                Pen pen = new Pen(Brushes.Gold, 3);
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, region);
                pen.Dispose();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            this.bmousedown = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.bmousedown = true;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(this.bmousedown)
{
                region.X = e.X;
                region.Y = e.Y;

                this.smallbmp.Dispose();

                // 오른쪽 패널의 새로운 비트맵 생성 및 그리기
                this.smallbmp = new Bitmap(this.panel1.Width, this.panel1.Height);
                Graphics gi = Graphics.FromImage(this.smallbmp);
                gi.DrawImage(this.originalbmp, this.panel1.ClientRectangle, this.region, GraphicsUnit.Pixel);
                gi.Dispose();

                // 왼쪽 , 오른쪽 패널 갱신
                this.panel2.Invalidate();
                this.panel1.Invalidate();
            }
        }

        #endregion

        #region 오른쪽 패널
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // 읽어 들인 이미지 확인
            if (this.originalbmp != null)
            {
                Graphics g = e.Graphics;
                // 패널을 힌색으로 칠함
                g.FillRectangle(Brushes.White, this.panel1.ClientRectangle);
                // 왼쪽 패널에서 설정한 이미지 출력
                g.DrawImage(this.smallbmp, 0, 0);
            }
        }
        #endregion

        #region 오른쪽 패널 비율 조절
        private void ResizeSmallImage()
        {
            // 이미지 배율
            float ratio = (float)(1.0 + this.ratio * .25);
            int w = (int)(this.panel1.Width * ratio);
            int h = (int)(this.panel1.Height * ratio);
            Rectangle rect = new Rectangle(0, 0, w, h);
            Bitmap tempBmp = new Bitmap(w, h);
            Graphics gi = Graphics.FromImage(tempBmp);
            gi.DrawImage(this.smallbmp, rect);
            this.smallbmp.Dispose();
            this.smallbmp = new Bitmap(w, h);
            this.smallbmp = tempBmp.Clone(rect, tempBmp.PixelFormat);
            gi.Dispose();
            tempBmp.Dispose();
            this.panel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ratio < 3)
            {
                this.ratio++;
                this.ResizeSmallImage();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ratio > -3)
            {
                this.ratio--;
                this.ResizeSmallImage();
            }

        }
        #endregion
    }
}
