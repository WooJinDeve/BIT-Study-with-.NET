using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace _0425_Papago_Server_
{
    public class Papago
    {
        public string OutputText(string in_lan, string out_lan, string intput_text)
        {
            bool[] bo = new bool[100];

            string url = "https://openapi.naver.com/v1/papago/n2mt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string client_id = "YIyRuy9TdDJebkHCJXUs";
            string client_secret = "vsyOxiOhua";

            in_lan = Language(in_lan);
            out_lan = Language(out_lan);

            //"source=ko&target=en&text="
            string change_language = "source=" + in_lan + "&target=" + out_lan + "&text=";


            request.Headers.Add("X-Naver-Client-Id", client_id);
            request.Headers.Add("X-Naver-Client-Secret", client_secret);
            request.Method = "POST";


            byte[] byteDataParams = Encoding.UTF8.GetBytes(change_language + intput_text);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;

            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            stream.Close();
            response.Close();
            reader.Close();

            return RetMssage(text);
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


        private string Language(string lan)
        {
            switch(lan)
            {
                case "한국어": return "ko";
                case "영어": return "en";
                case "일본어": return "ja";
                case "중국어 간체": return "zh-CN";
                case "중국어 번체": return "zh-TW";
                case "베트남어": return "vi";
                case "인도네시아어": return "id";
                case "태국어": return "th";
                case "독일어": return "de";
                case "러시아어": return "ru";
                case "스페인어": return "es";
                case "이탈리아어": return "it";
                case "프랑스어": return "fr";
                default: return "ko";
            }
        }
    }
}