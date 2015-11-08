using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace DatabaseAccess
{
    public class DBManager
    {
        private static string ConnectString = "server=.;Integrated Security=False;uid=sa;pwd=zhanglei;database=MyUsefulTools";

        public DBManager()
        {
        }

        //插入记录
        public static bool InsertRecord(String cmdText, SqlParameter[] sqlPara)
        {
            bool result = true;
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            if (sqlPara != null) cmd.Parameters.AddRange(sqlPara);//添加参数
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();//清空参数集合，为了使参数变量可以用在别的地方
            con.Close();
            return result;
        }

        //查询记录
        public static DataTable SelectRecords(String cmdText, SqlParameter[] sqlPara)
        {
            DataTable dt = new DataTable();
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            if(sqlPara != null) cmd.Parameters.AddRange(sqlPara);//添加参数
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            cmd.Parameters.Clear();//清空参数集合，为了使参数变量可以用在别的地方
            con.Close();
            return dt;
        }

        //删除记录
        public static bool DeleteRecords(String cmdText, SqlParameter[] sqlPara)
        {
            bool result = true;
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            if (sqlPara != null) cmd.Parameters.AddRange(sqlPara);//添加参数
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();//清空参数集合，为了使参数变量可以用在别的地方
            con.Close();
            return result;
        }

        //更新记录
        /// <summary>
        /// 返回更新的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="sqlPara"></param>
        /// <returns></returns>
        public static int UpdateRecords(String cmdText, SqlParameter[] sqlPara)
        {
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            if (sqlPara != null) cmd.Parameters.AddRange(sqlPara);//添加参数
            int number = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();//清空参数集合，为了使参数变量可以用在别的地方
            con.Close();
            return number;
        }

        // 执行存储过程返回一个表。
        // paraValues: 参数值列表。
        // return: DataTable
        public static DataTable ExecStorProcToDateTable(string _storeProcName, SqlParameter[] sqlPara)
        {
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand(_storeProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlPara != null) cmd.Parameters.AddRange(sqlPara);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return dt;
        }

        /// <summary>
        /// 执行SQL语句，不返回结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="sqlPara"></param>
        public static void ExeSQLStatement(string cmdText, SqlParameter[] sqlPara)
        {
            string connString = ConnectString;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdText;
            if (sqlPara != null) cmd.Parameters.AddRange(sqlPara);//添加参数
            int number = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();//清空参数集合，为了使参数变量可以用在别的地方
            con.Close();
        }
    }
}