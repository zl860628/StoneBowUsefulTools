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
    public class BOINCTaskRecord
    {
        public enum ConstructorMode
        {
            WuId
        }

        private int iD = -1;

        private string projectName = null;

        private string taskName = null;

        private string wuID = "";

        private string computerID = null;

        private DateTime sentTime = Constant.DateTime_MinValue;

        private DateTime reportedTime = Constant.DateTime_MinValue;

        private float runTime = 0.0F;

        private float cPUTime = 0.0F;

        private float credit = 0.0F;

        private string application = "";

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        public string WuID
        {
            get { return wuID; }
            set { wuID = value; }
        }

        public string ComputerID
        {
            get { return computerID; }
            set { computerID = value; }
        }

        public DateTime SentTime
        {
            get { return sentTime; }
            set { sentTime = value; }
        }

        public DateTime ReportedTime
        {
            get { return reportedTime; }
            set { reportedTime = value; }
        }

        public float RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }

        public float CPUTime
        {
            get { return cPUTime; }
            set { cPUTime = value; }
        }

        public float Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public string Application
        {
            get { return application; }
            set { application = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _iD)
        {
            string sqlstr = "select * from [BOINCTaskRecord] where ID=@id;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            paras[0].Value = _iD;

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);

            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                iD = Convert.ToInt32(dr["ID"]);

                projectName = dr["ProjectName"].ToString().Trim();

                taskName = dr["TaskName"].ToString().Trim();

                wuID = dr["WuID"].ToString().Trim();

                computerID = dr["ComputerID"].ToString().Trim();

                sentTime = DateTime.Parse(dr["SentTime"].ToString().Trim());

                reportedTime = DateTime.Parse(dr["ReportedTime"].ToString().Trim());

                runTime = (float)Convert.ToDouble(dr["RunTime"]);

                cPUTime = (float)Convert.ToDouble(dr["CPUTime"]);

                credit = (float)Convert.ToDouble(dr["Credit"]);

                application = dr["Application"].ToString().Trim();

                this.isRecord = true;
            }
        }

        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByTask(string _projectName, string _taskName)
        {
            string sqlstr = "select * from [BOINCTaskRecord] where ProjectName=@projectname and TaskName=@taskname;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@projectname", SqlDbType.VarChar, 100);
            paras[0].Value = _projectName;
            paras[1] = new SqlParameter("@taskname", SqlDbType.VarChar, 100);
            paras[1].Value = _taskName;

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);

            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                iD = Convert.ToInt32(dr["ID"]);

                projectName = dr["ProjectName"].ToString().Trim();

                taskName = dr["TaskName"].ToString().Trim();

                wuID = dr["WuID"].ToString().Trim();

                computerID = dr["ComputerID"].ToString().Trim();

                sentTime = DateTime.Parse(dr["SentTime"].ToString().Trim());

                reportedTime = DateTime.Parse(dr["ReportedTime"].ToString().Trim());

                runTime = (float)Convert.ToDouble(dr["RunTime"]);

                cPUTime = (float)Convert.ToDouble(dr["CPUTime"]);

                credit = (float)Convert.ToDouble(dr["Credit"]);

                application = dr["Application"].ToString().Trim();

                this.isRecord = true;
            }
        }
        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByWUId(string _projectName, string _wuId)
        {
            string sqlstr = "select * from [BOINCTaskRecord] where ProjectName=@projectname and WuID=@wuid;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@projectname", SqlDbType.VarChar, 100);
            paras[0].Value = _projectName;
            paras[1] = new SqlParameter("@wuid", SqlDbType.VarChar, 40);
            paras[1].Value = _wuId;

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);

            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                iD = Convert.ToInt32(dr["ID"]);

                projectName = dr["ProjectName"].ToString().Trim();

                taskName = dr["TaskName"].ToString().Trim();

                wuID = dr["WuID"].ToString().Trim();

                computerID = dr["ComputerID"].ToString().Trim();

                sentTime = DateTime.Parse(dr["SentTime"].ToString().Trim());

                reportedTime = DateTime.Parse(dr["ReportedTime"].ToString().Trim());

                runTime = (float)Convert.ToDouble(dr["RunTime"]);

                cPUTime = (float)Convert.ToDouble(dr["CPUTime"]);

                credit = (float)Convert.ToDouble(dr["Credit"]);

                application = dr["Application"].ToString().Trim();

                this.isRecord = true;
            }
        }
        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [BOINCTaskRecord] values(@projectname,@taskname,@wuid,@computerid,@senttime,@reportedtime,@runtime,@cputime,@credit,@application);";

            SqlParameter[] paras = new SqlParameter[10];
            paras[0] = new SqlParameter("@projectname", SqlDbType.VarChar, 100);
            paras[0].Value = projectName;

            paras[1] = new SqlParameter("@taskname", SqlDbType.VarChar, 100);
            paras[1].Value = taskName;

            paras[2] = new SqlParameter("@wuid", SqlDbType.VarChar, 40);

            if (wuID == null) paras[2].Value = DBNull.Value;
            else paras[2].Value = wuID;

            paras[3] = new SqlParameter("@computerid", SqlDbType.VarChar, 40);
            paras[3].Value = computerID;

            paras[4] = new SqlParameter("@senttime", SqlDbType.DateTime, 8);
            paras[4].Value = sentTime;

            paras[5] = new SqlParameter("@reportedtime", SqlDbType.DateTime, 8);
            paras[5].Value = reportedTime;

            paras[6] = new SqlParameter("@runtime", SqlDbType.Real, 4);
            paras[6].Value = runTime;

            paras[7] = new SqlParameter("@cputime", SqlDbType.Real, 4);
            paras[7].Value = cPUTime;

            paras[8] = new SqlParameter("@credit", SqlDbType.Real, 4);
            paras[8].Value = credit;

            paras[9] = new SqlParameter("@application", SqlDbType.VarChar, 100);

            if (application == null) paras[9].Value = DBNull.Value;
            else paras[9].Value = application;

            DBManager.InsertRecord(sqlstr, paras);
            sqlstr = "select ID from [BOINCTaskRecord] where ProjectName=@projectname and TaskName=@taskname and WuID=@wuid and ComputerID=@computerid and SentTime=@senttime and ReportedTime=@reportedtime and RunTime=@runtime and CPUTime=@cputime and Credit=@credit and Application=@application;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;

        }

        #region 自定义方法
        //构造方法
        public BOINCTaskRecord(int _id)
        {
            GetPropertiesByID(_id);
        }
        public BOINCTaskRecord(string _projectName, string _taskName)
        {
            GetPropertiesByTask(_projectName, _taskName);
        }
        public BOINCTaskRecord(string _projectName, string _wuId, ConstructorMode _mode)
        {
            switch (_mode)
            {
                case ConstructorMode.WuId: GetPropertiesByWUId(_projectName, _wuId); break;
            }
        }
        /// <summary>
        /// 使用数据行信息创建对象
        /// </summary>
        /// <param name="_datarow"></param>
        public BOINCTaskRecord(DataRow _datarow)
        {
            GetPropertiesByTask(_datarow["ProjectName"].ToString(), _datarow["TaskName"].ToString());
            if (!IsRecord)
            {
                this.projectName = _datarow["ProjectName"].ToString();
                this.taskName = _datarow["TaskName"].ToString();
                this.wuID = _datarow["WuId"].ToString();
                this.computerID = _datarow["ComputerId"].ToString();
                this.sentTime = (DateTime)_datarow["SentTime"];
                this.reportedTime = (DateTime)_datarow["ReportedTime"];
                this.runTime = (float)_datarow["RunTime"];
                this.cPUTime = (float)_datarow["CPUTime"];
                this.credit = (float)_datarow["Credit"];
                this.application = _datarow["Application"].ToString();
            }
        }
        /// <summary>
        /// 获得项目的所有程序名称
        /// </summary>
        /// <param name="_projectName"></param>
        /// <returns></returns>
        public static List<string> GetApplicationNames(string _projectName)
        {
            string sqlstr = "select distinct Application from BOINCTaskRecord where ProjectName=@projectname";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@projectname", SqlDbType.VarChar, 100);
            paras[0].Value = _projectName;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            List<string> names = new List<string>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                names.Add(dt.Rows[i][0].ToString());
            }
            return names;
        }
        /// <summary>
        /// 获得项目的所有计算机名称
        /// </summary>
        /// <param name="_projectName"></param>
        /// <returns></returns>
        public static List<string> GetComputerNames(string _projectName)
        {
            string sqlstr = "select distinct ComputerID from BOINCTaskRecord where ProjectName=@projectname";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@projectname", SqlDbType.VarChar, 100);
            paras[0].Value = _projectName;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            List<string> names = new List<string>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                names.Add(dt.Rows[i][0].ToString());
            }
            return names;
        }
        #endregion
    }
}
