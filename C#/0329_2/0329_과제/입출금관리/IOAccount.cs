using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제
{
    internal class IOAccount
    {
        public int Id       { get; private set; }              
        public int Input    { get; private set; }              
        public int Output   { get; private set; }    
        public int Balance  { get; private set; }  
        public DateTime Da  { get; private set; }

        #region 생성자
        public IOAccount(int _id, int _input, int _output, int _balance)
        {
            Id      = _id;
            Input   = _input;
            Output  = _output;
            Balance = _balance;            
            Da      = DateTime.Now;
        }

        public IOAccount(int _id, int _input, int _output, int _balance, DateTime date)
        {
            Id      = _id;
            Input   = _input;
            Output  = _output;
            Balance = _balance;
            Da      = date;
        }
        #endregion 


        public override string ToString()
        {
            string str = "";

            str += "[계좌 ID] " + Id + " ";
            str += "[입금액] " + Input + " ";
            str += "[출금액] " + Output + " ";
            str += "[잔액]" + Balance + " ";
            str += "[일자]" + Da.ToShortDateString() + " ";
            str += "[시간]" + Da.ToShortTimeString();

            return str;
        }
    }
}
