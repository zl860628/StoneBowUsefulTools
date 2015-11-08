using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DatabaseAccess;

namespace MyUsefulTools.DAO
{
    public class UnderWaterFishInfo
    {
        private bool isRecord = false;

        public bool IsRecord
        {
            get { return isRecord; }
        }
        //头部手工添加代码
        private int iD = -1;

        private string kindName = "";

        private float level = 0.0F;

        private int? buyNeedLevel = null;

        private float lifeLength = 0.0F;

        private float makeCoinInterval = 0.0F;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string KindName
        {
            get { return kindName; }
            set { kindName = value; }
        }

        public float Level
        {
            get { return level; }
            set { level = value; }
        }

        public int? BuyNeedLevel
        {
            get { return buyNeedLevel; }
            set { buyNeedLevel = value; }
        }

        public float LifeLength
        {
            get { return lifeLength; }
            set { lifeLength = value; }
        }

        public float MakeCoinInterval
        {
            get { return makeCoinInterval; }
            set { makeCoinInterval = value; }
        }

        /// <summary>
        /// 根据ID获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _id)
        {
            string sqlstr = "select * from [UnderWaterFishInfo] where ID=@id;";
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
                this.iD = _id;
                kindName = dt.Rows[0]["KindName"].ToString().Trim();

                level = float.Parse(dt.Rows[0]["Level"].ToString().Trim());

                object obj_buyNeedLevel = dt.Rows[0]["BuyNeedLevel"];
                if (obj_buyNeedLevel == DBNull.Value) buyNeedLevel = null;
                else buyNeedLevel = (int)obj_buyNeedLevel;

                lifeLength = float.Parse(dt.Rows[0]["LifeLength"].ToString().Trim());

                makeCoinInterval = float.Parse(dt.Rows[0]["MakeCoinInterval"].ToString().Trim());

                this.isRecord = true;
            }
        }
        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [UnderWaterFishInfo] values(@kindname,@level,@buyneedlevel,@lifelength,@makecoininterval);";

            SqlParameter[] paras = new SqlParameter[5];

            paras[0] = new SqlParameter("@kindname", SqlDbType.NVarChar, 10);
            paras[0].Value = kindName;

            paras[1] = new SqlParameter("@level", SqlDbType.Real, 4);
            paras[1].Value = level;

            paras[2] = new SqlParameter("@buyneedlevel", SqlDbType.Int, 4);
            if (buyNeedLevel == null) paras[2].Value = DBNull.Value;
            else paras[2].Value = buyNeedLevel;

            paras[3] = new SqlParameter("@lifelength", SqlDbType.Real, 4);
            paras[3].Value = lifeLength;

            paras[4] = new SqlParameter("@makecoininterval", SqlDbType.Real, 4);
            paras[4].Value = makeCoinInterval;

            DBManager.InsertRecord(sqlstr, paras);

            sqlstr = "select ID from [UnderWaterFishInfo] where KindName=@kindname and Level=@level and BuyNeedLevel=@buyneedlevel and LifeLength=@lifelength and MakeCoinInterval=@makecoininterval;";
            //当为空的时候，不能用等号来判断了，要特定为is null来判断
            if (buyNeedLevel == null) sqlstr = sqlstr.Replace("BuyNeedLevel=@buyneedlevel", "BuyNeedLevel is null");
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;
        }
        //尾部手工添加代码
        public UnderWaterFishInfo()
        { 
        
        }
        public UnderWaterFishInfo(int _id)
        {
            GetPropertiesByID(_id);
        }
        public UnderWaterFishInfo(string _kindName)
        {
            GetPropertiesByUnique(_kindName);
        }
        public void SetProperties(string _kindName, float _level, int? _buyNeedLevel, float _lifeLength, float _makeCoinInterval)
        {
            this.kindName = _kindName;
            this.level = _level;
            this.buyNeedLevel = _buyNeedLevel;
            this.lifeLength = _lifeLength;
            this.makeCoinInterval = _makeCoinInterval;
        }

        public static DataTable GetAllRecordDataTable()
        {
            string sqlstr = "select * from [UnderWaterFishInfo];";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            return dt;
        }

        //手工添加代码
        private void GetPropertiesByUnique(string _kindName)
        {
            string sqlstr = "select * from [UnderWaterFishInfo] where KindName=@kindname;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@kindname", SqlDbType.NVarChar, 10);
            paras[0].Value = _kindName;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                iD = (int)dt.Rows[0]["ID"];

                kindName = dt.Rows[0]["KindName"].ToString().Trim();

                level = float.Parse(dt.Rows[0]["Level"].ToString().Trim());

                object obj_buyNeedLevel = dt.Rows[0]["BuyNeedLevel"];
                if (obj_buyNeedLevel == DBNull.Value) buyNeedLevel = null;
                else buyNeedLevel = (int)obj_buyNeedLevel;

                lifeLength = float.Parse(dt.Rows[0]["LifeLength"].ToString().Trim());

                makeCoinInterval = float.Parse(dt.Rows[0]["MakeCoinInterval"].ToString().Trim());

                this.isRecord = true;
            }
        }
    }
}
