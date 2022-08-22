using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{
    enum Menu { PRINTALL = 0, INSERT, SELECT,UPDATE,DELETE, DELETEALL,SORT,EXIT }
    class Program
    {
        private Control con = new Control();

        public void Init()
        {
            WbPrint.Logo();
            con.FileLoad();
        }

        public void Run()
        {     
            while(true)
            {
                Console.Clear();
                //메뉴 출력
                WbPrint.MenuPrint();
                
                switch((Menu)WbLib.GetNumber(">> "))
                {
                    case Menu.PRINTALL: con.SelectAll(); break;
                    case Menu.INSERT: con.Insert(WbLib.GetLibrary("책 정보 입력"));  break;
                    case Menu.SELECT: con.Select(WbLib.GetString("책 제목 입력 : ")); break;
                    case Menu.UPDATE: con.Update(WbLib.GetString("기존 책 제목 입력 : "), WbLib.GetString("수정 책 제목 입력 : "));  break;
                    case Menu.DELETE: con.Delete(WbLib.GetString("책 제목 입력 : ")); break;
                    case Menu.DELETEALL: con.DeleteAll(); break;
                    case Menu.SORT: con.Sort();  break;
                    case Menu.EXIT:  return;
                }
                WbPrint.Pause();
            }
        }
        public void Exit()
        {

            con.FileSave();
            WbPrint.Ending();
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.Init();
            pr.Run();
            pr.Exit();
        }
    }
}
