using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace _0408_파파고
{
    static class Sample
    {
        #region 파파고
        public static string Exam1(string msg)
        {
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            //(서비스)요청객체
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "VBP_XY75qz3QuDYSVAu4");
            request.Headers.Add("X-Naver-Client-Secret", "EKCuBpRPQ4");
            request.Method = "POST";
            string query = msg;
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=ko&target=ja&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            //응답객체
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            //JSON String...
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            
            return RetMssage(text);
        }
    
        public static string RetMssage(string msg)
        {
            JObject root = JObject.Parse(msg);
            //Console.WriteLine(root.ToString());

            //Console.WriteLine();
            JObject message = (JObject)root["message"];
            //Console.WriteLine(message.ToString());

            Console.WriteLine();
            JObject result = (JObject)message["result"];
            //Console.WriteLine(result.ToString());

            string retmsg = result["translatedText"].ToString();
            return retmsg;
        }
        #endregion 

        public static void Exam2()
        {
            string query = "네이버 Open API"; // 검색할 문자열
            string url = "https://openapi.naver.com/v1/search/blog?query=" + query; // 결과가 JSON 포맷
            //string url = "https://openapi.naver.com/v1/search/blog.xml?query=" + query;  // 결과가 XML 포맷
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "4CszOgoPxQeE3mLafMnL"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "SpVuUegGYl");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }

    }
}
