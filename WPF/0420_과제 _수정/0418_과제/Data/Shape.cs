using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Data;

namespace _0418_과제
{
    internal enum ShapeType { NON, RECT, ELLIPSE }

    // [Serializable] <- 직렬화 X
    class Shape : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ShapeType type;
        public ShapeType Type 
        { 
            get { return type; }
            set
            {
                type = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
            }
        }

        private Point pos;
        public Point Pos
        {
            get { return pos; }
            set
            {
                pos = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pos"));
            }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Width"));
            }
        }

        private int heigth;
        public int Height
        {
            get { return heigth; }
            set
            {
                heigth = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Height"));
            }
        }

        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Color"));
            }
        }

        public Shape() { }

        public Shape(ShapeType type, Point pos, int width, int height, Color color)
        {
            Type = type;
            Pos = pos;
            Width = width;
            Height = height;
            Color = color;
        }

        public static System.Windows.Media.Color ConvertColor(System.Drawing.Color col)
        {
            return System.Windows.Media.Color.FromArgb(
                byte.Parse("255"),
                byte.Parse(col.R.ToString()),
                byte.Parse(col.G.ToString()),
                byte.Parse(col.B.ToString()));            
        }
    }

    #region ComboBox 도형정보 Convert
    [ValueConversion(/* 원본 형식 */ typeof(ShapeType), /* 대상 형식 */ typeof(string))]
    public class ShapeTypeToIntConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            ShapeType sh = (ShapeType)value;
            if (sh == ShapeType.NON)
                return null;
            else
                return sh == ShapeType.RECT ? 0 : 1;
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            int? sh = (int?)value;
            if (sh == null)
                return ShapeType.NON;
            else if (sh == 0)
                return ShapeType.RECT;
            else
                return ShapeType.ELLIPSE;
        }
    }
    #endregion
    #region RGB 
    [ValueConversion(/* 원본 형식 */ typeof(Color), /* 대상 형식 */ typeof(string))]
    public class ColorRtoStringConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            Color sh = (Color)value;

            if (sh == null)
                return null;
            else
                return sh.R.ToString();
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            string sh = (string)value;
            if (sh == null)
                return null;
            else
                return int.Parse(sh);
        }
    }

    [ValueConversion(/* 원본 형식 */ typeof(Color), /* 대상 형식 */ typeof(string))]
    public class ColorGtoStringConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            Color sh = (Color)value;
            if (sh == null)
                return null;
            else
                return sh.G.ToString();
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            string sh = (string)value;
            if (sh == null)
                return null;
            else
                return int.Parse(sh);
        }
    }

    [ValueConversion(/* 원본 형식 */ typeof(Color), /* 대상 형식 */ typeof(string))]
    public class ColorBtoStringConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            Color sh = (Color)value;
            if (sh == null)
                return null;
            else
                return sh.B.ToString();
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            string sh = (string)value;
            if (sh == null)
                return null;
            else
                return int.Parse(sh);
        }
    }

    [ValueConversion(/* 원본 형식 */ typeof(Color), /* 대상 형식 */ typeof(string))]
    public class ColorRGBtoStringConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            Color sh = (Color)value;
            if (sh == null)
                return null;
            else
                return "#" + sh.R.ToString("X2") + sh.G.ToString("X2") + sh.B.ToString("X2");
        }
        // UI 속성을 데이터 속성으로 변경할 때
        public object ConvertBack(object value, Type targetType, object
                parameter, System.Globalization.CultureInfo culture)
        {
            Color sh = (Color)value;
            if (sh == null)
                return null;
            else
                return null;
        }
    }
    #endregion
}
