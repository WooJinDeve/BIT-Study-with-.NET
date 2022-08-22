using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace _0412_실습
{
    internal class ShapeControl
    {
        private List<Shape> shapes = new List<Shape>();
        public Shape Curshape { get; set; }

        public ShapeControl()
        {
            this.Curshape = new Shape()
            {
                Type = ShapeType.RECT,
                PenSize = 1,
                BrushColor = Color.Red,
                ShapePoint = new Point(0, 0)
            };
        }

        public void LeftPrint(Graphics g)
        {
            Font font = new Font("Timesroman", 10);

            SolidBrush brush = new SolidBrush(Color.Black);

            string temp = string.Format("[도형타입] : {0}", Curshape.Type);
            g.DrawString(temp, font, brush, new RectangleF(10, 10, 200, 30));

            temp = string.Format("[펜두께] : {0}", Curshape.PenSize);
            g.DrawString(temp, font, brush, new RectangleF(10, 30, 200, 30));


            temp = string.Format("[색상] : {0}", Curshape.BrushColor);
            g.DrawString(temp, font, brush, new RectangleF(10, 50, 200, 30));

            temp = string.Format("[좌표] : {0}", Curshape.ShapePoint);
            g.DrawString(temp, font, brush, new RectangleF(10, 70, 200, 30));
        }

        public void SaveShape(Point pt)
        {
            Curshape.ShapePoint = pt;

            //임시 객체를 생성해야 무효화 과정에서 모든 도형이 출력됨..
            Shape shape = new Shape()
            {
                Type = Curshape.Type,
                PenSize = Curshape.PenSize,
                BrushColor = Curshape.BrushColor,
                ShapePoint = pt
            };

            shapes.Add(shape);
        }

        public void TitlePrint(Form1 form)
        {
            string str = string.Format("저장된 도형의 개수{0}개", shapes.Count);
            form.Text = str;
        }

        public void ShapePrintAll(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                Pen pen = new Pen(shape.BrushColor, shape.PenSize);
                Brush brush = new SolidBrush(shape.BrushColor);

                if (shape.Type == ShapeType.RECT)
                {
                    g.DrawRectangle(pen, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                    g.FillRectangle(brush, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                }
                else if (shape.Type == ShapeType.ELLIPSE)
                {
                    g.DrawEllipse(pen, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                    g.FillEllipse(brush, shape.ShapePoint.X, shape.ShapePoint.Y, 50, 50);
                }
                else if (shape.Type == ShapeType.RECT)
                {
                    Point[] points = { new Point(shape.ShapePoint.X-25, shape.ShapePoint.Y),
                        new Point(shape.ShapePoint.X+25, shape.ShapePoint.Y),
                        new Point(shape.ShapePoint.X, shape.ShapePoint.Y-43)
                    };
                    g.DrawPolygon(pen, points);
                    g.FillPolygon(brush, points);
                }
                pen.Dispose();
                brush.Dispose();
            }
        }

        public void DeleteShape()
        {
            try
            {
                shapes.RemoveAt(shapes.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("저장된 객체가 없습니다.");
            }
        }

        public void Save()
        {
            try
            {
                System.IO.Directory.Delete("save.txt");
                string msg = null;
                using (StreamWriter fp = new StreamWriter("save.txt", true, Encoding.Default))
                {
                    foreach (Shape sh in shapes)
                    {
                        string str = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}$", sh.Type, sh.PenSize, sh.BrushColor.R, sh.BrushColor.G, sh.BrushColor.B, sh.ShapePoint.X, sh.ShapePoint.Y);
                        msg += str;
                    }
                    fp.WriteLine(msg);
                    fp.Close();
                }
            }
            catch (Exception) { }
        }

        public void Load()
        {
            try
            {
                StreamReader fp = new StreamReader("save.txt");

                string[] fp1 = fp.ReadLine().Split('$');
                foreach (string sp in fp1)
                {
                    ShapeType shapeType = ShapeType.NON;
                    string[] fp2 = sp.Split('#');
                    if (fp2[0] == "RECT")
                        shapeType = ShapeType.RECT;
                    else if (fp2[0] == "ELLIPSE")
                        shapeType = ShapeType.RECT;
                    else if (fp2[0] == "TRIANGLE")
                        shapeType = ShapeType.RECT;
                    Shape shape = new Shape()
                    {
                        Type = shapeType,
                        PenSize = int.Parse(fp2[1]),
                        BrushColor = Color.FromArgb(int.Parse(fp2[2]), int.Parse(fp2[3]), int.Parse(fp2[4])),
                        ShapePoint = new Point(int.Parse(fp2[5]), int.Parse(fp2[6]))
                    };
                    shapes.Add(shape);
                }

            }
            catch (Exception) { }
        }
    }

}
