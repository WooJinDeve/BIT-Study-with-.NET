using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB
{
    internal class WbDB
    {
        const string connstring = @"Server=DESKTOP-0I86BTV\SQLEXPRESS;database=Sample;uid=wb35;pwd=1234";

        private SqlConnection con = null;

        #region Open + Close
        public bool Open()
        {
            try
            {
                con = new SqlConnection(connstring);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                return true;
            }
            return false;            
        }
        #endregion

        #region insert + update + delete
        public bool Product_InsertCommand(string pname, int price, string description)
        {
            string sql = string.Format("insert into Product values('{0}', {1},'{2}');",pname,price,description);

            return CommandNonQuery(sql);
        }

        //update Product set price = 500, Description = '설명문을 수정합니다' where PNAME = '환타';
        public bool Product_UpdateCommand(string find_name, int up_price, string ip_des)
        {
            string sql = string.Format("update Product set price = {0}, Description = '{1}' where PNAME = '{2}';"
                , up_price, ip_des, find_name);

            return CommandNonQuery(sql);
        }


        public bool Product_DeleteCommand(string del_name)
        {
            string sql = string.Format(" delete from Product where pname = '{0}';", del_name);
            return CommandNonQuery(sql);
        }

        private bool CommandNonQuery(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                if (cmd.ExecuteNonQuery() < 1) // 영향을 받은  row의 갯수
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Select
        public void SelectAll()
        {
            string sql = "select* from Product;";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader reader = com.ExecuteReader();
            //ROW데이터 정보
            while (reader.Read())
            {
                Console.WriteLine("{0}\t {1}\t {2}\t {3}",
                    //      reader[0], reader[1], reader[2], reader[3]);
                    reader["PID"], reader["PNAME"], reader["PRICE"], reader["DESCRIPTION"]);
            }

            //추가 정보
            Console.WriteLine("FieldCount(컬럼의 개수) : " + reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine(reader.GetName(i));  //컬럼명 출력
            }

            reader.Close();
        }

        public void SelectAll1()
        {
            string sql = "select* from Product;";
            string msg = CommandQuery(sql);

            string[] f1 = msg.Split('$');
            for(int i =0; i<f1.Length-1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("{0}, {1}, {2}, {3}", f2[0], f2[1], f2[2], f2[3]);
            }
            //Console.WriteLine(msg);
        }

        //select PNAME, PRICE from Product where pid = 1000;
        public void FindProDuct(int pid)
        {
            string sql = string.Format("select PNAME, PRICE from Product where pid = {0}",pid);
            string msg = CommandQuery(sql);

            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("{0}, {1}", f2[0], f2[1]);
            }
        }

        private string CommandQuery(string sql)
        {
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader reader = com.ExecuteReader();
            string msg = "";
            //ROW데이터 정보
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    msg += reader[i] + "#";
                }
                msg += "$";
            }

            reader.Close();
            return msg;
        }
        #endregion

        #region Select -하나의 값을 얻어 올 때
        ///select count(*) from product;
        public void SelectCount()
        {
            string sql = string.Format("select count(*) from product;");
            int count = (int)CommandScalar(sql);

            Console.WriteLine("Product의 데이터 개수 : {0}", count);
        }

        public void PNameToPrice(string pname)
        {
            try
            {
                string sql = string.Format("select price from product where pname = '{0}';", pname);
                int price = (int)CommandScalar(sql);

                Console.WriteLine("{0}의 가격 : {1}", pname, price);
            }
            catch(Exception ex)
            {
                Console.WriteLine("없다\t"+ex.Message);
            }
        }

        private object CommandScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            return cmd.ExecuteScalar();      
        }
        #endregion

    }
}
