using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_관리자
{
    static class Packet
    {
        //--[Client --> Server]
        private const string GETMEMBERALLLIST = "GetMemberAllList";        //@검색할정보

        public static string GetMemberAllList()
        {
            string packet = string.Format("{0}@",  GETMEMBERALLLIST);
            return packet;
        }        
    }
}
