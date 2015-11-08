using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseAccess;
using MyUsefulTools.DAO;

namespace MyUsefulTools.Forms.JingDong
{
    public partial class GetGoodsOnsaleDate : Form
    {
        public static int GetOnsaleDateCount = 0;
        
        public GetGoodsOnsaleDate()
        {
            InitializeComponent();
        }

        private void timer_refreshForm_Tick(object sender, EventArgs e)
        {
            lb_getOnsaleCount.Text = GetOnsaleDateCount.ToString();
        }

        private void btn_getAll_Click(object sender, EventArgs e)
        {
            timer_refreshForm.Start();
            bgwork_getOnsaleDate.RunWorkerAsync();
        }
        //停止获取
        private void btn_stopGetDate_Click(object sender, EventArgs e)
        {
            bgwork_getOnsaleDate.CancelAsync();
            timer_refreshForm.Stop();
        }

        private void bgwork_getOnsaleDate_DoWork(object sender, DoWorkEventArgs e)
        {
            string sqlstr = "select * from JingDongNewGoods where BeginSaleDate is null";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int id = (int)dr["ID"];
                string url = dr["WebUrl"].ToString().Trim();
                if (url.Equals("")) continue;

                string html = Utility.HtmlParse.HTMLCommonParser.GetAndGetHTML(url, null);
                string content = Utility.CSharpUtility.GetContent(html, "<li>上架时间：", "</li>", 1);
                try
                {
                    DateTime onsaleDate = DateTime.Parse(content);
                    JingDongNewGoodsDAO.UpdateBeginSaleDateByID(id, onsaleDate);
                    Forms.JingDong.GetGoodsOnsaleDate.GetOnsaleDateCount++;
                }
                catch (Exception ex)
                { }
            }
        }

    }
}
