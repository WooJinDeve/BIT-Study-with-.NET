using System;
using System.Collections.Generic;
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

        WbDB wb = new WbDB();

        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "MemberSample";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion

        #region Open + Close
        public bool Open()
        {
            if (wb.Open(server, database, id, pw) == true)
                return true;
            throw new Exception("데이터베이스 연결 실패");
        }

        public void Close()
        {
            wb.Close();
        }
        #endregion


        public string[] GetMemberListAll()
        {
            string sql = WbQuery.SelectAll();
            string msg = wb.CommandQuery(sql);

            string[] str = msg.Split('$');

            return str;
        }


    }
}
