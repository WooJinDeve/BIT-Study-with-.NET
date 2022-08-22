using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal static class WbQuery
    {
        //insert into Product values('생수', 500, '생수입니다');
        public static string Product_InsertCommand(string pname, int price, string description)
        {
            string sql = string.Format("insert into Product values('{0}', {1}, '{2}');",
                pname, price, description);

            return sql;
        }

        public static string SelectAll()
        {
            string sql = "select * from Product";

            return sql;
        }

        //select Price from Product where PNAME = '콜라';
        public static string PNameToPrice(string pname)
        {
            string sql = string.Format("select Price from Product where PNAME = '{0}';",
                    pname);

            return sql;
        }
    }
}
