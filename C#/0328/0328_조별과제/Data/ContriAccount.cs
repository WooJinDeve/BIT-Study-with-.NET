using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{
    class ContriAccount : Account
    {
        public int Contribution { get; private set; }

        public ContriAccount(int id, string name, int balance)
            :base(id, (int)(balance - balance * 0.01), name)
        {
            Contribution = (int)(balance * 0.01);
        }

        public override void AddMoney(int val)
        {
            AddMoney((int)(val - val * 0.01));
            Contribution += (int)(val * 0.01);
        }

        public override void ShowAllData()
        {
            base.ShowAllData();
            Console.Write("총 기부액 : " + Contribution);
        }
    }
}
