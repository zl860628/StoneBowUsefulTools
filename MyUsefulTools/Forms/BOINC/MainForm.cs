using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.Utility.HtmlParse;

namespace MyUsefulTools.Forms.BOINC
{
    public partial class MainForm : Form
    {
        //用来在界面上显示的信息
        public static string ShowMessage = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void 获取资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer_UpdateView.Start();
            //得到点击的按钮的名称
            ToolStripMenuItem control = (ToolStripMenuItem)sender;
            string projectNames = control.Name;
            backwork_GetAllRecord.RunWorkerAsync(projectNames);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (dgv_TaskRecord.DataSource == null) return;

            DataTable dt = (DataTable)dgv_TaskRecord.DataSource;
            int newCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                DAO.BOINCTaskRecord newRecord = new MyUsefulTools.DAO.BOINCTaskRecord(dr);
                if (!newRecord.IsRecord)
                {
                    newRecord.InsertNewRecord();
                    newCount++;
                }
            }
            MessageBox.Show(string.Format("共有{0}条记录添加", newCount));
        }
        /// <summary>
        /// 获取给定参数表示的项目的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backwork_GetAllRecord_DoWork(object sender, DoWorkEventArgs e)
        {
            BoincInfoParser parser = new BoincInfoParser();
            DataTable temptable = BoincInfoParser.GetInitDatatable();
            string projectNames = e.Argument.ToString();
            if (projectNames.Contains("所有"))
                projectNames = "CAS+PrimeGrid+WUProp+MilkyWay+Rosetta+WCG+SETI+Einstein";
            if (projectNames.Contains("CAS"))
                temptable.Merge(parser.GetCasHomeInfo());
            if (projectNames.Contains("PrimeGrid"))
                temptable.Merge(parser.GetPrimeGridInfo());
            if (projectNames.Contains("WUProp"))
                temptable.Merge(parser.GetWUPropHomeInfo());
            if (projectNames.Contains("MilkyWay"))
                temptable.Merge(parser.GetMilkyWayInfo());
            if (projectNames.Contains("Rosetta"))
                temptable.Merge(parser.GetRosettaInfo());
            if (projectNames.Contains("SETI"))
                temptable.Merge(parser.GetSETIHomeInfo());
            if (projectNames.Contains("WCG"))
                temptable.Merge(parser.GetWCGInfo());
            if (projectNames.Contains("Einstein"))
                temptable.Merge(parser.GetEinsteinInfo());
            BoincInfoParser.ShowMessage = "资料获取完毕";
            //调整列宽
            dgv_TaskRecord.BeginInvoke((MethodInvoker)delegate()
            {
                dgv_TaskRecord.DataSource = temptable;
                dgv_TaskRecord.AutoResizeColumns();
                timer_UpdateView.Stop();
            });
        }

        private void timer_UpdateView_Tick(object sender, EventArgs e)
        {
            label1.Text = BoincInfoParser.ShowMessage;
        }

        private void 打开积分信息系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditForm creditForm = new CreditForm();
            creditForm.Show();
        }

        private void 添加WCG记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertWCGItemForm insertForm = new InsertWCGItemForm();
            insertForm.ShowDialog();
        }
    }
}
