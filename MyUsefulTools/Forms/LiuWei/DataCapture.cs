using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MyUsefulTools.Utility.HtmlParse;
using System.Net;
using MySpace.Utils;
using System.Data;
using System.Threading;

namespace MyUsefulTools.Forms.LiuWei
{
    public class DataCapture
    {
        private DelegeteGetOneRecord GetOneRecord = null;

        private Queue<string> dealWebsitesQueue;
        private Queue<DataTable> dealItemdataQueue;
        private InternetTransport internetTransport;
        private LiuWeiSpaceItemParser itemParser;
        private CookieContainer loginCookieContainer;

        private bool DownloadWebsitesEnd = true;//表明网页下载结束
        public bool ParseWebsitesEnd = true;//表明网页分析结束

        public DataCapture(DelegeteGetOneRecord _getOneRecord)
        {
            GetOneRecord = _getOneRecord;
            internetTransport = new InternetTransport("六维空间", 10);
            dealWebsitesQueue = new Queue<string>();
            dealItemdataQueue = new Queue<DataTable>();
            loginCookieContainer = new CookieContainer();
            itemParser = new LiuWeiSpaceItemParser(GetOneRecord);
        }
        /// <summary>
        /// 启动线程前必须执行的初始化操作
        /// </summary>
        public void PrepareStart()
        {
            this.DownloadWebsitesEnd = false;
            this.ParseWebsitesEnd = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_urls">类型是List<string></param>
        public void DownloadWebsites(object _urls)
        {
            List<string> urls = (List<string>)_urls;
            LoginSystem();

            foreach (string url in urls)
            {
                string html = internetTransport.GetAndGetHTML(url, loginCookieContainer, Encoding.Default);
                dealWebsitesQueue.Enqueue(html);
            }
            this.DownloadWebsitesEnd = true;
        }
        public void ParseWebsites()
        {
            while (true)
            {
                if (dealWebsitesQueue.Count > 0)
                {
                    string html = dealWebsitesQueue.Dequeue();
                    DataTable dt = itemParser.GetItemDataTableFromHtml(html);
                    if (dt.Rows.Count > 0) dealItemdataQueue.Enqueue(dt);
                }
                else if (DownloadWebsitesEnd) break;
                else Thread.Sleep(500);
            }
            this.ParseWebsitesEnd = true;
        }
        /// <summary>
        /// 获取解析好的数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetOneParsedDatatable()
        {
            DataTable dt = null;
            lock (this.dealItemdataQueue)
            {
                if (dealItemdataQueue.Count > 0)
                {
                    dt = this.dealItemdataQueue.Dequeue();
                }
            }
            return dt;
        }
        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <returns></returns>
        public void LoginSystem()
        {
            Hashtable param = new Hashtable();//this is for keep post data.
            string urlLogin = "http://bt.neu6.edu.cn/logging.php?action=login";
            param.Add("loginfield", "username");
            param.Add("username", "stonebow");
            param.Add("password", Constant.PassWord2);
            param.Add("questionid", "0");
            param.Add("answer", "");
            param.Add("loginsubmit", "true");
            param.Add("cookietime", "2592000");
            internetTransport.PostAndGetHTML(urlLogin, this.loginCookieContainer, Encoding.Default, param);
        }
    }
}
