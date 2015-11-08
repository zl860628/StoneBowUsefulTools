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
    public class BOINCCredit
    {
        private int iD = -1;

        private string itemName = "";

        private DateTime insertDate = Constant.DateTime_MinValue;

        private float totalCredit = 0.0F;

        private float rACCredit = 0.0F;

        private int totalRank = -1;

        private int rACRank = -1;

        private int computerCount = -1;

        private int activeComputerCount = -1;

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public DateTime InsertDate
        {
            get { return insertDate; }
            set { insertDate = value; }
        }

        public float TotalCredit
        {
            get { return totalCredit; }
            set { totalCredit = value; }
        }

        public float RACCredit
        {
            get { return rACCredit; }
            set { rACCredit = value; }
        }

        public int TotalRank
        {
            get { return totalRank; }
            set { totalRank = value; }
        }

        public int RACRank
        {
            get { return rACRank; }
            set { rACRank = value; }
        }

        public int ComputerCount
        {
            get { return computerCount; }
            set { computerCount = value; }
        }

        public int ActiveComputerCount
        {
            get { return activeComputerCount; }
            set { activeComputerCount = value; }
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
            string sqlstr = "select * from [BOINCCredit] where ID=@id;";
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

                itemName = dr["ItemName"].ToString().Trim();

                insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

                totalCredit = (float)Convert.ToDouble(dr["TotalCredit"]);

                rACCredit = (float)Convert.ToDouble(dr["RACCredit"]);

                totalRank = Convert.ToInt32(dr["TotalRank"]);

                rACRank = Convert.ToInt32(dr["RACRank"]);

                computerCount = Convert.ToInt32(dr["ComputerCount"]);

                activeComputerCount = Convert.ToInt32(dr["ActiveComputerCount"]);

                this.isRecord = true;
            }
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [BOINCCredit] values(@itemname,@insertdate,@totalcredit,@raccredit,@totalrank,@racrank,@computercount,@activecomputercount);";

            SqlParameter[] paras = new SqlParameter[8];
            paras[0] = new SqlParameter("@itemname", SqlDbType.VarChar, 100);
            paras[0].Value = itemName;

            paras[1] = new SqlParameter("@insertdate", SqlDbType.DateTime, 8);
            paras[1].Value = insertDate;

            paras[2] = new SqlParameter("@totalcredit", SqlDbType.Real, 4);
            paras[2].Value = totalCredit;

            paras[3] = new SqlParameter("@raccredit", SqlDbType.Real, 4);
            paras[3].Value = rACCredit;

            paras[4] = new SqlParameter("@totalrank", SqlDbType.Int, 4);
            paras[4].Value = totalRank;

            paras[5] = new SqlParameter("@racrank", SqlDbType.Int, 4);
            paras[5].Value = rACRank;

            paras[6] = new SqlParameter("@computercount", SqlDbType.Int, 4);
            paras[6].Value = computerCount;

            paras[7] = new SqlParameter("@activecomputercount", SqlDbType.Int, 4);
            paras[7].Value = activeComputerCount;

            DBManager.InsertRecord(sqlstr, paras);
            sqlstr = "select ID from [BOINCCredit] where ItemName=@itemname and InsertDate=@insertdate and TotalCredit=@totalcredit and RACCredit=@raccredit and TotalRank=@totalrank and RACRank=@racrank and ComputerCount=@computercount and ActiveComputerCount=@activecomputercount;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;

        }
        #region 自己添加代码

        private void setFieldValueFromDatarow(DataRow dr)
        {
            if (dr.Table.Columns.Contains("ID"))
                iD = Convert.ToInt32(dr["ID"]);

            if (dr.Table.Columns.Contains("ItemName"))
                itemName = dr["ItemName"].ToString().Trim();

            if (dr.Table.Columns.Contains("InsertDate"))
                insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

            if (dr.Table.Columns.Contains("TotalCredit"))
                totalCredit = (float)Convert.ToDouble(dr["TotalCredit"]);

            if (dr.Table.Columns.Contains("RACCredit"))
                rACCredit = (float)Convert.ToDouble(dr["RACCredit"]);

            if (dr.Table.Columns.Contains("TotalRank"))
                totalRank = Convert.ToInt32(dr["TotalRank"]);

            if (dr.Table.Columns.Contains("RACRank"))
                rACRank = Convert.ToInt32(dr["RACRank"]);

            if (dr.Table.Columns.Contains("ComputerCount"))
                computerCount = Convert.ToInt32(dr["ComputerCount"]);

            if (dr.Table.Columns.Contains("ActiveComputerCount"))
                activeComputerCount = Convert.ToInt32(dr["ActiveComputerCount"]);
        }
        //---构造方法---
        public BOINCCredit()
        { 
        
        }
        public BOINCCredit(int _id)
        {
            GetPropertiesByID(_id);
        }
        public BOINCCredit(DataRow _dr)
        {
            setFieldValueFromDatarow(_dr);
        }

        //---其他数据库方法---
        /// <summary>
        /// 获取项目最近的一条记录
        /// </summary>
        /// <param name="_itemName"></param>
        /// <returns></returns>
        public static BOINCCredit GetLastRecord(string _itemName)
        {
            string sqlstr = @"select top 1 * from BOINCCredit where ItemName=@itemname order by InsertDate desc";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@itemname", SqlDbType.VarChar, 100);
            paras[0].Value = _itemName;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            if (dt.Rows.Count > 0)
            {
                //如果返回多条（不可能）取第一条记录
                BOINCCredit record = new BOINCCredit();
                record.setFieldValueFromDatarow(dt.Rows[0]);
                record.IsRecord = true;
                return record;
            }
            else return null;
        }
        #endregion
    }
}
