using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace _0420_Excel
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WbExcel excel = new WbExcel();
            //excel.ExcelLoad();
            excel.ExcelToSeatChanged("정우진",12,12);
        }
    }
}
