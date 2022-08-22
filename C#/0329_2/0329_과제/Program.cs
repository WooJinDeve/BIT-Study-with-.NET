using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제
{
    class Program
    {
		private Control control = Control.Singleton;

		public void Init()
        {
			WbPrint.Logo();
			control.FileLoad();

			control.InitData();
		}

		public void Run()
		{

			while (true)
			{
				Console.Clear();

				control.SelectAll();
				WbPrint.MenuPrint();
				Console.Write("선택 : ");
				int ch = int.Parse(Console.ReadLine());
				
				switch (ch)
				{
					case 1: control.Insert(); break;
					case 2: control.Update1(); break;
					case 3: control.Update2(); break;
					case 4: control.Select();	break;
					case 5: control.Delete(); break;
					case 6: return;
					default:
						Console.WriteLine("잘못된 메뉴 입력 ");
						break;
				}

				WbPrint.Pause();
			}
		}

		public void Exit()
		{
			control.FileSave();
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
