using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0418_과제
{
   
    public partial class MainWindow : Window
    {
        static string path = @"Shapes.txt";
        private Shape shapes = new Shape();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrintData(Shape sh)
        {
            ComboBox.Text = sh.Type == ShapeType.RECT ? "사각형" : "타원";
            RedTextBox.Text = sh.Color.R.ToString();
            GreenTextBox.Text = sh.Color.G.ToString();
            BlueTextBox.Text = sh.Color.B.ToString();

            XTextBox.Text = sh.Pos.X.ToString();
            YTextBox.Text = sh.Pos.Y.ToString();
            WidthTextBox.Text = sh.Width.ToString();
            HeightTextBox.Text = sh.Height.ToString();


            canvas.Children.Clear();
            if (ComboBox.Text == "사각형")
                Rectangle(shapes);

            else if (ComboBox.Text == "타원")
                Ellipse(shapes);
        }

        #region 도형 정보
        private void Rectangle(Shape sh)
        {
            Rectangle rt = new Rectangle();
            rt.Width = sh.Width;
            rt.Height = sh.Height;
            rt.Fill = new SolidColorBrush(sh.Color);

            Canvas.SetLeft(rt, sh.Pos.X);
            Canvas.SetTop(rt, sh.Pos.Y);

            canvas.Children.Add(rt);
        }

        private void Ellipse(Shape sh)
        {
            Ellipse rt = new Ellipse();
            rt.Width = sh.Width;
            rt.Height = sh.Height;
            rt.Fill = new SolidColorBrush(sh.Color);

            Canvas.SetLeft(rt, sh.Pos.X);
            Canvas.SetTop(rt, sh.Pos.Y);

            canvas.Children.Add(rt);
        }
        #endregion

        #region 구현2 - 현재 상태
        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point pos = e.GetPosition(canvas);

            string msg = "";
            msg += string.Format("1) 현재 날짜 : {0} ", DateTime.Now.ToShortDateString());
            msg += string.Format("2) 현재 시간 : {0}:{1}:{2} ", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            msg += string.Format("3) 현재 마우스 좌표 : {0}:{1}", pos.X, pos.Y);

            StatusBar1.Content = msg;
        }
        #endregion

        #region 구현3 - 색상 메뉴
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            RedTextBox.Text = "255";
            GreenTextBox.Text = "0";
            BlueTextBox.Text = "0";

            shapes.Color = Color.FromRgb(255, 0, 0);
            PrintData(shapes);
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            RedTextBox.Text = "0";
            GreenTextBox.Text = "255";
            BlueTextBox.Text = "0";

            shapes.Color = Color.FromRgb(0, 255, 0);
            PrintData(shapes);
        }

        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            RedTextBox.Text = "0";
            GreenTextBox.Text = "0";
            BlueTextBox.Text = "255";

            shapes.Color = Color.FromRgb(0, 0, 255);
            PrintData(shapes);
        }

        private void MenuItem4_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RedTextBox.Text = colorDialog.Color.R.ToString();
                GreenTextBox.Text = colorDialog.Color.G.ToString();
                BlueTextBox.Text = colorDialog.Color.B.ToString();


                shapes.Color = Color.FromRgb(byte.Parse(colorDialog.Color.R.ToString()), byte.Parse(colorDialog.Color.G.ToString()), byte.Parse(colorDialog.Color.B.ToString())); 
                PrintData(shapes);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            shapes.Color = Color.FromRgb(byte.Parse(RedTextBox.Text), byte.Parse(GreenTextBox.Text), byte.Parse(BlueTextBox.Text));

            PrintData(shapes);
        }
        #endregion

        #region 구현3,4 - 위치 정보 변경 / 도형(width,heigth) 정보 변경 / 속성정보 변경
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(canvas);

            if (ComboBox.Text == "사각형")
            {
                shapes = new Shape()
                {
                    Type = ShapeType.RECT,
                    Pos = pos,
                    Width = int.Parse(WidthTextBox.Text),
                    Height = int.Parse(HeightTextBox.Text),
                    Color = Color.FromRgb(byte.Parse(RedTextBox.Text), byte.Parse(GreenTextBox.Text), byte.Parse(BlueTextBox.Text))
                };
            }
            else if (ComboBox.Text == "타원")
            {
                shapes = new Shape()
                {
                    Type = ShapeType.ELLIPSE,
                    Pos = pos,
                    Width = int.Parse(WidthTextBox.Text),
                    Height = int.Parse(HeightTextBox.Text),
                    Color = Color.FromRgb(byte.Parse(RedTextBox.Text), byte.Parse(GreenTextBox.Text), byte.Parse(BlueTextBox.Text))
                };
            }

            PrintData(shapes);
        }

        private void ComboBoxItem1_Selected(object sender, RoutedEventArgs e)
        {
            // == Rectangle(shapes);
            shapes.Type = ShapeType.RECT;

            PrintData(shapes);
        }

        private void ComboBoxItem2_Selected(object sender, RoutedEventArgs e)
        {
            // == Ellipse(shapes);
            shapes.Type = ShapeType.ELLIPSE;

            PrintData(shapes);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int width = int.Parse(HeightTextBox.Text);

            if (Keyboard.IsKeyDown(Key.PageUp) == true)
            {
                if (width == 150)
                {
                    shapes.Width -= 10;
                    shapes.Height -= 10;
                }
                shapes.Width += 10;
                shapes.Height += 10;
            }

            if (Keyboard.IsKeyDown(Key.PageDown) == true)
            {
                if (width == 0)
                {
                    shapes.Width += 10;
                    shapes.Height += 10;
                }
                shapes.Width -= 10;
                shapes.Height -= 10;
            }
            PrintData(shapes);
        }
        #endregion

        #region 구현5 - Close, Save, Load
        private void ClostItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = File.CreateText(path);

            if (shapes != null)
            {
                Shape boo = shapes;
                sw.WriteLine(boo.Type.ToString() + " "
                                 + boo.Pos.X + " "
                                 + boo.Pos.Y + " "
                                 + boo.Width + " "
                                 + boo.Height + " "
                                 + boo.Color.R.ToString() + " "
                                 + boo.Color.G.ToString() + " "
                                 + boo.Color.B.ToString());
            }

            sw.Dispose();
        }

        private void LoadItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string shapestr = sr.ReadLine();
                    string[] sp = shapestr.Split(' ');

                    ShapeType type = (ShapeType)Enum.Parse(typeof(ShapeType), sp[0]);
                    Point pensize = new Point(double.Parse(sp[1]), double.Parse(sp[2]));
                    int width = int.Parse(sp[3]);
                    int height = int.Parse(sp[4]);
                    Color color = Color.FromRgb(byte.Parse(sp[5]), byte.Parse(sp[6]), byte.Parse(sp[7]));

                    Shape temp = new Shape(type, pensize, width, height, color);
                    shapes = temp;

                    PrintData(shapes);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

      
    }
}
