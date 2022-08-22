using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0414_Server
{
    class Program
    {      
        private Control con = Control.Instance;

        public bool Init()
        {
            return con.Init();
        }

        public void Run()
        {
            WbLib.Pause();
        }

        public void Exit()
        {
            con.Exit();
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            if (program.Init() == true)
                program.Run();            
            program.Exit();
        }
    }
}
