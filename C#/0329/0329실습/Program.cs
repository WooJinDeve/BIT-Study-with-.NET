using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329실습
{
    enum Menu { NONE, INSERT,
        SELECT, UPDATE, DELETE, CLEAR, SORT, EXIT }

    class Program
    {
        private Control con = new Control();
    
        public bool Init()
        {
            WbPrint.Logo();
            con.FileLoad();
            return true;
        }
        
        public void Run()
        {          
            while(true)
            {
                Console.Clear();
                con.SelectAll();
                WbPrint.MenuPrint();
                switch ((Menu)WbLib.GetNumber("선택"))
                {                    
                    case Menu.INSERT:   con.Insert();       break;
                    case Menu.SELECT:   con.Select();       break;
                    case Menu.UPDATE:   con.Update();       break;
                    case Menu.DELETE:   con.Delete();       break;
                    case Menu.CLEAR:    con.DeleteAll();    break;
                    case Menu.SORT:     con.Sort();         break;
                    case Menu.EXIT:     return;
                }
                WbPrint.Pause();
            }
        }
        
        public void Exit()
        {            
            con.FileSave();
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
