using System;
using System.Data;
using System.Data.SqlClient;

namespace _0428_REST_DB_SERVER_
{
    public class WbDB
    {

        #region 싱글톤
        public static WbDB Instance { get; private set; }
        static WbDB()
        {
            Instance = new WbDB();
        }
        private WbDB() { }
        #endregion

        #region DB연결정보
        private const string server = "DESKTOP-M3GPKIV\\SQLEXPRESS";
        private const string database = "Member";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion

        private string connstring = null;

        private SqlConnection con = null;

        #region OPEN and Close

        public bool Open()
        {
            connstring = String.Format("Server={0};database={1};uid={2};pwd={3}", server, database, id, pw);
            return InnerOpen();
        }

        public bool Open(string sql)
        {
            connstring = String.Format(sql);
            return InnerOpen();
        }
        private bool InnerOpen()
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

        #region Query 메서드

        public void CommandNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            if (cmd.ExecuteNonQuery() < 1)  //영향을 받은 row의 갯수 
                throw new Exception("실패");
        }

        public string CommandQuery(string sql)
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

        public object CommandScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            return cmd.ExecuteScalar();
        }

        #endregion
    }
}