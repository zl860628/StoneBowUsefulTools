using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DatabaseAccess;
using MyUsefulTools.DAO;
using MyUsefulTools.Utility;
using MySpace.Utils;
using System.Data.SqlClient;

namespace MyUsefulTools.Forms.JingDong
{
    public partial class GoodsQuery : Form
    {
        public GoodsQuery()
        {
            InitializeComponent();
        }
        private DataTable GetBindData(int _pageSize, int _nowPage)
        {
            string sqlstr = @" WITH OrderTable AS
                                (
                                  SELECT ROW_NUMBER() OVER(ORDER BY Name ASC) AS RowNumber,*
                                  FROM JingDongNewGoods
                                  where InterestValue>0
                                )
                                SELECT * FROM OrderTable 
                                WHERE RowNumber BETWEEN @PageSize*(@PageIndex-1) + 1 AND @PageSize * @PageIndex;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("PageSize", SqlDbType.Int);
            paras[0].Value = _pageSize;
            paras[1] = new SqlParameter("PageIndex", SqlDbType.Int);
            paras[1].Value = _nowPage;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            JingDongNewGoodsDAO[] newGoods = new JingDongNewGoodsDAO[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newGoods[i] = new JingDongNewGoodsDAO((int)dt.Rows[i]["ID"]);
            }
            DataTable binddt = new DataTable();
            binddt.Columns.Add(new DataColumn("ID", typeof(string)));
            binddt.Columns.Add(new DataColumn("GoodsName", typeof(string)));
            binddt.Columns.Add(new DataColumn("Description", typeof(string)));
            binddt.Columns.Add(new DataColumn("Image", typeof(Bitmap)));
            binddt.Columns.Add(new DataColumn("Price", typeof(Bitmap)));
            binddt.Columns.Add(new DataColumn("WebUrl", typeof(string)));
            binddt.Columns.Add(new DataColumn("BeginSaleDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("InsertDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("InterestValue", typeof(string)));

            foreach (JingDongNewGoodsDAO aNewGoods in newGoods)
            {
                DataRow dr = binddt.NewRow();
                dr["ID"] = aNewGoods.ID.ToString();
                dr["GoodsName"] = aNewGoods.Name;
                dr["Image"] = CSharpUtility.GetImageFromByteArray(aNewGoods.Image);
                dr["Price"] = CSharpUtility.GetImageFromByteArray(aNewGoods.PriceImg);
                dr["Description"] = aNewGoods.Description;
                dr["WebUrl"] = aNewGoods.WebUrl;
                dr["InsertDate"] = aNewGoods.InsertDate.ToString();
                if (aNewGoods.BeginSaleDate == null) dr["BeginSaleDate"] = "";
                else dr["BeginSaleDate"] = aNewGoods.BeginSaleDate.ToString();
                dr["InterestValue"] = aNewGoods.InterestValue.ToString();
                binddt.Rows.Add(dr);
            }
            return binddt;
        }
        private int GetRecordCount()
        {
            string sqlstr = "select count(*) from [JingDongNewGoods] where InterestValue>0;";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            int count = (int)dt.Rows[0][0];
            return count;
        }

        private void pageSelectControl1_PageChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //双击的时候，跳转到对应的网址
                string weburl = dataGridView1.Rows[e.RowIndex].Cells["WebUrl"].Value.ToString().Trim();
                if (!weburl.Equals(""))
                {
                    System.Diagnostics.Process.Start(weburl);
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Context.ToString());
        }
        //我搜我搜我搜搜搜
        private void btn_search_Click(object sender, EventArgs e)
        {
            pageSelectControl1.DataSource = GetRecordCount();
            dataGridView1.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
    }
}
