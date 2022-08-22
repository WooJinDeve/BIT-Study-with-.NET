using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{    
    class Library:IComparable<Library>
    {
        public string Name { get; set; }
        // 출판사
        public string Publisher { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
       

        public Library()
        {
            Name = string.Empty;
            Publisher = string.Empty;
            Price = int.MinValue;
            Date = DateTime.MinValue;
        }
        public Library(string name, string publisher, int price)
        {
            Name = name;
            Publisher = publisher;
            Price = price;
            Date = DateTime.UtcNow;
        }
        public Library(string name, string publisher, int price, DateTime date)
        {
            Name = name;
            Publisher = publisher;
            Price = price;
            Date = date;
        }

         public override string ToString()
        {
            return "책 제목 : "+Name + " : "+ "출판사 : " + Publisher + " : " + "가격 : " + Price + ": " + "입력일 : " + Date.Year + "/" + Date.Month +"/" + Date.Day;
        }

        //sort 기능 추가 //가격으로 정렬
        public int CompareTo(Library other)
        {
            return Price - other.Price; // - 0 +
        }
    }
}
