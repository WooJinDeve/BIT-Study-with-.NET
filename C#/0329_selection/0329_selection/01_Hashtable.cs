using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_selection
{
    class Class1
    {
        //Hashtable 생성 : Add(저장)
        public static void exam1()
        {
            Hashtable ht = new Hashtable();

            //저장
            ht.Add("홍길동", "율도국");
            ht.Add("장언휴", "이에이치");

            //중복키 저장 불가!
            ht.Add("홍길동", "예외발생");

            //전체 순회
            foreach (DictionaryEntry d in ht)
            {
                Console.WriteLine("{0}:{1}", d.Key, d.Value);
            }
        }

        //Hashtable 생성 : 인덱서 사용(저장 및 수정)
        public static void exam2()
        {
            Hashtable ht = new Hashtable();
            
            //새로운 키 : 저장
            ht["홍길동"] = "율도국";
            ht["장언휴"] = "이에이치";

            //동일한 키 추가 : 검색 후 수정
            //홍길동 : 율도국 -> 홍길동 : 대한민국
            ht["홍길동"] = "대한민국";

            foreach (DictionaryEntry d in ht)
            {
                Console.WriteLine("{0}:{1}", d.Key, d.Value);
            }
        }

        //Hashtable 검색
        public static void exam3()
        {
            Hashtable ht = new Hashtable();

            //새로운 키 : 저장
            ht["홍길동"] = "율도국";
            ht["장언휴"] = "이에이치";

            string value = (string)ht["홍길동"];
            Console.WriteLine("출력결과 : " + value);

            string value1 = (string)ht["홍길동1"];
            if (value1 != null)
                Console.WriteLine("출력결과 : " + value);
            else
                Console.WriteLine("출력결과 : " + "없다");
        }

        public static void exam4()
        {
            Hashtable ht = new Hashtable();
            ht["홍길동"] = "율도국";
            ht["장언휴"] = "이에이치";
            ht["김 구"] = "대한민국";
            ht.Remove("홍길동");
            foreach (DictionaryEntry d in ht)
            {
                Console.WriteLine("{0}:{1}", d.Key, d.Value);
            }
            ht.Clear();
            Console.WriteLine("보관된 요소 개수:{0}", ht.Count);
        }

        public static void Main()
        {
            exam4();
        }
    }
}
