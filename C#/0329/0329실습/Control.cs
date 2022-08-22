using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace _0329실습
{  
     class Control
     {
        #region 정렬 관련 클래스
        
        class NameComparer : IComparer<Member>
        {
            public int Compare(Member x, Member y) // IComparer에서 약속한 기능 구현
            {

                return x.Name.CompareTo(y.Name);
            }
        }
        class PhoneComparer : IComparer<Member>
        {
            public int Compare(Member x, Member y) // IComparer에서 약속한 기능 구현
            {

                return x.Phone.CompareTo(y.Phone);
            }
        }
        class GenderComparer : IComparer<Member>
        {
            public int Compare(Member x, Member y) // IComparer에서 약속한 기능 구현
            {

                return x.Gender.CompareTo(y.Gender);
            }
        }

        #endregion 

        private List<Member> members = new List<Member>();

        public Control()
        {
            //list = new List<Member>();
        }

        #region 파일 IO
        public void FileLoad()
        {
            WbFile.ReadFile(members);
        }
        public  void FileSave()
        {
            WbFile.WriteFile(members);
        }
        #endregion

        #region 기능 함수들
        public void SelectAll()
        {
           Console.WriteLine("[{0}]",  members.Count);
           foreach(Member mem in members)
            {
                Console.WriteLine(mem);
            }
        }
        
        public void Insert()
        {
            try
            {
                string name = WbLib.GetString("이름");
                string phone = WbLib.GetString("전화번호");
                string gender = WbLib.GetString("성별");

                Member mem = new Member(name, phone, gender);

                members.Add(mem);
                Console.WriteLine("저장되었습니다");
            }
            catch(Exception ex)
            {
                Console.WriteLine("저장 오류");
                Console.WriteLine(ex.Message);
            }
        }
   
        private int NameToIdx(string name)
        {
            for(int i=0; i<members.Count; i++)
            {
                Member mem = members[i];
                if(mem.Name == name)
                    return i;
            }
            return -1;
        }

        public void Select()
        {
            try
            {
                string name = WbLib.GetString("검색할 이름");
                int idx = NameToIdx(name);
                if (idx == -1)
                {
                    Console.WriteLine("없다");
                }
                else
                {
                    Console.WriteLine(members[idx]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("검색 오류");
                Console.WriteLine(ex.Message);
            }
        }
       
        public void Delete()
        {
            try
            {
                string name = WbLib.GetString("삭제할 이름");
                int idx = NameToIdx(name);
                if (idx == -1)
                {
                    Console.WriteLine("없다");
                }
                else
                {
                    members.RemoveAt(idx);
                    Console.WriteLine("삭제되었습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("삭제 오류");
                Console.WriteLine(ex.Message);
            }
        }
        
        public  void Update()
        {
            try
            {
                string name = WbLib.GetString("수정할 이름");
                int idx = NameToIdx(name);
                if (idx == -1)
                {
                    Console.WriteLine("없다");
                }
                else
                {
                    string phone = WbLib.GetString("변경할 전화번호");
                    members[idx].Phone = phone;
                    Console.WriteLine("수정 되었습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("수정 오류");
                Console.WriteLine(ex.Message);
            }
        }
        
        public  void DeleteAll()
        {
            try
            {
                members.Clear();
                Console.WriteLine("삭제 되었습니다.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("전체 삭제 오류");
                Console.WriteLine(ex.Message);
            }
        }
        
        public void Sort()
        {
            Console.WriteLine("[F1] 이름순 정렬");
            Console.WriteLine("[F2] 전화번호 정렬 ");
            Console.WriteLine("[F3] 성별 정렬 ");
            ConsoleKey key = WbLib.GetKey("선택");
            
            switch(key)
            {
                case ConsoleKey.F1: 
                    members.Sort(new NameComparer()); break;
                case ConsoleKey.F2: 
                    members.Sort(new PhoneComparer()); break;
                case ConsoleKey.F3: 
                    members.Sort(new GenderComparer()); break;
            }    
        }
        
        #endregion
    }
}
