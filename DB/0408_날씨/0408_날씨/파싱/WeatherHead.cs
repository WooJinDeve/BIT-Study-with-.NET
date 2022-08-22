using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0408_날씨
{
    public class WeatherHead
    {
        internal string Title { get; private set; }
        internal string Link { get; private set; }
        internal string Description { get; private set; }
        internal string Generator { get; private set; }
        internal string PubDate { get; private set; }       

        public WeatherHead(string title, string link, string description, string generator, string pubdate)
        {
            Title = title;
            Link = link;
            Description = description;
            Generator = description;
            PubDate = pubdate;          
        }

        public void Print()
        {
            Console.WriteLine("[TITLE] " + Title);
            Console.WriteLine("[Link] " + Link);
            Console.WriteLine("[Description] " + Description);
            Console.WriteLine("[Generator] " + Generator);
            Console.WriteLine("[PubDate] " + PubDate);
        }
    }
}
