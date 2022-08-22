using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0407_메모리DB구성
{
    internal class WbDataBase
    {
        public DataTable Member { get; private set; }
        public DataTable Account { get; private set; }
        public DataTable Accountio { get; private set; }

        #region 테이블 디자인

        public void DesignTable()
        {
            try
            {
                Design_MemberTable();
                Design_AccountTable();
                Design_AccountIOTable();
                RelationTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("테이블 생성 오류");
            }
        }

        private void Design_MemberTable()
        {
            Member = new DataTable("Member");

            DataColumn dc_memid = new DataColumn("memid", typeof(int));
            dc_memid.AutoIncrement = true;
            dc_memid.AutoIncrementSeed = 100;
            dc_memid.AutoIncrementStep = 1;
            Member.Columns.Add(dc_memid);  
              
            DataColumn dc_name = new DataColumn("name", typeof(string));
            dc_name.AllowDBNull = false;
            Member.Columns.Add(dc_name);

            DataColumn dc_phone = new DataColumn("phone", typeof(string));
            dc_phone.AllowDBNull = false;
            Member.Columns.Add(dc_phone);

            DataColumn[] pkeys = new DataColumn[1];
            pkeys[0] = dc_memid;
            Member.PrimaryKey = pkeys;
        }

        private void Design_AccountTable()
        {
            Account = new DataTable("Account");

            DataColumn dc_accid = new DataColumn("accid", typeof(int));
            dc_accid.AutoIncrement = true;
            dc_accid.AutoIncrementSeed = 1000;
            dc_accid.AutoIncrementStep = 1;
            Account.Columns.Add(dc_accid);

            DataColumn dc_memid = new DataColumn("memid", typeof(int));
            dc_memid.AllowDBNull = false;
            Account.Columns.Add(dc_memid);

            DataColumn dc_balance = new DataColumn("balance", typeof(int));
            dc_balance.AllowDBNull = false;
            dc_balance.DefaultValue = 0;
            Account.Columns.Add(dc_balance);

            DataColumn dc_newtime = new DataColumn("newtime", typeof(DateTime));
            dc_newtime.AllowDBNull = false;
            Account.Columns.Add(dc_newtime);

            DataColumn[] pkeys = new DataColumn[1];
            pkeys[0] = dc_accid;
            Account.PrimaryKey = pkeys;
        }

        private void Design_AccountIOTable()
        {
            Accountio = new DataTable("Accountio");

            DataColumn dc_accio = new DataColumn("accio", typeof(int));
            dc_accio.AutoIncrement = true;
            dc_accio.AutoIncrementSeed = 1;
            dc_accio.AutoIncrementStep = 1;
            Accountio.Columns.Add(dc_accio);

            DataColumn dc_accid = new DataColumn("accid", typeof(int));
            dc_accid.AllowDBNull = false;
            Accountio.Columns.Add(dc_accid);

            DataColumn dc_input = new DataColumn("input", typeof(int));
            dc_input.AllowDBNull = false;
            Accountio.Columns.Add(dc_input);

            DataColumn dc_output = new DataColumn("output", typeof(int));
            dc_output.AllowDBNull = false;
            Accountio.Columns.Add(dc_output);

            DataColumn dc_balance = new DataColumn("balance", typeof(int));
            dc_balance.AllowDBNull = false;
            Accountio.Columns.Add(dc_balance);

            DataColumn dc_newtime = new DataColumn("newtime", typeof(DateTime));
            dc_newtime.AllowDBNull = false;
            Accountio.Columns.Add(dc_newtime);

            DataColumn[] pkeys = new DataColumn[1];
            pkeys[0] = dc_accio;
            Accountio.PrimaryKey = pkeys;
        }

        public void RelationTable()
        {
            //Account테이블의 memid 를 FK로 설정
            ForeignKeyConstraint AccountMemIDFK = 
                new ForeignKeyConstraint("AccountMemIDFK",
                    Member.Columns["memid"],
                    Account.Columns["memid"]);
            AccountMemIDFK.DeleteRule = Rule.None;

            Account.Constraints.Add(AccountMemIDFK);

            //AccountIO테이블의 accid 를 FK로 설정
            ForeignKeyConstraint AccountIOAccIDIDFK =
                new ForeignKeyConstraint("AccountIOAccIDIDFK",
                    Account.Columns["accid"],
                    Accountio.Columns["accid"]);
            AccountIOAccIDIDFK.DeleteRule = Rule.None;

            Accountio.Constraints.Add(AccountIOAccIDIDFK);            
        }
        
        #endregion

        #region 테이블 스키마 정보 출력

        public void TableSchemaPrint()
        {
            try
            {
                Console.WriteLine("---------------------------");
                TableSchemaPrint(Member);
                Console.WriteLine("---------------------------\n");
                TableSchemaPrint(Account);
                Console.WriteLine("---------------------------\n");
                TableSchemaPrint(Accountio);
                Console.WriteLine("---------------------------");

                PrintRelationTalbe();
            }
            catch(Exception )
            {
                throw new Exception("[F1]을 선택하여 테이블을 먼저 생성하세요");
            }
        }

        private void TableSchemaPrint(DataTable dt)
        {
            Console.WriteLine("테이블 명: {0}", dt.TableName);
            Console.WriteLine("컬럼 개수: {0}", dt.Columns.Count);
            foreach (DataColumn dc in dt.Columns)
            {
                Console.Write("{0}:{1}\t", dc.ColumnName, dc.DataType);
            }
            Console.WriteLine("");
        }


        private void PrintRelationTalbe()
        {
            Console.WriteLine("[Account] 제약조건 ");
            ConstraintCollection con1 = Account.Constraints;
            foreach (Constraint c in con1)
            {
                Console.Write(c.ConstraintName + "\t");
            }

            Console.WriteLine("\n\n[Accountio] 제약조건 ");
            ConstraintCollection con = Accountio.Constraints;
            foreach(Constraint c in con)
            {
                Console.Write(c.ConstraintName + "\t");
            }
            Console.WriteLine();
        }

        #endregion

        #region 데이터 저장
        public void MemberAdd()
        {
            MemberAdd("홍길동", "010-1111-1111");
            MemberAdd("이길동", "010-2222-2222");
            MemberAdd("김길동", "010-3333-3333");
            MemberAdd("허길동", "010-4444-4444");
        }

        private void MemberAdd(string name, string phone)
        {
            try
            {
                DataRow dr = Member.NewRow();
                dr["name"] = name;
                dr["phone"] = phone;
                Member.Rows.Add(dr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Member 저장 실패");
            }
        }
        
        public void AccountAdd()
        {
            string name = WbLib.getString("이름");
            int memid = NameToId(name);
            if (memid == -1)
                throw new Exception("없는 이름입니다.");

            int balance = WbLib.getNumber("입금액");

            int accid = AccountAdd(memid, balance);

            AccountioAdd(accid, balance, 0, balance);
            //AccountioAdd(LastAccId(), balance, 0, balance);
        }

        private int LastAccId()
        {
            int accid = int.Parse(Account.Rows[Account.Rows.Count - 1]["accid"].ToString());
            return accid;
        }

        private int NameToId(string name)
        {
            foreach(DataRow row in Member.Rows)
            {
                if (row["name"].ToString() == name)
                    return int.Parse(row["memid"].ToString());
            }
            return -1;
        }

        private int AccountAdd(int memid, int balance)
        {
            try
            {
                DataRow dr = Account.NewRow();
                dr["memid"] = memid;
                dr["balance"] = balance;
                dr["newtime"] = DateTime.Now;
                Account.Rows.Add(dr);

                return int.Parse(dr["accid"].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Member 저장 실패");
            }
        }

        private void AccountioAdd(int accid, int input, int output,  int balance)
        {
            try
            {
                DataRow dr = Accountio.NewRow();
                dr["accid"] = accid;
                dr["input"] = input;
                dr["output"] = output;
                dr["balance"] = balance;
                dr["newtime"] = DateTime.Now;
                Accountio.Rows.Add(dr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Member 저장 실패");
            }
        }

        #endregion

        #region 저장정보 출력

        public void PrintAll()
        {
            Console.WriteLine("[Member]");
            PrintAll(Member);
            Console.WriteLine("------------------------------------------\n");

            Console.WriteLine("[Account]");
            PrintAll(Account);
            Console.WriteLine("------------------------------------------\n");

            Console.WriteLine("[Accountio]");
            PrintAll(Accountio);
            Console.WriteLine("------------------------------------------\n");
        }

        private void PrintAll(DataTable dt)
        {
            Console.WriteLine("저장개수 : {0}", dt.Rows.Count);
            
            foreach(DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Console.Write("{0}\t", row[i]);
                }
                Console.WriteLine();
            }
        }

        #endregion 
    }
}
