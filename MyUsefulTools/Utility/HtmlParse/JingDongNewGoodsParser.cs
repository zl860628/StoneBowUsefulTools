using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Nodes;
using System.Net;
using MyUsefulTools.DAO;
using System.Collections;

namespace MyUsefulTools.Utility.HtmlParse
{
    /// <summary>
    /// 建立委托：表明当前已经获取了一条记录
    /// </summary>
    public delegate void DelegeteGetOneRecord(ArrayList _parasList);

    class JingDongNewGoodsParser
    {

        /// <summary>
        /// 根据京东商城最新商品页面获得相关的信息
        /// 版本一，当京东网对应改版的时候，则需要更改获取信息的方法
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static DataTable GetJingdongNewGoodsData(string _url, DelegeteGetOneRecord GetOneRecord,
            InternetTransport _internetTransport)
        {
            //准备数据存储表
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("ImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("PriceImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("WebUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("InsertDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("BeginSaleDate", typeof(DateTime)));
            //获取网页页面HTML代码
            string html = "";
            while (html.Equals(""))
            {
                html = _internetTransport.GetAndGetHTML(_url, null, Encoding.Default);
            }
            //将代码中的单引号更换为双引号
            html = html.Replace('\'', '"');
            //下面解析HTML文档，并定位有用信息位置
            string short1html = CSharpUtility.GetContent(html, "<ul class=\"list-h\">", "</ul>", 1);
            string[] itemhtmls = null;
            CSharpUtility.GetContent(short1html, "<li", "</li>", 1, out itemhtmls);
            for (int i = 0; i < itemhtmls.Length; i++)
            {
                string name, description, imageurl, priceimgurl, commodityUrl;
                byte[] imageBytes = new byte[0];
                byte[] priceImgBytes = new byte[0];//用来存储图片的字节形式

                string imgdivhtml = CSharpUtility.GetContent(itemhtmls[i], "<div class=\"p-img\">", "</div>", 1);
                //获得商品名称
                name = CSharpUtility.GetContent(imgdivhtml, "alt=\"", "\"", 1);
                //*****判断此商品是否在数据库中，在的话就跳过这条记录
                JingDongNewGoodsDAO dao = new JingDongNewGoodsDAO(name);
                if (dao.IsRecord) continue;
                //获取商品的链接
                commodityUrl = CSharpUtility.GetContent(imgdivhtml, "href=\"", "\"", 1);
                if (!commodityUrl.StartsWith("http"))
                {
                    commodityUrl = "http://www.360buy.com" + commodityUrl;
                }
                //获得商品图片URL
                imageurl = CSharpUtility.GetContent(imgdivhtml, " (?:src|src2|data-lazyload)=\"", "\"", 1);

                string namedivhtml = CSharpUtility.GetContent(itemhtmls[i], "class=\"p-name\">", "</div>", 1);
                string description_temp = CSharpUtility.GetContent(namedivhtml, "\">", "</a>", 1);
                //获得商品描述
                description = CSharpUtility.StripHTML(description_temp);

                string pricedivhtml = CSharpUtility.GetContent(itemhtmls[i], "class=\"p-price\">", "</div>", 1);
                //获得商品价格图片URL
                priceimgurl = CSharpUtility.GetContent(pricedivhtml, @" src\s*=""", "\"", 1);

                //根据图片URL，获取对应的图片字节数组
                try
                {
                    //获取两张图片的字节数组
                    //imageBytes = _internetTransport.GetImageByteArrayFromUrl(imageurl);
                    //priceImgBytes = _internetTransport.GetImageByteArrayFromUrl(priceimgurl);
                }
                catch (WebException webex)
                {
                    //如果获取图片出现异常，则抛弃整条记录
                    Console.WriteLine(webex.Message);
                    continue;
                }
                //将对应信息加入到表的一行中
                DataRow dr = dt.NewRow();
                dr["Name"] = name;
                dr["ImageBytes"] = imageBytes;
                dr["PriceImageBytes"] = priceImgBytes;
                dr["Description"] = description;
                dr["InsertDate"] = DateTime.Now;
                dr["WebUrl"] = commodityUrl;
                //获取商品上架时间
                string commodityhtml = "";
                while (commodityhtml.Equals(""))
                {
                    commodityhtml = _internetTransport.GetAndGetHTML(commodityUrl, null, Encoding.Default);
                }
                string content = Utility.CSharpUtility.GetContent(commodityhtml, "<li>上架时间：", "</li>", 1);
                try
                {
                    dr["BeginSaleDate"] = DateTime.Parse(content);
                }
                catch (Exception ex)
                {
                    dr["BeginSaleDate"] = DBNull.Value;
                }
                dt.Rows.Add(dr);
                //添加进度代码
                ArrayList parasList = new ArrayList();
                parasList.Add(dr["BeginSaleDate"]);
                GetOneRecord(parasList);
            }
            return dt;
        }

        private DataTable GetJingdongNewGoodsData_V1(string _url, DelegeteGetOneRecord GetOneRecord,
            InternetTransport _internetTransport)
        {
            //准备数据存储表
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("ImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("PriceImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("WebUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("InsertDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("BeginSaleDate", typeof(DateTime)));
            //获取网页页面HTML代码
            string html = _internetTransport.GetAndGetHTML(_url, null, Encoding.Default);
            //下面解析HTML文档，并定位有用信息位置
            Lexer lexer = new Lexer(html);
            Parser parser = new Parser(lexer);
            //新建分析文档需要的属性筛选器
            HasAttributeFilter attrFilter = new HasAttributeFilter();
            attrFilter.AttributeName = "id";
            attrFilter.AttributeValue = "plist";
            HasAttributeFilter attrFilter_img = new HasAttributeFilter();
            attrFilter_img.AttributeName = "class";
            attrFilter_img.AttributeValue = "p-img";
            HasAttributeFilter attrFilter_name = new HasAttributeFilter();
            attrFilter_name.AttributeName = "class";
            attrFilter_name.AttributeValue = "p-name";
            HasAttributeFilter attrFilter_price = new HasAttributeFilter();
            attrFilter_price.AttributeName = "class";
            attrFilter_price.AttributeValue = "p-price";
            //应用第一步筛选器，得到需要的局部HTML
            NodeList htmlNodes = parser.Parse(attrFilter);
            INode listnode = htmlNodes[0];
            //将局部HTML作为文档对象进一步分析每个元素
            html = listnode.ToHtml();
            lexer = new Lexer(html);
            parser = new Parser(lexer);
            TagNameFilter tagnameFilter = new TagNameFilter("li");
            htmlNodes = parser.Parse(tagnameFilter);
            //分析每个元素，并将分析结果存放到数据表中
            for (int i = 0; i < htmlNodes.Count; i++)
            {
                string name, description, imageurl, priceimgurl, commodityUrl;
                byte[] imageBytes = new byte[0];
                byte[] priceImgBytes = new byte[0];//用来存储图片的字节形式

                INode node = htmlNodes[i];
                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode imgNode = (TagNode)parser.Parse(attrFilter_img)[0].Children[0].Children[0];
                name = imgNode.Attributes["ALT"].ToString();//获得商品名称
                //*****判断此商品是否在数据库中，在的话就跳过这条记录
                JingDongNewGoodsDAO dao = new JingDongNewGoodsDAO(name);
                if (dao.IsRecord) continue;

                imageurl = imgNode.Attributes["SRC"].ToString();//获得商品图片URL

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode commodityUrlNode = (TagNode)parser.Parse(attrFilter_img)[0].Children[0];
                commodityUrl = commodityUrlNode.Attributes["HREF"].ToString();//获取商品的链接

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                description = parser.Parse(attrFilter_name)[0].ToPlainTextString();//获得商品描述

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode priceNode = (TagNode)parser.Parse(attrFilter_price)[0].Children[1];
                priceimgurl = priceNode.Attributes["SRC"].ToString();//获得商品价格图片URL
                //根据图片URL，获取对应的图片字节数组
                try
                {
                    //获取两张图片的字节数组
                    imageBytes = _internetTransport.GetImageByteArrayFromUrl(imageurl);
                    priceImgBytes = _internetTransport.GetImageByteArrayFromUrl(priceimgurl);
                }
                catch (WebException webex)
                {
                    //如果获取图片出现异常，则抛弃整条记录
                    Console.WriteLine(webex.Message);
                    continue;
                }
                //将对应信息加入到表的一行中
                DataRow dr = dt.NewRow();
                dr["Name"] = name;
                dr["ImageBytes"] = imageBytes;
                dr["PriceImageBytes"] = priceImgBytes;
                dr["Description"] = description;
                dr["InsertDate"] = DateTime.Now;
                dr["WebUrl"] = commodityUrl;
                //获取商品上架时间
                string commodityhtml = _internetTransport.GetAndGetHTML(commodityUrl, null, Encoding.Default);
                string content = Utility.CSharpUtility.GetContent(commodityhtml, "<li>上架时间：", "</li>", 1);
                try
                {
                    dr["BeginSaleDate"] = DateTime.Parse(content);
                }
                catch (Exception ex)
                {
                    dr["BeginSaleDate"] = DBNull.Value;
                }
                dt.Rows.Add(dr);
                //添加进度代码
                ArrayList parasList = new ArrayList();
                parasList.Add(dr["BeginSaleDate"]);
                GetOneRecord(parasList);
            }
            return dt;
        }
    }
}