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
        public DataSet ds = new DataSet("BankDataSet");        

        public DataTable Member { get; private set; }
        public DataTable Account { get; private set; }
        public DataTable Accountio { get; private set; }

        private const string schema_fdataset = "bank.xsd";
        private const string fdataset = "bank.xml";

        private const string schema_fmembers = "members.xsd";
        private const string fmembers = "members.xml";

        private const string schema_faccounts = "accounts.xsd";
        private const string faccounts = "accounts.xml";

        private const string schema_faccountios = "accountios.xsd";
        private const string faccountios = "accountios.xml";

        #region 테이블 디자인 & DataSet 생성

        public void DesignTable()
        {
            try
            {
                Design_MemberTable();
                Design_AccountTable();
                Design_AccountIOTable();
                Design_DataSet();
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

        public void Design_DataSet()
        {
            ds.Tables.Add(Member);
            ds.Tables.Add(Account);
            ds.Tables.Add(Accountio);

            DataColumn dc_member_mid = Member.Columns["memid"];
            DataColumn dc_account_mid = Account.Columns["memid"];
            DataRelation dr = new DataRelation("MemberAccount", dc_member_mid, dc_account_mid);
            ds.Relations.Add(dr);

            DataColumn dc_account_accid = Account.Columns["accid"];
            DataColumn dc_Accountio_accid = Accountio.Columns["accid"];
            DataRelation dr1 = new DataRelation("AccountAccountIO", dc_account_accid, dc_Accountio_accid);
            ds.Relations.Add(dr1);
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

                PrintRelationTalbe(ds.Relations);
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


        private void PrintRelationTalbe(DataRelationCollection drc)
        {
            Console.WriteLine("관계 수:{0}", drc.Count);
            foreach (DataRelation dr in drc)
            {
                Console.WriteLine("관계명:{0}", dr.RelationName);
                DataColumn dc_parent = dr.ParentColumns[0];
                DataColumn dc_child = dr.ChildColumns[0];
                Console.WriteLine("부모:{0}-{1}", dc_parent.Table.TableName, dc_parent.ColumnName);
                Console.WriteLine("자식:{0}-{1}", dc_child.Table.TableName, dc_child.ColumnName);
            }
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
            PrintAll(ds.Tables[0]);
            Console.WriteLine("------------------------------------------\n");

            Console.WriteLine("[Account]");
            PrintAll(ds.Tables[1]);
            Console.WriteLine("------------------------------------------\n");

            Console.WriteLine("[Accountio]");
            PrintAll(ds.Tables[2]);
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

        #region 계좌 입출금
        public void InputMoney()
        {
            try
            {
                int accid = WbLib.getNumber("계좌번호");
                int money = WbLib.getNumber("입금액");

                int balance = InputMoney(accid, money);
                UpdateAccountIO(accid, money, 0, balance);
                Account.AcceptChanges();
                Accountio.AcceptChanges();
            }
            catch(Exception)
            {
                Account.RejectChanges();
                Accountio.RejectChanges();
            }
        }

        private int InputMoney(int accid, int money)
        {
            DataRow dr = Account.Rows.Find(accid);
            if (dr == null)
                throw new Exception("없는 계좌번호 입니다.");

            dr["balance"] = int.Parse(dr["balance"].ToString()) + money;

            return int.Parse(dr["balance"].ToString());
        }

        private void UpdateAccountIO(int accid, int input, int output, int balance)
        {
            DataRow row = Accountio.NewRow();

            row["accid"] = accid;
            row["input"] = input;
            row["output"] = output;
            row["balance"] = balance;
            row["newtime"] = DateTime.Now;

            Accountio.Rows.Add(row);
        }

        public void OutputMoney()
        {
            int accid = WbLib.getNumber("계좌번호");
            int money = WbLib.getNumber("출금액");

            int balance = OutputMoney(accid, money);
            UpdateAccountIO(accid, 0, money, balance);
        }

        private int OutputMoney(int accid, int money)
        {
            DataRow dr = Account.Rows.Find(accid);
            if (dr == null)
                throw new Exception("없는 계좌번호 입니다.");

            dr["balance"] = int.Parse(dr["balance"].ToString()) - money;

            return int.Parse(dr["balance"].ToString());
        }
        #endregion

        #region 계좌 삭제

        public void AccountDelete()
        {
            int accid = WbLib.getNumber("계좌번호");

            AccoutIODelete(accid);
            AccountDelete(accid);
        }

        private void AccoutIODelete(int accid)
        {           
            foreach(DataRow dr in Accountio.Rows)
            {
                if( int.Parse(dr["accid"].ToString()) == accid)
                    Accountio.Rows.Remove(dr);
            }
        }

        private void AccountDelete(int accid)
        {
            DataRow row = Account.Rows.Find(accid);
            Account.Rows.Remove(row);
        }


        #endregion

        #region XML 저장 및 불러오기

        public void Save()
        {
            ds.WriteXmlSchema(schema_fdataset);
            ds.WriteXml(fdataset);

            //Member.WriteXmlSchema(schema_fmembers, true);
            //Member.WriteXml(fmembers, true);

            //Account.WriteXmlSchema(schema_faccounts, true);
            //Account.WriteXml(faccounts, true);

            //Accountio.WriteXmlSchema(schema_faccountios, true);
            //Accountio.WriteXml(faccountios, true);
        }

        public void Load()
        {
            try
            {
                ds.ReadXmlSchema(schema_fdataset);
                ds.ReadXml(fdataset);
                //Member = new DataTable();
                //Account = new DataTable();
                //Accountio = new DataTable();

                //Member.ReadXmlSchema(schema_fmembers);
                //Member.ReadXml(fmembers);

                //Account.ReadXmlSchema(schema_faccounts);
                //Account.ReadXml(faccounts);

                //Accountio.ReadXmlSchema(schema_faccountios);
                //Accountio.ReadXml(faccountios);
            }
            catch(Exception)
            {
            }
        }

        #endregion

        public void Commit()
        {
            //기능이 완료될때마다 호출필요
            Member.AcceptChanges();

        }

        public void Rollback()
        {
            Member.RejectChanges();
        }
    }
}
