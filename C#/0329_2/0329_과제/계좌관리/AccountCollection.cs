using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제
{
    internal class AccountCollection: List<Account>
    {
        public AccountCollection()
        {           
        }

        public void InitData()
        {
            Add(new Account(111, "홍길동", 1000));
            Add(new Account(222, "이길동", 2000));
            Add(new Account(333, "공길동", 3000));
            Add(new Account(444, "허길동", 4000));

            IOControl.Singleton.OnAddAccount(this,
                new 이벤트처리.AddAccountEventArgs(111, "홍길동", 1000, DateTime.Now));

            IOControl.Singleton.OnAddAccount(this,
                new 이벤트처리.AddAccountEventArgs(222, "이길동", 2000, DateTime.Now));

            IOControl.Singleton.OnAddAccount(this,
                new 이벤트처리.AddAccountEventArgs(333, "공길동", 3000, DateTime.Now));

            IOControl.Singleton.OnAddAccount(this,
                new 이벤트처리.AddAccountEventArgs(444, "허길동", 4000, DateTime.Now));

        }
    }
}
