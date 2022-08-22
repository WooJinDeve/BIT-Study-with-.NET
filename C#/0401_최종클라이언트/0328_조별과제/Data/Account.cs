using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{
    public class Account
    {
        public int Id       { get; private set; }
        public int Balance  { get; private set; }        
        public string Name  { get; private set; }

        public DateTime Datetime { get; private set; }

        public Account(int _id, int _balance, string _name)
        {
            Id      = _id;
            Balance = _balance;
            Name    = _name;
            Datetime = DateTime.Now;
        }
 

        public virtual void AddMoney(int val)
        {
            Balance += val;
        }

        public void MinMoney(int val)
        {
            Balance -= val;
        }

        public virtual void ShowAllData()
        {
            Console.Write("계좌 ID : " + Id + "\t");
            Console.Write("이   름 {0}\t" , Name);
            Console.Write("잔 액: " + Balance + "원" + "\t");
        }
    }
}
