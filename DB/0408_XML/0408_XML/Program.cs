using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0408_XML
{
    class Program
    {
        //파서 사용하기 
        static void Exam1()
        {
            List<Book> ar = new List<Book>();

            XmlReader reader = XmlReader.Create("data.xml");
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.IsStartElement("book"))
                {
                    Book book = Book.MakeBook(reader); 
                    if (book != null) 
                           ar.Add(book); 
                }
            }
            Console.WriteLine("도서 개수:{0}", ar.Count); 
            
            foreach (Book book in ar)
            {
                Console.WriteLine(book);
            }
        }
        static void Main(string[] args)
        {
            Exam1();
        }
    }
}
