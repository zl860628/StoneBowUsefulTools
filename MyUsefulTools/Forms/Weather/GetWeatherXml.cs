using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MyUsefulTools.BLL;
using MySpace.Utils;
using System.Net;
using System.IO;
using MyUsefulTools.Utility.HtmlParse;
using System.Threading;

namespace MyUsefulTools.Forms.Weather
{
    public partial class GetWeatherXml : Form
    {
        public GetWeatherXml()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(tb_xml.Text);
                WeatherRecordBLL wbBBL = new WeatherRecordBLL();
                DataTable dt = wbBBL.WeatherXmlToDatatable(xmldoc);
                Forms.Weather.SaveRecord saveRecordForm = new SaveRecord(dt);
                saveRecordForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 打开配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_openConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //设置外部程序名(记事本用 notepad.exe 计算器用 calc.exe)
            info.FileName = Constant.WeatherConfigFilePath;//直接打开对应文件
            //设置外部程序的启动参数
            info.Arguments = "";
            //设置外部程序工作目录为c:\windows
            info.WorkingDirectory = "c:/windows/";
            try
            {
                //启动外部程序
                proc = System.Diagnostics.Process.Start(info);
            }
            catch
            {
                MessageBox.Show("系统找不到指定的程序文件", "错误提示！");
                return;
            }
        }

        private List<string> getWeatherUrls()
        {
            string urlmode = "http://flash.weather.com.cn/sk2/{0}.xml";
            List<string> urls = new List<string>();
            StreamReader sr = new StreamReader(Constant.WeatherConfigFilePath);
            while (true)
            {
                string readline = sr.ReadLine();
                if (readline == null) break;

                //北京 101010100
                string cityid = readline.Split(new char[] { ' ' })[1];
                urls.Add(string.Format(urlmode, cityid));
            }
            return urls;
        }
        /// <summary>
        /// 从网络中获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btn_getFromWeb_Click(object sender, EventArgs e)
        {
            InternetTransport internetTransport = new InternetTransport("中国气象网天气实况");            
            //获得所有网址
            List<string> urls = getWeatherUrls();
            WeatherRecordBLL wrBLL = new WeatherRecordBLL();
            DataTable dt = WeatherRecordBLL.GenerEmptyWeatherDatatable();
            for (int i = 0; i < urls.Count; i++)
            {
                int trycount = 1;//表示每个网址的尝试次数
                while (trycount > 0)
                {
                    trycount--;
                    string htmlstr = "";
                    try
                    {
                        htmlstr = GetWeatherXmlByUrl(urls[i]);
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(htmlstr);
                        DataTable subdt = wrBLL.WeatherXmlToDatatable(xmldoc);
                        dt.Merge(subdt);
                        break;
                    }
                    catch (Exception ex)
                    {
                        //txt_remark.Text = "获取数据错误：" + urls[i] + "\t剩余尝试次数为：" + trycount;
                        txt_remark.Text = htmlstr;
                    }
                }
            }
            internetTransport.SaveRecord();
            Forms.Weather.SaveRecord saveRecordForm = new SaveRecord(dt);
            saveRecordForm.Show();
        }
        /// <summary>
        /// 下载天气数据到文件，文件默认保存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_download_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 选取文件夹，将里面所有文件中的天气数据获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_getFromFile_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 查看天气数据网址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_viewDataUrl_Click(object sender, EventArgs e)
        {
            List<string> urls = getWeatherUrls();
            for (int i = 0; i < urls.Count; i++)
            {
                txt_remark.AppendText(urls[i] + "\n");
            }
        }

        private string GetWeatherXmlByUrl(string _url)
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
                request.ContentType = "text/xml";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:6.0) Gecko/20100101 Firefox/6.0";
                //request.Headers["Accept-Encoding"] = "gzip, deflate";
                //request.Headers["Accept-Language"] = "zh-cn,zh;q=0.5";
                request.Timeout = 5 * 1000;
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(stream, Encoding.UTF8);
                result = streamreader.ReadToEnd();
            }
            catch (Exception ex)
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
    }
}
