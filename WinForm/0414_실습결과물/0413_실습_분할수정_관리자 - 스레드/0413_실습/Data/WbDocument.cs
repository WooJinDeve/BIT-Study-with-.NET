using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    internal class WbDocument : IDisposable
    {

        #region 싱글톤 패턴
        private WbDocument()
        {
            Open();
        }
        public static WbDocument Instance { get; private set; }
        static WbDocument()
        {
            Instance = new WbDocument();
        }
        #endregion

        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "Sample";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion 

        private WbDB db = new WbDB();

        #region DB연결(WbDocument의 생성자) 및 해제(WbDocument의 Dispose)

        public bool Open()
        {
            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }

        #endregion 

        //회원전체정보 반환
        public List<Member> GetMemberAllList()
        {
            try
            {
                List<Member> list = new List<Member>();
                string sql = WbQuery.GetMemberAllList();
                string msg = db.CommandQuery(sql);

                string[] f1 = msg.Split('$');
                for(int i=0; i< f1.Length-1; i++)
                {                    
                    string[] f2 = f1[i].Split('#');

                    list.Add(new Member(int.Parse(f2[5]), f2[0], f2[1], f2[2], f2[3], int.Parse(f2[4]), DateTime.Parse(f2[6])));
                }
                return list;
            }
            catch (Exception)
            {
                throw new Exception("없는 ID입니다.");
            }
        }
       
        public void Dispose()
        {
            Close();
        }
    }
}
