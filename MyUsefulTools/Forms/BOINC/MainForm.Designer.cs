namespace MyUsefulTools.Forms.BOINC
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取所有项目新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取CAS新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取WUProp新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取PrimeGrid新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取MilkyWay新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取Rosetta新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取SETI新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加WCG记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取Einstein新资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开积分信息系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_TaskRecord = new System.Windows.Forms.DataGridView();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WuID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComputerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPUTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Save = new System.Windows.Forms.Button();
            this.backwork_GetAllRecord = new System.ComponentModel.BackgroundWorker();
            this.timer_UpdateView = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TaskRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.命令ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 命令ToolStripMenuItem
            // 
            this.命令ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取所有项目新资料ToolStripMenuItem,
            this.获取CAS新资料ToolStripMenuItem,
            this.获取WUProp新资料ToolStripMenuItem,
            this.获取PrimeGrid新资料ToolStripMenuItem,
            this.获取MilkyWay新资料ToolStripMenuItem,
            this.获取Rosetta新资料ToolStripMenuItem,
            this.获取SETI新资料ToolStripMenuItem,
            this.添加WCG记录ToolStripMenuItem,
            this.获取Einstein新资料ToolStripMenuItem,
            this.打开积分信息系统ToolStripMenuItem});
            this.命令ToolStripMenuItem.Name = "命令ToolStripMenuItem";
            this.命令ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.命令ToolStripMenuItem.Text = "命令";
            // 
            // 获取所有项目新资料ToolStripMenuItem
            // 
            this.获取所有项目新资料ToolStripMenuItem.Name = "获取所有项目新资料ToolStripMenuItem";
            this.获取所有项目新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取所有项目新资料ToolStripMenuItem.Text = "获取所有项目新资料";
            this.获取所有项目新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取CAS新资料ToolStripMenuItem
            // 
            this.获取CAS新资料ToolStripMenuItem.Name = "获取CAS新资料ToolStripMenuItem";
            this.获取CAS新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取CAS新资料ToolStripMenuItem.Text = "获取CAS新资料";
            this.获取CAS新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取WUProp新资料ToolStripMenuItem
            // 
            this.获取WUProp新资料ToolStripMenuItem.Name = "获取WUProp新资料ToolStripMenuItem";
            this.获取WUProp新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取WUProp新资料ToolStripMenuItem.Text = "获取WUProp新资料";
            this.获取WUProp新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取PrimeGrid新资料ToolStripMenuItem
            // 
            this.获取PrimeGrid新资料ToolStripMenuItem.Name = "获取PrimeGrid新资料ToolStripMenuItem";
            this.获取PrimeGrid新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取PrimeGrid新资料ToolStripMenuItem.Text = "获取PrimeGrid新资料";
            this.获取PrimeGrid新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取MilkyWay新资料ToolStripMenuItem
            // 
            this.获取MilkyWay新资料ToolStripMenuItem.Name = "获取MilkyWay新资料ToolStripMenuItem";
            this.获取MilkyWay新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取MilkyWay新资料ToolStripMenuItem.Text = "获取MilkyWay新资料";
            this.获取MilkyWay新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取Rosetta新资料ToolStripMenuItem
            // 
            this.获取Rosetta新资料ToolStripMenuItem.Name = "获取Rosetta新资料ToolStripMenuItem";
            this.获取Rosetta新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取Rosetta新资料ToolStripMenuItem.Text = "获取Rosetta新资料";
            this.获取Rosetta新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取SETI新资料ToolStripMenuItem
            // 
            this.获取SETI新资料ToolStripMenuItem.Name = "获取SETI新资料ToolStripMenuItem";
            this.获取SETI新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取SETI新资料ToolStripMenuItem.Text = "获取SETI新资料";
            this.获取SETI新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 添加WCG记录ToolStripMenuItem
            // 
            this.添加WCG记录ToolStripMenuItem.Name = "添加WCG记录ToolStripMenuItem";
            this.添加WCG记录ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.添加WCG记录ToolStripMenuItem.Text = "添加WCG记录";
            this.添加WCG记录ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 获取Einstein新资料ToolStripMenuItem
            // 
            this.获取Einstein新资料ToolStripMenuItem.Name = "获取Einstein新资料ToolStripMenuItem";
            this.获取Einstein新资料ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.获取Einstein新资料ToolStripMenuItem.Text = "获取Einstein新资料";
            this.获取Einstein新资料ToolStripMenuItem.Click += new System.EventHandler(this.获取资料ToolStripMenuItem_Click);
            // 
            // 打开积分信息系统ToolStripMenuItem
            // 
            this.打开积分信息系统ToolStripMenuItem.Name = "打开积分信息系统ToolStripMenuItem";
            this.打开积分信息系统ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.打开积分信息系统ToolStripMenuItem.Text = "打开积分信息系统";
            this.打开积分信息系统ToolStripMenuItem.Click += new System.EventHandler(this.打开积分信息系统ToolStripMenuItem_Click);
            // 
            // dgv_TaskRecord
            // 
            this.dgv_TaskRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TaskRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectName,
            this.TaskName,
            this.WuID,
            this.ComputerID,
            this.SentTime,
            this.ReportedTime,
            this.RunTime,
            this.CPUTime,
            this.Credit,
            this.Application,
            this.ID});
            this.dgv_TaskRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_TaskRecord.Location = new System.Drawing.Point(0, 63);
            this.dgv_TaskRecord.Name = "dgv_TaskRecord";
            this.dgv_TaskRecord.RowTemplate.Height = 23;
            this.dgv_TaskRecord.Size = new System.Drawing.Size(1284, 499);
            this.dgv_TaskRecord.TabIndex = 1;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Width = 80;
            // 
            // TaskName
            // 
            this.TaskName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TaskName.DataPropertyName = "TaskName";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TaskName.DefaultCellStyle = dataGridViewCellStyle3;
            this.TaskName.HeaderText = "任务名称";
            this.TaskName.Name = "TaskName";
            this.TaskName.Width = 61;
            // 
            // WuID
            // 
            this.WuID.DataPropertyName = "WuID";
            this.WuID.HeaderText = "WuID";
            this.WuID.Name = "WuID";
            // 
            // ComputerID
            // 
            this.ComputerID.DataPropertyName = "ComputerID";
            this.ComputerID.HeaderText = "计算机ID";
            this.ComputerID.Name = "ComputerID";
            // 
            // SentTime
            // 
            this.SentTime.DataPropertyName = "SentTime";
            this.SentTime.HeaderText = "任务发送时间";
            this.SentTime.Name = "SentTime";
            this.SentTime.Width = 120;
            // 
            // ReportedTime
            // 
            this.ReportedTime.DataPropertyName = "ReportedTime";
            this.ReportedTime.HeaderText = "任务报告时间";
            this.ReportedTime.Name = "ReportedTime";
            this.ReportedTime.Width = 120;
            // 
            // RunTime
            // 
            this.RunTime.DataPropertyName = "RunTime";
            this.RunTime.HeaderText = "运行时间（秒）";
            this.RunTime.Name = "RunTime";
            // 
            // CPUTime
            // 
            this.CPUTime.DataPropertyName = "CPUTime";
            this.CPUTime.HeaderText = "CPU使用时间（秒）";
            this.CPUTime.Name = "CPUTime";
            // 
            // Credit
            // 
            this.Credit.DataPropertyName = "Credit";
            this.Credit.HeaderText = "获得积分";
            this.Credit.Name = "Credit";
            // 
            // Application
            // 
            this.Application.DataPropertyName = "Application";
            this.Application.HeaderText = "执行程序";
            this.Application.Name = "Application";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(12, 34);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // backwork_GetAllRecord
            // 
            this.backwork_GetAllRecord.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backwork_GetAllRecord_DoWork);
            // 
            // timer_UpdateView
            // 
            this.timer_UpdateView.Tick += new System.EventHandler(this.timer_UpdateView_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(237, 34);
            this.label1.MaximumSize = new System.Drawing.Size(500, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 14);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.dgv_TaskRecord);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "BOINC主窗口";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TaskRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取所有项目新资料ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_TaskRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WuID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SentTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPUTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.Button btn_Save;
        private System.ComponentModel.BackgroundWorker backwork_GetAllRecord;
        private System.Windows.Forms.Timer timer_UpdateView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 获取WUProp新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取PrimeGrid新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取MilkyWay新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取CAS新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取Rosetta新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取SETI新资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开积分信息系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加WCG记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取Einstein新资料ToolStripMenuItem;
    }
}