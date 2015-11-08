using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace MyUsefulTools.BLL
{
    public class WeatherRecordBLL
    {
        /// <summary>
        /// 将天气网站上获取的xml文件的内容写入DataTable中
        /// </summary>
        /// <param name="_xmldoc"></param>
        public DataTable WeatherXmlToDatatable(XmlDocument _xmldoc)
        {
            //xml中的时间为24小时的时间点上的值ptime（共25条），主节点上写明了生成xml的时间。
            //最上面的为最新的数据，将ptime作为起始时间，下面每条减少一个小时即可
            XmlNode rootnode = _xmldoc.FirstChild.NextSibling;
            string cityName = rootnode.Attributes["city"].Value;
            string ptime = rootnode.Attributes["ptime"].Value;
            DateTime recordTime = Convert.ToDateTime(ptime);
            XmlNodeList qwNodeList = rootnode.ChildNodes;

            DataTable dt = GenerEmptyWeatherDatatable();
            for (int i = 0; i < qwNodeList.Count; i++)
            {
                try
                {
                    XmlNode qwNode = qwNodeList[i];
                    DataRow dr = dt.NewRow();
                    dr["城市名称"] = cityName;
                    dr["记录时间"] = recordTime;
                    recordTime = recordTime.AddHours(-1);//为下一条记录减少一个小时
                    dr["温度"] = float.Parse(qwNode.Attributes["wd"].Value);
                    dr["相对湿度"] = float.Parse(qwNode.Attributes["sd"].Value);
                    dr["降水"] = float.Parse(qwNode.Attributes["js"].Value);
                    dr["风力"] = float.Parse(qwNode.Attributes["fl"].Value);
                    dr["风向"] = float.Parse(qwNode.Attributes["fx"].Value);
                    if (qwNode.Attributes["qy"] != null)
                        dr["气压"] = float.Parse(qwNode.Attributes["qy"].Value);
                    else dr["气压"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
                catch (Exception ex)
                { }
            }
            return dt;
        }
        /// <summary>
        /// 生成定义好的空WeatherDataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable GenerEmptyWeatherDatatable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("城市名称", typeof(string)));
            dt.Columns.Add(new DataColumn("记录时间", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("温度", typeof(float)));
            dt.Columns.Add(new DataColumn("相对湿度", typeof(float)));
            dt.Columns.Add(new DataColumn("降水", typeof(float)));
            dt.Columns.Add(new DataColumn("风力", typeof(float)));
            dt.Columns.Add(new DataColumn("风向", typeof(float)));
            dt.Columns.Add(new DataColumn("气压", typeof(float)));
            return dt;
        }
    }
}