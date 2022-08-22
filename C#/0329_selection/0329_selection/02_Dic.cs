using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_selection
{
    class Member:IComparable<Member>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Member(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public override string ToString()
        {
            return Id + " : " +  Name +" : "+ Phone;
        }

        public int CompareTo(Member other)
        {
            return Id - other.Id; // - 0 +
        }
    }

    class NameComparer : IComparer<Member>
    {
        public int Compare(Member x, Member y) // IComparer에서 약속한 기능 구현
        {           
            return x.Name.CompareTo(y.Name);
        }
    }

    internal class Class2
    {
        public static void exam1()
        {
            Dictionary<int, Member> members = 
                new Dictionary<int, Member>();

            //저장
            members[10] = new Member(10, "홍길동", "010-1111-1111");
            members[11] = new Member(11, "이길동", "010-2222-2222");
            members[12] = new Member(12, "공길동", "010-3333-3333");

            //검색
            Member findMember = members[11];
            Console.WriteLine(findMember.ToString());

            //수정
            findMember.Name = "홍길순";
            findMember.Phone = "010-7777-7777";

            //삭제
            members.Remove(10);

            //전체 출력
            foreach (Member mem in members.Values)
            {
                Console.WriteLine(mem);
            }

        }

        public static void exam2()
        {
            List<Member> members = new List<Member>();

            members.Add(new Member(11, "홍길동", "010"));
            members.Add(new Member(9, "김길동", "080"));
            members.Add(new Member(20, "허길동", "020"));
            members.Add(new Member(8, "이길동", "090"));

            //members.Sort();
            members.Sort(new NameComparer());


            foreach(Member mem in members)
            {
                Console.WriteLine(mem);
            }

        }

        public static void Main()
        {
            exam2();
        }
    }
}
