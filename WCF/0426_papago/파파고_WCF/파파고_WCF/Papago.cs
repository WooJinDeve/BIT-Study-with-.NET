using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 파파고_WCF
{
    class Papago : IPapago
    {
        public string papago(string source, string target, string text)
        {
            string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "llS5N1CptUiRlEG0jMhx");
            request.Headers.Add("X-Naver-Client-Secret", "jI_z8oPwLZ");
            request.Method = "POST";
            string msg = string.Format("source={0}&target={1}&text={2}", source, target, text);
            byte[] byteDataParams = Encoding.UTF8.GetBytes(msg);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string texteng = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            return RetMssage(texteng);
        }

        public string RetMssage(string msg)
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
    }
}
