using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

    }
}
