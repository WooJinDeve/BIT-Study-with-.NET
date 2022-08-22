using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_관리자
{
    class WbControl
    {
        #region 싱글톤 패턴
        private WbControl() { }
        public static WbControl Instance { get; private set; }
        static WbControl()
        {
            Instance = new WbControl();
        }
        #endregion

        private WbDB db = new WbDB();


        public DataTable GetMemberListAll()
        {
            return db.GetMemberAllList();
        }


    }
}
