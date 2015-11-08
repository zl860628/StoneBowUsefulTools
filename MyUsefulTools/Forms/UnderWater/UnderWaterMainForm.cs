using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.DAO;
using DatabaseAccess;

namespace MyUsefulTools.Forms.UnderWater
{
    public partial class UnderWaterMainForm : Form
    {
        public UnderWaterMainForm()
        {
            InitializeComponent();
        }

        private void UnderWaterMainForm_Load(object sender, EventArgs e)
        {
            //控件绑定
            pycb_myfish.DataSource = GetAllFishName();
            pycb_friendfish.DataSource = GetAllFishName();
            pycb_babyfish.DataSource = GetAllFishName();
            dataGridView1.DataSource = GetGridViewBindData();
        }
        /// <summary>
        /// 添加新的交配记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_subnew_Click(object sender, EventArgs e)
        {
            string selfFishKindName = pycb_myfish.Text;
            string friendFishKindName = pycb_friendfish.Text;
            string babyFishKindName = pycb_babyfish.Text;
            bool selfIsZhen = cb_myfishisZhen.Checked;
            bool friendIsZhen = cb_friendfishisZhen.Checked;
            bool babyIsZhen = cb_babyfishisZhen.Checked;
            FishAmphimixis fa = new FishAmphimixis();
            fa.SetProperties(
                new UnderWaterFishInfo(selfFishKindName), selfIsZhen,
                new UnderWaterFishInfo(friendFishKindName), friendIsZhen,
                new UnderWaterFishInfo(babyFishKindName), babyIsZhen,
                DateTime.Now);
            fa.InsertNewRecord();
            //重新绑定表格控件
            dataGridView1.DataSource = GetGridViewBindData();
            //将焦点移动到第一个需要输入的地方
            pycb_myfish.Focus();
        }
        /// <summary>
        /// 得到所有的小鱼的名称
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllFishName()
        {
            DataTable dt = UnderWaterFishInfo.GetAllRecordDataTable();
            List<string> fishNameList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fishNameList.Add(dt.Rows[i]["KindName"].ToString());
            }
            return fishNameList;
        }

        private DataTable GetGridViewBindData()
        {
            string sqlstr = "select top 20 * from FishAmphimixis order by InsertDate desc";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            //将“真”加入到小鱼名称中
            List<FishAmphimixis> amphimixisRecords = FishAmphimixis.GetEntitiesFromDataTable(dt);
            DataTable binddt = new DataTable();
            binddt.Columns.Add(new DataColumn("ID", typeof(int)));
            binddt.Columns.Add(new DataColumn("SelfFishName", typeof(string)));
            binddt.Columns.Add(new DataColumn("FriendFishName", typeof(string)));
            binddt.Columns.Add(new DataColumn("BabyFishName", typeof(string)));
            binddt.Columns.Add(new DataColumn("InsertDate", typeof(DateTime)));
            foreach (FishAmphimixis fa in amphimixisRecords)
            {
                DataRow dr = binddt.NewRow();
                string selfFishName = fa.SelfFishKind.KindName;
                if (fa.SelfIsZhen) selfFishName = "真·" + selfFishName;
                string friendFishName = fa.FriendFishKind.KindName;
                if (fa.FriendIsZhen) friendFishName = "真·" + friendFishName;
                string babyFishName = fa.BabyFishKind.KindName;
                if (fa.BabyIsZhen) babyFishName = "真·" + babyFishName;

                dr["ID"] = fa.ID;
                dr["SelfFishName"] = selfFishName;
                dr["FriendFishName"] = friendFishName;
                dr["BabyFishName"] = babyFishName;
                dr["InsertDate"] = fa.InsertDate;
                binddt.Rows.Add(dr);
            }
            return binddt;
        }
        //删除选中行
        private void btn_del_Click(object sender, EventArgs e)
        {
            int delRowCount = dataGridView1.SelectedRows.Count;
            DialogResult dialogResult = MessageBox.Show(String.Format("确定要删除{0}条记录吗？", delRowCount), "警告", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            for (int i = 0; i < delRowCount; i++)
            {
                int delid = (int)dataGridView1.SelectedRows[i].Cells["ID"].Value;
                FishAmphimixis.DeleteRecordByID(delid);
            }
            dataGridView1.DataSource = GetGridViewBindData();
        }
    }
}
