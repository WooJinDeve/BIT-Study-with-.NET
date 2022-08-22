using System;
using System.Drawing;

namespace _0418_과제
{
    internal enum ShapeType { NON, RECT, ELLIPSE }

    [Serializable]
    class Shape
    {
        public ShapeType Type { get; set; }
        public Point Pos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

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
}
