namespace MyUsefulTools.Forms.LiuWei
{
    partial class LiuWeiMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_updateData = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_getHotData = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.time_end = new System.Windows.Forms.DateTimePicker();
            this.tb_pageCount = new System.Windows.Forms.TextBox();
            this.lb_dealPage = new System.Windows.Forms.Label();
            this.lb_updateCount = new System.Windows.Forms.Label();
            this.lb_insertCount = new System.Windows.Forms.Label();
            this.lb_dealCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.datagv_unread = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HasRead = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cb_allSelect = new System.Windows.Forms.CheckBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.pageSelectControl1 = new MyUsefulTools.MyControl.PageSelectControl();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagv_unread)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1050, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_updateData,
            this.menu_getHotData});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // menu_updateData
            // 
            this.menu_updateData.Name = "menu_updateData";
            this.menu_updateData.Size = new System.Drawing.Size(148, 22);
            this.menu_updateData.Text = "更新数据";
            this.menu_updateData.Click += new System.EventHandler(this.menu_updateData_Click);
            // 
            // menu_getHotData
            // 
            this.menu_getHotData.Name = "menu_getHotData";
            this.menu_getHotData.Size = new System.Drawing.Size(148, 22);
            this.menu_getHotData.Text = "更新热门资源";
            this.menu_getHotData.Click += new System.EventHandler(this.menu_getHotData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.time_end);
            this.groupBox1.Controls.Add(this.tb_pageCount);
            this.groupBox1.Controls.Add(this.lb_dealPage);
            this.groupBox1.Controls.Add(this.lb_updateCount);
            this.groupBox1.Controls.Add(this.lb_insertCount);
            this.groupBox1.Controls.Add(this.lb_dealCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(884, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 350);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "更新数据控制";
            // 
            // time_end
            // 
            this.time_end.Location = new System.Drawing.Point(8, 165);
            this.time_end.Name = "time_end";
            this.time_end.Size = new System.Drawing.Size(112, 21);
            this.time_end.TabIndex = 3;
            this.time_end.Value = new System.DateTime(2010, 9, 28, 0, 0, 0, 0);
            // 
            // tb_pageCount
            // 
            this.tb_pageCount.Location = new System.Drawing.Point(65, 117);
            this.tb_pageCount.Name = "tb_pageCount";
            this.tb_pageCount.Size = new System.Drawing.Size(51, 21);
            this.tb_pageCount.TabIndex = 2;
            this.tb_pageCount.Text = "100";
            // 
            // lb_dealPage
            // 
            this.lb_dealPage.AutoSize = true;
            this.lb_dealPage.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_dealPage.Location = new System.Drawing.Point(74, 80);
            this.lb_dealPage.Name = "lb_dealPage";
            this.lb_dealPage.Size = new System.Drawing.Size(22, 21);
            this.lb_dealPage.TabIndex = 1;
            this.lb_dealPage.Text = "0";
            // 
            // lb_updateCount
            // 
            this.lb_updateCount.AutoSize = true;
            this.lb_updateCount.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_updateCount.Location = new System.Drawing.Point(74, 60);
            this.lb_updateCount.Name = "lb_updateCount";
            this.lb_updateCount.Size = new System.Drawing.Size(22, 21);
            this.lb_updateCount.TabIndex = 1;
            this.lb_updateCount.Text = "0";
            // 
            // lb_insertCount
            // 
            this.lb_insertCount.AutoSize = true;
            this.lb_insertCount.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_insertCount.Location = new System.Drawing.Point(74, 39);
            this.lb_insertCount.Name = "lb_insertCount";
            this.lb_insertCount.Size = new System.Drawing.Size(22, 21);
            this.lb_insertCount.TabIndex = 1;
            this.lb_insertCount.Text = "0";
            // 
            // lb_dealCount
            // 
            this.lb_dealCount.AutoSize = true;
            this.lb_dealCount.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_dealCount.Location = new System.Drawing.Point(74, 18);
            this.lb_dealCount.Name = "lb_dealCount";
            this.lb_dealCount.Size = new System.Drawing.Size(22, 21);
            this.lb_dealCount.TabIndex = 1;
            this.lb_dealCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "截止日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "截取页数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "当前页";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "更新信息数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "新添加数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "已处理数";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // datagv_unread
            // 
            this.datagv_unread.AllowUserToAddRows = false;
            this.datagv_unread.BackgroundColor = System.Drawing.Color.MediumAquamarine;
            this.datagv_unread.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagv_unread.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.HasRead,
            this.Title,
            this.gSize,
            this.SeedCount,
            this.URL,
            this.CreateDate});
            this.datagv_unread.Location = new System.Drawing.Point(12, 63);
            this.datagv_unread.MultiSelect = false;
            this.datagv_unread.Name = "datagv_unread";
            this.datagv_unread.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.datagv_unread.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.datagv_unread.RowTemplate.Height = 23;
            this.datagv_unread.Size = new System.Drawing.Size(866, 598);
            this.datagv_unread.TabIndex = 2;
            this.datagv_unread.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagv_unread_CellValueChanged);
            this.datagv_unread.Sorted += new System.EventHandler(this.datagv_unread_Sorted);
            this.datagv_unread.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagv_unread_CellDoubleClick);
            this.datagv_unread.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.datagv_unread_CellPainting);
            this.datagv_unread.DataSourceChanged += new System.EventHandler(this.datagv_unread_DataSourceChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // HasRead
            // 
            this.HasRead.DataPropertyName = "HasRead";
            this.HasRead.HeaderText = "是否阅读";
            this.HasRead.Name = "HasRead";
            this.HasRead.Width = 60;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Title.DefaultCellStyle = dataGridViewCellStyle3;
            this.Title.HeaderText = "标题";
            this.Title.Name = "Title";
            this.Title.Width = 460;
            // 
            // Size
            // 
            this.gSize.DataPropertyName = "Size";
            this.gSize.HeaderText = "文件大小";
            this.gSize.Name = "Size";
            // 
            // SeedCount
            // 
            this.SeedCount.DataPropertyName = "SeedCount";
            this.SeedCount.HeaderText = "种子数";
            this.SeedCount.Name = "SeedCount";
            // 
            // URL
            // 
            this.URL.DataPropertyName = "URL";
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.ReadOnly = true;
            this.URL.Visible = false;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            this.CreateDate.HeaderText = "创建日期";
            this.CreateDate.Name = "CreateDate";
            // 
            // cb_allSelect
            // 
            this.cb_allSelect.AutoSize = true;
            this.cb_allSelect.Location = new System.Drawing.Point(400, 35);
            this.cb_allSelect.Name = "cb_allSelect";
            this.cb_allSelect.Size = new System.Drawing.Size(48, 16);
            this.cb_allSelect.TabIndex = 4;
            this.cb_allSelect.Text = "全选";
            this.cb_allSelect.UseVisualStyleBackColor = true;
            this.cb_allSelect.CheckedChanged += new System.EventHandler(this.cb_allSelect_CheckedChanged);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(467, 30);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(157, 21);
            this.txt_search.TabIndex = 5;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(630, 28);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(56, 23);
            this.btn_search.TabIndex = 6;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // pageSelectControl1
            // 
            this.pageSelectControl1.DataSource = null;
            this.pageSelectControl1.Location = new System.Drawing.Point(12, 27);
            this.pageSelectControl1.Name = "pageSelectControl1";
            this.pageSelectControl1.NowPage = 1;
            this.pageSelectControl1.Size = new System.Drawing.Size(379, 30);
            this.pageSelectControl1.TabIndex = 3;
            this.pageSelectControl1.PageChanged += new MyUsefulTools.MyControl.PageSelectControl.PageChangedEventHandler(this.pageSelectControl1_PageChanged);
            // 
            // LiuWeiMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 673);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.cb_allSelect);
            this.Controls.Add(this.pageSelectControl1);
            this.Controls.Add(this.datagv_unread);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LiuWeiMainForm";
            this.Text = "LiuWeiMainForm";
            this.Load += new System.EventHandler(this.LiuWeiMainForm_Load);
            this.Resize += new System.EventHandler(this.LiuWeiMainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagv_unread)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_updateData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_insertCount;
        private System.Windows.Forms.Label lb_dealCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView datagv_unread;
        private MyUsefulTools.MyControl.PageSelectControl pageSelectControl1;
        private System.Windows.Forms.DateTimePicker time_end;
        private System.Windows.Forms.TextBox tb_pageCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HasRead;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn gSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.CheckBox cb_allSelect;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ToolStripMenuItem menu_getHotData;
        private System.Windows.Forms.Label lb_updateCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_dealPage;
        private System.Windows.Forms.Label label6;
    }
}