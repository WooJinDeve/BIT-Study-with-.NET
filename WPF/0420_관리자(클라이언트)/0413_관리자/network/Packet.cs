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
        private const string GETAIRPLANEALLLIST = "GetAirPlaneAllList";        //@검색할정보

        public static string GetAirPlaneAllList()
        {
            string packet = string.Format("{0}@", GETAIRPLANEALLLIST);
            return packet;
        }        
    }
}
