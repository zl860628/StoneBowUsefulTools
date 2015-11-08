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
using System.Globalization;

namespace MyUsefulTools.Utility.HtmlParse
{
    public class BoincInfoParser
    {
        public static readonly string CAS_Home_ProjectName = "CAS@home";
        public static readonly string WUProp_ProjectName = "WUProp@Home";
        public static readonly string PrimeGrid_ProjectName = "PrimeGrid";
        public static readonly string Rosetta_ProjectName = "Rosetta@home";
        public static readonly string MilkyWay_ProjectName = "MilkyWay@home";
        public static readonly string SETI_ProjectName = "SETI@home";
        public static readonly string WorldCommunityGrid_ProjectName = "World Community Grid";
        public static readonly string Einstein_ProjectName = "Einstein@Home";
        public static string ShowMessage = "";
        //配置文件BoincComputerMapping.xml存入到此文档中
        private static XmlDocument computerMappingXmlDoc = null;

        public BoincInfoParser()
        {
            if (computerMappingXmlDoc == null)
            {
                computerMappingXmlDoc = new XmlDocument();
                computerMappingXmlDoc.Load(Constant.BoincComputerMappingFilePath);
            }
        }

        public DataTable GetCasHomeInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + CAS_Home_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + CAS_Home_ProjectName;
            string urlLogin = "http://casathome.ihep.ac.cn/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = GetInitDatatable();

            for (int i = 0; ; i += 20)
            {
                string url = "http://casathome.ihep.ac.cn/results.php?userid=139&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = CAS_Home_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[8].Trim());
                    dr["Application"] = drStrs[9].Trim();
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }

        public DataTable GetEinsteinInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + Einstein_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + Einstein_ProjectName;
            string urlLogin = "http://einstein.phys.uwm.edu/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = GetInitDatatable();

            for (int i = 0; ; i += 20)
            {
                string url = "http://einstein.phys.uwm.edu/results.php?userid=500614&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = Einstein_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[9].Trim());
                    dr["Application"] = drStrs[10].Trim();
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }

        public DataTable GetWUPropHomeInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + WUProp_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + WUProp_ProjectName;
            string urlLogin = "http://wuprop.boinc-af.org/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("TaskName", typeof(string));
            dt.Columns.Add("WuId", typeof(string));
            dt.Columns.Add("ComputerId", typeof(string));
            dt.Columns.Add("SentTime", typeof(DateTime));
            dt.Columns.Add("ReportedTime", typeof(DateTime));
            dt.Columns.Add("RunTime", typeof(float));
            dt.Columns.Add("CPUTime", typeof(float));
            dt.Columns.Add("Credit", typeof(float));
            dt.Columns.Add("Application", typeof(string));

            for (int i = 0; ; i += 20)
            {
                string url = "http://wuprop.boinc-af.org/results.php?userid=1626&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = WUProp_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    string computerid = dr["ComputerId"].ToString();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[8].Trim());
                    dr["Application"] = BoincInfoParser.GetMappingApplicationName(dr["ProjectName"].ToString(), computerid, drStrs[9].Trim());
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }

        public DataTable GetPrimeGridInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + PrimeGrid_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + PrimeGrid_ProjectName;
            string urlLogin = "http://www.primegrid.com/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            string test = transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("TaskName", typeof(string));
            dt.Columns.Add("WuId", typeof(string));
            dt.Columns.Add("ComputerId", typeof(string));
            dt.Columns.Add("SentTime", typeof(DateTime));
            dt.Columns.Add("ReportedTime", typeof(DateTime));
            dt.Columns.Add("RunTime", typeof(float));
            dt.Columns.Add("CPUTime", typeof(float));
            dt.Columns.Add("Credit", typeof(float));
            dt.Columns.Add("Application", typeof(string));

            for (int i = 0; ; i += 20)
            {
                string url = "http://www.primegrid.com/results.php?userid=60141&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = "";
                try
                {
                    htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                }
                catch (Exception ex)
                {
                    continue;
                }
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs = new string[0];
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = PrimeGrid_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[8].Trim());
                    dr["Application"] = drStrs[9].Trim();
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }

