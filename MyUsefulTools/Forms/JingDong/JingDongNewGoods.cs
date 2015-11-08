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
    public partial class JingDongNewGoods : Form
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        private string sortField;
        
        private DataTable GetBindData(int _pageSize, int _nowPage, string _sortField)
        {
            string sqlstr = @" WITH OrderTable AS
                                (
                                  SELECT ROW_NUMBER() OVER(ORDER BY "+_sortField+@" ASC) AS RowNumber,*
                                  FROM JingDongNewGoods
                                  where HasRead='否'
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
            binddt.Columns.Add(new DataColumn("Image", typeof(Bitmap)));
            binddt.Columns.Add(new DataColumn("Price", typeof(Bitmap)));
            binddt.Columns.Add(new DataColumn("Description", typeof(string)));
            binddt.Columns.Add(new DataColumn("WebUrl", typeof(string)));
            binddt.Columns.Add(new DataColumn("InsertDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("HasRead", typeof(bool)));
            binddt.Columns.Add(new DataColumn("BeginSaleDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("InterestValue", typeof(string)));

            foreach (JingDongNewGoodsDAO aNewGoods in newGoods)
            {
                DataRow dr = binddt.NewRow();
                dr["ID"] = aNewGoods.ID.ToString();
                dr["GoodsName"] = aNewGoods.Name;
                try
                {
                    dr["Image"] = CSharpUtility.GetImageFromByteArray(aNewGoods.Image);
                    dr["Price"] = CSharpUtility.GetImageFromByteArray(aNewGoods.PriceImg);
                }
                catch (Exception ex)
                { }
                dr["Description"] = aNewGoods.Description;
                dr["WebUrl"] = aNewGoods.WebUrl;
                dr["InsertDate"] = aNewGoods.InsertDate.ToString();
                dr["HasRead"] = aNewGoods.HasRead;
                if (aNewGoods.BeginSaleDate == null) dr["BeginSaleDate"] = "";
                else dr["BeginSaleDate"] = aNewGoods.BeginSaleDate.ToString();
                dr["InterestValue"] = aNewGoods.InterestValue.ToString();
                binddt.Rows.Add(dr);
            }
            return binddt;
        }
        public int GetRecordCount()
        {
            string sqlstr = "select count(*) from [JingDongNewGoods] where HasRead='否';";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            int count = (int)dt.Rows[0][0];
            return count;
        }

        public JingDongNewGoods()
        {
            InitializeComponent();
        }
        private DataTable GetSortFieldData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Rows.Add("产品名称", "Name");
            dt.Rows.Add("上架日期", "BeginSaleDate");
            dt.Rows.Add("添加日期", "InsertDate");
            return dt;
        }
        /// <summary>
        /// 初始化页面各个控件
        /// </summary>
        private void InitFormControls()
        {
            //准备sortField下拉列表的内容
            cb_sortField.DataSource = GetSortFieldData();
            cb_sortField.DisplayMember = "Text";
            cb_sortField.ValueMember = "Value";
            this.sortField = cb_sortField.SelectedValue.ToString();

            pageSelectControl1.DataSource = GetRecordCount();
            dataGridView1.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage, sortField);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
        //页面初始化时
        private void JingDongNewGoods_Load(object sender, EventArgs e)
        {
            InitFormControls();
        }
        //窗口大小改变时
        private void JingDongNewGoods_SizeChanged(object sender, EventArgs e)
        {
            //当窗口大小改变的时候，调整内部控件的大小和位置
            dataGridView1.Height = panel1.Height - 40;
        }
        //获取网页数据
        private void tsbtn_getNewData_Click(object sender, EventArgs e)
        {
            //调用获取网页数据对话框执行相应任务
            Dialog_GetNewData dialog = new Dialog_GetNewData();
            dialog.ShowDialog();
            //对话框关闭后，初始化控件
            InitFormControls();
        }
        /// <summary>
        /// 打开URL配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_openUrlFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //设置外部程序名(记事本用 notepad.exe 计算器用 calc.exe)
            //info.FileName = "winword.exe";
            info.FileName = Constant.JingDongGoodsUrlFilePath;//直接打开对应文件
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
        private void pageSelectControl1_PageChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage, sortField);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);

                if (e.ColumnIndex == 7)
                {//修改“已阅读”状态时
                    bool value = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    JingDongNewGoodsDAO.UpdateHasReadByID(id, value);
                }
                else if (e.ColumnIndex == 9)
                {//为商品设置关注度时
                    string valuestr = dataGridView1.Rows[e.RowIndex].Cells["InterestValue"].Value.ToString();
                    short value = short.Parse(valuestr);
                    JingDongNewGoodsDAO.UpdateInterestValueByID(id, value);
                }
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

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            InitFormControls();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Context.ToString());
        }

        private void tsbtn_getOnsaleDate_Click(object sender, EventArgs e)
        {
            Forms.JingDong.GetGoodsOnsaleDate form_GetGoodsOnsaleDate = new GetGoodsOnsaleDate();
            form_GetGoodsOnsaleDate.Show();
        }

        private void tsbtn_query_Click(object sender, EventArgs e)
        {
            Forms.JingDong.GoodsQuery form_GoodsQuery = new GoodsQuery();
            form_GoodsQuery.Show();
        }

        private void JingDongNewGoods_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form f = (Form)sender;
            f.Dispose();
        }
        //修改排序状态
        private void cb_sortExpress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_sortField.SelectedValue is DataRowView) return;
            this.sortField = cb_sortField.SelectedValue.ToString();

            pageSelectControl1.DataSource = GetRecordCount();
            dataGridView1.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage, sortField);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
        //设置表格字体大小
        private void tscb_gvFontSize_TextChanged(object sender, EventArgs e)
        {
            int fontSize = 10;//默认
            try
            {
                fontSize = Int32.Parse(tscb_gvFontSize.SelectedItem.ToString());
                dataGridView1.RowsDefaultCellStyle.Font = new Font(dataGridView1.RowsDefaultCellStyle.Font.FontFamily, fontSize);
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
/*
--页面大小
declare @PageSize as int = 20
--页面索引 
declare @PageIndex as int = 1;

WITH OrderTable AS
(
  SELECT ROW_NUMBER() OVER(ORDER BY Name ASC) AS RowNumber,*
  FROM JingDongNewGoods
  where HasRead='否'
)
SELECT * FROM OrderTable 
WHERE RowNumber BETWEEN @PageSize*(@PageIndex-1) + 1 AND @PageSize * @PageIndex;
*/