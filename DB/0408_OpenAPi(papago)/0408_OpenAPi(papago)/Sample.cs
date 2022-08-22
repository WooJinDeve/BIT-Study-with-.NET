using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _0408_OpenAPi_papago_
{
    static class Sample
    {
        static public string Exam1(string msg)
        {
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            //(서비스 요청객체)
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "YIyRuy9TdDJebkHCJXUs");
            request.Headers.Add("X-Naver-Client-Secret", "WSie82bJLU");

            request.Method = "POST";
            string query = msg;
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=ko&target=en&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            //응답객체
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            return RetMassage(text);
        }

        public static string RetMassage(string msg)
        {
            JObject root = JObject.Parse(msg);
            //Console.WriteLine(root);

            JObject result = (JObject)root["message"]["result"];

            return result["translatedText"].ToString();
        }
    }
}
