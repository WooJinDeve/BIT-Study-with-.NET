using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0407_메모리DB구성
{    
    class Control
    {
        #region 싱글톤

        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();   
        }
        private Control() { }

        #endregion n

        private WbDataBase database = new WbDataBase();

        #region Program-Run 호출 기능

        public void DesignTable()
        {
            try
            {
                database.DesignTable();
                Console.WriteLine("테이블 생성 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void TableSchemaPrint()
        {
            try
            {
                database.TableSchemaPrint();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        public void MemberAdd()
        {
            try
            {
                database.MemberAdd();
                Console.WriteLine("저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccountAdd()
        {
            try
            {
                database.AccountAdd();
                Console.WriteLine("저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintAll()
        {
            try
            {
                database.PrintAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion 
    }
}
