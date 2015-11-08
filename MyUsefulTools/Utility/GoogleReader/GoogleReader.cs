using System.Net;
using System.IO;
using MyUsefulTools.Utility.HtmlParse;
using System.Text;
using System;
using System.Collections.Generic;
using DataAccessTools.Entities.GoogleRSS;
using System.Xml;

namespace Utility
{
    public class GoogleReader
    {
        private string _sid = null;
        private string _auth = null;
        public string SID
        {
            get { return _sid; }
        }

        private string _token = null;
        public string Token
        {
            get { return _token; }
        }

        private string _username;
        private string _password;

        /// <summary>
        /// Give the google account email and password to login and get SID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public GoogleReader(string username, string password)
        {
            _username = username;
            _password = password;

            GetSid();
        }

        private long getUnixTimeNow()
        {
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            long unixTime = (long)ts.TotalSeconds;
            return unixTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        private string GetGoogleReaderResponse(string _url, Encoding _encoding)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            StringBuilder resultSbd = new StringBuilder();
            string result = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(_url);
                request.Method = "GET";
                request.ContentType = "application/xml";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; ccdotnet ;.NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                request.Timeout = 20 * 1000;
                request.Headers.Add("Authorization", "GoogleLogin auth=" + _auth);

                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(stream, _encoding);
                result = streamreader.ReadToEnd();
            }
            catch (WebException ex)
            {
                //记录日志
            }
            finally
            {
                if (responseStream != null) responseStream.Close();
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
            return result;
        }

        public void GetSid()
        {
            string requestUrl = string.Format
                ("https://www.google.com/accounts/ClientLogin?service=reader&Email={0}&Passwd={1}",
                _username, _password);
            InternetTransport it = new InternetTransport("", 20000);
            string resp = it.GetAndGetHTML(requestUrl, null, Encoding.ASCII);
            int indexSid = resp.IndexOf("SID=") + 4;
            int indexLsid = resp.IndexOf("LSID=");
            int indexAuth = resp.IndexOf("Auth=");
            _sid = resp.Substring(indexSid, indexLsid - 5);
            _auth = resp.Substring(indexAuth + 5).Trim();
        }

        public string GetToken()
        {
            string url = "http://www.google.com/reader/api/0/token";
            _token = GetGoogleReaderResponse(url, Encoding.Default);
            if (string.IsNullOrEmpty(_token))
            {
                GetSid();
                _token = GetToken();
            }
            return _token;
        }

        /// <summary>
        /// 获取包含未读条目的订阅列表
        /// </summary>
        /// <returns></returns>
        public List<GoogleReaderSubscription> GetUnreadSubscriptionList()
        {
            List<GoogleReaderSubscription> googleUnreadSubscriptions = new List<GoogleReaderSubscription>();

            string url = string.Format("http://www.google.com/reader/api/0/subscription/list");
            string resp = GetGoogleReaderResponse(url, Encoding.UTF8);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(resp);
            XmlNodeList feedlist = xmldoc.SelectNodes(@"/object/list/object");
            foreach (XmlNode feednode in feedlist)
            {
                string feedid = feednode.SelectSingleNode("string[@name='id']").InnerText.Trim();
                if (!feedid.StartsWith("feed")) continue;//ignore "user/-/state/com.google/reading-list"
                string feedtitle = feednode.SelectSingleNode("string[@name='title']").InnerText.Trim();

                GoogleReaderSubscription grs = new GoogleReaderSubscription { Title = feedtitle, FeedUrl = feedid };
                googleUnreadSubscriptions.Add(grs);
            }
            return googleUnreadSubscriptions;
        }

        public GoogleReaderItem GetItemFromXml(string _itemxml)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(_itemxml);

            string itemid = xmldoc.SelectSingleNode("id").InnerText.Trim();
            string title = xmldoc.SelectSingleNode("title").InnerText.Trim();
            string publishedTimeStr = xmldoc.SelectSingleNode("published").InnerText.Trim();
            DateTime publishedTime = DateTime.Parse(publishedTimeStr);
            string itemurl = xmldoc.SelectSingleNode("link").Attributes["href"].Value;
            string summary = xmldoc.SelectSingleNode("summary").InnerText.Trim();
            string sourceFeedUrl = xmldoc.SelectSingleNode("source").Attributes["gr:stream-id"].Value;
            bool hasRead = false;
            bool isStarred = false;
            XmlNodeList categoryList = xmldoc.SelectNodes("category");
            foreach (XmlNode category in categoryList)
            {
                string label = category.Attributes["label"].Value;
                switch (label)
                {
                    case "read": hasRead = true; break; //如果已经阅读就会有这个标签，不管是不是人工的标为未读
                    case "starred ": isStarred = true; break;
                    default: break;
                }
            }

            GoogleReaderItem item = new GoogleReaderItem
            {
                Title = title,
                PublishDate = publishedTime,
                Link = itemurl,
                Summary = summary,
                SubscriptionFeedUrl = sourceFeedUrl,
                HasRead = hasRead,
                IsStar = isStarred
            };

            return item;
        }
    }
}