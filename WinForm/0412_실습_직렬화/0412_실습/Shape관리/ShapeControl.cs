using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_실습
{
    internal class ShapeControl
    {

        public List<Shape> shapes = new List<Shape>();
        public Shape Curshape { get; set; }

        public ShapeControl()
        {
            this.Curshape = new Shape()
            {
                Type = ShapeType.RECT,
                Pensize = 1,
                BrushColor = Color.Aqua,
                ShapePoint = new Point(0, 0)
            };
        }

        public void LeftPrint(Graphics g)
        {
            Font font = new Font("Timesroman", 10);
            SolidBrush brush = new SolidBrush(Color.Black);

            string temp = string.Format("[도형타입] {0}", Curshape.Type);
            g.DrawString(temp, font, brush, new RectangleF(10, 10, 200, 30));

            temp = string.Format("[펜두께] {0}", Curshape.Pensize);
            g.DrawString(temp, font, brush, new RectangleF(10, 30, 200, 30));

            temp = string.Format("[색상] {0}", Curshape.BrushColor);
            g.DrawString(temp, font, brush, new RectangleF(10, 50, 200, 30));

            temp = string.Format("[색상] {0}", Curshape.ShapePoint);
            g.DrawString(temp, font, brush, new RectangleF(10, 80, 200, 30));
        }

        public void SaveShape(Point pt)
        {
            Curshape.ShapePoint = pt;

            Shape CpyShape = new Shape()
            {
                Type = Curshape.Type,
                Pensize = Curshape.Pensize,
                BrushColor = Curshape.BrushColor,
                ShapePoint = pt
            };
            shapes.Add(CpyShape);
        }

        public void TitlePrint(Form1 form)
        {
            string str = string.Format("저장된 도형의 개수 ({0}개)", shapes.Count);
            form.Text = str;
        }

        public void ShapePrintAll(Graphics g)
        {
            foreach(Shape shape in shapes)
            {
                Pen pen = new Pen(Color.Black, shape.Pensize);
                Brush brush = new SolidBrush(shape.BrushColor);
                if(shape.Type == ShapeType.RECT)
                {
                    g.DrawRectangle(pen, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                    g.FillRectangle(brush,shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                }
                else if (shape.Type == ShapeType.ELLIPSE)
                {
                    g.DrawEllipse(pen, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                    g.FillEllipse(brush, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);

                }
                else if (shape.Type == ShapeType.TRIANGLE)
                {
                    Point[] Point = new Point[3];

                    Point[0] = new Point(shape.ShapePoint.X, shape.ShapePoint.Y +50);
                    Point[1] = new Point(shape.ShapePoint.X + 25, shape.ShapePoint.Y );
                    Point[2] = new Point(shape.ShapePoint.X + 50, shape.ShapePoint.Y + 50);

                    g.DrawPolygon(pen, Point);
                    g.FillPolygon(brush, Point);
                }
            }
        }

        public void DeleteShape()
        {
            try
            {
                shapes.RemoveAt(shapes.Count - 1);
            }
            catch(Exception)
            {
                MessageBox.Show("저장된 객체가 없습니다");
            }
        }
    }
}