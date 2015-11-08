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
    public class WeatherRecord
    {

        private string cityName = null;

        private DateTime recordTime = Constant.DateTime_MinValue;

        private float? temperature = null;

        private float? humidity = null;

        private float? precipitation = null;

        private float? windPower = null;

        private float? windDirection = null;

        private float? airPressure = null;

        private bool isRecord = false;

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public DateTime RecordTime
        {
            get { return recordTime; }
            set { recordTime = value; }
        }

        public float? Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public float? Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public float? Precipitation
        {
            get { return precipitation; }
            set { precipitation = value; }
        }

        public float? WindPower
        {
            get { return windPower; }
            set { windPower = value; }
        }

        public float? WindDirection
        {
            get { return windDirection; }
            set { windDirection = value; }
        }

        public float? AirPressure
        {
            get { return airPressure; }
            set { airPressure = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }


        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [WeatherRecord] values(@cityname,@recordtime,@temperature,@humidity,@precipitation,@windpower,@winddirection,@airpressure);";

            SqlParameter[] paras = new SqlParameter[8];
            paras[0] = new SqlParameter("@cityname", SqlDbType.NVarChar, 20);
            paras[0].Value = cityName;

            paras[1] = new SqlParameter("@recordtime", SqlDbType.DateTime, 8);
            paras[1].Value = recordTime;

            paras[2] = new SqlParameter("@temperature", SqlDbType.Real, 4);
            if (temperature == null) paras[2].Value = DBNull.Value;
            else paras[2].Value = temperature;

            paras[3] = new SqlParameter("@humidity", SqlDbType.Real, 4);
            if (humidity == null) paras[3].Value = DBNull.Value;
            else paras[3].Value = humidity;

            paras[4] = new SqlParameter("@precipitation", SqlDbType.Real, 4);
            if (precipitation == null) paras[4].Value = DBNull.Value;
            else paras[4].Value = precipitation;

            paras[5] = new SqlParameter("@windpower", SqlDbType.Real, 4);
            if (windPower == null) paras[5].Value = DBNull.Value;
            else paras[5].Value = windPower;

            paras[6] = new SqlParameter("@winddirection", SqlDbType.Real, 4);
            if (windDirection == null) paras[6].Value = DBNull.Value;
            else paras[6].Value = windDirection;

            paras[7] = new SqlParameter("@airpressure", SqlDbType.Real, 4);
            if (airPressure == null) paras[7].Value = DBNull.Value;
            else paras[7].Value = airPressure;

            DBManager.InsertRecord(sqlstr, paras);

            isRecord = true;

        }
        #region 手工写的代码
        //构造方法
        public WeatherRecord()
        { 
        
        }
        public WeatherRecord(string _cityName, DateTime _recordTime)
        {
            GetPropertiesByUnique(_cityName, _recordTime);
        }
        /// <summary>
        /// 根据唯一项获取对象属性
        /// </summary>
        private void GetPropertiesByUnique(string _cityName, DateTime _recordTime)
        {
            string sqlstr = "select * from [WeatherRecord] where CityName=@cityname and RecordTime=@recordtime;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@cityname", SqlDbType.NVarChar, 20);
            paras[0].Value = _cityName;
            paras[1] = new SqlParameter("@recordtime", SqlDbType.DateTime, 8);
            paras[1].Value = _recordTime;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                cityName = dr["CityName"].ToString().Trim();

                recordTime = DateTime.Parse(dr["RecordTime"].ToString().Trim());

                if (dr["Temperature"] == DBNull.Value) temperature = null;
                else temperature = (float)Convert.ToDouble(dr["Temperature"]);

                if (dr["Humidity"] == DBNull.Value) humidity = null;
                else humidity = (float)Convert.ToDouble(dr["Humidity"]);

                if (dr["Precipitation"] == DBNull.Value) precipitation = null;
                else precipitation = (float)Convert.ToDouble(dr["Precipitation"]);

                if (dr["WindPower"] == DBNull.Value) windPower = null;
                else windPower = (float)Convert.ToDouble(dr["WindPower"]);

                if (dr["WindDirection"] == DBNull.Value) windDirection = null;
                else windDirection = (float)Convert.ToDouble(dr["WindDirection"]);

                if (dr["AirPressure"] == DBNull.Value) airPressure = null;
                else airPressure = (float)Convert.ToDouble(dr["AirPressure"]);

                this.isRecord = true;
            }
        }
        #endregion
    }
}
