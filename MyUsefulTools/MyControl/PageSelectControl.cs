using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyUsefulTools.MyControl
{
    public partial class PageSelectControl : UserControl
    {
        private int nowPage = 1;
        private int totalPage = 1;
        private int pageSize = 20;
        private Object dataSource = null;

        public delegate void PageChangedEventHandler(object sender, EventArgs e);//事件所需的委托
        //当颜色改变时触发事件
        public event PageChangedEventHandler PageChanged;//定义一个ColorChanged事件
        //事件触发方法
        protected virtual void OnPageChanged(EventArgs e)
        {
            //改变页码的显示
            mtxt_now.Text = nowPage.ToString();

            if (PageChanged != null)
            {//判断事件是否为空
                PageChanged(this, e);//触发事件
            }
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int NowPage
        {
            get { return nowPage; }
            set { nowPage = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get { return totalPage; }
        }
        /// <summary>
        /// 每页记录数
        /// </summary>
        [Description("每页记录数")]　//显示在属性设计视图中的描述
        [DefaultValue(20)]//给予初始值
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public Object DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                DealDataSource();
            }
        }

        public PageSelectControl()
        {
            InitializeComponent();
            mtxt_now.Text = nowPage.ToString();
        }

        /// <summary>
        /// 内部处理DataSource的方法
        /// </summary>
        private void DealDataSource()
        {
            if (dataSource == null) return;

            int totalcount = 0;
            //根据DataSource的类型不同，完成不同的处理
            if (dataSource.GetType().Equals(typeof(int)))
            {
                totalcount = (int)dataSource;
            }
            else if (dataSource.GetType().Equals(typeof(DataTable)))
            { //DataTable的时候
                DataTable dt = (DataTable)dataSource;
                totalcount = dt.Rows.Count;
            }
            double totalPageFloat = Math.Ceiling(1.0 * totalcount / pageSize);
            totalPage = (int)totalPageFloat;
            lb_total.Text = totalPage.ToString();
            nowPage = 1;
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            nowPage = 1;
            OnPageChanged(new EventArgs());
        }

        private void btn_pre_Click(object sender, EventArgs e)
        {
            if (nowPage > 1)
            {
                nowPage--;
            }
            OnPageChanged(new EventArgs());
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (nowPage < totalPage)
            {
                nowPage++;
            }
            OnPageChanged(new EventArgs());
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            nowPage = totalPage;
            OnPageChanged(new EventArgs());
        }
    }
}
/*
[Description("当前页码")]　//显示在属性设计视图中的描述
[DefaultValue(typeof(Color), "Black")]//给予初始值
*/