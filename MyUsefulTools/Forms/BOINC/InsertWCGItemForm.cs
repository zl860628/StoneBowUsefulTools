using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.DAO;

namespace MyUsefulTools.Forms.BOINC
{
    public partial class InsertWCGItemForm : Form
    {

        private string projectName = "World Community Grid";
        public InsertWCGItemForm()
        {
            InitializeComponent();
            InitControllers();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControllers()
        { 
            //初始化程序下拉列表
            cbb_Application.DataSource = BOINCTaskRecord.GetApplicationNames(projectName);
            //初始化计算机名下拉列表
            cbb_ComputerID.DataSource = BOINCTaskRecord.GetComputerNames(projectName);
        }
        private DateTime parseToDateTime(string _timestr, string _errmessage)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                dt = DateTime.Parse(_timestr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_errmessage);
            }
            return dt;
        }
        private float parseToFloat(string _floatstr, string _errmessage, float _errvalue)
        {
            float f = _errvalue;
            try
            {
                f = float.Parse(_floatstr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_errmessage);
            }
            return f;
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            //获取数据
            string itemName = txt_ItemName.Text.Trim();
            string computerID = cbb_ComputerID.Text.Trim();
            string receiveTimeStr = txt_ReceiveTime.Text.Trim();
            string upTimeStr = txt_UpTime.Text.Trim();
            string creditStr = txt_Credit.Text.Trim();
            string cpuTimeStr = txt_CPUTime.Text.Trim();
            string application = cbb_Application.Text.Trim();
            DateTime receiveTime = parseToDateTime(receiveTimeStr, "---项目接收时间---填写错误");
            DateTime upTime = parseToDateTime(upTimeStr, "---项目提交时间---填写错误");
            float cpuTime = parseToFloat(cpuTimeStr, "---项目CPU用时---填写错误", -1F);
            float credit = parseToFloat(creditStr, "---项目获得积分---填写错误", -1F);
            if (receiveTime.Equals(DateTime.MinValue) ||
                upTime.Equals(DateTime.MinValue) ||
                cpuTime.Equals(-1F) || credit.Equals(-1F))
            {//当有错误的时候
                return;
            }
            BOINCTaskRecord record = new BOINCTaskRecord(projectName, itemName);
            if (record.IsRecord)
            {
                MessageBox.Show("任务已存在");
                return;
            }
            record.ProjectName = projectName;
            record.TaskName = itemName;
            record.WuID = "";
            record.ComputerID = computerID;
            record.SentTime = receiveTime;
            record.ReportedTime = upTime;
            record.RunTime = 0F;
            record.CPUTime = cpuTime * 3600;//小时转化为秒
            record.Credit = credit;
            record.Application = application;
            record.InsertNewRecord();
            MessageBox.Show("添加成功");
        }
        
        /// <summary>
        /// 判断此任务是否已经存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ItemName_TextChanged(object sender, EventArgs e)
        {
            BOINCTaskRecord record = new BOINCTaskRecord(projectName, txt_ItemName.Text.Trim());
            if (record.IsRecord)
            {
                label_warning.Text = "当前任务已经存在";
                btn_submit.Enabled = false;
            }
            else
            {
                label_warning.Text = "";
                btn_submit.Enabled = true;
            }
        }
    }
}
