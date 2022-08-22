using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0414_Server
{
    class MemberDatabase
    {
        WbDB db = new WbDB();


        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "MemberSample";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion 


        #region Open + Close
        public bool Open()
        {
            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }
        #endregion

        //[관리자]
        public List<Member> GetMemberAllList()
        {
            try
            {
                List<Member> list = new List<Member>();
                string sql = WbQuery.GetMemberAllList();
                string msg = db.CommandQuery(sql);

                string[] f1 = msg.Split('$');
                for (int i = 0; i < f1.Length - 1; i++)
                {
                    string[] f2 = f1[i].Split('#');

                    list.Add(new Member(bool.Parse(f2[0]), f2[1], f2[2], f2[3], f2[4], int.Parse(f2[5]), DateTime.Parse(f2[6])));
                }
                return list;
            }
            catch (Exception)
            {
                return null;
                //throw new Exception("없는 ID입니다.");
            }
        }

        //[사용자]
        #region

        public Member IdtoMember(string id)
        {
            string sql = WbQuery.IdtoMember(id);
            string msg = db.CommandQuery(sql);

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

                db.CommandNonQuery(WbQuery.IsLoginTrueChange(id));
                return member.Name;
            }
            throw new Exception("패스워드가 일치하지 않습니다."); ;
        }

        //회원가입
        public bool NewMember(Member mem)
        {
            string msg = WbQuery.NewMember(mem);
            db.CommandNonQuery(msg);
            return true;
        }


        //삭제
        public void DeleteMember(string delid)
        {
            string msg = WbQuery.DeleteMember(delid);
            db.CommandNonQuery(msg);
        }

        public void UpdateMember(string id, string phone, int age)
        {
            string sql = WbQuery.UpdateMember(id, phone, age);
            db.CommandNonQuery(sql);
        }

        //로그아웃
        public void Logout(string id)
        {
            string sql = WbQuery.Logout(id);
            db.CommandNonQuery(sql);
        }

        #endregion
    }
}
