using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0408_XML
{
    class Book
    {
        internal string Title   { get; private set; }
        internal int Price      { get; private set; }
        
        internal static Book MakeBook(XmlReader xr)
        {
            string title = string.Empty; 
            int price = 0;
            
            xr.ReadToDescendant("title");
            title = xr.ReadElementString("title"); 
            
            xr.ReadToNextSibling("가격");
            string temp = xr.ReadElementString("가격");
            price = int.Parse(temp);
            //xr.ReadStartElement("가격"); 
            //price = int.Parse(xr.Value); 
            return new Book(title, price);
        }

        Book(string title, int price)
        {
            Title = title; 
            Price = price;
        }

        public override string ToString()
        {
            return Title + "\t" + Price;
        }
    }
}
