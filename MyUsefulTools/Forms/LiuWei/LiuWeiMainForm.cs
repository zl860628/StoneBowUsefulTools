using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.Utility.HtmlParse;
using MyUsefulTools.DAO;
using DatabaseAccess;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Collections;
using System.Threading;

namespace MyUsefulTools.Forms.LiuWei
{
    public partial class LiuWeiMainForm : Form
    {
        private static Random random = new Random((int)DateTime.Now.ToBinary());
        private Size oldSize;
        private DataCapture dataCapture = null;

        public LiuWeiMainForm()
        {
            InitializeComponent();
        }

        #region 控件事件方法

        private void LiuWeiMainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormControls();
            //为DateTime控件设置初始值，定位当前日期的3天前
            time_end.Value = DateTime.Now.AddDays(-3);
            oldSize = this.Size;
        }

        private void menu_updateData_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync("menu_updateData");
        }

        private void menu_getHotData_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync("menu_getHotData");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //重置统计信息
            lb_dealCount.Text = "0";
            lb_insertCount.Text = "0";
            lb_updateCount.Text = "0";
            if (((string)e.Argument).Equals("menu_updateData"))
            {
                GetNewData();
            }
            else if (((string)e.Argument).Equals("menu_getHotData"))
            {
                GetMostDownloadedData();
            }
            else return;
            MessageBox.Show("获取信息成功");
        }

        /// <summary>
        /// 当数据源改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagv_unread_DataSourceChanged(object sender, EventArgs e)
        {
            //修改每行表头的颜色为随机色
            for (int i = 0; i < datagv_unread.Rows.Count; i++)
            {
                //datagv_unread.Rows[i].HeaderCell.Style.SelectionBackColor = Color.FromArgb(random.Next());
            }
        }
        private void datagv_unread_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                //判断鼠标是否在单元格内
                bool mouseOver = e.CellBounds.Contains(datagv_unread.PointToClient(Cursor.Position));
                //设置线性画刷
                LinearGradientBrush brush = new LinearGradientBrush(
                    e.CellBounds,
                    mouseOver ? Color.Yellow : Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)), //鼠标在单元格中的时候，加亮显示
                    Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)),
                    LinearGradientMode.Horizontal);

                using (brush)
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Width -= 1;
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }
        /// <summary>
        /// 双击鼠标时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagv_unread_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击的时候，跳转到对应的网址
            string weburl = datagv_unread.Rows[e.RowIndex].Cells["URL"].Value.ToString().Trim();
            if (!weburl.Equals(""))
            {
                System.Diagnostics.Process.Start(weburl);
            }
        }
        /// <summary>
        /// 分页控件改变页码时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageSelectControl1_PageChanged(object sender, EventArgs e)
        {
            //重新绑定数据
            datagv_unread.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage);
            datagv_unread.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            //这句话会修改阅读状态，所以要放在后面
            cb_allSelect.Checked = false;
        }
        /// <summary>
        /// 单元格值改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagv_unread_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                int id = Convert.ToInt32(datagv_unread.Rows[e.RowIndex].Cells["ID"].Value);
                bool value = Convert.ToBoolean(datagv_unread.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                LiuWeiSpaceItem.UpdateHasReadByID(id, value);
            }
        }

        private void cb_allSelect_CheckedChanged(object sender, EventArgs e)
        {
            bool select = cb_allSelect.Checked;
            for (int i = 0; i < datagv_unread.Rows.Count; i++)
            {
                datagv_unread.Rows[i].Cells[6].Value = select;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string key = txt_search.Text.Trim();
            if (key.Equals(""))
            {
                MessageBox.Show("请输入关键字");
                return;
            }
            try
            {
                DataTable binddt = GetSearchData(txt_search.Text.Trim(), 300);
                datagv_unread.DataSource = binddt;
                datagv_unread.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 访问“六维空间”网站，获取需要的新的数据
        /// </summary>
        private void GetNewData()
        {
            //获取新的数据
            DateTime? endDateTime = time_end.Value;
            int pageCount = 10;
            try
            {
                pageCount = Int32.Parse(tb_pageCount.Text.Trim());
            }
            catch (Exception ex)
            { }
            LiuWeiSpaceItemParser parser = new LiuWeiSpaceItemParser(IncreaseDealCount);
            try
            {
                DataTable itemdata = parser.GetItemDataTable(pageCount, endDateTime);
                for (int i = 0; i < itemdata.Rows.Count; i++)
                {
                    DataRow dr = itemdata.Rows[i];
                    LiuWeiSpaceItem item = new LiuWeiSpaceItem(dr["Title"].ToString());
                    if (!item.IsRecord)
                    {//数据库中不含有当前记录时
                        item = new LiuWeiSpaceItem()
                        {
                            Title = dr["Title"].ToString(),
                            SeedCount = (int)dr["SeedCount"],
                            Size = dr["Size"].ToString(),
                            URL = dr["URL"].ToString(),
                            CreateDate = Convert.ToDateTime(dr["CreateDate"]),
                            HasRead = false
                        };
                        item.InsertNewRecord();
                        IncreaseInsertRecordCount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 更新下载数最多的资源项目，即热门资源
        /// </summary>
        private void GetMostDownloadedData()
        {
            dataCapture = new DataCapture(IncreaseDealCount);
            dataCapture.PrepareStart();
            //获取新的数据
            int pageCount = 10;
            try
            {
                pageCount = Int32.Parse(tb_pageCount.Text.Trim());
            }
            catch (Exception ex)
            { }

            List<string> urls = new List<string>();
            for (int i = 0; i < pageCount; i++)
            {
                urls.Add("http://bt.neu6.edu.cn/forumdisplay.php?fid=2&orderby=downloaded&page=" + i.ToString());
            }
            Thread downloadWebsitesThread = new Thread(dataCapture.DownloadWebsites);
            downloadWebsitesThread.Start(urls);

            Thread parserThread = new Thread(dataCapture.ParseWebsites);
            parserThread.Start();

            while (true)
            {//将获取到的数据持久化
                DataTable itemdata = dataCapture.GetOneParsedDatatable();
                if (itemdata != null)
                {
                    for (int i = 0; i < itemdata.Rows.Count; i++)
                    {
                        DataRow dr = itemdata.Rows[i];
                        LiuWeiSpaceItem item = new LiuWeiSpaceItem(dr["Title"].ToString());
                        if (!item.IsRecord)
                        {//数据库中不含有当前记录时
                            item = new LiuWeiSpaceItem()
                            {
                                Title = dr["Title"].ToString(),
                                SeedCount = (int)dr["SeedCount"],
                                Size = dr["Size"].ToString(),
                                URL = dr["URL"].ToString(),
                                CreateDate = Convert.ToDateTime(dr["CreateDate"]),
                                HasRead = false
                            };
                            item.InsertNewRecord();
                            IncreaseInsertRecordCount();
                        }
                        else if ((int)dr["SeedCount"] > item.SeedCount)
                        {
                            item.UpdateSeedCount((int)dr["SeedCount"]);
                            IncreaseUpdateRecordCount();
                        }
                    }
                }
                else if (dataCapture.ParseWebsitesEnd) break;
                else
                {
                    //如果暂时没有数据，等待一段时间
                    Thread.Sleep(500);
                }
            }
        }
        private void IncreaseDealCount(ArrayList _parasList)
        {
            int count = Int32.Parse(lb_dealCount.Text) + 1;
            lb_dealCount.Text = count.ToString();
            //更换文字颜色
            Color randomColor = Color.FromArgb(random.Next());
            lb_dealCount.ForeColor = randomColor;
        }
        private void IncreaseInsertRecordCount()
        {
            int count = Int32.Parse(lb_insertCount.Text) + 1;
            lb_insertCount.Text = count.ToString();
            //更换文字颜色
            Color randomColor = Color.FromArgb(random.Next());
            lb_insertCount.ForeColor = randomColor;
        }
        private void IncreaseUpdateRecordCount()
        {
            int count = Int32.Parse(lb_updateCount.Text) + 1;
            lb_updateCount.Text = count.ToString();
            //更换文字颜色
            Color randomColor = Color.FromArgb(random.Next());
            lb_updateCount.ForeColor = randomColor;
        }

        /// <summary>
        /// 获取表格绑定分页数据
        /// </summary>
        /// <param name="_pageSize"></param>
        /// <param name="_nowPage"></param>
        /// <returns></returns>
        private DataTable GetBindData(int _pageSize, int _nowPage)
        {
            int beforecount = _pageSize * (_nowPage - 1);
            string sqlstr = @"   
with OrderTable as
(
    select ROW_NUMBER() over(order by seedcount desc) as RowNumber,*
    from LiuWeiSpaceItem where HasRead=0
)
select * from OrderTable
where RowNumber between @PageSize*(@PageIndex-1)+1 and @PageSize*@PageIndex;";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("PageSize", SqlDbType.Int);
            paras[0].Value = _pageSize;
            paras[1] = new SqlParameter("PageIndex", SqlDbType.Int);
            paras[1].Value = _nowPage;
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            LiuWeiSpaceItem[] newItems = new LiuWeiSpaceItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newItems[i] = new LiuWeiSpaceItem((int)dt.Rows[i]["ID"]);
            }
            DataTable binddt = new DataTable();
            binddt.Columns.Add(new DataColumn("ID", typeof(string)));
            binddt.Columns.Add(new DataColumn("Title", typeof(string)));
            binddt.Columns.Add(new DataColumn("Size", typeof(string)));
            binddt.Columns.Add(new DataColumn("SeedCount", typeof(string)));
            binddt.Columns.Add(new DataColumn("URL", typeof(string)));
            binddt.Columns.Add(new DataColumn("CreateDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("HasRead", typeof(bool)));
            foreach (LiuWeiSpaceItem aNewItem in newItems)
            {
                DataRow dr = binddt.NewRow();
                dr["ID"] = aNewItem.ID.ToString();
                dr["Title"] = aNewItem.Title;
                dr["Size"] = aNewItem.Size;
                dr["SeedCount"] = aNewItem.SeedCount.ToString();
                dr["URL"] = aNewItem.URL;
                dr["CreateDate"] = aNewItem.CreateDate.ToShortDateString();
                dr["HasRead"] = aNewItem.HasRead;
                binddt.Rows.Add(dr);
            }
            return binddt;
        }
        public int GetRecordCount()
        {
            string sqlstr = "select count(*) from [LiuWeiSpaceItem] where HasRead=0;";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            int count = (int)dt.Rows[0][0];
            return count;
        }

        private DataTable GetSearchData(string _key, int _maxItemCount)
        {
            string sqlstr = @"
select top {0} * from LiuWeiSpaceItem where HasRead=0 and Title like '%{1}%' order by Title
";
            sqlstr = string.Format(sqlstr, _maxItemCount.ToString(), _key);
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            if (dt.Rows.Count > _maxItemCount)
            {
                MessageBox.Show(string.Format("搜索结果过多（{0}条结果），无法显示", dt.Rows.Count));
            }

            LiuWeiSpaceItem[] newItems = new LiuWeiSpaceItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newItems[i] = new LiuWeiSpaceItem((int)dt.Rows[i]["ID"]);
            }

            DataTable binddt = new DataTable();
            binddt.Columns.Add(new DataColumn("ID", typeof(string)));
            binddt.Columns.Add(new DataColumn("Title", typeof(string)));
            binddt.Columns.Add(new DataColumn("Size", typeof(string)));
            binddt.Columns.Add(new DataColumn("SeedCount", typeof(string)));
            binddt.Columns.Add(new DataColumn("URL", typeof(string)));
            binddt.Columns.Add(new DataColumn("CreateDate", typeof(string)));
            binddt.Columns.Add(new DataColumn("HasRead", typeof(bool)));
            foreach (LiuWeiSpaceItem aNewItem in newItems)
            {
                DataRow dr = binddt.NewRow();
                dr["ID"] = aNewItem.ID.ToString();
                dr["Title"] = aNewItem.Title;
                dr["Size"] = aNewItem.Size;
                dr["SeedCount"] = aNewItem.SeedCount.ToString();
                dr["URL"] = aNewItem.URL;
                dr["CreateDate"] = aNewItem.CreateDate.ToShortDateString();
                dr["HasRead"] = aNewItem.HasRead;
                binddt.Rows.Add(dr);
            }
            return binddt;
        }
        /// <summary>
        /// 初始化页面各个控件
        /// </summary>
        private void InitFormControls()
        {
            pageSelectControl1.DataSource = GetRecordCount();
            datagv_unread.DataSource = GetBindData(pageSelectControl1.PageSize, pageSelectControl1.NowPage);
            datagv_unread.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
        #endregion
        /// <summary>
        /// 排序后执行的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagv_unread_Sorted(object sender, EventArgs e)
        {
            //调整行高为自适应
            datagv_unread.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void LiuWeiMainForm_Resize(object sender, EventArgs e)
        {
            int datagvWidth = datagv_unread.Width + this.Size.Width - oldSize.Width;
            int datagvHeight = datagv_unread.Height + this.Size.Height - oldSize.Height;

            if (datagvWidth > 0 && datagvHeight > 0)
            {   //窗体最小化以后会导致datagv的长高为0，因为这两个值不能为负，产生错误
                //不单单是最小化，当窗口大小过小的时候也会产生这个问题
                //重新布局主要控件
                groupBox1.Location = new Point(groupBox1.Location.X + this.Size.Width - oldSize.Width, groupBox1.Location.Y);
                //窗口大小变了多少，datagv就变多少
                datagv_unread.Width = datagvWidth;
                datagv_unread.Height = datagvHeight;

                oldSize = this.Size;
            }
        }

    }
}
