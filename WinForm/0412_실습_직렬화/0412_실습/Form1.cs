using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _0412_실습
{
    public partial class Form1 : Form
    {
        static string path = @"E:\비트고급\winform\0412_실습\0412_실습\Shapes.txt";
        private ShapeControl con = new ShapeControl();
        public Form1()
        {
            InitializeComponent();
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            con.LeftPrint(e.Graphics);
            this.Invalidate();

        }



        #region 메뉴핸들러(도형선택)
    

        private void ShapeMenuItem_Click(object sender, EventArgs e)
        {
            if((ToolStripMenuItem)sender== 사각형RToolStripMenuItem)
            {
                con.Curshape.Type = ShapeType.RECT;
              
            }
            else if ((ToolStripMenuItem)sender == 타원EToolStripMenuItem)
            {
                con.Curshape.Type = ShapeType.ELLIPSE;
               

            }
            else if ((ToolStripMenuItem)sender == 삼각형TToolStripMenuItem)
            {
                con.Curshape.Type = ShapeType.TRIANGLE;
               
            }
            panel1.Invalidate();
        }

        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        
            if (e.KeyCode == Keys.Up)
            {
                if (con.Curshape.Pensize == 5)
                    con.Curshape.Pensize = 0;
                con.Curshape.Pensize++;

 
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (con.Curshape.Pensize == 1)
                    con.Curshape.Pensize = 6;
                con.Curshape.Pensize--;
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

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            con.Curshape.ShapePoint = e.Location;
            panel1.Invalidate();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                con.DeleteShape();         
            else
                con.SaveShape(e.Location);
          
            con.TitlePrint(this);
            panel2.Invalidate(false);
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            con.ShapePrintAll(e.Graphics);
        }

        private void 프로그램종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream ws = new FileStream("shapes.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            //개수 
            int count = con.shapes.Count;
            serializer.Serialize(ws, count);
            foreach(Shape shape in con.shapes)
            {
                serializer.Serialize(ws, shape);    
            }
            ws.Close();

            //StreamWriter sw = File.CreateText(path);

            //sw.WriteLine(con.shapes.Count);

            //for (int i = 0; i < con.shapes.Count; i++)
            //{
            //    Shape boo = con.shapes[i];
            //    sw.WriteLine(boo.Type.ToString() + " "
            //                     + boo.Pensize + " "
            //                     + boo.BrushColor.ToString() + " "
            //                     + boo.ShapePoint.X.ToString() + " "
            //                     + boo.ShapePoint.Y.ToString());
            //}
            //sw.Dispose();
        }

        private void 불러오기LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Stream rs = new FileStream("shapes.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                int size = (int)formatter.Deserialize(rs);
                for(int i=0; i<size; i++)
                {
                    Shape shape = (Shape)formatter.Deserialize(rs);
                    con.shapes.Add(shape);
                }
                rs.Close();
                panel2.Invalidate();
                //using (StreamReader sr = File.OpenText(path))
                //{
                //    int size = int.Parse(sr.ReadLine());
                //    for (int i = 0; i < size; i++)
                //    {
                //        string shapestr = sr.ReadLine();
                //        string[] sp = shapestr.Split(' ');

                //        string sp1 = sp[3].Remove(0, 1);
                //        string sp2 = sp1.Remove(sp1.Length - 1);

                //        ShapeType type = (ShapeType)Enum.Parse(typeof(ShapeType), sp[0]);
                //        int pensize = int.Parse(sp[1]);
                //        Color brushcolor = ColorTranslator.FromHtml(sp2);
                //        Point shapepoint = new Point(int.Parse(sp[4]), int.Parse(sp[5]));

                //        Shape temp = new Shape(type, pensize, brushcolor, shapepoint);
                //        con.shapes.Add(temp);
                //    }
                //}
                //MessageBox.Show("읽기 끝");
                //panel2.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }
    }
}
