using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DatabaseAccess;
using System.Data.SqlClient;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DataBase
{
    /// <summary>
    /// 数据库表中的列
    /// </summary>
    public class Column
    {
        public static string DataType_Char = "char";
        
        private string _colName = ""; //字段名
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColName
        {
            set { _colName = value; }
            get { return _colName; }
        }

        private string _tableName = "";//所属表名
        /// <summary>
        /// 所属表名
        /// </summary>
        public string TableName
        {
            set { _tableName = value; }
            get { return _tableName; }
        }

        private string _dataType = ""; //字段类型
        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType
        {
            set { _dataType = value; }
            get { return _dataType; }
        }

        private int _dataSize = 0; //字段数据长度，单位字节
        /// <summary>
        /// 字段数据长度，单位字节
        /// </summary>
        public int DataSize
        {
            set { _dataSize = value; }
            get { return _dataSize; }
        }

        private bool _isPrime = false;//主码
        /// <summary>
        /// 主码
        /// </summary>
        public bool IsPrime
        {
            set { _isPrime = value; }
            get { return _isPrime; }
        }

        //构造方法
        public Column(string colname, string tablename, string dataType, int dataSize, bool isprime)
        {
            _colName = colname;
            _tableName = tablename;
            _dataType = dataType;
            _dataSize = dataSize;
            _isPrime = isprime;
        }

        public string GetTestString()
        {
            string teststr = ColName + "\t" + TableName + "\t" + DataType + "\t" + DataSize.ToString() + "\t" + IsPrime.ToString();
            return teststr;
        }
        /// <summary>
        /// 用来测试：读取数据库表中所有的列信息
        /// </summary>
        public static void Test001()
        {
            string sqlstr = @"
                --创建临时表，记录指定表的所有外键信息
                select * into #TableFK from
                (
                select TObject.name TableName,TObject.id TableID, FKObject.name FKName, FKObject.id FKID
                from sysobjects TObject, sysobjects FKObject 
                where TObject.id=FKObject.parent_obj and FKObject.xtype='F' and TObject.name=@table
                )as T1;

                select * from #TableFK;

                DROP Table #TableFK";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@table", SqlDbType.Char, 100);
            paras[0].Value = "MyUser";

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Rows[i][j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }
        public static Column[] GetTableColumns(string tablename)
        {
            string sqlstr = @"
                --找一个表的所有列相关信息
                select thecol.name ColName, thetable.name TableName, thetype.name ColType, thecol.length ColLength,
	                   COLUMNPROPERTY(thecol.id, thecol.name, 'IsIdentity') IsIdentity,
	                   CASE WHEN EXISTS
                          (SELECT 1
                          FROM dbo.sysindexes si INNER JOIN
                               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
                               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
                               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
                          WHERE sc.id = thecol.id AND sc.colid = thecol.colid) THEN 'y' ELSE 'n' END AS IsPrime
                from syscolumns thecol inner join
	                 sysobjects thetable on thecol.id=thetable.id left outer join
	                 systypes thetype on thecol.xusertype=thetype.xusertype
                where thetable.type='U' and thetable.name=@tablename
                ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@tablename", SqlDbType.Char, 128);
            paras[0].Value = tablename;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            Column[] columns = new Column[dt.Rows.Count];
            for (int i = 0; i < columns.Length; i++)
            {
                string colname = dt.Rows[i]["ColName"].ToString();
                string tablename2 = dt.Rows[i]["TableName"].ToString();
                string coltype = dt.Rows[i]["ColType"].ToString();
                int collength = Int32.Parse(dt.Rows[i]["ColLength"].ToString());
                Column newcol = new Column(colname, tablename2, coltype, collength, dt.Rows[i]["IsPrime"].ToString().Equals("y"));
                columns[i] = newcol;
            }
            return columns;
        }
    }
}
