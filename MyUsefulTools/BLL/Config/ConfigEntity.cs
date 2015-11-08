using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyUsefulTools.BLL.Config
{
    public class ConfigEntity
    {
        public float Version;
        public Dictionary<string, string> Paths;

        private static ConfigEntity _instance = null;

        public static ConfigEntity Instance
        {
            get
            {
                if (_instance == null) _instance = new ConfigEntity();
                return _instance;
            }
        }
        private ConfigEntity()
        {
            //get config entity properties from config file of this machine
            ConfigInit ci = new ConfigInit();
            XmlDocument cfgdoc = ci.GetConfigXmldoc();

        }
    }
}
