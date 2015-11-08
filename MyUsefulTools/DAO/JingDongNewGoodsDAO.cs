using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MySpace.Utils;
using DatabaseAccess;

namespace MyUsefulTools.DAO
{
    public class JingDongNewGoodsDAO
    {
        private int iD = -1;

        private string name = null;

        private string description = null;

        private string webUrl = null;

        private byte[] image = null;

        private byte[] priceImg = null;

        private DateTime insertDate = Constant.DateTime_MinValue;

        private string hasRead = null;
        private bool log_hasRead = false;

        private DateTime? beginSaleDate = null;

        private short interestValue = -1;

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string WebUrl
        {
            get { return webUrl; }
            set { webUrl = value; }
        }

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        public byte[] PriceImg
        {
            get { return priceImg; }
            set { priceImg = value; }
        }

        public DateTime InsertDate
        {
            get { return insertDate; }
            set { insertDate = value; }
        }

        public bool HasRead
        {
            get { return log_hasRead; }
        }
        public void SetHasRead(bool _value)
        {
            //首先给逻辑属性赋值
            log_hasRead = _value;
            //其次进行转换
            hasRead = _value ? "是" : "否";
        }
        private void TransHasRead()
        {
            //执行转换
            log_hasRead = hasRead.Equals("是") ? true : false;
        }


        public DateTime? BeginSaleDate
        {
            get { return beginSaleDate; }
            set { beginSaleDate = value; }
        }

        public short InterestValue
        {
            get { return interestValue; }
            set { interestValue = value; }
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
            string sqlstr = "select * from [JingDongNewGoods] where ID=@id;";
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

                name = dr["Name"].ToString().Trim();

                description = dr["Description"].ToString().Trim();

                webUrl = dr["WebUrl"].ToString().Trim();

                image = (byte[])dr["Image"];

                priceImg = (byte[])dr["PriceImg"];

                insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

                hasRead = dr["HasRead"].ToString().Trim();
                TransHasRead();

                if (dr["BeginSaleDate"] == DBNull.Value) beginSaleDate = null;
                else beginSaleDate = DateTime.Parse(dr["BeginSaleDate"].ToString().Trim());

                interestValue = Convert.ToInt16(dr["InterestValue"]);

                this.isRecord = true;
            }
        }

        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByName(string _name)
        {
            string sqlstr = "select * from [JingDongNewGoods] where Name=@name;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            paras[0].Value = _name;

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

                name = dr["Name"].ToString().Trim();

                description = dr["Description"].ToString().Trim();

                webUrl = dr["WebUrl"].ToString().Trim();

                image = (byte[])dr["Image"];

                priceImg = (byte[])dr["PriceImg"];

                insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

                hasRead = dr["HasRead"].ToString().Trim();
                TransHasRead();

                if (dr["BeginSaleDate"] == DBNull.Value) beginSaleDate = null;
                else beginSaleDate = DateTime.Parse(dr["BeginSaleDate"].ToString().Trim());

                interestValue = Convert.ToInt16(dr["InterestValue"]);

                this.isRecord = true;
            }
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [JingDongNewGoods] values(@name,@description,@weburl,@image,@priceimg,@insertdate,@hasread,@beginsaledate,@interestvalue);";

            SqlParameter[] paras = new SqlParameter[9];
            paras[0] = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            paras[0].Value = name;

            paras[1] = new SqlParameter("@description", SqlDbType.NVarChar, 100);

            if (description == null) paras[1].Value = DBNull.Value;
            else paras[1].Value = description;

            paras[2] = new SqlParameter("@weburl", SqlDbType.NVarChar, 100);

            if (webUrl == null) paras[2].Value = DBNull.Value;
            else paras[2].Value = webUrl;

            paras[3] = new SqlParameter("@image", SqlDbType.VarBinary, -1);

            if (image == null) paras[3].Value = DBNull.Value;
            else paras[3].Value = image;

            paras[4] = new SqlParameter("@priceimg", SqlDbType.VarBinary, -1);

            if (priceImg == null) paras[4].Value = DBNull.Value;
            else paras[4].Value = priceImg;

            paras[5] = new SqlParameter("@insertdate", SqlDbType.DateTime, 8);
            paras[5].Value = insertDate;

            paras[6] = new SqlParameter("@hasread", SqlDbType.NChar, 1);
            paras[6].Value = hasRead;

            paras[7] = new SqlParameter("@beginsaledate", SqlDbType.DateTime, 8);

