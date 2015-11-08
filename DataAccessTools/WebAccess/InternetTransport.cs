using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;


namespace DataAccessTools.WebAccess
{
    /// <summary>
    /// 网络传输类
    /// </summary>
    public class InternetTransport
    {
        private string itemName;
        private DateTime beginTransportTime;
        private long uploadFlux;
        private long downloadFlux;
        private int timeoutSecond = 20;

        /// <summary>
        /// 超时时间，单位为秒
        /// </summary>
        public int TimeoutSecond
        {
            get { return this.timeoutSecond; }
            set { this.timeoutSecond = value; }
        }
        public void InitProperties()
        {
            itemName = "";
            beginTransportTime = DateTime.Now;
            uploadFlux = 0;
            downloadFlux = 0;
        }
        #region ITransportFluxStore 成员

        public long UploadFlux
        {
            get { return uploadFlux; }
        }

        public long DownloadFlux
        {
            get { return downloadFlux; }
        }

        public string ItemName
        {
            get { return itemName; }
        }

        public DateTime BeginTransportTime
        {
            get { return beginTransportTime; }
        }

        #endregion

        public InternetTransport(string _itemName)
        {
            InitProperties();
            this.itemName = _itemName;
        }
        public InternetTransport(string _itemName, int _timeoutSecond)
            : this(_itemName)
        {
            this.timeoutSecond = _timeoutSecond;
        }
        /// <summary>
        /// 使用Post方式获得HTML文档，并添加获取到的Cookie
        /// </summary>
        /// <param name="targetURL"></param>
        /// <param name="cc">this is for keeping cookies and sessions</param>
        /// <param name="param">this is the data need post inside form</param>
        /// <returns>html page</returns>
        public string PostAndGetHTML(string targetURL, CookieContainer cc, Encoding _encoding, Hashtable param)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream requestStream = null;
            Stream responseStream = null;
            string result = "";
            try
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

                request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";

                requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                this.uploadFlux += data.Length;//上传流量添加

                request.CookieContainer = cc;
                response = (HttpWebResponse)request.GetResponse();
                cc.Add(response.Cookies);
                responseStream = response.GetResponseStream();
                result = new StreamReader(responseStream, _encoding).ReadToEnd();
                this.downloadFlux += _encoding.GetByteCount(result);//下载流量添加
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (requestStream != null) requestStream.Close();
                if (responseStream != null) responseStream.Close();
                if (response != null) response.Close();
                //HttpWebRequest实例在不需要的时候及时释放资源，可以重复使用而不会阻塞
                if (request != null) request.Abort();
            }
            return result;
        }
        /// <summary>
        /// 使用Get方式获得HTML文档
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_cc">Get方式需要的Cookie容器</param>
        /// <returns></returns>
        public string GetAndGetHTML(string _url, CookieContainer _cc, Encoding _encoding)
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
                request.CookieContainer = _cc;
                request.Timeout = timeoutSecond * 1000;

                response = (HttpWebResponse)request.GetResponse();
                if (_cc != null) _cc.Add(response.Cookies);
                Stream stream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(stream, _encoding);
                result = streamreader.ReadToEnd();
                this.downloadFlux += _encoding.GetByteCount(result);//下载流量添加
            }
            catch (WebException ex)
            {
                //记录日志
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (responseStream != null) responseStream.Close();
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
            return result;
        }
        /// <summary>
        /// 使用WebClient方法获得网页的html，并使用默认的编码方式
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        public string GetPageHTMLByWebclient(string _url)
        {
            return GetPageHTMLByWebclient(_url, Encoding.Default);
        }
        public string GetPageHTMLByWebclient(string _url, Encoding _encoding)
        {
            string html = "";
            try
            {
                System.Net.WebClient aWebClient = new System.Net.WebClient();
                aWebClient.Encoding = _encoding;
                html = aWebClient.DownloadString(_url);
                this.downloadFlux += Encoding.Default.GetByteCount(html);//下载流量添加
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return html;
        }
        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            // write the required bytes  
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }
        /// <summary>
        /// 根据URL获得文件的字节数组
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        /// <exception cref="WebException">需要捕获WebException</exception>
        public byte[] GetImageByteArrayFromUrl(string _url)
        {
            //string html = new HttpHelper().GetHtml(new HttpItem(_url));
            byte[] imgbytes;// = Encoding.Default.GetBytes(html);
            
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            myWebRequest.AllowAutoRedirect = true;
            myWebRequest.Method = "GET";
            myWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            using (HttpWebResponse response = (HttpWebResponse)myWebRequest.GetResponse())
            {
                //从这里开始我们要无视编码了
                MemoryStream _stream = new MemoryStream();
                //Thread.Sleep(3000);
                _stream = GetMemoryStream(response.GetResponseStream());
                imgbytes = _stream.ToArray();
            }
            
            
            /*
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            myWebRequest.Method = "get";
            myWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; ccdotnet ;.NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            myWebRequest.KeepAlive = true;
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream streamResponse = myWebResponse.GetResponseStream();
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            // write the required bytes  
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            int piclength = (int)myWebResponse.ContentLength;
            if (piclength == -1)
            {//对于有重定向的URI访问时
                if (myWebResponse.ResponseUri != null)
                    imgbytes = GetImageByteArrayFromUrl(myWebResponse.ResponseUri.ToString());
                return imgbytes;
            }
            imgbytes = new byte[piclength];
            //使用缓冲来读取网络图片
            int buffercount = 10000;//每次读取的字节数
            int bufferposi = 0;
            while (true)
            {
                if (piclength <= buffercount + bufferposi)
                {
                    buffercount = piclength - bufferposi;
                }
                int readcount = _stream.Read(imgbytes, bufferposi, buffercount);
                bufferposi += readcount;
                this.downloadFlux += readcount;
                if (readcount == 0) break;
            }
            _stream.Close();
            myWebResponse.Close();
             */
            return imgbytes;
        }
    }
}
