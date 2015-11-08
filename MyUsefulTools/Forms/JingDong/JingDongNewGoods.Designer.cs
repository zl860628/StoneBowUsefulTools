namespace MyUsefulTools.Forms.JingDong
{
    partial class JingDongNewGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JingDongNewGoods));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_getNewData = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_openUrlFile = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_getOnsaleDate = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_query = new System.Windows.Forms.ToolStripButton();
            this.tscb_gvFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_sortField = new System.Windows.Forms.ComboBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.HasRead = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.InterestValue = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.Price = new System.Windows.Forms.DataGridViewImageColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeginSaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InsertDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WebUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSelectControl1 = new MyUsefulTools.MyControl.PageSelectControl();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_getNewData,
            this.tsbtn_openUrlFile,
            this.tsbtn_getOnsaleDate,
            this.tsbtn_query,
            this.tscb_gvFontSize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_getNewData
            // 
            this.tsbtn_getNewData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_getNewData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_getNewData.Image")));
            this.tsbtn_getNewData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_getNewData.Name = "tsbtn_getNewData";
            this.tsbtn_getNewData.Size = new System.Drawing.Size(72, 22);
            this.tsbtn_getNewData.Text = "获取新数据";
            this.tsbtn_getNewData.Click += new System.EventHandler(this.tsbtn_getNewData_Click);
            // 
            // tsbtn_openUrlFile
            // 
            this.tsbtn_openUrlFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_openUrlFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_openUrlFile.Name = "tsbtn_openUrlFile";
            this.tsbtn_openUrlFile.Size = new System.Drawing.Size(84, 22);
            this.tsbtn_openUrlFile.Text = "打开网址文件";
            this.tsbtn_openUrlFile.Click += new System.EventHandler(this.tsbtn_openUrlFile_Click);
            // 
            // tsbtn_getOnsaleDate
            // 
            this.tsbtn_getOnsaleDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_getOnsaleDate.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_getOnsaleDate.Image")));
            this.tsbtn_getOnsaleDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_getOnsaleDate.Name = "tsbtn_getOnsaleDate";
            this.tsbtn_getOnsaleDate.Size = new System.Drawing.Size(84, 22);
            this.tsbtn_getOnsaleDate.Text = "获取上架日期";
            this.tsbtn_getOnsaleDate.Click += new System.EventHandler(this.tsbtn_getOnsaleDate_Click);
            // 
            // tsbtn_query
            // 
            this.tsbtn_query.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_query.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_query.Name = "tsbtn_query";
            this.tsbtn_query.Size = new System.Drawing.Size(36, 22);
            this.tsbtn_query.Text = "搜索";
            this.tsbtn_query.Click += new System.EventHandler(this.tsbtn_query_Click);
            // 
            // tscb_gvFontSize
            // 
            this.tscb_gvFontSize.Items.AddRange(new object[] {
            "8",
            "10",
            "12",
            "14",
            "16"});
            this.tscb_gvFontSize.Name = "tscb_gvFontSize";
            this.tscb_gvFontSize.Size = new System.Drawing.Size(121, 25);
            this.tscb_gvFontSize.TextChanged += new System.EventHandler(this.tscb_gvFontSize_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_sortField);
            this.panel1.Controls.Add(this.btn_refresh);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.pageSelectControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 637);
            this.panel1.TabIndex = 3;
            // 
            // cb_sortField
            // 
            this.cb_sortField.FormattingEnabled = true;
            this.cb_sortField.Location = new System.Drawing.Point(464, 9);
            this.cb_sortField.Name = "cb_sortField";
            this.cb_sortField.Size = new System.Drawing.Size(95, 20);
            this.cb_sortField.TabIndex = 9;
            this.cb_sortField.SelectedIndexChanged += new System.EventHandler(this.cb_sortExpress_SelectedIndexChanged);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(374, 7);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 8;
            this.btn_refresh.Text = "刷新";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HasRead,
            this.InterestValue,
            this.GoodsName,
            this.Image,
            this.Price,
            this.Description,
            this.BeginSaleDate,
            this.InsertDate,
            this.ID,
            this.WebUrl});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 36);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(984, 601);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // HasRead
            // 
            this.HasRead.DataPropertyName = "HasRead";
            this.HasRead.HeaderText = "是否已看";
            this.HasRead.Name = "HasRead";
            // 
            // InterestValue
            // 
            this.InterestValue.DataPropertyName = "InterestValue";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InterestValue.DefaultCellStyle = dataGridViewCellStyle1;
            this.InterestValue.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.InterestValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InterestValue.HeaderText = "关注度";
            this.InterestValue.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.InterestValue.Name = "InterestValue";
            this.InterestValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InterestValue.Width = 50;
            // 
            // GoodsName
            // 
            this.GoodsName.DataPropertyName = "GoodsName";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GoodsName.DefaultCellStyle = dataGridViewCellStyle2;
            this.GoodsName.HeaderText = "产品名称";
            this.GoodsName.Name = "GoodsName";
            this.GoodsName.Width = 150;
            // 
            // Image
            // 
            this.Image.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Image.DataPropertyName = "Image";
            this.Image.HeaderText = "图片";
            this.Image.Name = "Image";
            this.Image.Width = 5;
            // 
            // Price
            // 
            this.Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "价格";
            this.Price.Name = "Price";
            this.Price.Width = 32;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle3;
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            // 
            // BeginSaleDate
            // 
            this.BeginSaleDate.DataPropertyName = "BeginSaleDate";
            this.BeginSaleDate.HeaderText = "上架日期";
            this.BeginSaleDate.Name = "BeginSaleDate";
            this.BeginSaleDate.ReadOnly = true;
            // 
            // InsertDate
            // 
            this.InsertDate.DataPropertyName = "InsertDate";
            this.InsertDate.HeaderText = "添加日期";
            this.InsertDate.Name = "InsertDate";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // WebUrl
            // 
            this.WebUrl.DataPropertyName = "WebUrl";
            this.WebUrl.HeaderText = "WebUrl";
            this.WebUrl.Name = "WebUrl";
            this.WebUrl.Visible = false;
            // 
            // pageSelectControl1
            // 
            this.pageSelectControl1.DataSource = null;
            this.pageSelectControl1.Location = new System.Drawing.Point(3, 3);
            this.pageSelectControl1.Name = "pageSelectControl1";
            this.pageSelectControl1.NowPage = 1;
            this.pageSelectControl1.PageSize = 50;
            this.pageSelectControl1.Size = new System.Drawing.Size(376, 32);
            this.pageSelectControl1.TabIndex = 7;
            this.pageSelectControl1.PageChanged += new MyUsefulTools.MyControl.PageSelectControl.PageChangedEventHandler(this.pageSelectControl1_PageChanged);
            // 
            // JingDongNewGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "JingDongNewGoods";
            this.Text = "JingDongNewGoods";
            this.Load += new System.EventHandler(this.JingDongNewGoods_Load);
            this.SizeChanged += new System.EventHandler(this.JingDongNewGoods_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.JingDongNewGoods_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_getNewData;
        private System.Windows.Forms.Panel panel1;
        private MyUsefulTools.MyControl.PageSelectControl pageSelectControl1;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.ToolStripButton tsbtn_openUrlFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HasRead;
        private System.Windows.Forms.DataGridViewComboBoxColumn InterestValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.DataGridViewImageColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeginSaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InsertDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WebUrl;
        private System.Windows.Forms.ToolStripButton tsbtn_query;
        private System.Windows.Forms.ComboBox cb_sortField;
        private System.Windows.Forms.ToolStripButton tsbtn_getOnsaleDate;
        private System.Windows.Forms.ToolStripComboBox tscb_gvFontSize;
    }
}