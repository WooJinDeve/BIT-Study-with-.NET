using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_과제
{
    class FileControl
    {
        List<FileInfo> fileinfos = new List<FileInfo>();

        #region [기능 1] 
        string directoryPath = @"C:\Users\user\Desktop\0412_과제\0412_과제\bin\Debug";

        public static List<string> GetImageFiles(string directoryPath)
        {
            List<string> imageFileList = new List<string>();
            foreach (string fileName in Directory.GetFiles(directoryPath))
            {
                if (Regex.IsMatch(fileName, @".jpg|.png|.bmp|.JPG|.PNG|.BMP|.JPEG|.jpeg$"))
                {
                    imageFileList.Add(fileName);
                } 
            }
            return imageFileList; 
        }

        public void LoadFileInfo()
        {
            List<string> fileArr = GetImageFiles(directoryPath);
            foreach (string fileName in fileArr)
            {
                fileinfos.Add(new FileInfo(fileName));
            }
        }

        public void ListBoxPrint(ListBox listBox)
        {
            foreach (FileInfo fi in fileinfos)
            {
                listBox.Items.Add(fi.Name);
            }
        }
        #endregion

        public void TextBoxPrint(TextBox textBox, string filename)
        {
            int idx = NamebyIndex(filename);

            string str = string.Format("[파일명] : {0} [파일생성일자] : {1} [최근접근일] {2}", fileinfos[idx].Name, fileinfos[idx].CreationTime, fileinfos[idx].LastAccessTime);
            textBox.Text = str;
        }

        public int NamebyIndex(string filename)
        {
            int idx = 0;
            foreach(FileInfo fi in fileinfos)
            {
                if (fi.Name == filename)
                    return idx;
                idx++;
            }
            return -1;
        }
        public void ImagePrint(Graphics g, string filename)
        {
            try
            {
                Image jpg = Image.FromFile(filename);
                g.DrawImage(jpg, 0, 0);
                g.Dispose();
            }
            catch (Exception) { }
        }
  
    }
}