        /// <summary>
        /// 独特的结构，没有ComputerID
        /// </summary>
        /// <returns></returns>
        public DataTable GetMilkyWayInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + MilkyWay_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + MilkyWay_ProjectName;
            string urlLogin = "http://milkyway.cs.rpi.edu/milkyway/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            string test = transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = GetInitDatatable();

            for (int i = 0; ; i += 20)
            {
                string url = "http://milkyway.cs.rpi.edu/milkyway/results.php?userid=100768&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = MilkyWay_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    string computerid = dr["ComputerId"].ToString();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[8].Trim());
                    dr["Application"] = BoincInfoParser.GetMappingApplicationName(dr["ProjectName"].ToString(), computerid, drStrs[9].Trim());
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }
        /// <summary>
        /// 独特的结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetRosettaInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + Rosetta_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + Rosetta_ProjectName;
            string urlLogin = "http://boinc.bakerlab.org/rosetta/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            string test = transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = GetInitDatatable();

            for (int i = 0; ; i += 20)
            {
                string url = "http://boinc.bakerlab.org/rosetta/results.php?userid=379110&offset={0}";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "<table border=\"1\" cellpadding=\"5\" width=\"100%\">", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr>", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td>", "</td>", 1, out drStrs);

                    string ProjectName = Rosetta_ProjectName;
                    string TaskIDStr = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    string TaskUrl = "http://boinc.bakerlab.org/rosetta/result.php?resultid=" + TaskIDStr;
                    string WorkUnitIDStr = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    ////判断当前记录是否已经在数据库当中
                    if (new MyUsefulTools.DAO.BOINCTaskRecord(ProjectName, WorkUnitIDStr, MyUsefulTools.DAO.BOINCTaskRecord.ConstructorMode.WuId).IsRecord) continue;
                    //////////////////////////////////
                    string WorkUnitUrl = "http://boinc.bakerlab.org/rosetta/workunit.php?wuid=" + WorkUnitIDStr;
                    string SentTimeStr = drStrs[2].Replace("UTC", "+00:00");
                    string ReportedTimeStr = drStrs[3].Replace("UTC", "+00:00");
                    string CPUTimeStr = Utility.CSharpUtility.StripHTML(drStrs[7]).Trim();
                    string CreditStr = Utility.CSharpUtility.StripHTML(drStrs[9]).Trim();
                    float CPUTime = 0.0F;
                    float Credit = 0.0F;
                    DateTime SentTime = Constant.DateTime_MinValue;
                    DateTime ReportedTime = Constant.DateTime_MinValue;
                    try
                    {
                        CPUTime = float.Parse(CPUTimeStr);
                        Credit = float.Parse(CreditStr);
                        SentTime = DateTime.ParseExact(SentTimeStr, "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                        ReportedTime = DateTime.ParseExact(ReportedTimeStr, "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    }
                    catch (Exception ex)
                    {
                        //表示当前为未完成任务（此页面总完成与未完成的任务混在一起）
                        continue;
                    }
                    //获取TaskName和ComputerID以及Application，需要根据TaskID和WUID进入相应的页面获取
                    //Task页面可以得到TaskName和程序版本号，电脑号
                    ShowMessage = "当前处理页面：" + TaskUrl;
                    string taskhtml = transportTool.GetAndGetHTML(TaskUrl, cc, Encoding.Default);
                    string TaskName = Utility.CSharpUtility.GetContent(taskhtml, ">Name</td>", "</td>", 1);
                    TaskName = Utility.CSharpUtility.StripHTML(TaskName).Trim();

                    string AppVersionStr = Utility.CSharpUtility.GetContent(taskhtml, ">application version</td>", "</td>", 1);
                    AppVersionStr = Utility.CSharpUtility.StripHTML(AppVersionStr).Trim();
                    string ComputerIDStr = Utility.CSharpUtility.GetContent(taskhtml, Utility.CSharpUtility.GetZhuanyiString("show_host_detail.php?hostid="), ">", 1);
                    ComputerIDStr = ComputerIDStr.Trim();
                    //WU页面可以得到应用程序名
                    ShowMessage = "当前处理页面：" + WorkUnitUrl;
                    string wuhtml = transportTool.GetAndGetHTML(WorkUnitUrl, cc, Encoding.Default);
                    string AppName = Utility.CSharpUtility.GetContent(wuhtml, ">application</td>", "</td>", 1);
                    AppName = Utility.CSharpUtility.StripHTML(AppName).Trim();
                    AppName = AppName + " " + AppVersionStr;

                    dr["ProjectName"] = ProjectName;
                    dr["TaskName"] = TaskName;
                    dr["WuId"] = WorkUnitIDStr;
                    dr["ComputerId"] = ComputerIDStr;
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    dr["SentTime"] = SentTime;
                    dr["ReportedTime"] = ReportedTime;
                    dr["RunTime"] = 0.0F;
                    dr["CPUTime"] = CPUTime;
                    dr["Credit"] = Credit;
                    dr["Application"] = AppName;
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }
        /// <summary>
        /// 和CAS一样的结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetSETIHomeInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + SETI_ProjectName);
            //先登录，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            ShowMessage = "正在登陆" + SETI_ProjectName;
            string urlLogin = "http://setiathome.berkeley.edu//login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            transportTool.PostAndGetHTML(urlLogin, cc, Encoding.Default, param);
            DataTable dt = GetInitDatatable();

