using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MyUsefulTools.Utility.FileOperator
{
    public class PinyinDeal
    {
        /// <summary>
        /// 把内容拿出来
        /// </summary>
        public static void TransFormat()
        {
            StreamReader sr = new StreamReader("./files/pinyin_table_ISCCD_no_tune.php");
            StreamWriter sw = new StreamWriter("./files/resltpinyin.txt");
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("=>") && line.Contains("'"))
                {
                    //0 => 'kao a mo',
                    Regex r1 = new Regex(@"(?<=').*(?=')");
                    string pinyin = r1.Match(line).Value;
                    pinyin = pinyin.Replace(" ", ",");
                    Regex r2 = new Regex(@"\d(?= =>)");
                    string num = r2.Match(line).Value;
                    if (num == "0")
                    {
                        sw.WriteLine();
                        sw.Write(pinyin);
                    }
                    else
                    {
                        sw.Write(",");
                        sw.Write(pinyin);
                    }
                }
            }
            sw.Close();
            sr.Close();
        }
        /// <summary>
        /// 将table_ISCCD.txt和上面的结果拼在一起，直接生成自定义的XML格式
        /// </summary>
        public static void TransFormat2()
        { 
        
        }
    }
}
