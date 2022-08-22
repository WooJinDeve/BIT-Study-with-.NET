using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;   //System.Drawing 어셈블리
using System.Drawing.Imaging;
using System.Threading;
using System.Net.Sockets;

namespace _0411_SearchServer
{
    class Control
    {
        #region 싱글톤
        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();
        }
        private Control() { }
        #endregion

        #region 네트웤 사용 필드
        private const int SERVER_PORT = 7000;
        private WbServer server = null;
        #endregion


        private BookPasing wp   = new BookPasing();
        private BookDataBase db = new BookDataBase();

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})",
                        flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(Socket client, string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "SearchBook": OnSearchBook(client, sp[1]); break;
                case "SelectAllBookName": OnSelectAllBookName(client, sp[1]); break;
                case "SelectBookName": OnSelectBookName(client, sp[1]); break;
                case "SelectAllBook": OnSelectAllBook(client, sp[1]); break;
            }
        }
        #endregion

        #region 수신처리를 위한 핸들러함수
        private void OnSearchBook(Socket client, string msg)
        {
            bool b = SearchBook(msg);
            string packet = Packet.SearchBook_ACK(b);
            server.SendData(client, packet);
        }
        private bool SearchBook(string msg)
        {
            try
            {
                List<Book> books = wp.URLPasing2(msg);

                db.InsertAllBooks(books);
                Console.WriteLine("데이터베이스 저장 성공");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void OnSelectAllBookName(Socket client, string msg)
        {
            List<string> booknames;
            bool b = SelectAllBookName(out booknames);
            string packet = Packet.SelectAllBookName_ACK(b, booknames);
            server.SendData(client, packet);
        }
        private bool SelectAllBookName(out List<string> booknames)
        {
            try
            {
                booknames = db.SelectAllBookName();
                return true;
            }
            catch(Exception ex)
            {
                booknames = null;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void OnSelectBookName(Socket client, string msg)
        {
            string sql;
            bool b = SelectBookName(msg, out sql);
            string packet = Packet.SelectBookName_ACK(b, sql);
            server.SendData(client, packet);
        }
        private bool SelectBookName(string msg, out string sql)
        {
            try
            {
                sql = db.SelectBookName(msg);
                return true;
            }
            catch(Exception ex)
            {
                sql = null;
                Console.WriteLine(ex.Message);
                return false; 
            }
        }

        private void OnSelectAllBook(Socket client, string msg)
        {
            List<Book> books;
            bool b = SelectAllBook(out books);
            string packet = Packet.SelectAllBook_ACK(b, books);
            server.SendData(client, packet);
        }
        private bool SelectAllBook(out List<Book> books)
        {
            try
            {
                books = db.SelectAllBooks();
             
                return true;
            }
            catch(Exception ex)
            {
                books = null;
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion



        #region 시작과 종료시점(데이터 베이스 연결/소켓 및 종료 처리)

        public bool Init()
        {
            if (db.Open() == false)
                return false;

            server = new WbServer(SERVER_PORT); //소켓생성--> listen
            server.Start(LogMessage, RecvMessage); //ListenThread
            return true;
        }

        public void Exit()
        {
            server.Dispose(); //ListenThread를 종료, 대기소켓close
            server = null;

            db.Close();
        }
        #endregion 

        #region 기능 메서드
        public void Parsing()
        {
            try
            {
                string msg = WbLib.getString("검색 도서명");
                List<Book> books = wp.URLPasing2(msg);

                db.InsertAllBooks(books);
                Console.WriteLine("데이터베이스 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintAll()
        {
            List<Book> books = db.SelectAllBooks(); 
            foreach (Book book in books)
            {
                book.PrintData();
                Console.WriteLine();
            }
        }

        #endregion

        #region 부가 기능-이미지 파일 처리(c:\\image폴더 생성)

        public void ImageDownLoad()
        {
            List<string> urls =  db.SelectImageUrl();

            //폴더 생성
            string strPath = "c://image";
            DirectoryInfo di = new DirectoryInfo(strPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < urls.Count; i++)
                {
                    string url = urls[i];

                    byte[] imgArray = client.DownloadData(url);

                    using (MemoryStream memstr = new MemoryStream(imgArray))
                    {
                        Image img = Image.FromStream(memstr);   //참조 추가!!!!
                        string filename = string.Format("c://image//Image{0}.jpeg", i);
                        img.Save(filename, ImageFormat.Jpeg);
                    }
                }
            }
            PrintFileList();
        }

        private void PrintFileList()
        {
            string FolderName = @"c://image";
            DirectoryInfo di = new DirectoryInfo(FolderName);
            foreach (FileInfo File in di.GetFiles())
            {
                Console.WriteLine(File.Name);
            }
        }
        #endregion 
    }
}
