using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MyUsefulTools.Utility
{
    public class CSharpUtility
    {
        /// <summary>
        /// 根据图片字节数组，返回图片对象
        /// </summary>
        /// <param name="_imgbytes"></param>
        /// <returns></returns>
        public static Image GetImageFromByteArray(byte[] _imgbytes)
        {
            MemoryStream memoryStream = new MemoryStream(_imgbytes);
            return Image.FromStream(memoryStream);
        }
        #region 获取代码间内容
        /// <summary>
        /// 返回根据内容开始及结束代码分析出的第i条内容（不包括开始和结束代码）
        /// </summary>
        /// <param name="ContentCode">内容代码</param>
        /// <param name="StartCode">内容所在开始代码</param>
        /// <param name="EndCode">内容所在结束代码</param>
        /// <param name="index">取第几条[从1开始]</param>
        /// <returns></returns>
        public static string GetContent(string contentCode, string startCode, string endCode, int index)
        {
            string[] matchItems = null;
            return GetContent(contentCode, startCode, endCode, index, out matchItems);
        }
        /// <summary>
        /// 返回根据内容开始及结束代码分析出的第i条内容（不包括开始和结束代码），并将所有获取到的内容放到传入的matchItems数组中
        /// </summary>
        /// <param name="ContentCode">内容代码</param>
        /// <param name="StartCode">内容所在开始代码</param>
        /// <param name="EndCode">内容所在结束代码</param>
        /// <param name="index">取第几条[从1开始]</param>
        /// <param name="matchItems">存放所有匹配内容的传出数组</param>
        /// <returns></returns>
        public static string GetContent(string contentCode, string startCode, string endCode, int index, out string[] matchItems)
        {
            matchItems = null;
            string returnValue = string.Empty;
            if (string.IsNullOrEmpty(startCode) && string.IsNullOrEmpty(endCode))
            {
                return contentCode;
            }
            Regex regObj = new Regex(startCode + @"([\S\s]*?)" + endCode, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matchItemList = regObj.Matches(contentCode);
            if (matchItemList != null && matchItemList.Count >= index)
            {
                matchItems = new string[matchItemList.Count];
                for (int i = 0; i < matchItemList.Count; i++)
                {
                    matchItems[i] = matchItemList[i].Groups[1].Value;
                }
                index = index > 0 ? index - 1 : 0;
                returnValue = matchItems[index];
            }
            else
            {
                matchItems = new string[0];
            }
            return returnValue;
        }
        public static string GetContent(string contentCode, string regexPattern, int index)
        {
            string[] matchItems = null;
            return GetContent(contentCode, regexPattern, index, out matchItems);
        }
        public static string GetContent(string contentCode, string regexPattern, int index, out string[] matchItems)
        {
            matchItems = null;
            string returnValue = string.Empty;
            if (string.IsNullOrEmpty(regexPattern))
            {
                return contentCode;
            }
            Regex regObj = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matchItemList = regObj.Matches(contentCode);
            if (matchItemList != null && matchItemList.Count >= index)
            {
                matchItems = new string[matchItemList.Count];
                for (int i = 0; i < matchItemList.Count; i++)
                {
                    matchItems[i] = matchItemList[i].Groups[1].Value;
                }
                index = index > 0 ? index - 1 : 0;
                returnValue = matchItems[index];
            }
            else
            {
                matchItems = new string[0];
            }
            return returnValue;
        }
        public static string GetZhuanyiString(string _str)
        {
            //进行正则转义
            string[,] zhuanyi = new string[,] {
                { "\\", "\\u005C" },
                { ".", "\\u002E" } ,
                { "$", "\\u0024" } ,
                { "^", "\\u005E" } ,
                { "{", "\\u007B" } ,
                { "[", "\\u005B" } ,
                { "(", "\\u0028" } ,
                { "|", "\\u007C" } ,
                { ")", "\\u0029" } ,
                { "*", "\\u002A" } ,
                { "+", "\\u002B" } ,
                { "?", "\\u003F" }
            
            };
            for (int i = 0; i < zhuanyi.GetLength(0); i++)
            {
                _str = _str.Replace(zhuanyi[i, 0], zhuanyi[i, 1]);
            }
            return _str;
        }
        #endregion
        /// <summary>
        /// 解析URL，获取制定查询名称对应的值
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_queryKey"></param>
        /// <returns></returns>
        public static string GetQueryStringFromUrl(string _url, string _queryKey)
        {
            int wenhao_index = _url.IndexOf('?');
            if (wenhao_index != -1)
            {
                string temp = _url.Substring(wenhao_index + 1);
                string[] pairs = temp.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < pairs.Length; i++)
                {
                    string[] values = pairs[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    //values[0]为key,values[1]为value
                    values[0] = values[0].Trim().ToLower();
                    values[1] = values[1].Trim();
                    if (values[0].Equals(_queryKey.ToLower()))
                    {
                        return values[1];
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="strHtml">包括HTML的源码</param>
        /// <returns>已经去除后的文字</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={
              @"<script[^>]*?>.*?</script>",
              @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""']?)(\\[""'tbnr]?|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
              @"([ ])[\s]+",
              @"&(quot|#34);",
              @"&(amp|#38);",
              @"&(lt|#60);",
              @"&(gt|#62);",
              @"&(nbsp|#160);",
              @"&(iexcl|#161);",
              @"&(cent|#162);",
              @"&(pound|#163);",
              @"&(copy|#169);",
              @"&#(\d+);",
              @"-->",
              @"<!--.* "};

            string[] aryRep = {
               "",
               "",
               "",
               "\"",
               "&",
               "<",
               ">",
               " ",
               "\xa1",//chr(161),
               "\xa2",//chr(162),
               "\xa3",//chr(163),
               "\xa9",//chr(169),
               "",
               " ",
               ""};
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace(" ", "");

            return strOutput;
        }
        /// <summary>
        /// 将给定的数据大小转换为合适的表达方式，逢1024改变单位
        /// </summary>
        /// <param name="_size"></param>
        /// <returns>如1.8MB，387KB</returns>
        public static string GetSuitableDataSize(double _size)
        {
            string[] danwei = { "B", "KB", "MB", "GB", "TB" };
            int index = 0;
            while (_size >= 1000)
            {
                _size /= 1024;
                index++;
            }
            return _size.ToString() + danwei[index];
        }
    }
}
