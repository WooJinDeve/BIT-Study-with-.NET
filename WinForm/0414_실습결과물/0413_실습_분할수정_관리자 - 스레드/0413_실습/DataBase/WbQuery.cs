using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    internal static class WbQuery
    {
        public static string GetMemberAllList()
        {
            string sql = string.Format("select * from Member;");
            return sql;
        }        
    }
}