            for (int i = 0; ; i += 20)
            {
                string url = "http://setiathome.berkeley.edu/results.php?userid=9251803&offset={0}&show_names=1&state=3";
                url = string.Format(url, i);
                ShowMessage = "当前处理页面：" + url;
                string htmlCode = transportTool.GetAndGetHTML(url, cc, Encoding.Default);
                string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
                string[] itemStrs;
                Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
                if (itemStrs.Length <= 1) break;

                for (int j = 1; j < itemStrs.Length; j++)
                {
                    DataRow dr = dt.NewRow();
                    string[] drStrs;
                    Utility.CSharpUtility.GetContent(itemStrs[j], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                    dr["ProjectName"] = SETI_ProjectName;
                    dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                    //判断当前记录是否已经在数据库当中
                    DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                        dr["TaskName"].ToString());
                    if (recordDao.IsRecord) continue;

                    dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                    dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                    string computerid = dr["ComputerId"].ToString();
                    dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), dr["ComputerId"].ToString());
                    drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                    drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                    dr["SentTime"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["ReportedTime"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy | H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                    dr["RunTime"] = float.Parse(drStrs[6].Trim());
                    dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                    dr["Credit"] = float.Parse(drStrs[8].Trim());
                    dr["Application"] = BoincInfoParser.GetMappingApplicationName(dr["ProjectName"].ToString(), computerid, drStrs[9].Trim());
                    dt.Rows.Add(dr);
                }
            }
            transportTool.SaveRecord();
            return dt;
        }
        /// <summary>
        /// 获取World Community Grid中的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetWCGInfo()
        {
            InternetTransport transportTool = new InternetTransport("BOINC--" + WorldCommunityGrid_ProjectName, 50);
            string url = "https://secure.worldcommunitygrid.org/j_security_check";
            string resulturl = "https://secure.worldcommunitygrid.org/ms/viewBoincResults.do?filterDevice=0&filterStatus=4&projectId=-1&sortBy=returnedTime&pageNum={0}";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream requestStream = null;
            CookieContainer cc = new CookieContainer();

            DataTable dt = GetInitDatatable();
            try
            {
                ShowMessage = "登陆——开始获取任务页面";
                string formData = "j_username=zl860628&j_password=" + Constant.PassWord1;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(formData);
                //将请求时的Cookie加入
                ShowMessage = "登陆——正在准备登陆信息";
                cc.Add(new Cookie("__utma", "2464182.1748491084.1301203514.1301203514.1301234717.2", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmz", "2464182.1301203514.1.1.utmccn=(direct)|utmcsr=(direct)|utmcmd=(none)", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmc", "2464182", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmb", "2464182", "/", "secure.worldcommunitygrid.org"));

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
                request.Accept = "text/html,application/xhtml+xml,*/*";
                request.ContentLength = data.Length;
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 100 * 1000;
                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "en";
                request.Headers["Accept-Charset"] = "GB2312,utf-8";
                requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);

                ShowMessage = "登陆——正在获取服务器响应";
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cc.GetCookies(request.RequestUri);
                //获取登陆后的Cookie
                cc.Add(response.Cookies);
                //使用得到的Cookie，获取任务结果页面
                string firstpageurl = string.Format(resulturl, 1);
                ShowMessage = "获取首页，并获取页码...";
                string html = transportTool.GetAndGetHTML(firstpageurl, cc, Encoding.Default);
                string totalpageStr = CSharpUtility.GetContent(html, "pageNum=(\\d)\" class=\"contentLink\">Last", 1);
                int totalpage = 1;
                try
                {
                    totalpage = Int32.Parse(totalpageStr);
                }
                catch (Exception ex)
                { }
                int recordCount = 0;
                for (int pi = 1; pi <= totalpage; pi++)
                {
                    if (pi > 1) html = transportTool.GetAndGetHTML(string.Format(resulturl, pi), cc, Encoding.Default);

                    html = CSharpUtility.GetContent(html, "<table width=\"564\" border=\"0\" cellspacing=\"3\" cellpadding=\"3\">",
                        "</table>", 1);
                    string[] taskstrs;
                    Utility.CSharpUtility.GetContent(html, "<tr height", "</tr>", 1, out taskstrs);

                    for (int i = 0; i < taskstrs.Length; i++)
                    {
                        ShowMessage = "开始获取任务子页面" + (++recordCount).ToString();
                        string taskhtml = taskstrs[i].Trim();
                        //获取WorkunitID
                        string workunitidStr = CSharpUtility.GetContent(taskhtml, "workunitId=", "'", 1);
                        string taskurl = "https://secure.worldcommunitygrid.org/ms/device/viewWorkunitStatus.do?workunitId=" + workunitidStr;
                        //获取计算机名
                        string computerName = CSharpUtility.GetContent(taskhtml, "contentTextSmall\">", "</span>", 2);
                        //使用taskurl获取任务详细信息
                        string taskdetailhtml = transportTool.GetAndGetHTML(taskurl, cc, Encoding.Default);
                        string taskdetailhtml_part = CSharpUtility.GetContent(taskdetailhtml,
                            "<table width=\"575\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">", "</table>", 2);
                        //获取程序名
                        string applicationName = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<td align=\"left\" width=\"395\"><span class=\"contentText\">", "</span>", 1);
                        //获取任务名称
                        string taskName = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<td align=\"left\"><span class=\"contentText\">", "</span>", 2);
                        //找到自己的任务
                        taskdetailhtml_part = CSharpUtility.GetContent(taskdetailhtml,
                            "<tr height=\"54\" class=\"lightorange\">", "</tr>", 1);
                        //获取版本号
                        string applicationVersion = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<span class=\"contentTextSmall\">", "</a>", 2);
                        ////对版本号进行修饰，将640改成6.40，策略是小数点后面留两位
                        applicationVersion = applicationVersion.Substring(0, applicationVersion.Length - 2) + "." +
                            applicationVersion.Substring(applicationVersion.Length - 2, 2);
                        applicationName = applicationName.Trim() + " " + applicationVersion.Trim();
                        //发布时间和提交时间
                        string sentTimeStr = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<span class=\"contentTextSmall\">", "</span>", 4);
                        string reportedTimeStr = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<span class=\"contentTextSmall\">", "</span>", 5);
                        //使用时间
                        string cputimeStr = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<span class=\"contentTextSmall\">", "</span>", 6);
                        //获得分数，得到“38.8&nbsp;/&nbsp;40.1”的形式，要进行处理，后面的是实际获得的分数
                        string creditStr = CSharpUtility.GetContent(taskdetailhtml_part,
                            "<span class=\"contentTextSmall\">", "</span>", 7);
                        string[] temparray = creditStr.Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);
                        creditStr = temparray[temparray.Length - 1];

                        //将所有内容写入到表格中
                        DataRow dr = dt.NewRow();
                        dr["ProjectName"] = WorldCommunityGrid_ProjectName;
                        dr["TaskName"] = taskName.Trim();
                        //判断当前记录是否已经在数据库当中
                        DAO.BOINCTaskRecord recordDao = new MyUsefulTools.DAO.BOINCTaskRecord(dr["ProjectName"].ToString(),
                            dr["TaskName"].ToString());
                        if (recordDao.IsRecord) continue;

                        dr["WuId"] = workunitidStr.Trim();
                        dr["ComputerId"] = BoincInfoParser.GetComputerNameByID(dr["ProjectName"].ToString(), computerName.Trim());
                        dr["SentTime"] = DateTime.ParseExact(sentTimeStr.Trim(), "M/d/yy H:mm:ss", CultureInfo.GetCultureInfo("en-US"));
                        dr["ReportedTime"] = DateTime.ParseExact(reportedTimeStr.Trim(), "M/d/yy H:mm:ss", CultureInfo.GetCultureInfo("en-US"));
                        dr["RunTime"] = 0;
                        dr["CPUTime"] = float.Parse(cputimeStr.Trim()) * 3600;
                        dr["Credit"] = float.Parse(creditStr.Trim());
                        dr["Application"] = applicationName.Trim();
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (WebException ex)
            {
            }
            finally
            {
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
            return dt;
        }

        public static DataTable GetInitDatatable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("TaskName", typeof(string));
            dt.Columns.Add("WuId", typeof(string));
            dt.Columns.Add("ComputerId", typeof(string));
            dt.Columns.Add("SentTime", typeof(DateTime));
            dt.Columns.Add("ReportedTime", typeof(DateTime));
            dt.Columns.Add("RunTime", typeof(float));
            dt.Columns.Add("CPUTime", typeof(float));
            dt.Columns.Add("Credit", typeof(float));
            dt.Columns.Add("Application", typeof(string));
            return dt;
        }

        /// <summary>
        /// 将网页上解析出来的ComputerID转化为能够人为识别的ComputerName，从而指向一台确定的电脑
        /// </summary>
        /// <param name="_computerID"></param>
        /// <returns></returns>
        public static string GetComputerNameByID(string _projectName, string _computerID)
        {
            string computerName = "";
            //文件中的计算机编号
            string computerid = "";
            string xpath = "/root/projects/project[@name='{0}']/computer[@id='{1}']";
            xpath = string.Format(xpath, _projectName, _computerID);
            XmlNode node = computerMappingXmlDoc.SelectSingleNode(xpath);
            if (node != null)
            {
                computerid = node.Attributes["mappingto"].Value;
            }
            xpath = "/root/computers/computer[@id='{0}']";
            xpath = string.Format(xpath, computerid);
            node = computerMappingXmlDoc.SelectSingleNode(xpath);
            if (node != null)
            {
                computerName = node.Attributes["name"].Value;
            }
            return computerName;
        }

        public static string GetMappingApplicationName(string _projectName, string _computerID, string _orginApplicationName)
        {
            string applicationName = _orginApplicationName;
            string xpath = "/root/projects/project[@name='{0}']/computer[@id='{1}']/application[@name='{2}']";
            xpath = string.Format(xpath, _projectName, _computerID, _orginApplicationName);
            XmlNode node = computerMappingXmlDoc.SelectSingleNode(xpath);
            if (node != null)
            {
                applicationName = node.Attributes["mappingto"].Value;
            }
            return applicationName;
        }
    }
}
