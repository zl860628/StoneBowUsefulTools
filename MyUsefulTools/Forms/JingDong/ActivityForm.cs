using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MyUsefulTools.Utility.HtmlParse;
using MyUsefulTools.Utility;

namespace MyUsefulTools.Forms.JingDong
{
    public partial class ActivityForm : Form
    {
        private Hashtable hasItemMap;
        private InternetTransport itran;
        private bool isRunning = false;

        public ActivityForm()
        {
            InitializeComponent();
            hasItemMap = new Hashtable();
            itran = new InternetTransport("京东促销");
            timer1.Interval = 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 180000;
            if(!isRunning) GetItems();
        }

        //抓取促销商品，显示在文本框中
        private void GetItems()
        {
            isRunning = true;
            bool hasnew = false;
            string[] urls = new string[] { 
                "http://www.360buy.com/digital.html",
                "http://www.360buy.com/electronic.html",
                "http://www.360buy.com/pop/",
                "http://www.360buy.com/computer.html"};
            for (int i = 0; i < urls.Length; i++)
            {
                string htmlstr = itran.GetAndGetHTML(urls[i], null, Encoding.Default);
                int strpoint = 0;
                while (true)
                {
                    if (htmlstr.Equals("")) break;
                    strpoint = htmlstr.IndexOf("老刘专场", strpoint + 1);
                    if (strpoint == -1) break;
                    try
                    {
                        string thename = htmlstr.Substring(strpoint, 30);

                        int point = htmlstr.LastIndexOf("href", strpoint);
                        string subhtml = htmlstr.Substring(point, strpoint - point);
                        string theurl = CSharpUtility.GetContent(subhtml, "'", "'", 1);
                        if (hasItemMap[theurl] == null)
                        {
                            richTextBox1.AppendText(DateTime.Now.ToShortTimeString() + ":" + thename + "\n" + theurl + "\n");
                            hasItemMap[theurl] = true;
                            hasnew = true;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            if (hasnew)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;
                this.TopMost = true;
                //MessageBox.Show("有新商品啦！！！！！！");
                this.TopMost = false;
            }
            isRunning = false;
        }

        private void 年618老刘专场开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
