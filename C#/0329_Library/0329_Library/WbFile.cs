using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{
    static class WbFile
    {
        //static string path = @"C:\Users\user\Desktop\0329_Library\0329_Librarytest.txt";       
        static string path = @"0329_Librarytest.txt";

        static public void WriteFile(List<Library> books)
        {
            StreamWriter sw = File.CreateText(path);      
            
            sw.WriteLine(books.Count);
            for (int i = 0; i < books.Count; i++)
            {
                Library boo = books[i];
                sw.WriteLine(boo.Name + " "
                                + boo.Publisher + " "
                                + boo.Price + " "
                                + boo.Date.Year + " " + boo.Date.Month + " " + boo.Date.Day);
            }
            sw.Dispose();
        }
        static public void ReadFile(List<Library> books)
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    int size = int.Parse(sr.ReadLine());

                    for (int i = 0; i < size; i++)
                    {
                        string book = sr.ReadLine();
                        string[] sp = book.Split(' ');

                        string title = sp[0];
                        string publisher = sp[1];
                        int price = int.Parse(sp[2]);
                        DateTime publishtime = new DateTime(int.Parse(sp[3]),
                            int.Parse(sp[4]),
                            int.Parse(sp[5]));

                        Library temp = new Library(title, publisher, price, publishtime);
                        books.Add(temp);
                    }            
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
