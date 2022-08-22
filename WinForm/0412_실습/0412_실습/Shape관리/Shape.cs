using System.Drawing;

namespace _0412_실습
{
    internal enum ShapeType { NON, RECT, ELLIPSE, TRIANGLE }
    internal class Shape
    {
        public ShapeType Type { get; set; }
        public int PenSize { get; set; }
        public Color BrushColor { get; set; }
        public Point ShapePoint { get; set; }

        public Shape() { }
        public Shape(ShapeType t, int p, Color b, Point sp)
        {
            Type = t;
            PenSize = p;
            BrushColor = b;
            ShapePoint = sp;
        }
    }
}
