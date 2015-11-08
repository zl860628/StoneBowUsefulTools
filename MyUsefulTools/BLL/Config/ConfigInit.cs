using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyUsefulTools.BLL.Config
{
    public class ConfigInit
    {
        private string GetLocalInitFileFullPath()
        {
            string hostname = Dns.GetHostName();
            string localInitFileName = hostname + "_local.xml";
            string path = Environment.CurrentDirectory + "\\config\\";
            string localInitFileFullName = path + localInitFileName;
            return localInitFileFullName;
        }

        public bool HasLocalInitFile()
        {
            string localInitFileFullName = GetLocalInitFileFullPath();
            return File.Exists(localInitFileFullName);
        }
        /// <summary>
        /// 本地配置文件默认创建在执行目录/config/{hostname}_local.xml
        /// </summary>
        public void CreateLocalInitFile()
        {
            string localInitFileFullName = GetLocalInitFileFullPath();
            string localInitFilePath = Path.GetDirectoryName(localInitFileFullName);
            if (!Directory.Exists(localInitFilePath)) Directory.CreateDirectory(localInitFilePath);

            if (!File.Exists(localInitFileFullName))
            { //When the file doesn't exist, create it by using default data!
                string configtext = @"
<config version=""0.1"">
  <path>
    <skydrive></skydrive>
  </path>
</config>";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(configtext);
                xmldoc.Save(localInitFileFullName);
            }
        }

        public string GetInitFileInfo()
        {
            StringBuilder sbd = new StringBuilder();
            string hostname = Dns.GetHostName();
            sbd.AppendLine("本机计算机名：" + hostname);

            string localInitFileFullName = GetLocalInitFileFullPath();
            if (!File.Exists(localInitFileFullName))
            {
                sbd.AppendLine("本地配置文件未找到，路径为：" + localInitFileFullName);
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(localInitFileFullName);
                string skydrivePath = xmldoc.SelectSingleNode("/config/path/skydrive").Value;
                sbd.AppendLine("用于同步的SkyDrive本地路径为：" + skydrivePath);
            }
            return sbd.ToString();
        }

        public XmlDocument GetConfigXmldoc()
        {
            string localInitFileFullName = GetLocalInitFileFullPath();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(localInitFileFullName);
            return xmldoc;
        }

        #region config update

        public bool UpdateConfig()
        {
            return false;
        }
        #endregion
    }
}
