using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_과제
{
    class Control
    {
        static string path = "Shapes.txt";
        public Shape Shape { get; set; }

        public Control()
        {
            Shape = new Shape(ShapeType.RECT, new Point(100, 100), 80, 80, Color.Black);
        }

        public void FileSave()
        {
            StreamWriter sw = File.CreateText(path);

            if (Shape != null)
            {
                sw.WriteLine(Shape.Type.ToString() + " "
                                 + Shape.Pos.X + " "
                                 + Shape.Pos.Y + " "
                                 + Shape.Width + " "
                                 + Shape.Height + " "
                                 + Shape.Color.R.ToString() + " "
                                 + Shape.Color.G.ToString() + " "
                                 + Shape.Color.B.ToString());
            }

            sw.Dispose();
        }

        public void FileLoad()
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string shapestr = sr.ReadLine();
                    string[] sp = shapestr.Split(' ');                 

                    Shape.Type = (ShapeType)Enum.Parse(typeof(ShapeType), sp[0]);
                    Shape.Pos = new Point(int.Parse(sp[1]), int.Parse(sp[2]));
                    Shape.Width = int.Parse(sp[3]);
                    Shape.Height = int.Parse(sp[4]);
                    Shape.Color = Color.FromArgb(int.Parse(sp[5]), int.Parse(sp[6]), int.Parse(sp[7]));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
