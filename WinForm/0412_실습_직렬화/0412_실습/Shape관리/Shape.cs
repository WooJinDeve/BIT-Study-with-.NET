using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0412_실습
{
    internal enum ShapeType { NON, RECT ,ELLIPSE, TRIANGLE}
    //타입(사각형, 타원,삼각형),펜의 두께(1,3,5), 브러쉬 색상(R,G,B) ,좌표
    
    [Serializable]
    internal class Shape
    {
        public ShapeType Type { get; set; }
        public int Pensize { get; set; }
        public Color BrushColor { get; set; }
        public Point ShapePoint { get; set; }

        public Shape() { }
        public Shape(ShapeType t, int p, Color b,Point sp)
        {
            Type = t;
            Pensize = p;
            BrushColor = b;
            ShapePoint = sp;
        }



    }
}
