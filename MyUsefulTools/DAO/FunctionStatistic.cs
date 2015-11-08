using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DatabaseAccess;
using MySpace.Utils;


namespace MyUsefulTools.DAO
{
    class FunctionStatistic
    {
        private string functionName = null;

        private string className = null;

        private string methodName = null;

        private short parameterCount = -1;

        private int useCount = -1;

        private bool isValid = false;

        private bool isRecord = false;

        public string FunctionName
        {
            get { return functionName; }
            set { functionName = value; }
        }

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        public short ParameterCount
        {
            get { return parameterCount; }
            set { parameterCount = value; }
        }

        public int UseCount
        {
            get { return useCount; }
            set { useCount = value; }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByName(string _functionName)
        {
            string sqlstr = "select * from [FunctionStatistic] where FunctionName=@functionname;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@functionname", SqlDbType.NVarChar, 20);
            paras[0].Value = _functionName;

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);

            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                functionName = dr["FunctionName"].ToString().Trim();

                className = dr["ClassName"].ToString().Trim();

                methodName = dr["MethodName"].ToString().Trim();

                parameterCount = Convert.ToInt16(dr["ParameterCount"]);

                useCount = Convert.ToInt32(dr["UseCount"]);

                isValid = Convert.ToBoolean(dr["IsValid"]);

                this.isRecord = true;
            }
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [FunctionStatistic] values(@functionname,@classname,@methodname,@parametercount,@usecount,@isvalid);";

            SqlParameter[] paras = new SqlParameter[6];
            paras[0] = new SqlParameter("@functionname", SqlDbType.NVarChar, 20);
            paras[0].Value = functionName;

            paras[1] = new SqlParameter("@classname", SqlDbType.VarChar, 200);
            paras[1].Value = className;

            paras[2] = new SqlParameter("@methodname", SqlDbType.VarChar, 50);
            paras[2].Value = methodName;

            paras[3] = new SqlParameter("@parametercount", SqlDbType.SmallInt, 2);
            paras[3].Value = parameterCount;

            paras[4] = new SqlParameter("@usecount", SqlDbType.Int, 4);
            paras[4].Value = useCount;

            paras[5] = new SqlParameter("@isvalid", SqlDbType.Bit, 1);
            paras[5].Value = isValid;

            DBManager.InsertRecord(sqlstr, paras);

            isRecord = true;
        }
    }
}