            if (beginSaleDate == null) paras[7].Value = DBNull.Value;
            else paras[7].Value = beginSaleDate;

            paras[8] = new SqlParameter("@interestvalue", SqlDbType.SmallInt, 2);
            paras[8].Value = interestValue;

            DBManager.InsertRecord(sqlstr, paras);
            sqlstr = "select ID from [JingDongNewGoods] where Name=@name;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;

        }
        
        //以下为手工添加的代码
        public void UpdateRecord()
        {
            string sqlstr = "update [JingDongNewGoods] set description=@description,weburl=@weburl,image=@image,priceimg=@priceimg,insertdate=@insertdate,beginsaledate=@beginsaledate where name=@name;";

            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            paras[0].Value = name;

            paras[1] = new SqlParameter("@description", SqlDbType.NVarChar, 100);

            if (description == null) paras[1].Value = DBNull.Value;
            else paras[1].Value = description;

            paras[2] = new SqlParameter("@weburl", SqlDbType.NVarChar, 100);

            if (webUrl == null) paras[2].Value = DBNull.Value;
            else paras[2].Value = webUrl;

            paras[3] = new SqlParameter("@image", SqlDbType.VarBinary, -1);

            if (image == null) paras[3].Value = DBNull.Value;
            else paras[3].Value = image;

            paras[4] = new SqlParameter("@priceimg", SqlDbType.VarBinary, -1);

            if (priceImg == null) paras[4].Value = DBNull.Value;
            else paras[4].Value = priceImg;

            paras[5] = new SqlParameter("@insertdate", SqlDbType.DateTime, 8);
            paras[5].Value = insertDate;

            paras[6] = new SqlParameter("@beginsaledate", SqlDbType.DateTime, 8);

            if (beginSaleDate == null) paras[6].Value = DBNull.Value;
            else paras[6].Value = beginSaleDate;

            DBManager.UpdateRecords(sqlstr, paras);
        }
        /// <summary>
        /// 根据记录ID更改阅读状态
        /// </summary>
        /// <param name="_hasRead"></param>
        public static void UpdateHasReadByID(int _id, bool _hasRead)
        {
            string sqlstr = "update [JingDongNewGoods] set HasRead=@hasread where ID=@id;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@hasread", SqlDbType.NChar, 1);
            paras[0].Value = _hasRead ? "是" : "否";
            paras[1] = new SqlParameter("@id", SqlDbType.Int);
            paras[1].Value = _id;
            DBManager.UpdateRecords(sqlstr, paras);
        }
        /// <summary>
        /// 根据记录ID更改关注度
        /// </summary>
        /// <param name="_hasRead"></param>
        public static void UpdateInterestValueByID(int _id, short _value)
        {
            string sqlstr = "update [JingDongNewGoods] set InterestValue=@interestvalue where ID=@id;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@interestvalue", SqlDbType.SmallInt);
            paras[0].Value = _value;
            paras[1] = new SqlParameter("@id", SqlDbType.Int);
            paras[1].Value = _id;
            DBManager.UpdateRecords(sqlstr, paras);
        }
        /// <summary>
        /// 根据记录ID更改上架时间
        /// </summary>
        /// <param name="_hasRead"></param>
        public static void UpdateBeginSaleDateByID(int _id, DateTime _onsaleDate)
        {
            string sqlstr = "update [JingDongNewGoods] set BeginSaleDate=@beginSaleDate where ID=@id;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@beginSaleDate", SqlDbType.DateTime);
            paras[0].Value = _onsaleDate;
            paras[1] = new SqlParameter("@id", SqlDbType.Int);
            paras[1].Value = _id;
            DBManager.UpdateRecords(sqlstr, paras);
        }

        public void SetProperties(string _name, string _description, string _webUrl, byte[] _image, 
            byte[] _priceImg, DateTime _insertDate, bool _hasRead, DateTime? _beginSaleDate, short _interestValue)
        {
            GetPropertiesByName(_name);

            this.name = _name;
            this.description = _description;
            this.webUrl = _webUrl;
            this.image = _image;
            this.priceImg = _priceImg;
            this.insertDate = _insertDate;
            SetHasRead(_hasRead);
            this.beginSaleDate = _beginSaleDate;
            this.interestValue = _interestValue;
        }
        //构造方法
        public JingDongNewGoodsDAO()
        {
        }
        public JingDongNewGoodsDAO(int _id)
        {
            GetPropertiesByID(_id);
        }
        public JingDongNewGoodsDAO(string _name)
        {
            GetPropertiesByName(_name);
        }
    }
}
