using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    class WbDocument : IDisposable
    {
        private List<Member> members = new List<Member>();

        #region 싱글톤 패턴
        private WbDocument() 
        {
            WbFile.Load(members);
        }
        public static WbDocument Instance { get; private set; }
        static WbDocument()
        {
            Instance = new WbDocument();
        }
        #endregion

        public  Member IdtoMember(string id)
        {
            foreach(Member member in members)
            {
                if (member.Id == id)
                    return member;
            }
            throw new Exception("존재하지 않는 ID입니다.");
        }
        //로그인
        public string Login(string id, string pw)
        {
            
            Member member = IdtoMember(id);
            if (member.Pw == pw)
            {
                member.IsLogin = true;
                return member.Name;
            }
            throw new Exception("패스워드가 일치하지 않습니다."); ;
        }

        //ID 중복체크( 중복되면 True )
        public bool IdContainCheck(string id)
        {
            foreach (Member member in members)
            {
                if (member.Id == id)
                    return true;
            }
            return false;
        }

        //회원가입
        public bool NewMember(Member mem)
        {
            members.Add(mem);
            return true;
        }

        //회원전체정보 반환
        public List<Member> GetMemberListAll()
        {
            return members;
        }

        //삭제
        public void DeleteMember(string delid)
        {
            Member mem = IdtoMember(delid);
            members.Remove(mem);
        }

        //회원 수정
        public void UpdateMember(string id, string phone, int age)
        {
            Member mem = IdtoMember(id);
            mem.Phone = phone;
            mem.Age = age;
        }

        //로그아웃
        public void Logout(string id)
        {
            Member mem = IdtoMember(id);
            mem.IsLogin = false;
        }

        public void Dispose()
        {
            WbFile.Save(members);
        }
    }
}
