using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0411_SearchServer
{
    internal static class WbQuery
    {
        public static string Book_InsertCommand(Book book)
        {
             string sql = string.Format("insert into Book values('{0}', '{1}', '{2}', {3}, '{4}', '{5}');",
                book.Title, book.Author, book.Image, book.Price, book.Publisher, book.Description);
            return sql;
        }

        public static string Book_SelectAll()
        {
            string sql = "select * from Book";

            return sql;
        }

        public static string SelectAllBookName()
        {
            string sql = "select title from Book";
            return sql;
        }

        public static string SelectBookName(string title)
        {
            string sql = string.Format("select * from Book where title = '{0}'", title);
            return sql;
        }

        public static string Book_SelectImage()
        {
            string sql = "select image from Book";
            return sql;
        }

      
    }
}
