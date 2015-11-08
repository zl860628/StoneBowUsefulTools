using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.DAO;
using MyUsefulTools.BLL;

namespace MyUsefulTools.Forms.Weather
{
    public partial class SaveRecord : Form
    {
        public SaveRecord(DataTable _weatherDatatable)
        {
            InitializeComponent();
            dataGridView1.DataSource = _weatherDatatable;
        }
        /// <summary>
        /// 将表格中的保存到数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            int insertCount = 0;
            DataTable savedDt = WeatherRecordBLL.GenerEmptyWeatherDatatable();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string cityName = (string)dataGridView1.Rows[i].Cells["城市名称"].Value;
                if (cityName == null) continue;
                DateTime recordTime = (DateTime)dataGridView1.Rows[i].Cells["记录时间"].Value;
                float temperature = (float)dataGridView1.Rows[i].Cells["温度"].Value;
                float humidity = (float)dataGridView1.Rows[i].Cells["相对湿度"].Value;
                float precipitation = (float)dataGridView1.Rows[i].Cells["降水"].Value;
                float windPower = (float)dataGridView1.Rows[i].Cells["风力"].Value;
                float windDirection = (float)dataGridView1.Rows[i].Cells["风向"].Value;
                float? airPressure = null;
                if(dataGridView1.Rows[i].Cells["气压"].Value != DBNull.Value)
                    airPressure = (float)dataGridView1.Rows[i].Cells["气压"].Value;
                WeatherRecord newRecord = new WeatherRecord(cityName, recordTime);
                if (!newRecord.IsRecord)
                {
                    newRecord = new WeatherRecord()
                    {
                        AirPressure = airPressure,
                        CityName = cityName,
                        Humidity = humidity,
                        Precipitation = precipitation,
                        RecordTime = recordTime,
                        Temperature = temperature,
                        WindDirection = windDirection,
                        WindPower = windPower
                    };
                    try
                    {
                        newRecord.InsertNewRecord();
                        insertCount++;
                        DataRow dr = savedDt.NewRow();
                        dr["城市名称"] = cityName;
                        dr["记录时间"] = recordTime;
                        dr["温度"] = temperature;
                        dr["相对湿度"] = humidity;
                        dr["降水"] = precipitation;
                        dr["风力"] = windPower;
                        dr["风向"] = windDirection;
                        if (airPressure == null)
                            dr["气压"] = DBNull.Value;
                        else dr["气压"] = airPressure;
                        savedDt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    { 
                    }
                }
            }
            dataGridView1.DataSource = savedDt;
            MessageBox.Show(string.Format("成功存储{0}条数据", insertCount));
        }
    }
}
