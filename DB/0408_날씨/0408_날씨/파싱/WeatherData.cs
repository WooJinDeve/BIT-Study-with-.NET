using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0408_날씨
{
    public enum LocationID : long 
    { 
        NONE = 0,
        동구자양동   = 3011059000,
        중구문화동   = 3014072000,
        서구둔산동   = 3017063000,       
        유성구노은동 = 3020054600,
        대덕구법동   = 3023060000
    }

    public class WeatherData
    {
        internal int Hour       { get; private set; }
        internal float Temp     { get; private set; }
        internal int Sky        { get; private set; }
        internal int Pty        { get; private set; }
        internal string Wfkor   { get; private set; }
        internal float R12        { get; private set; }
        internal float Ws       { get; private set; }
        internal string Wdkor   { get; private set; }        

        public WeatherData(int hour, float temp, int sky, int pty, string wfkor, float r12, float ws, string wdkor )
        {
            Hour = hour;
            Temp = temp;
            Sky = sky;
            Pty = pty;
            Wfkor = wfkor;
            R12 = r12;
            Ws = ws;
            Wdkor = wdkor;
        }

        public override string ToString()
        {
            string str = string.Format("{0,3}시 {1,3}도 {2,9} {3,10} {4,10} {5,10} {6,10}(m/s) {7,7}",
                Hour, Temp, GetSkyString(Sky), GetPtyString(Pty),
                Wfkor, R12, Ws, Wdkor);
            return str;
            //return Hour + "\t" + Temp + "\t" + GetSkyString(Sky) + "\t" 
            //    + GetPtyString(Pty) + "\t" + Wfkor
            //    + "\t" + R12 + "\t" + Ws + "\t" + Wdkor;
        }

        private string GetSkyString(int sky)
        {
            switch(sky)
            {
                case 1: return "맑음";
                case 3: return "구름많음";
                case 4: return "흐림";
                default: return "";
            }
        }

        private string GetPtyString(int sky)
        {
            switch (sky)
            {
                case 0: return "없음";
                case 1: return "비";
                case 2: return "비/눈";
                case 3: return "눈";
                case 4: return "소나기";
                case 5: return "빗방울";
                case 6: return "빗방울/눈날림";
                case 7: return "눈날림";
                default: return "";
            }
        }
    }
}
