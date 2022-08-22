using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0411_SearchServer
{
    public class BookPasing
    {
        public string Xmlstring { get; private set; }

        #region 데이터 파싱

        //레퍼런스 기반
        public void URLPasing1(string msg)
        {
            string query = msg; // 검색할 문자열
            //string url = "https://openapi.naver.com/v1/search/book?query=" + query; // 결과가 JSON 포맷
            string url = "https://openapi.naver.com/v1/search/book.xml?query=" + query;  // 결과가 XML 포맷
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "dCVxppNn_pMNLtCLicmL"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "H5xCfL4JNk");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                //text에 XML문서가 들어있음.....--> DOM파싱...
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }
        
        //교재 기반
        public List<Book> URLPasing2(string msg)
        {
            List<Book> books = new List<Book>();

            string query = msg; // 검색할 문자열
            //string url = "https://openapi.naver.com/v1/search/book?query=" + query; // 결과가 JSON 포맷
            string url = "https://openapi.naver.com/v1/search/book.xml?query=" + query;  // 결과가 XML 포맷
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "dCVxppNn_pMNLtCLicmL"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "H5xCfL4JNk");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Xmlstring = text;
                //----------------------------- 교재 내용 --------------------------------------

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(text);   //doc.Load("XML문서가 있는 URL경로");
                XmlNode node = doc.SelectSingleNode("rss");
                XmlNode n = node.SelectSingleNode("channel");
                Book book = null;
                foreach (XmlNode el in n.SelectNodes("item"))
                {
                    book = MakeBook(el);
                    books.Add(book);
                }
                return books;
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
                return null;
            }            
        }
        
        private Book MakeBook(XmlNode xn)
        {
            string title = string.Empty;
            string author = string.Empty;
            string image = string.Empty;
            int price = 0;
            string publisher = string.Empty;
            string description = string.Empty;

            XmlNode title_node = xn.SelectSingleNode("title");
            title = ConvertString(title_node.InnerText);

            XmlNode author_node = xn.SelectSingleNode("author");
            author = ConvertString(author_node.InnerText);

            XmlNode image_node = xn.SelectSingleNode("image");
            image = ConvertImageString(image_node.InnerText);

            XmlNode price_node = xn.SelectSingleNode("price");
            price = int.Parse(price_node.InnerText);

            XmlNode publisher_node = xn.SelectSingleNode("publisher");
            publisher = ConvertString(publisher_node.InnerText);

            XmlNode description_node = xn.SelectSingleNode("description");
            description = ConvertString(description_node.InnerText);

            return new Book(title, author,image, price, publisher, description);
        }

        private  string ConvertString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf('<');
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.IndexOf('>');
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }

        private string ConvertImageString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf('?');
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.Length - 1;// str.IndexOf('?');
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }

        #endregion
    }
}
