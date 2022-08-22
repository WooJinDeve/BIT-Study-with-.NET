using System;
using System.Collections.Generic;
using _0329_과제.이벤트처리;

namespace _0329_과제
{
    internal class IOControl
    {
        #region 싱글톤 패턴
        public static IOControl Singleton { get; private set; }

        static IOControl()
        {
            Singleton = new IOControl();
        }

        private IOControl()
        {           
            Control.Singleton.AddAccount += OnAddAccount;
            Control.Singleton.InputAccount += OnInputMoney;
            Control.Singleton.OutputAccount += OnOutputMoney;
        }

        #endregion

        private List<IOAccount> io_accounts = new List<IOAccount>();

        #region 이벤트 핸들러
        public void OnAddAccount(object obj, AddAccountEventArgs e)
        {
            // 이벤트 처리 구문
            io_accounts.Add(
                new IOAccount(e.Id, e.Balance, 0, e.Balance, e.IOTime));
        }

        public void OnInputMoney(object obj, AddIOEventArgs e)
        {
            // 이벤트 처리 구문
            io_accounts.Add(
                new IOAccount(e.Id, e.Input, e.Output, e.Balance, e.IOTime));
        }

        public void OnOutputMoney(object obj, AddIOEventArgs e)
        {
            // 이벤트 처리 구문
            io_accounts.Add(
                new IOAccount(e.Id, e.Input, e.Output, e.Balance, e.IOTime));

        }

        #endregion
    
        public void PrintAccount(int accid)
        {
            foreach(IOAccount io in io_accounts)
            {
                if(io.Id == accid)
                {
                    Console.WriteLine(io);
                }
            }
        }
    }
}
