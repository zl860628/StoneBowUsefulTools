using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using MySpace.Utils;
using System.IO;

namespace MyUsefulTools.Utility
{
    /// <summary>
    /// 用于处理汉字和拼音之间转换等相关功能
    /// </summary>
    public class PinyinTools
    {
        private static Hashtable map_ZiToPinyin = null;
        /// <summary>
        /// 使用ZiToPinyin.xml建立字到拼音的映射
        /// </summary>
        private void ConfigZiToPinyinMap(string _xmlpath)
        {
            if (map_ZiToPinyin != null) return;

            map_ZiToPinyin = new Hashtable();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(_xmlpath);
            XmlNode rootnode = xmldoc.FirstChild;
            XmlNodeList charlist = rootnode.ChildNodes;
            foreach (XmlNode charnode in charlist)
            {
                XmlNode zinode = charnode.SelectSingleNode("./zi");
                XmlNodeList pylist = charnode.SelectNodes("./py");
                string zistr = zinode.InnerText;
                string[] pystrs = new string[pylist.Count];
                for (int i = 0; i < pylist.Count; i++)
                {
                    pystrs[i] = pylist[i].InnerText;
                }
                map_ZiToPinyin[zistr] = pystrs;
            }
        }
        //构造方法
        public PinyinTools()
        {
            string xmlpath = Constant.AppRunPath + "files/ZiToPinyin.xml";
            ConfigZiToPinyinMap(xmlpath);
        }

        public string[] GetPinyin(string _zi)
        {
            return (string[])map_ZiToPinyin[_zi];
        }

        /// <summary>
        /// 创建自己格式的拼音xml文件
        ///  <char>
        ///     <zi>张</zi>
        ///     <py>zhang</py>
        ///  </char>
        /// </summary>
        public static void CreateZiToPinyinXmlFile(string _filePath, Dictionary<string, List<string>> _dictionary)
        {
            //首先准备XML文档
            XmlDocument xmldoc = new XmlDocument();
            XmlNode rootnode = xmldoc.CreateNode(XmlNodeType.Element, "root", "");
            xmldoc.AppendChild(rootnode);
            for (int i = 0; i < _dictionary.Count; i++)
            {
                string zi = _dictionary.Keys.ElementAt(i);
                List<string> py = _dictionary.Values.ElementAt(i);
                XmlNode charnode = xmldoc.CreateNode(XmlNodeType.Element, "char", "");
                XmlNode zinode = xmldoc.CreateNode(XmlNodeType.Element, "zi", "");
                zinode.InnerText = zi;
                charnode.AppendChild(zinode);
                for (int j = 0; j < py.Count; j++)
                {//一个字可能有多个音
                    XmlNode pynode = xmldoc.CreateNode(XmlNodeType.Element, "py", "");
                    pynode.InnerText = py[j];
                    charnode.AppendChild(pynode);
                }
                rootnode.AppendChild(charnode);
            }
            xmldoc.Save(_filePath);
        }
    }
}
