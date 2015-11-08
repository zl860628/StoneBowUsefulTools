using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace MyUsefulTools.Utility.HtmlParse
{
    public class HTMLCommonParser
    {
        /// <summary>
        /// 使用Post方式获得HTML文档，并添加获取到的Cookie
        /// </summary>
        /// <param name="targetURL"></param>
        /// <param name="cc">this is for keeping cookies and sessions</param>
        /// <param name="param">this is the data need post inside form</param>
        /// <returns>html page</returns>
        public static string PostAndGetHTML(string targetURL, CookieContainer cc, Hashtable param)
        {
            //prepare the submit data
            string formData = "";
            foreach (DictionaryEntry de in param)
            {
                formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
            }
            if (formData.Length > 0)
                formData = formData.Substring(0, formData.Length - 1); //remove last '&'

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(formData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
            request.Method = "POST";    //post
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);

            newStream.Close();

            request.CookieContainer = cc;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cc.Add(response.Cookies);
            Stream stream = response.GetResponseStream();
            string result = new StreamReader(stream, System.Text.Encoding.Default).ReadToEnd();
            return result;
        }
        /// <summary>
        /// 使用Get方式获得HTML文档
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_cc">Get方式需要的Cookie容器</param>
        /// <returns></returns>
        public static string GetAndGetHTML(string _url, CookieContainer _cc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "GET";    //post
            request.ContentType = "application/xml";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
            request.CookieContainer = _cc;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string result = new StreamReader(stream, Encoding.Default).ReadToEnd();
            return result;
        }
    }
}
