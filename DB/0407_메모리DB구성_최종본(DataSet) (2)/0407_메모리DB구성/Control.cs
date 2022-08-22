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

        public void InputMoney()
        {
            try
            {
                database.InputMoney();
                Console.WriteLine("입금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void  OutputMoney()
        {
            try
            {
                database.OutputMoney();
                Console.WriteLine("출금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccountDelete()
        {
            try
            {
                database.AccountDelete();
                Console.WriteLine("삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void Save()
        {
            try
            {
                database.Save();
                Console.WriteLine("저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Load()
        {
            try
            {
                database.Load();
                Console.WriteLine("불러오기 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion 
    
        public void Commit()
        {

            database.Commit();
            Console.WriteLine("Commit성공");
            
        }

        public void Rollback()
        {
            database.Rollback();
            Console.WriteLine("Rollback");
        }
    }
}
