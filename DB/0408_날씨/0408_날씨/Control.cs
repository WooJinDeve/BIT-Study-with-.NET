using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0408_날씨
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

        private List<WeatherData> datas = null;
        private WeatherHead head = null;

        private WeatherPasing wp = new WeatherPasing();
        private string url = "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=";
       

        #region 기능 메서드
        public void Parsing()
        {
            try
            {
                Console.WriteLine("[1]동구(자양동) [2]중구(문화동) [3]서구(둔산동) [4]유성구(노은동) [5]대덕구(법동)");
                int number = WbLib.getNumber("지역 선택");
                switch (number)
                {
                    case 1:  url = url + LocationID.동구자양동;  break;
                    case 2: url = url + LocationID.중구문화동;   break;
                    case 3: url = url + LocationID.서구둔산동;   break;
                    case 4: url = url + LocationID.유성구노은동; break;
                    case 5: url = url + LocationID.대덕구법동;   break;
                };

                datas = wp.URLPasing(url);
                Console.WriteLine("데이터 파싱 성공");

                head = wp.URLPasingHead(url);
                Console.WriteLine("데이터 헤더 파싱 성공");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintAll()
        {
            head.Print();
            Console.WriteLine("\n");

            foreach (WeatherData wdata in datas)
            {
                Console.WriteLine(wdata);
            }
        }
              
        #endregion
    }
}
