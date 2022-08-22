using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0408_XML
{
    static class WBXMLSample
    {
        #region XMl Write
        //요소쓰기
        public static void Example1()
        {
            XmlWriterSettings xsettings = new XmlWriterSettings();
            xsettings.Indent = true;

            XmlWriter xwriter = XmlWriter.Create("data.xml", xsettings);
            // <!--   -->
            xwriter.WriteComment("[주석]XmlWriter 개체 만들기 실습 예제");
            xwriter.WriteStartElement("books"); //<books>

            xwriter.WriteStartElement("book");  //    <book>ADO.NET</book>
            xwriter.WriteValue("ADO.NET");
            xwriter.WriteEndElement();

            xwriter.WriteStartElement("book");  //    <book>XML.NET</book>
            xwriter.WriteValue("XML.NET");
            xwriter.WriteEndElement();

            xwriter.WriteEndElement();          //</books>

            xwriter.Close();
        }

        //요소쓰기(2가지 방법) - XML문서 콘솔 화면 출력
        public static void Example2()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("data.xml", settings);

            writer.WriteComment("XmlWriter 개체로 요소 쓰기");

            writer.WriteStartElement("books"); //루트 요소 쓰기

            //---
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteStartElement("title");//title 요소 쓰기
            writer.WriteName("XML.NET");        //????????
            writer.WriteEndElement();//title 요소 닫기

            writer.WriteStartElement("가격");//가격 요소 쓰기
            writer.WriteValue(12000);
            writer.WriteEndElement();//가격 요소 닫기

            writer.WriteEndElement();//book 요소 닫기
            //----

            //---book
            writer.WriteStartElement("book");//book 요소 쓰기

            writer.WriteElementString("title", "ADO.NET");//title 요소와 값 쓰기

            writer.WriteStartElement("가격");//가격 요소 쓰기
            writer.WriteValue(15000);
            writer.WriteEndElement();//가격 요소 닫기
            writer.WriteEndElement();//book 요소 닫기
            //---

            writer.WriteEndElement();//루트 요소 닫기
            writer.Close();

            XmlReader xreader = XmlReader.Create("data.xml"); //XmlReader 개체 생성
            XmlWriter xwriter = XmlWriter.Create(Console.Out); //콘솔 출력 스트림으로 XmlWriter 개체 생성
            xwriter.WriteNode(xreader, false); //xreader 개체로 읽어온 데이터를 xwriter 개체에 복사
            xwriter.Close();
            xreader.Close();
        }

        //특성쓰기(3가지 방법)
        public static void Example3()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("data.xml", settings);

            writer.WriteComment("XmlWriter 개체로 특성 쓰기");

            writer.WriteStartElement("books"); //루트 요소 쓰기

            //--book 
            writer.WriteStartElement("book");//book 요소 쓰기
            //-- 특성 작성1
            writer.WriteStartAttribute("title"); //title 특성 쓰기
            writer.WriteString("XML.NET"); //title 특성 값 쓰기
            writer.WriteEndAttribute(); //title 특성 닫기

            //-- 특성 작성2
            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(12000); //가격 특성 값 쓰기
            writer.WriteEndAttribute(); //가격 특성 닫기

            writer.WriteEndElement(); //book 요소 닫기
            //--

            //--book 
            writer.WriteStartElement("book");//book 요소 쓰기

            //--특성 작성3
            writer.WriteAttributeString("title", "ADO.NET");//title 특성과 값 쓰기

            writer.WriteStartAttribute("가격");//가격 특성 쓰기
            writer.WriteValue(15000);//가격 특성 값 쓰기
            writer.WriteEndAttribute();//가격 특성 닫기

            writer.WriteEndElement();//book 요소 닫기
            writer.WriteEndElement();//루트 요소 닫기
            writer.Close();

            //XmlReader객체로 라인다인로 읽고
            //XmlWriter객체로 Console 에 XmlReadr로 읽어온 정보를 분석해서 출력
            XmlReader xreader = XmlReader.Create("data.xml"); //XmlReader 개체 생성
            XmlWriter xwriter = XmlWriter.Create(Console.Out, settings); //XmlWriter 개체 생성

            while (xreader.Read())
            {
                if (xreader.NodeType == XmlNodeType.Element)
                {
                    xwriter.WriteStartElement(xreader.Name);
                    xwriter.WriteAttributes(xreader, false); //xreader의 현재 특성을 쓰기
                    if (xreader.IsEmptyElement)
                    {
                        xwriter.WriteEndElement();
                    }
                }
                else if (xreader.NodeType == XmlNodeType.EndElement)
                {
                    xwriter.WriteEndElement();
                }
                else if (xreader.NodeType == XmlNodeType.Comment)
                {
                    xwriter.WriteComment(xreader.Value);
                }
            }
            xwriter.Close();
            xreader.Close();
            Console.WriteLine();
        }
        #endregion

        #region Xml Reader : 객체 사용 법(XML문서와 XMLReader객체 연결)
        private static void WirteConsole(XmlReader reader)
        {
            XmlWriter xwriter = XmlWriter.Create(Console.Out);
            xwriter.WriteNode(reader, false);
            xwriter.Close();
            Console.WriteLine();
        }

        //Create(FileStream input);
        public static void Example4()
        {
            FileStream fs = new FileStream("data.xml",
                    FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //reader1은 xml파일에 연결...
            XmlReader reader1 = XmlReader.Create(fs);
            WirteConsole(reader1);

            reader1.Close();
            fs.Close();
        }

        //Create(FileStream input, XmlReaderSettings settings);
        //주석 제거
        public static void Example5()
        {
            FileStream fs = new FileStream("data.xml",
                FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;

            XmlReader reader2 = XmlReader.Create(fs, settings);

            WirteConsole(reader2);
            reader2.Close();
            fs.Close();
        }

        //Create(파일 경로);
        public static void Example6()
        {
            XmlReader reader3 = XmlReader.Create("data.xml");
            WirteConsole(reader3);
            reader3.Close();
        }

        //Create(string uri, XmlReaderSettings settings);
        public static void Example7()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader4 = XmlReader.Create("data.xml", settings);
            WirteConsole(reader4);
            reader4.Close();
        }
        #endregion

        #region URL경로를 이용하여 외부 xml파일 정보 연결(RSS)

        public static void Example8()
        {
            string url = "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=3011059000";
            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = resolver;

            XmlReader reader = XmlReader.Create(url, settings); 
            
            WirteConsole(reader);
            reader.Close();
        }
        #endregion

        #region XML 정방향 파싱
        public static void exam1()
        {
            XmlReader reader = XmlReader.Create("data.xml"); 
            //reader.MoveToContent();
            while (reader.Read())
            {
                WriteNodeInfo(reader);
            }
        }
        private static void WriteNodeInfo(XmlReader reader)
        {
            Console.Write("노드 형:{0}\t",       reader.NodeType);
            Console.Write("  ▷ 노드 이름:{0}\t", reader.Name); 
            Console.WriteLine("  ▷노드 데이터:{0}", reader.Value);
        }
        #endregion 
    }
}
