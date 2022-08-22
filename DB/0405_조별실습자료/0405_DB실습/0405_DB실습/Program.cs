using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal class Program
    {
        private Control con = Control.Instance;

        public bool Init()
        {
            if (con.DB_Open() == false)
                return false;   
            WbPrint.Logo();

            return true;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKey key = WbPrint.MenuPrint();
                Console.Write("\b");
                switch (key)
                {                    
                    case ConsoleKey.F1:     con.Insert();           break;
                    case ConsoleKey.F2:     con.Delete();           break;
                    case ConsoleKey.F3:     con.CustomTotalCount(); break;
                    case ConsoleKey.F4:     con.TotalPrice();       break;
                    case ConsoleKey.F5:     con.TablePrice();       break;
                    case ConsoleKey.Escape: return;
                }
                WbPrint.Pause();
            }
        }

        public void Exit()
        {
            con.DB_Close();
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
