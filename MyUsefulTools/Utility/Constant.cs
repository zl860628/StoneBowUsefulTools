using System;
using System.Data;
using System.Configuration;

namespace MySpace.Utils
{
    /// <summary>
    /// 常量类
    /// </summary>
    public class Constant
    {
        public static readonly string AppRunPath = System.AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 自定义系统可接受最小日期，用来取代DateTime的null
        /// </summary>
        public static readonly DateTime DateTime_MinValue = new DateTime(1800, 1, 1);
        /// <summary>
        /// 默认字段分隔符，为英文分号
        /// </summary>
        public static readonly string DefaultSeparatorString = ";";
        /// <summary>
        /// 京东商品获取中网页链接文件路径
        /// </summary>
        public static readonly string JingDongGoodsUrlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\files\JingDongNewGoodsUrls.txt";
        /// <summary>
        /// 拼音字典xml文件
        /// </summary>
        public static readonly string ZiToPinyinFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\files\ZiToPinyin.xml";
        /// <summary>
        /// 天气配置文件
        /// </summary>
        public static readonly string WeatherConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\files\WeatherConfig.txt";
        /// <summary>
        /// Boinc计算机名映射文件
        /// </summary>
        public static readonly string BoincComputerMappingFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\files\BoincComputerMapping.xml";
        /// <summary>
        /// 此项目默认数据库连接字符串
        /// </summary>
        public static readonly string DefaultDatabaseConnectionString = "server=.;Integrated Security=False;uid=sa;pwd=zhanglei;database=MyUsefulTools";
        
        /// <summary>
        /// 
        /// </summary>
        public static readonly string PassWord1 = "";
        public static readonly string PassWord2 = "";
        public Constant()
        {

        }
    }
}