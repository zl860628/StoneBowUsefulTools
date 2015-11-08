using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUsefulTools.Utility;
using System.Windows.Forms;
using System.Data;

namespace MyUsefulTools.MyControl
{
    public class PinyinSearchComboBox : System.Windows.Forms.ComboBox
    {
        private List<string> orginStrList = null;
        private List<string> pinyinStrList = null;
        private Object dataSource = null;

        public new Object DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                DealDataSource();
                base.DataSource = orginStrList;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public PinyinSearchComboBox()
        {
            this.KeyUp += new KeyEventHandler(PinyinSearchComboBox_KeyUp);
        }
        /// <summary>
        /// 内部处理DataSource的方法
        /// </summary>
        private void DealDataSource()
        {
            if (dataSource == null) return;
            //根据DataSource的类型不同，完成不同的处理
            if (dataSource.GetType().Equals(typeof(List<string>)))
            {//当获得的类型为List<string>时
                orginStrList = (List<string>)dataSource;
                pinyinStrList = new List<string>();
                //根据绑定项获得对应的拼音列表，多音字暂时只使用第一个音
                PinyinTools pinyintool = new PinyinTools();
                foreach (string str in orginStrList)
                {
                    StringBuilder pysbd = new StringBuilder();
                    for (int i = 0; i < str.Length; i++)
                    {
                        string[] pinyinstrs = pinyintool.GetPinyin(str[i].ToString());
                        if (pinyinstrs == null)
                        {
                            pysbd.Append(str[i]);
                        }
                        else
                        {
                            pysbd.Append(pinyinstrs[0]);
                        }
                    }
                    pinyinStrList.Add(pysbd.ToString());
                }
            }
        }
        private void PinyinSearchComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 18)
            {//当按Alt键的时候进行筛选绑定
                string subitemstr = this.Text;
                List<string> bindlist = getContainInputItems(subitemstr);
                base.DataSource = bindlist;
                this.Text = subitemstr;
                this.DroppedDown = true;
            }
        }
        /// <summary>
        /// 找到包含输入参数的项目的集合
        /// </summary>
        /// <returns></returns>
        private List<string> getContainInputItems(string subitem)
        { 
            //遍历原列表和拼音列表，找到匹配的交集
            bool[] containflag = new bool[orginStrList.Count];
            for (int i = 0; i < orginStrList.Count; i++)
            {
                if (orginStrList[i].Contains(subitem) || pinyinStrList[i].Contains(subitem))
                {
                    containflag[i] = true;
                }
                else
                {
                    containflag[i] = false;
                }
            }
            List<string> matchedList = new List<string>();
            for (int i = 0; i < containflag.Length; i++)
            {
                if (containflag[i])
                {
                    matchedList.Add(orginStrList[i]);
                }
            }
            return matchedList;
        }
    }
}
