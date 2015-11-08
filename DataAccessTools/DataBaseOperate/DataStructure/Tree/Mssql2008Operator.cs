using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseAccess;
using System.Data.SqlClient;
using System.Data;
using StoneUtils.Interface;

namespace DataBaseOperate.DataStructure.Tree
{
    /// <summary>
    /// Sql Server 2008对于树的一系列操作方法
    /// </summary>
    public class Mssql2008Operator
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 索引数据库名称，存放树形结构的表名
        /// </summary>
        private string treeTableName;
        private DBCommonManager dBManager;
        
        public Mssql2008Operator(string _connectionString, string _treeTableName)
        {
            connectionString = _connectionString;
            treeTableName = _treeTableName;
            dBManager = new DBCommonManager(connectionString);
        }
        /// <summary>
        /// 创建默认的表
        /// </summary>
        public void CreateDefaultTable()
        {
            string sqlstr = @"
declare @sql varchar(5000)

set @sql = 
'
CREATE TABLE '+ @tablename +'(
	[NodeID] [int] IDENTITY(1,1) NOT NULL,
	[FatherNodeID] [int] NOT NULL,
	[DataID] [int] NOT NULL,
	CONSTRAINT [PK_'+ @tablename +'] PRIMARY KEY CLUSTERED(
		[NodeID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
ALTER TABLE '+ @tablename +' WITH CHECK ADD CONSTRAINT [FK_'+ @tablename +'_'+ @tablename +'] FOREIGN KEY([FatherNodeID]) REFERENCES '+ @tablename +' ([NodeID])
';

EXEC(@sql);
";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@tablename", SqlDbType.VarChar, 100);
            paras[0].Value = treeTableName;
            dBManager.ExeSQLStatement(sqlstr, paras);
        }
        /// <summary>
        /// 添加新的数据记录
        /// </summary>
        /// <param name="_fatherNodeID">父节点ID</param>
        /// <param name="_nodeData">关联的数据对象</param>
        /// <returns>新添加的记录ID</returns>
        public int AddNode(int _fatherNodeID, ITreeNodeData _nodeData)
        {
            //树表中插入一条新记录
            string sqlstr = string.Format("insert into {0} values(@fathernodeid, @dataid);", treeTableName);
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@fathernodeid", SqlDbType.Int);
            paras[0].Value = _fatherNodeID;
            paras[1] = new SqlParameter("@dataid", SqlDbType.Int);
            paras[1].Value = _nodeData.ID;
            dBManager.InsertRecord(sqlstr, paras);
            //获取新添加记录的ID
            sqlstr = string.Format("select NodeID from {0} where FatherNodeID=@fathernodeid and DataID=@dataid;", treeTableName);
            DataTable dt = dBManager.SelectRecords(sqlstr, paras);
            int newid = (int)dt.Rows[0][0];
            return newid;
        }

        public DataTable GetAllRecord()
        { 
            string sqlstr = "select * from {0};";
            sqlstr = string.Format(sqlstr, treeTableName);
            DataTable dt = dBManager.SelectRecords(sqlstr, null);
            return dt;
        }
    }
}
