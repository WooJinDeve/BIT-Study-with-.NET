using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0407_메모리DB구성
{
    internal class Program
    {
        private Control con = Control.Instance;

        public bool Init()
        {
            WbPrint.Logo();

            return true;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                con.PrintAll();
                ConsoleKey key = WbPrint.MenuPrint();
                Console.Write("\b");
                switch (key)
                {                    
                    case ConsoleKey.F1:     con.DesignTable();      break;
                    case ConsoleKey.F2:     con.TableSchemaPrint(); break;
                    case ConsoleKey.F3:     con.MemberAdd();        break;
                    case ConsoleKey.F4:     con.AccountAdd();       break;
                    case ConsoleKey.F5:     con.PrintAll();         break;
                    case ConsoleKey.F6:     con.InputMoney();       break;
                    case ConsoleKey.F7:     con.OutputMoney();      break;
                    case ConsoleKey.F8:     con.AccountDelete();    break;
                    case ConsoleKey.F9:     con.Save();             break;
                    case ConsoleKey.F10:    con.Load();             break;
                    case ConsoleKey.C:    con.Commit();           break;
                    case ConsoleKey.R:    con.Rollback();         break;
                    case ConsoleKey.Escape: return;
                }
                WbPrint.Pause();
            }
        }

        public void Exit()
        {
            WbPrint.Ending();
        }

        public static void Main()
        {
            Program pr = new Program();

            if (pr.Init() == true)
            {
                pr.Run();
            }

            pr.Exit();

            return;
        }

    }
}
