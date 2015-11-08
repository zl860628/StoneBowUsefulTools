using System;
using System.Data;
using System.Windows.Forms;
using MyUsefulTools.DAO;
using MyUsefulTools.Utility.HtmlParse;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MySpace.Utils;
using MyUsefulTools.Utility;

namespace MyUsefulTools.Forms.JingDong
{
    public partial class Dialog_GetNewData : Form
    {
        private InternetTransport internetTransport = null;
        public static string PageInfo = "";
        public static int NewCount = 0;
        public static DateTime? BeginSaleDate = null;

        public Dialog_GetNewData()
        {
            InitializeComponent();
            internetTransport = new InternetTransport("京东新商品");
        }
        /// <summary>
        /// 从配置文件中获取需要的网址
        /// </summary>
        /// <param name="_filepath"></param>
        /// <returns></returns>
        private List<string> GetUrlsFromFile(string _filepath)
        {
            List<string> urlStrs = new List<string>();
            StreamReader sr = new StreamReader(_filepath, Encoding.Default);
            string linestr;
            while ((linestr = sr.ReadLine()) != null)
            {
                //对配置字符串进行解释，只支持一个{}标记
                //http://www.360buy.com/special.aspx?id=5&page={1-20}表示加入page=1到page=20共20个网址
                Regex r = new Regex(@"{\d+-\d+}");
                if (r.IsMatch(linestr))
                {
                    MatchCollection mc = r.Matches(linestr);
                    string mcstr = mc[0].Value.Replace("{", "").Replace("}", "");
                    string[] mcstrs = mcstr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    int fromnum = Convert.ToInt32(mcstrs[0]);
                    int tonum = Convert.ToInt32(mcstrs[1]);
                    string linestr2 = r.Replace(linestr, "stonebow1");
                    for (int i = fromnum; i <= tonum; i++)
                    {
                        urlStrs.Add(linestr2.Replace("stonebow1", i.ToString()));
                    }
                }
                else
                {
                    urlStrs.Add(linestr);
                }
            }
            return urlStrs;
        }

        public void GetNewData()
        {
            Dialog_GetNewData.NewCount = 0;

            IList<string> urlStrs = GetUrlsFromFile(Constant.JingDongGoodsUrlFilePath);
            foreach (string url in urlStrs)
            {
                //后台线程终止条件
                if (backgroundWorker1.CancellationPending) return;

                Dialog_GetNewData.PageInfo = "当前处理：" + url;
                //准备数据
                DataTable dt = JingDongNewGoodsParser.GetJingdongNewGoodsData(url, GetOneRecord, internetTransport);
                //将数据写入数据库中
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JingDongNewGoodsDAO aNewGoods = new JingDongNewGoodsDAO();
                    DateTime? beginSaleDate = null;
                    if (dt.Rows[i]["BeginSaleDate"] != DBNull.Value)
                        beginSaleDate = (DateTime)dt.Rows[i]["BeginSaleDate"];
                    aNewGoods.SetProperties(
                        dt.Rows[i]["Name"].ToString(),
                        dt.Rows[i]["Description"].ToString(),
                        dt.Rows[i]["WebUrl"].ToString(),
                        (byte[])dt.Rows[i]["ImageBytes"],
                        (byte[])dt.Rows[i]["PriceImageBytes"],
                        (DateTime)dt.Rows[i]["InsertDate"], false, beginSaleDate, 0);
                    try
                    {
                        if (!aNewGoods.IsRecord)
                        {//当不存在这条记录的时候，再插入
                            aNewGoods.InsertNewRecord();
                            Dialog_GetNewData.NewCount++;
                        }
                        else
                        {
                            aNewGoods.UpdateRecord();
                            Dialog_GetNewData.NewCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            internetTransport.SaveRecord();
            timer1.Stop();
            MessageBox.Show("新商品获取完毕，共添加新商品" + NewCount.ToString() + "件！");
        }
        /// <summary>
        /// 使用定时器更新界面
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_newcount.Text = NewCount.ToString();
            lb_pageInfo.Text = PageInfo;
            lb_beginSaleDate.Text = BeginSaleDate.ToString();
            lb_downflux.Text = CSharpUtility.GetSuitableDataSize(internetTransport.DownloadFlux);
            TimeSpan downloadTime = DateTime.Now - internetTransport.BeginTransportTime;
            double downloadSpeed = internetTransport.DownloadFlux / downloadTime.TotalSeconds;
            lb_downspeed.Text = CSharpUtility.GetSuitableDataSize(downloadSpeed) + "/s";
        }
        /// <summary>
        /// 当页面分析器获取到一条记录的时候
        /// </summary>
        public void GetOneRecord(ArrayList _parasList)
        {
            if (_parasList[0] == DBNull.Value) BeginSaleDate = null;
            else BeginSaleDate = (DateTime)_parasList[0];
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            GetNewData();
        }

        private void Dialog_GetNewData_Shown(object sender, EventArgs e)
        {
            timer1.Start();
            if(!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void Dialog_GetNewData_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

    }
}
