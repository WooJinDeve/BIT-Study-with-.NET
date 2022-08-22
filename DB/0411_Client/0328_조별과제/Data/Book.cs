using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0328_조별과제
{

    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Image { get; private set; }
        public int Price { get; private set; }
        public string Publisher { get; private set; }
        public string Description { get; private set; }

        public Book(string title, string author, string image, int price, string publisher, string description)
        {
            Title = title;
            Author = author;
            Image = image;
            Price = price;
            Publisher = publisher;
            Description = description;
        }

        public override string ToString()
        {
            string str = string.Format("{0,3} {1,3} {2,9} {3,10} {4,10} {5}",
                Title, Author, Price, Publisher, Description, Image);
            return str;
        }

        public void PrintData()
        {
            Console.WriteLine("[Title] " + Title);
            Console.WriteLine("[Author] " + Author);
            Console.WriteLine("[Image] " + Image);
            Console.WriteLine("[Price] " + Price);
            Console.WriteLine("[Publisher] " + Publisher);
            Console.WriteLine("[Description] " + Description);
        }
    }
}
