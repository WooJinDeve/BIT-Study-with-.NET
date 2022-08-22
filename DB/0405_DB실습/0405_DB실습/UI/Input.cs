using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal static class Input
    {
        public static void InsertData(out string pname, out int price, out string description)
        {
            pname = WbLib.getString("제품명");
            price = WbLib.getNumber("가격");
            description = WbLib.getString("제품설명");
        }

        public static void InputPName(out string pname)
        {
            pname = WbLib.getString("제품명");
        }
    }
}
