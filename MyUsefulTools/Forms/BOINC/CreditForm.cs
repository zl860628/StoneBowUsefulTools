using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MyUsefulTools.Utility.HtmlParse;
using System.Reflection;
using MyUsefulTools.DAO;
using DatabaseAccess;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyUsefulTools.Forms.BOINC
{
    public partial class CreditForm : Form
    {
        private static DataTable bindDataTable = null;
        
        private void ClearEvent(BackgroundWorker backworker, string eventname)
        {
            if (backworker == null) return;
            if (string.IsNullOrEmpty(eventname)) return;

            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(BackgroundWorker);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(backworker, null);
            FieldInfo fieldInfo = (typeof(BackgroundWorker)).GetField("doWorkKey", mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(backworker)];
            
            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);

            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(backworker, dx);
        }

        private void InitializeField()
        {
            bindDataTable = null;
        }
        public CreditForm()
        {
            InitializeComponent();
            InitializeField();
            timer1.Start();
        }

        private void CreditForm_SizeChanged(object sender, EventArgs e)
        {
            tabControl1.Height = this.Height - 80;
        }

        private void getNewCreditData(object sender, DoWorkEventArgs e)
        {
            DataTable datadt = getInitDataTable();
            
            InternetTransport transportor = new InternetTransport("BOINCCredit");
            string htmlstr = transportor.GetAndGetHTML(@"http://boinc.netsoft-online.com/get_user.php?cpid=a5cd25f6b5afcddf6b694a8c6f335f31",
                null, Encoding.Default);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(htmlstr);
            XmlNodeList nodelist = xmldoc.SelectNodes("user/project");
            //添加总分数内容
            XmlNode rootnode = xmldoc.SelectSingleNode("user");
            DataRow dr = datadt.NewRow();
            dr["ItemName"] = "总分";
            dr["InsertDate"] = DateTime.Now;
            dr["TotalCredit"] = float.Parse(rootnode.SelectSingleNode("total_credit").InnerText);
            dr["RACCredit"] = float.Parse(rootnode.SelectSingleNode("expavg_credit").InnerText);
            dr["TotalRank"] = Int32.Parse(rootnode.SelectSingleNode("world_rank_total_credit").InnerText);
            dr["RACRank"] = Int32.Parse(rootnode.SelectSingleNode("world_rank_expavg_credit").InnerText);
            dr["ComputerCount"] = 0;
            dr["ActiveComputerCount"] = 0;
            datadt.Rows.Add(dr);
            //添加子项目的内容
            foreach (XmlNode node in nodelist)
            {
                dr = datadt.NewRow();
                dr["ItemName"] = node.SelectSingleNode("name").InnerText;
                dr["InsertDate"] = DateTime.Now;
                dr["TotalCredit"] = float.Parse(node.SelectSingleNode("total_credit").InnerText);
                dr["RACCredit"] = float.Parse(node.SelectSingleNode("expavg_credit").InnerText);
                dr["TotalRank"] = Int32.Parse(node.SelectSingleNode("project_rank_total_credit").InnerText);
                dr["RACRank"] = Int32.Parse(node.SelectSingleNode("project_rank_expavg_credit").InnerText);
                dr["ComputerCount"] = Int32.Parse(node.SelectSingleNode("computer_count").InnerText);
                dr["ActiveComputerCount"] = Int32.Parse(node.SelectSingleNode("active_computer_count").InnerText);
                datadt.Rows.Add(dr);
            }
            CreditForm.bindDataTable = datadt;
            MessageBox.Show("数据获取完成");
        }

        private DataTable getInitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("InsertDate", typeof(DateTime));
            dt.Columns.Add("TotalCredit", typeof(float));
            dt.Columns.Add("RACCredit", typeof(float));
            dt.Columns.Add("TotalRank", typeof(int));
            dt.Columns.Add("RACRank", typeof(int));
            dt.Columns.Add("ComputerCount", typeof(int));
            dt.Columns.Add("ActiveComputerCount", typeof(int));
            return dt;
        }
        private void btn_获取新数据_Click(object sender, EventArgs e)
        {
            InitializeField();
            ClearEvent(backgroundWorker1, "DoWork");
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getNewCreditData);
            backgroundWorker1.RunWorkerAsync();
        }
        /// <summary>
        /// 根据保存策略，保存获取到的数据
        /// </summary>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //获取表格中的数据
            if (dataGridView1.DataSource == null) return;

            DataTable dt = (DataTable)dataGridView1.DataSource;
            int newCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                DAO.BOINCCredit newRecord = new DAO.BOINCCredit(dr);
                DAO.BOINCCredit lastRecord = DAO.BOINCCredit.GetLastRecord(newRecord.ItemName);
                //根据规则确定是否添加记录
                bool savable = false;
                if (lastRecord == null)
                    savable = true;
                else
                {
                    if (newRecord.TotalCredit == lastRecord.TotalCredit)
                    {
                        //项目总分相同时，最少需要间隔10天
                        if (newRecord.InsertDate.CompareTo(lastRecord.InsertDate.AddDays(10)) > 0)
                        {
                            savable = true;
                        }
                    }
                    else
                    { 
                        //项目总分不相同时，最少需要间隔12小时
                        if (newRecord.InsertDate.CompareTo(lastRecord.InsertDate.AddHours(12)) > 0)
                        {
                            savable = true;
                        }
                    }
                }
                if (savable)
                {
                    newRecord.InsertNewRecord();
                    newCount++;
                }
            }
            MessageBox.Show(string.Format("共有{0}条记录添加", newCount));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bindDataTable != null)
            {
                this.dataGridView1.DataSource = bindDataTable;
                timer1.Stop();
                bindDataTable = null;
            }
        }
        
        //切换选项卡时进行的操作
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
            if (tabControl1.SelectedIndex == 1)
            {
                
                //按照默认配置对图标进行更新
                /*
                string sqlstr = "select * from BOINCCredit where ItemName='milkyway@home';";
                DataTable dt = DBManager.SelectRecords(sqlstr, null);
                chart_project.DataSource = dt;
                chart_project.Series["Series1"].XValueMember = "InsertDate";
                chart_project.Series["Series1"].YValueMembers = "TotalCredit";
                chart_project.DataBind();
                */
                chart_project.Series.Clear();
                string sqlstr = "select * from BOINCCredit where ItemName<>'总分'";
                DataTable dt = DBManager.SelectRecords(sqlstr, null);
                chart_project.DataBindCrossTable(dt.DefaultView, "ItemName", "InsertDate", "TotalCredit", "ToolTip=ItemName");
                foreach (Series series in chart_project.Series)
                {
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 3;
                    series.IsVisibleInLegend = false;
                }
                chart_project.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
                chart_project.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
                chart_project.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
                chart_project.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
                chart_project.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
                chart_project.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            }
        }

    }
}