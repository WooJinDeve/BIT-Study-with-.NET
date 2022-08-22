using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0419.데이터바인딩
{
    internal class People : List<Person>
    {
        public People()
        {
            Add(new Person("홍길동", "010-1111-1234", 20, true));
            Add(new Person("일지매", "010-2222-1234", 30, false));
            Add(new Person("임꺽정", "010-3333-1234", 40, true));
        }
    }
}
