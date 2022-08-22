using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_실습
{
    internal static class Input
    {
        public static void InsertPhone(out int phone)
        {
            phone = WbLib.getNumber(">> 전화 번호 입력");
        }
        
        public static void InsertTID(out int TID)
        {
            TID = WbLib.getNumber(">> 좌석 선택 입력");
        }
    }
}
