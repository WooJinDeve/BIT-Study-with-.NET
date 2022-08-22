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
            //members.Add(new Member("111", "111", "홍길동", "010-1111-1111", 20));
            //members.Add(new Member("222", "222", "고길동", "010-2222-2222", 30));
            //members.Add(new Member("333", "333", "김길동", "010-3333-3333", 10));
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

        //검색
        public Member IdToMember(string id)
        {
            try
            {
                string sql = WbQuery.IdToMember(id);
                string msg = db.CommandQuery(sql);

                string[] f1 = msg.Split('$');
                string[] f2 = f1[0].Split('#');

                return new Member(f2[0], f2[1], f2[2], f2[3], int.Parse(f2[4]), DateTime.Parse(f2[6]));
            }
            catch (Exception)
            {
                throw new Exception("없는 ID입니다.");
            }
        }

        //로그인
        public string LogIn(string id, string pw)
        {
            try
            {
                string sql = WbQuery.LogIn(id, pw);
                string name = (string)db.CommandScalar(sql);

                sql = WbQuery.LogInUpdate(id);
                db.CommandNonQuery(sql);
                return name;
             }
            catch (Exception)
            {
                throw new Exception("패스워드가 틀립니다.");
            }
        }

        //Id중복체크(중복되면 true)
        public bool IdContainCheck(string id)
        {
            try
            {
                Member mem = IdToMember(id);
                if (mem.Id == id)
                    return true;
                else
                    return false;
            }
            catch(Exception )
            {
                return false;
            }
        }

        //회원가입
        public bool NewMember(Member mem)
        {
            try
            {
                string sql = WbQuery.NewMember(mem);
                db.CommandNonQuery(sql);
                return true;
             }
            catch (Exception)
            {
                throw new Exception("없는 ID입니다.");
            }
        }
    
        //회원삭제
        public void DeleteMember(string delid)
        {
            try
            {
                string sql = WbQuery.DeleteMember(delid);
                db.CommandNonQuery(sql);
            }
            catch (Exception)
            {
                throw new Exception("없는 ID입니다.");
            }
        }

        //회원정보수정
        public void UpdateMember(string id, string phone, int age)
        {
            try
            {
                string sql = WbQuery.UpdateMember(id, phone, age);
                db.CommandNonQuery(sql);
            }
            catch (Exception)
            {
                throw new Exception("없는 ID입니다.");
            }
        }

        //로그아웃
        public void LogOut(string id)
        {
            try
            {
                string sql = WbQuery.LogOut(id);
                db.CommandNonQuery(sql);
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
