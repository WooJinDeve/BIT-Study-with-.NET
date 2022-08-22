using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0408_날씨
{
    public class WeatherPasing
    {               
        private const string findelement = "data";
        private const string findelementhead = "channel";

        #region 헤더 파싱
        public WeatherHead URLPasingHead(string url)
        {
            WeatherHead head = null;

            XmlReader reader = XmlReader.Create(url);
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.IsStartElement(findelementhead))
                {
                    head = MakeWeatherHead(reader);
                }
            }
            reader.Close();
            return head;
        }

        private WeatherHead MakeWeatherHead(XmlReader xr)
        {
            string title = string.Empty,        link = string.Empty;
            string description = string.Empty,  generator = string.Empty;
            string pubdate = string.Empty;

            xr.ReadToDescendant("title");
            title = xr.ReadElementString("title");

            xr.ReadToNextSibling("link");
            link = xr.ReadElementString("link");

            xr.ReadToNextSibling("description");
            description = xr.ReadElementString("description");
  
            xr.ReadToNextSibling("generator");
            generator = xr.ReadElementString("generator");

            xr.ReadToNextSibling("pubDate");
            pubdate = xr.ReadElementString("pubDate");
          
            return new WeatherHead(title, link, description, generator, pubdate);
        }
        #endregion 

        #region 데이터 파싱
        public List<WeatherData> URLPasing(string url)
        {
            List<WeatherData> datas = new List<WeatherData>();

            XmlReader reader = XmlReader.Create(url);
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.IsStartElement(findelement))
                {
                    WeatherData data = MakeWeather(reader);
                    if (data != null)
                        datas.Add(data);
                }
            }
            reader.Close();
            return datas;
        }

        private WeatherData MakeWeather(XmlReader xr)
        {
            int hour = 0, sky = 0, pty = 0; 
            float temp = 0.0f, ws = 0.0f, r12 = 0; 
            string wfkor = string.Empty, wdkor = string.Empty;

            string s;
            xr.ReadToDescendant("hour");
            s = xr.ReadElementString("hour");
            hour = int.Parse(s);

            xr.ReadToNextSibling("temp");
            s = xr.ReadElementString("temp");
            temp = float.Parse(s);

            xr.ReadToNextSibling("sky");
            s = xr.ReadElementString("sky");
            sky = int.Parse(s);

            xr.ReadToNextSibling("pty");
            s = xr.ReadElementString("pty");
            pty = int.Parse(s);

            xr.ReadToNextSibling("wfKor");
            wfkor = xr.ReadElementString("wfKor");

            xr.ReadToNextSibling("r12");
            s = xr.ReadElementString("r12");
            r12 = float.Parse(s);

            xr.ReadToNextSibling("ws");
            s = xr.ReadElementString("ws");
            ws = float.Parse(s);

            xr.ReadToNextSibling("wdKor");
            wdkor = xr.ReadElementString("wdKor");

            return new WeatherData(hour, temp, sky, pty, wfkor, r12, ws, wdkor);
        }
        #endregion 
    }
}
