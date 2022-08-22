using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0411_SearchServer
{    
    class BookDataBase
    {    

        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "Sample3";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion 

        private WbDB db = new WbDB();

        #region DB연결 및 해제

        public bool Open()
        {
            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }

        #endregion 

        #region 기능

        public void InsertAllBooks(List<Book> books)
        {
            try
            {
                foreach (Book book in books)
                {
                    string sql = WbQuery.Book_InsertCommand(book);
                    db.CommandNonQuery(sql);
                }                
            }
            catch(Exception)
            {
                throw new Exception("DB저장 실패");
            }            
        }

        public List<string> SelectAllBookName()
        {
            try
            {
                List<string> booknames = new List<string>();

                string sql = WbQuery.SelectAllBookName();
                string msg = db.CommandQuery(sql);

                string[] f1 = msg.Split('$');
                for (int i = 0; i < f1.Length - 1; i++)
                {
                    booknames.Add(f1[i]);
                }
                return booknames;
            }
            catch (Exception)
            {
                throw new Exception("DB저장 실패");
            }
        }
  


        //selectall 사용 예제(수정하여 사용할 것)
        public List<Book> SelectAllBooks()
        {
            try
            {
                string sql = WbQuery.Book_SelectAll();
                string msg = db.CommandQuery(sql);

                return SelectAllBooks(msg);
            }
            catch(Exception)
            {
                Console.WriteLine("검색 오류");
                return null;
            }
        }

        private List<Book> SelectAllBooks(string msg)
        {
            List<Book> books = new List<Book>();

            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                //bookid, title, author, image, price, publisher, description
                string[] f2 = f1[i].Split('#');
                books.Add(new Book(f2[1], f2[2], f2[3], int.Parse(f2[4]), f2[5], f2[6]));                
            }
            return books;
        }


        public string SelectBookName(string title)
        {
            string sql = WbQuery.SelectBookName(title);
            string msg = db.CommandQuery(sql);
            return msg;
        }
        #endregion

        #region 부가기능 -이미지 추가 기능
        public List<string> SelectImageUrl()
        {
            try
            {
                string sql = WbQuery.Book_SelectImage();
                string msg = db.CommandQuery(sql);

                return SelectImageUrl(msg);
            }
            catch (Exception)
            {
                Console.WriteLine("검색 오류");
                return null;
            }
        }

        private List<string> SelectImageUrl(string msg)
        {
            List<string> images = new List<string>();

            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                //image
                string[] f2 = f1[i].Split('#');
                images.Add(f2[0]);
            }
            return images;
        }

        #endregion
    }
}
