using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Nodes;
using System.Net;
using System.Collections;
using System.IO;
using System.Xml;
using MySpace.Utils;

namespace MyUsefulTools.Utility.HtmlParse
{
    public class LiuWeiSpaceItemParser
    {
        private DelegeteGetOneRecord GetOneRecord = null;

        public LiuWeiSpaceItemParser(DelegeteGetOneRecord _getOneRecord)
        {
            this.GetOneRecord = _getOneRecord;
        }
        /// <summary>
        /// 得到DataTable的初始化结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitDataTableStructor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("SeedCount", typeof(int)));
            dt.Columns.Add(new DataColumn("Size", typeof(string)));
            dt.Columns.Add(new DataColumn("URL", typeof(string)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
            return dt;
        }
        /// <summary>
        /// 从HTML文件中截取特定的内容，以DataTable格式输出
        /// </summary>
        /// <param name="_html"></param>
        /// <returns></returns>
        public DataTable GetItemDataTableFromHtml_v1(string _html)
        {
            //准备数据存储表
            DataTable dt = GetInitDataTableStructor();
            //获取网页页面HTML代码
            string html = _html;

            //下面解析HTML文档，并定位有用信息位置
            Lexer lexer = new Lexer(html);
            Parser parser = new Parser(lexer);
            //新建分析文档需要的属性筛选器
            TagNameFilter tbodyFilter = new TagNameFilter("tbody");
            NodeList itemNodes = parser.Parse(tbodyFilter);
            for (int i = 0; i < itemNodes.Count; i++)
            {
                TagNode node = (TagNode)itemNodes[i].NextSibling.NextSibling;//Winista对tbody没有支持
                string seedCount = node.Children[3].ToPlainTextString().Trim();
                if (seedCount.Equals("&nbsp;") || seedCount.Equals("")) continue;
                string size = node.Children[9].ToPlainTextString().Trim();
                string title = node.Children[11].Children[3].Children[0].ToPlainTextString().Trim();
                TagNode urlTagNode = (TagNode)node.Children[11].Children[3].Children[0];
                string url = "http://bt.neu6.edu.cn/" + urlTagNode.Attributes["HREF"].ToString().Trim();
                string date = node.Children[15].Children[8].ToPlainTextString();
                //将对应信息加入到表的一行中
                DataRow dr = dt.NewRow();
                dr["Title"] = title;
                dr["SeedCount"] = Int32.Parse(seedCount);
                dr["Size"] = size;
                dr["URL"] = url;
                dr["CreateDate"] = DateTime.Parse(date);
                dt.Rows.Add(dr);
                //添加进度代码
                if (GetOneRecord != null) GetOneRecord(null);
            }
            return dt;
        }
        public DataTable GetItemDataTableFromHtml(string _html)
        {
            //准备数据存储表
            DataTable dt = GetInitDataTableStructor();
            //获取网页页面HTML代码
            string html = _html;
            //下面解析HTML文档，并定位有用信息位置
            string[] itemStrs;
            CSharpUtility.GetContent(html, @"<tbody id=[\S]+?>", "</tbody>", 1, out itemStrs);
            for (int i = 0; i < itemStrs.Length; i++)
            {
                html = itemStrs[i];
                string titlehtml = CSharpUtility.GetContent(html, "<th class=\"subject[\\s\\S]+?\">", "</th>", 1);
                string title = CSharpUtility.GetContent(titlehtml, @"<a[\s\S]+?>", "</a>", 1);
                string seedCountStr = CSharpUtility.GetContent(html, @"<td class=""downloaded""[\s\S]+?>", "</td>", 1);
                string sizeStr = CSharpUtility.GetContent(html, @"<td class=""archivesize"">", "</td>", 1);
                string urlpart2 = CSharpUtility.GetContent(html, "<a href=\"", "\" title", 1);
                string dateStr = CSharpUtility.GetContent(html, "<em>", "</em>", 1);

                DataRow dr = dt.NewRow();
                dr["Title"] = title;
                dr["SeedCount"] = Int32.Parse(seedCountStr);
                dr["Size"] = sizeStr;
                dr["URL"] = "http://bt.neu6.edu.cn/" + urlpart2;
                dr["CreateDate"] = DateTime.Parse(dateStr);
                dt.Rows.Add(dr);
                //添加进度代码
                if (GetOneRecord != null) GetOneRecord(null);
            }
            return dt;
        }
        /// <summary>
        /// 从六维空间中得到需要的索引数据
        /// 两个参数表明获取信息在分析最大页数的时候，或者未达到最大页数但资讯发布日期已经早于最早截止日期的时候
        /// </summary>
        /// <returns></returns>
        /// <param name="_pageCount">分析最大页数</param>
        /// <param name="_endTime">最早的截止日期，分析到此日期的时候停止继续分析</param>
        public DataTable GetItemDataTable(int _pageCount, DateTime? _endTime)
        {
            DataTable itemdt = GetInitDataTableStructor();

            //利用Cookie信息获取需要的内容页面
            for (int i = 0; i < _pageCount; i++)
            {//这里只获取最新的10页
                string url = "http://bt.neu6.edu.cn/forumdisplay.php?fid=2&page=" + i.ToString();
                
                DataTable dt = GetItemDataTableFromHtml("");
                if (dt.Rows.Count == 0)
                {
                    i--;
                    continue;
                }
                itemdt.Merge(dt);
                //判断最后一条记录日期是否超过最早截止日期
                if (_endTime != null)
                {
                    DateTime nowdate = (DateTime)dt.Rows[dt.Rows.Count - 1]["CreateDate"];
                    if (nowdate.CompareTo(_endTime) < 0)
                    {//比最早截止日期还早的时候，就不再获取了
                        break;
                    }
                }
            }
            return itemdt;
        }
    }
}
/*
<tbody id="normalthread_549123"> 
    <tr> 
        0<td class="folder"> 
            <a href="viewthread.php?tid=549123&amp;extra=page%3D1" title="新窗口打开" target="_blank"> 
            <img src="images/default/folder_new.gif" /></a> 
        </td> 
        1<td class="complete" style="color: red; font-weight: bold;">1</td> 
        2<td class="downloading" style="color: blue; font-weight: bold;">4</td> 
        3<td class="downloaded" style="">0</td> 
        4<td class="archivesize">187.13 MB</td> 
        5<th class="subject new"> 
            <label>&nbsp;</label> 
            <span id="thread_549123"><a href="viewthread.php?tid=549123&amp;extra=page%3D1">[耶鲁大学开放课程—哲学：死亡][Open Yale course—Philosophy：Death][RMVB][中英字幕]][人人影视][[第七集]</a></span> 
            <a href="attachment.php?aid=1875952&amp;k=1"><img src="images/icons/icon17.gif" class="attach" /></a> 
        </th> 
        6<td class="avatar"><img src="http://bt.neu6.edu.cn/uc_server/data/avatar/000/09/44/87_avatar_small.jpg" onerror="this.onerror=null;this.src='http://bt.neu6.edu.cn/uc_server/images/noavatar_small.gif'" style="width: 24px; padding-right: 5px;" /></td> 
        7<td class="author"> 
            <cite> 
                <a href="space.php?uid=94487">shuimo17</a> 
            </cite> 
            <em>2010-9-21</em> 
        </td> 
    </tr> 
</tbody>
*/