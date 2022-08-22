using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0411_SearchServer
{ 
    static class Packet
    {
        private const string SEARCHBOOK    = "SearchBook"; //@검색할 정보
        private const string SELECTALLBOOKNAME = "SelectAllBookName"; //@
        private const string SELECTBOOKNAME = "SelectBookName"; //@책의 이름 
        private const string SELECTALLBOOK = "SelectAllBook"; //@

        #region [Server -> Client]

        public static string SearchBook_ACK(bool b)
        {
            string packet = string.Format("{0}@{1}",
                                            SEARCHBOOK, b);
            return packet;
        }


        public static string SelectAllBookName_ACK(bool b, List<string> booknames)
        {
            string packet = string.Format("{0}@{1}#", SELECTALLBOOKNAME,b);
            foreach(string s in booknames)
            {
                packet += s;
            }
            return packet;
        }

        public static string SelectBookName_ACK(bool b, string msg)
        {
            string packet = string.Format("{0}@{1}#{2}", SELECTBOOKNAME, b, msg);
            return packet;
        }

        public static string SelectAllBook_ACK(bool b, List<Book> books)
        {
            string packet = string.Format("{0}@{1}$", SELECTALLBOOK,b);
            string msg;
            foreach(Book book in books)
            {
                msg = string.Format("{0}#{1}#{2}#{3}#{4}#{5}$", book.Title, book.Author, book.Image, book.Price, book.Publisher, book.Description);
                packet += msg;
            }

            return packet;                
        }
        #endregion


    }
}
