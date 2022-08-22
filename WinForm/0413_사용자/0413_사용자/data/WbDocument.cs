using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_사용자
{
    class WbDocument
    {
        #region 싱글톤 패턴
        private WbDocument()  { }
        public static WbDocument Instance { get; private set; }
        static WbDocument()
        {
            Instance = new WbDocument();
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

        #region 기능 
        public Member IdtoMember(string id)
        {
            string sql = WbQuery.IdtoMember(id);
            string msg = wb.CommandQuery(sql);

            string[] str = msg.Split('#');

            if (str[0] != "")
            {
                return new Member(bool.Parse(str[0]), str[1], str[2], str[3], str[4], int.Parse(str[5]), DateTime.Parse(str[6]));
            }
            throw new Exception("존재하지 않는 ID입니다.");
        }

        public string Login(string id, string pw)
        {
            Member member = IdtoMember(id);
            if (member.Pw == pw)
            {
                if (member.IsLogin == true)
                    throw new Exception("이미 로그인된 아이디입니다.");
                            
                wb.CommandNonQuery(WbQuery.IsLoginTrueChange(id));
                return member.Name;
            }
            throw new Exception("패스워드가 일치하지 않습니다."); ;
        }

        //회원가입
        public bool NewMember(Member mem)
        {
            string msg = WbQuery.NewMember(mem);
            wb.CommandNonQuery(msg);
            return true;
        }
             

        //삭제
        public void DeleteMember(string delid)
        {
            string msg = WbQuery.DeleteMember(delid);
            wb.CommandNonQuery(msg);
        }

        public void UpdateMember(string id, string phone, int age)
        {
            string sql = WbQuery.UpdateMember(id, phone, age);
            wb.CommandNonQuery(sql);
        }

        //로그아웃
        public void Logout(string id)
        {
            string sql = WbQuery.Logout(id);
            wb.CommandNonQuery(sql);
        }
        #endregion 

    }
}
