namespace MyUsefulTools.Forms.BOINC
{
    partial class CreditForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InsertDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RACCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RACRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComputerCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActiveComputerCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_获取新数据 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_save = new System.Windows.Forms.Button();
            this.chart_project = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_project)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(849, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(849, 509);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(841, 483);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Credit表格";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.InsertDate,
            this.TotalCredit,
            this.RACCredit,
            this.TotalRank,
            this.RACRank,
            this.ComputerCount,
            this.ActiveComputerCount});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(835, 477);
            this.dataGridView1.TabIndex = 0;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "项目名称";
            this.ItemName.Name = "ItemName";
            // 
            // InsertDate
            // 
            this.InsertDate.DataPropertyName = "InsertDate";
            this.InsertDate.HeaderText = "插入日期";
            this.InsertDate.Name = "InsertDate";
            // 
            // TotalCredit
            // 
            this.TotalCredit.DataPropertyName = "TotalCredit";
            this.TotalCredit.HeaderText = "总分数";
            this.TotalCredit.Name = "TotalCredit";
            // 
            // RACCredit
            // 
            this.RACCredit.DataPropertyName = "RACCredit";
            this.RACCredit.HeaderText = "RAC分数";
            this.RACCredit.Name = "RACCredit";
            // 
            // TotalRank
            // 
            this.TotalRank.DataPropertyName = "TotalRank";
            this.TotalRank.HeaderText = "总分数排名";
            this.TotalRank.Name = "TotalRank";
            // 
            // RACRank
            // 
            this.RACRank.DataPropertyName = "RACRank";
            this.RACRank.HeaderText = "RAC分数排名";
            this.RACRank.Name = "RACRank";
            // 
            // ComputerCount
            // 
            this.ComputerCount.DataPropertyName = "ComputerCount";
            this.ComputerCount.HeaderText = "计算机个数";
            this.ComputerCount.Name = "ComputerCount";
            // 
            // ActiveComputerCount
            // 
            this.ActiveComputerCount.DataPropertyName = "ActiveComputerCount";
            this.ActiveComputerCount.HeaderText = "活动计算机个数";
            this.ActiveComputerCount.Name = "ActiveComputerCount";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(841, 483);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart_project);
            this.splitContainer1.Size = new System.Drawing.Size(835, 477);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_获取新数据
            // 
            this.btn_获取新数据.Location = new System.Drawing.Point(4, 0);
            this.btn_获取新数据.Name = "btn_获取新数据";
            this.btn_获取新数据.Size = new System.Drawing.Size(75, 23);
            this.btn_获取新数据.TabIndex = 3;
            this.btn_获取新数据.Text = "获取新数据";
            this.btn_获取新数据.UseVisualStyleBackColor = true;
            this.btn_获取新数据.Click += new System.EventHandler(this.btn_获取新数据_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(85, 0);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(53, 23);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // chart_project
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_project.ChartAreas.Add(chartArea2);
            this.chart_project.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart_project.Legends.Add(legend2);
            this.chart_project.Location = new System.Drawing.Point(0, 0);
            this.chart_project.Name = "chart_project";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart_project.Series.Add(series2);
            this.chart_project.Size = new System.Drawing.Size(661, 477);
            this.chart_project.TabIndex = 0;
            this.chart_project.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 477);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // CreditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 537);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_获取新数据);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "CreditForm";
            this.Text = "CreditForm";
            this.SizeChanged += new System.EventHandler(this.CreditForm_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_project)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_获取新数据;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InsertDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn RACCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn RACRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActiveComputerCount;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_project;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}