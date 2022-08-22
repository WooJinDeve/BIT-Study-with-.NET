using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제.이벤트처리
{
    /// <summary>
    /// 계좌생성시 사용되는 인자
    /// </summary>
    internal class AddAccountEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Balance { get; private set; }
        public DateTime IOTime { get; private set; }

        public AddAccountEventArgs(int id, string name, int balance, DateTime ioteime)
        {
            Id = id;
            Name = name;
            Balance = balance;
            IOTime = ioteime;  
        }
    }

    internal class AddIOEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public int Input { get; private set; }
        public int Output { get; private set; }
        public int Balance { get; private set; }
        public DateTime IOTime { get; private set; }

        public AddIOEventArgs(int id, int input, int output, int balance, DateTime ioteime)
        {
            Id = id;
            Input = input;
            Output = output;
            Balance = balance;
            IOTime = ioteime;
        }
    }

}
