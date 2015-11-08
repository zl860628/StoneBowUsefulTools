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
    class TransportFlux
    {
        private int iD = -1;

        private string itemName = null;

        private DateTime beginTransportTime = Constant.DateTime_MinValue;

        private long uploadFlux = -1;

        private long downloadFlux = -1;

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

        public DateTime BeginTransportTime
        {
            get { return beginTransportTime; }
            set { beginTransportTime = value; }
        }

        public long UploadFlux
        {
            get { return uploadFlux; }
            set { uploadFlux = value; }
        }

        public long DownloadFlux
        {
            get { return downloadFlux; }
            set { downloadFlux = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        public void SetProperties(string _itemName, long _uploadFlux, long _downloadFlux, DateTime _beginTransportTime)
        {
            this.itemName = _itemName;
            this.uploadFlux = _uploadFlux;
            this.downloadFlux = _downloadFlux;
            this.beginTransportTime = _beginTransportTime;
        }
        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _iD)
        {
            string sqlstr = "select * from [TransportFlux] where ID=@id;";
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

                beginTransportTime = DateTime.Parse(dr["BeginTransportTime"].ToString().Trim());

                uploadFlux = Convert.ToInt64(dr["UploadFlux"]);

                downloadFlux = Convert.ToInt64(dr["DownloadFlux"]);

                this.isRecord = true;
            }
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [TransportFlux] values(@itemname,@begintransporttime,@uploadflux,@downloadflux);";

            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("@itemname", SqlDbType.NVarChar, 100);
            paras[0].Value = itemName;

            paras[1] = new SqlParameter("@begintransporttime", SqlDbType.DateTime, 8);
            paras[1].Value = beginTransportTime;

            paras[2] = new SqlParameter("@uploadflux", SqlDbType.BigInt, 8);
            paras[2].Value = uploadFlux;

            paras[3] = new SqlParameter("@downloadflux", SqlDbType.BigInt, 8);
            paras[3].Value = downloadFlux;

            DBManager.InsertRecord(sqlstr, paras);
            sqlstr = "select ID from [TransportFlux] where ItemName=@itemname and BeginTransportTime=@begintransporttime and UploadFlux=@uploadflux and DownloadFlux=@downloadflux;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;
        }
    }
}
