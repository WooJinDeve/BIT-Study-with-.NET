using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

/*
 * [UI]
 * 설정 값 변경(사용자 입력을 통한)
 * 1) 현재 설정 값 변경
 * 2) 화면 갱신[좌측 / 우측]  PrintData()
 * 
 * 출력
 * PrintData() 화면 갱신[좌측 / 우측]
 */

namespace _0418_과제
{
   
    public partial class MainWindow : Window
    {
        private Control con = new Control();

        public MainWindow()
        {
            InitializeComponent();

            canvas.Focusable = true;
        }

        #region 출력- 좌측패널(정보출력) PrintData
        private void PrintData()
        {
            Shape sh = con.Shape;

            ComboBox.Text = sh.Type == ShapeType.RECT ? "사각형" : "타원";
            RedTextBox.Text = sh.Color.R.ToString();
            GreenTextBox.Text = sh.Color.G.ToString();
            BlueTextBox.Text = sh.Color.B.ToString();

            XTextBox.Text = sh.Pos.X.ToString();
            YTextBox.Text = sh.Pos.Y.ToString();
            WidthTextBox.Text = sh.Width.ToString();
            HeightTextBox.Text = sh.Height.ToString();

            canvas.Children.Clear();
            if (sh.Type == ShapeType.RECT)
                Rectangle(sh);

            else if (sh.Type == ShapeType.ELLIPSE)
                Ellipse(sh);
        }

        private void Rectangle(Shape sh)
        {
            Rectangle rt = new Rectangle();
            rt.Width = sh.Width;
            rt.Height = sh.Height;
            rt.Fill = new SolidColorBrush(Shape.ConvertColor(sh.Color));

            Canvas.SetLeft(rt, sh.Pos.X);
            Canvas.SetTop(rt, sh.Pos.Y);

            canvas.Children.Add(rt);
        }



        private void Ellipse(Shape sh)
        {
            Ellipse rt = new Ellipse();
            rt.Width = sh.Width;
            rt.Height = sh.Height;
            rt.Fill = new SolidColorBrush(Shape.ConvertColor(sh.Color));

            Canvas.SetLeft(rt, sh.Pos.X);
            Canvas.SetTop(rt, sh.Pos.Y);

            canvas.Children.Add(rt);
        }
        #endregion

       
        #region 구현2 - 상태바
        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point pos = e.GetPosition(canvas);

            StatusBar1.Content = DateTime.Now.ToShortDateString();
            StatusBar2.Content = DateTime.Now.ToShortTimeString();
            StatusBar3.Content = pos.ToString();
        }
        #endregion

        #region 구현3 - 색상 메뉴
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = System.Drawing.Color.Red;
            PrintData();
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = System.Drawing.Color.Green;           
            PrintData();
        }

        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = System.Drawing.Color.Blue;           
            PrintData();
        }

        private void MenuItem4_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                con.Shape.Color = System.Drawing.Color.FromArgb(byte.Parse(colorDialog.Color.R.ToString()), byte.Parse(colorDialog.Color.G.ToString()), byte.Parse(colorDialog.Color.B.ToString()));
                PrintData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = System.Drawing.Color.FromArgb(byte.Parse(RedTextBox.Text), byte.Parse(GreenTextBox.Text), byte.Parse(BlueTextBox.Text));
            PrintData();
        }
        #endregion

        #region 구현3,4 - 위치 정보 변경 / 도형(width,heigth) 정보 변경 / 속성정보 변경
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(canvas);
            con.Shape.Pos = new System.Drawing.Point((int)pos.X, (int)pos.Y);
            PrintData();
        }

        private void ComboBoxItem1_Selected(object sender, RoutedEventArgs e)
        {
            // == Rectangle(shapes);
            con.Shape.Type = ShapeType.RECT;
            PrintData();
        }

        private void ComboBoxItem2_Selected(object sender, RoutedEventArgs e)
        {
            // == Ellipse(shapes);
            con.Shape.Type = ShapeType.ELLIPSE;
            PrintData();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {            
            int width = int.Parse(HeightTextBox.Text);
            
            if (Keyboard.IsKeyDown(Key.PageUp) == true)
            {
                
                if (width == 150)
                {
                    con.Shape.Width -= 10;
                    con.Shape.Height -= 10;
                }
                con.Shape.Width += 10;
                con.Shape.Height += 10;
                PrintData();
            }

            if (Keyboard.IsKeyDown(Key.PageDown) == true)
            {
                if (width == 0)
                {
                    con.Shape.Width += 10;
                    con.Shape.Height += 10;
                }
                con.Shape.Width -= 10;
                con.Shape.Height -= 10;
                PrintData();
            }
            
        }
        #endregion

        #region 구현5 - Close, Save, Load
        private void ClostItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            con.FileSave();
        }

        private void LoadItem_Click(object sender, RoutedEventArgs e)
        {
            con.FileLoad();
            PrintData();
        }
        #endregion
        
    }
}
