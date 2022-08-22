using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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

            penel.DataContext = con.Shape;

            canvas.Focusable = true;

        }


        #region 구현2 - 상태바
        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Point pos = e.GetPosition(canvas);

            StatusBar1.Content = DateTime.Now.ToShortDateString();
            StatusBar2.Content = DateTime.Now.ToShortTimeString();
            StatusBar3.Content = pos.ToString();
        }
        #endregion

        #region 구현3 - 색상 메뉴
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = Color.Red;
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = Color.Green;           
        }

        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = Color.Blue;           
        }

        private void MenuItem4_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                con.Shape.Color = Color.FromArgb(byte.Parse(colorDialog.Color.R.ToString()), byte.Parse(colorDialog.Color.G.ToString()), byte.Parse(colorDialog.Color.B.ToString()));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con.Shape.Color = Color.FromArgb(int.Parse(RedTextBox.Text), int.Parse(GreenTextBox.Text), int.Parse(BlueTextBox.Text));
        }
        #endregion

        #region 구현3,4 - 위치 정보 변경 / 도형(width,heigth) 정보 변경 / 속성정보 변경
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (con.Shape.Type == ShapeType.RECT)
            {
                rec.Opacity = 100;
                eli.Opacity = 0;
            }
            if (con.Shape.Type == ShapeType.ELLIPSE)
            {
                rec.Opacity = 0;
                eli.Opacity = 100;
            }
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pos = e.GetPosition(canvas);
            con.Shape.Pos = new System.Drawing.Point((int)pos.X, (int)pos.Y);            
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
        }
        #endregion
    }
}
