namespace MyUsefulTools.Forms.JingDong
{
    partial class GoodsQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_likerate = new System.Windows.Forms.ComboBox();
            this.pageSelectControl1 = new MyUsefulTools.MyControl.PageSelectControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.InterestValue = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.Price = new System.Windows.Forms.DataGridViewImageColumn();
            this.BeginSaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InsertDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WebUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pageSelectControl1);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(884, 662);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(67, 135);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "我搜";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_likerate);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "按照关注度筛选";
            // 
            // cb_likerate
            // 
            this.cb_likerate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_likerate.FormattingEnabled = true;
            this.cb_likerate.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cb_likerate.Location = new System.Drawing.Point(33, 20);
            this.cb_likerate.Name = "cb_likerate";
            this.cb_likerate.Size = new System.Drawing.Size(121, 20);
            this.cb_likerate.TabIndex = 0;
            // 
            // pageSelectControl1
            // 
            this.pageSelectControl1.DataSource = null;
            this.pageSelectControl1.Location = new System.Drawing.Point(15, 620);
            this.pageSelectControl1.Name = "pageSelectControl1";
            this.pageSelectControl1.NowPage = 1;
            this.pageSelectControl1.PageSize = 40;
            this.pageSelectControl1.Size = new System.Drawing.Size(371, 30);
            this.pageSelectControl1.TabIndex = 2;
            this.pageSelectControl1.PageChanged += new MyUsefulTools.MyControl.PageSelectControl.PageChangedEventHandler(this.pageSelectControl1_PageChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InterestValue,
            this.GoodsName,
            this.Description,
            this.Image,
            this.Price,
            this.BeginSaleDate,
            this.InsertDate,
            this.ID,
            this.WebUrl});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(668, 662);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
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
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle3;
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
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
            this.Price.Width = 35;
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
            // GoodsQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GoodsQuery";
            this.Text = "商品查询";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ComboBox cb_likerate;
        private MyUsefulTools.MyControl.PageSelectControl pageSelectControl1;
        private System.Windows.Forms.DataGridViewComboBoxColumn InterestValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.DataGridViewImageColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeginSaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InsertDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WebUrl;
    }
}