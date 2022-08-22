using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _0418_과제
{
    internal class Control
    {
        static string path = "Shapes.txt";
        public Shape Shape { get; set; }

        public Control()
        {
            Shape = new Shape(ShapeType.ELLIPSE, new Point(100, 100), 80, 70, Color.Red);
        }

        public void FileSave()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Shape);
            stream.Close();
        }

        public void FileLoad()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Shape = (Shape)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}
