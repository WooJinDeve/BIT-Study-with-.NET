using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{
    static class Packet
    {
        private const string SEARCHBOOK = "SearchBook"; //@검색할 정보
        private const string SELECTALLBOOKNAME = "SelectAllBookName"; //@
        private const string SELECTBOOKNAME = "SelectBookName"; //@책의 이름 
        private const string SELECTALLBOOK = "SelectAllBook"; //@

        public static string SearchBook(string msg)
        {
            string packet = string.Format("{0}@{1}", SEARCHBOOK, msg);
            return packet;
        }
        public static string SelectAllBookName()
        {
            string packet = string.Format("{0}@", SELECTALLBOOKNAME);
            return packet;
        }
        public static string SelectBookName(string msg)
        {
            string packet = string.Format("{0}@{1}", SELECTBOOKNAME, msg);
            return packet;
        }
        public static string SelectAllBook()
        {
            string packet = string.Format("{0}@", SELECTALLBOOK);
            return packet;
        }

    }
}
