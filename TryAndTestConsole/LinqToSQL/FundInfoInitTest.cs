using log4net;
using StoneUtils;
using StoneUtils.Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TryAndTestConsole.LinqToSQL
{
    public class FundInfoInitTest
    {
        private static FundBasicInfoDataContext dataContext = new FundBasicInfoDataContext();

        /// <summary>
        /// Get all fund information from internet
        /// </summary>
        /// <returns></returns>
        public List<FundBasicInfo> GetAllFundInfoFromInternet()
        {
            InternetTransport it = new InternetTransport("");
            string FundInfo_html = it.GetAndGetHTML(@"http://jingzhi.funds.hexun.com/jz/", null, Encoding.Default);
            string fundlist_html = CSharpUtility.GetContent(FundInfo_html, "<ul class=\"fundList\">", "</ul>", 1);
            string[] fundlistitems = fundlist_html.Split(new string[] { "</li>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in fundlistitems)
            {
                string fundUrl = CSharpUtility.GetContent(item, "<a href=\"", "\"", 1);
                string fundType = CSharpUtility.GetContent(item, CSharpUtility.GetZhuanyiString(fundUrl) + "\">", "</a>", 1);

                string fundinfo_url = @"http://jingzhi.funds.hexun.com" + fundUrl;
                FundInfo_html = it.GetAndGetHTML(fundinfo_url, null, Encoding.Default);
                string data_html = CSharpUtility.GetContent(FundInfo_html, "<!-- 以下是数据列表-->", "<!-- 数据列表结束 -->", 1).Trim();
                string[] data_tr_htmls = null;
                CSharpUtility.GetContent(data_html, "<tr[\\S\\s]*?>", "</tr>", 1, out data_tr_htmls);
                //data_tr_htmls中前两个tr是表头
                for (int i = 2; i < data_tr_htmls.Length; i++)
                {
                    string[] data_td_htmls = null;
                    CSharpUtility.GetContent(data_tr_htmls[i], "<td[\\S\\s]*?>", "</td>", 1, out data_td_htmls);
                    string fundid = CSharpUtility.StripHTML(data_td_htmls[1].Trim());
                    string fundname = CSharpUtility.GetContent(data_td_htmls[2].Trim(), "<a[\\S\\s]*?>", "</a>", 1).Trim();
                    var fundinfos = from fundinfo in dataContext.FundBasicInfo where fundinfo.FundId == fundid select fundinfo;
                    if (fundinfos.Count() == 1)
                    {
                        FundBasicInfo fundBasicInfo = fundinfos.First();
                        fundBasicInfo.FundType = fundBasicInfo.FundType + ";" + fundType;
                    }
                    else
                    {
                        FundBasicInfo newitem = new FundBasicInfo
                        {
                            FundId = fundid,
                            FundName = fundname,
                            FundType = fundType
                        };
                        dataContext.FundBasicInfo.InsertOnSubmit(newitem);
                    }
                    Console.Write("+");
                }
                dataContext.SubmitChanges();
            }

            return null;
        }
        public void GetCurrencyFundEverydayValue(DateTime _startdate, DateTime _enddate)
        {
            InternetTransport it = new InternetTransport("");
            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            var currencyFunds = from f in dataContext.FundBasicInfo
                                where f.FundType.Contains("货币型") || f.FundType.Contains("创新理财型")
                                select f;

            foreach (FundBasicInfo f in currencyFunds)
            {
                string url = string.Format("http://jingzhi.funds.hexun.com/database/jzzshb.aspx?fundcode={0}&startdate={1}&enddate={2}",
                    f.FundId, _startdate.ToString("yyyy-MM-dd"), _enddate.ToString("yyyy-MM-dd"));
                string html = it.GetAndGetHTML(url, null, Encoding.Default);
                string[] data_tr_htmls = null;
                CSharpUtility.GetContent(html, "<tr>", "</tr>", 1, out data_tr_htmls);
                foreach (string tr in data_tr_htmls)
                {
                    string[] data_td_htmls = null;
                    CSharpUtility.GetContent(tr, "<td align=\"center\">", "</td>", 1, out data_td_htmls);
                    DateTime date = DateTime.Now;
                    float value = 0;
                    try
                    {
                        date = DateTime.Parse(data_td_htmls[0]);
                        value = float.Parse(data_td_htmls[1]);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    try
                    {
                        CurrencyFundDailyRevenue cfr = dataContext.CurrencyFundDailyRevenue.Where(cf => cf.FundId == f.FundId && cf.RevenueDate == date).Single();
                        //log.Debug(f.FundName + "\t" + date.ToString() + "\t" + value.ToString());
                    }
                    catch (Exception ex)
                    {
                        CurrencyFundDailyRevenue cfr = new CurrencyFundDailyRevenue();
                        cfr.RevenueDate = date;
                        cfr.FundId = f.FundId;
                        cfr.Revenue10Thousand = value;
                        dataContext.CurrencyFundDailyRevenue.InsertOnSubmit(cfr);
                    }

                }
                dataContext.SubmitChanges();
            }
        }

    }
}
