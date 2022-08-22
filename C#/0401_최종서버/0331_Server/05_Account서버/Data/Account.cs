using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Server._05_Account서버
{
    public class Account
    {
        public int Id { get; private set; }        
        public string Name { get; private set; }
        public int Balance { get; private set; }

        public DateTime Datetime { get; private set; }

        public Account(int _id, string _name, int _balance)
        {
            Id = _id;
            Balance = _balance;
            Name = _name;
            Datetime = DateTime.Now;
        }

        public bool AddMoney(int val)
        {
            if (val <= 0)
                return false;

            Balance += val;
            return true;
        }

        public bool MinMoney(int val)
        {
            if (val <= 0)
                return false;

            if (Balance < val)
                return false;

            Balance -= val;
            return true;
        }

        public virtual void ShowAllData()
        {
            Console.Write("계좌 ID : " + Id + "\t");
            Console.Write("이   름 {0}\t", Name);
            Console.Write("잔 액: " + Balance + "원" + "\t");
        }
    }
}
