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
    public class LiuWeiSpaceItem
    {
        private int iD = -1;

        private string title = null;

        private string size = null;

        private int seedCount = -1;

        private string uRL = null;

        private DateTime createDate = Constant.DateTime_MinValue;

        private bool hasRead = false;

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public int SeedCount
        {
            get { return seedCount; }
            set { seedCount = value; }
        }

        public string URL
        {
            get { return uRL; }
            set { uRL = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public bool HasRead
        {
            get { return hasRead; }
            set { hasRead = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        /// <summary>
        /// 根据ID获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _id)
        {
            string sqlstr = "select * from [LiuWeiSpaceItem] where ID=@id;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", SqlDbType.Int);
            paras[0].Value = _id;
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

                title = dr["Title"].ToString().Trim();

                size = dr["Size"].ToString().Trim();

                seedCount = Convert.ToInt32(dr["SeedCount"]);

                uRL = dr["URL"].ToString().Trim();

                createDate = DateTime.Parse(dr["CreateDate"].ToString().Trim());

                hasRead = Convert.ToBoolean(dr["HasRead"]);

                this.isRecord = true;
            }
        }
        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [LiuWeiSpaceItem] values(@title,@size,@seedcount,@url,@createdate,@hasread);";

            SqlParameter[] paras = new SqlParameter[6];
            paras[0] = new SqlParameter("@title", SqlDbType.NVarChar, 300);
            paras[0].Value = title;

            paras[1] = new SqlParameter("@size", SqlDbType.VarChar, 20);
            paras[1].Value = size;

            paras[2] = new SqlParameter("@seedcount", SqlDbType.Int, 4);
            paras[2].Value = seedCount;

            paras[3] = new SqlParameter("@url", SqlDbType.VarChar, 300);
            paras[3].Value = uRL;

            paras[4] = new SqlParameter("@createdate", SqlDbType.DateTime, 8);
            paras[4].Value = createDate;

            paras[5] = new SqlParameter("@hasread", SqlDbType.Bit, 1);
            paras[5].Value = hasRead;

            DBManager.InsertRecord(sqlstr, paras);

            sqlstr = "select ID from [LiuWeiSpaceItem] where Title=@title and Size=@size and SeedCount=@seedcount and URL=@url and CreateDate=@createdate and HasRead=@hasread;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;

        }


        //自定义方法
        private void GetPropertiesByUnique(string _title)
        {
            string sqlstr = "select * from [LiuWeiSpaceItem] where Title=@title;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@title", SqlDbType.NVarChar, 300);
            paras[0].Value = _title;
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

                title = dr["Title"].ToString().Trim();

                size = dr["Size"].ToString().Trim();

                seedCount = Convert.ToInt32(dr["SeedCount"]);

                uRL = dr["URL"].ToString().Trim();

                createDate = DateTime.Parse(dr["CreateDate"].ToString().Trim());

                hasRead = Convert.ToBoolean(dr["HasRead"]);

                this.isRecord = true;
            }
        }
        public LiuWeiSpaceItem()
        { }
        public LiuWeiSpaceItem(int _id)
        {
            GetPropertiesByID(_id);
        }
        public LiuWeiSpaceItem(string _title)
        {
            GetPropertiesByUnique(_title);
        }

        public void UpdateSeedCount(int _seedCount)
        {
            string sqlstr = "update [LiuWeiSpaceItem] set SeedCount=@seedcount where ID=@id;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@seedcount", SqlDbType.Int);
            paras[0].Value = _seedCount;
            paras[1] = new SqlParameter("@id", SqlDbType.Int);
            paras[1].Value = this.ID;
            DBManager.UpdateRecords(sqlstr, paras);
        }

        public static void UpdateHasReadByID(int _id, bool _hasRead)
        {
            string sqlstr = "update [LiuWeiSpaceItem] set HasRead=@hasread where ID=@id;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@hasread", SqlDbType.Bit);
            paras[0].Value = _hasRead;
            paras[1] = new SqlParameter("@id", SqlDbType.Int);
            paras[1].Value = _id;
            DBManager.UpdateRecords(sqlstr, paras);
        }
    }
}
