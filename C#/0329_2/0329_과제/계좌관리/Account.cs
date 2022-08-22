using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제
{
    class Account
    {
        public int Id { get; set; }               // 계좌번호
        public int Balance { get; set; }          // 잔    액
        public string Name { get; set; }          // 고객 이름
        public DateTime Da { get; set; }

 
        //생성자
        public Account(int _id, string _name, int _balance)
        {
            Id = _id;
            Balance = _balance;
            Name = _name;
            Da = DateTime.Now;
        }

        public Account(int _id, string _name, int _balance, DateTime date)
        {
            Id = _id;
            Balance = _balance;
            Name = _name;
            Da = date;
        }

        public void AddMoney(int val)
        {
            Balance += val;
        }
        //---------------------------------------------------

        public void MinMoney(int val)
        {
            Balance -= val;
        }

        public void ShowAllData()
        {
            Console.WriteLine("계좌 ID :" + Id);
            Console.WriteLine("이름 :" + Name);
            Console.WriteLine("잔액 :" + Balance);
            Console.WriteLine("일시 : " + Da);
        }

        public override string ToString()
        {
            return "계좌 ID: " + Id + ", 이름 :" + Name + ". 잔액 :" + Balance + ", 일시 :" + Da;
        }
    }
}
