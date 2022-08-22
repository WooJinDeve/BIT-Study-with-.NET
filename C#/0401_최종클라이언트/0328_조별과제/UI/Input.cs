using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{
    static class Input
    {
        public static void MakeAccount(out int id, out string name, out int balance)
        {
            id = WbLib.getNumber("ID 입력");
            name = WbLib.getString("이름 입력");
            balance = WbLib.getNumber("입금금액 입력");
        }
      
        public static void SelectAccount(out int id)
        {
            id = WbLib.getNumber("ID 입력");
        }

        public static void InputAccount(out int id, out int money)
        {
            id = WbLib.getNumber("ID 입력");
            money = WbLib.getNumber("입금금액 입력");
        }

        public static void OutputAccount(out int id, out int money)
        {
            id = WbLib.getNumber("ID 입력");
            money = WbLib.getNumber("출금금액 입력");
        }

        public static void DeleteAccount(out int id)
        {
            id = WbLib.getNumber("ID 입력");
        }

        public static void TransAccount(out int fromid, out int toid, out int money)
        {
            fromid = WbLib.getNumber("출금 계좌 ID 입력");
            toid = WbLib.getNumber("입금 계좌 ID 입력");
            money = WbLib.getNumber("입금금액 입력");
        }
    }
}
