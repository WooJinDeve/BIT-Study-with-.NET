using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_Form1
{
    public partial class Paint_Form : Form
    {
        //좌표 저장 List
        private List<Point> points = new List<Point>();

        private Image image = null;

        public Paint_Form()
        {
            InitializeComponent();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //}

        private void Paint_Form_Paint(object sender, PaintEventArgs e)
        {
            //Graphics grfx = e.Graphics;
            //grfx.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            //--------------------------------
            //Graphics grfx = e.Graphics;
            //foreach (Point p in points)
            //{
            //    grfx.DrawRectangle(new Pen(Color.White), p.X, p.Y, 50, 50);
            //}
            //grfx.Dispose();

            //이미지 출력
            Graphics grfx = e.Graphics;
            if (image != null)
                grfx.DrawImage(image, 200, 100);
        }

        private void Paint_Form_MouseDown(object sender, MouseEventArgs e)
        {
            //using (Graphics g = this.CreateGraphics()) // GetDC(hwnd)
            //{ 
            //    g.DrawRectangle(new Pen(Color.Aqua), e.X, e.Y, 50, 50);
            //    g.Dispose();
            //}

            //좌표 저장 -> 무효화 (Paint그리기)
            points.Add(new Point(e.X, e.Y));
            this.Invalidate();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics; // = listbox1.CreateGraphics(); Dispose();
            Brush brush = Brushes.Black;
            switch (e.Index)
            {
                case 0: brush = Brushes.Red;    break;
                case 1: brush = Brushes.Blue;   break;
                case 2: brush = Brushes.Green;  break;
            }
            g.DrawString(listBox1.Items[e.Index].ToString(),
                          e.Font, brush, e.Bounds, StringFormat.GenericDefault);

            Console.WriteLine("{0} : DrawItem 이벤트 실행", e.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image imageFile = Image.FromFile("ocean.jpg");
            //grfx : 이미지에 출력할 수 있는 객체
            using (Graphics grfx = Graphics.FromImage(imageFile))
            {

                Font font = new Font("돋음", 20);
                Brush brush = Brushes.Pink;

                grfx.DrawString("이미지에 글자쓰기", font, brush, 10, 10);
                grfx.Dispose();
            }

            imageFile.Save("ocean.gif", ImageFormat.Gif);

            this.image = Image.FromFile("ocean.gif");
            this.Invalidate(this.ClientRectangle);
        }
    }
}
