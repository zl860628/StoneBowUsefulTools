namespace MyUsefulTools.Forms.UnderWater
{
    partial class UnderWaterMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnderWaterMainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_babyfishisZhen = new System.Windows.Forms.CheckBox();
            this.cb_friendfishisZhen = new System.Windows.Forms.CheckBox();
            this.cb_myfishisZhen = new System.Windows.Forms.CheckBox();
            this.btn_subnew = new System.Windows.Forms.Button();
            this.pycb_babyfish = new MyUsefulTools.MyControl.PinyinSearchComboBox();
            this.pycb_friendfish = new MyUsefulTools.MyControl.PinyinSearchComboBox();
            this.pycb_myfish = new MyUsefulTools.MyControl.PinyinSearchComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_del = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelfFishKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FriendFishKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BabyFishKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InsertDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton1.Text = "鱼儿信息库";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_del);
            this.groupBox1.Controls.Add(this.cb_babyfishisZhen);
            this.groupBox1.Controls.Add(this.cb_friendfishisZhen);
            this.groupBox1.Controls.Add(this.cb_myfishisZhen);
            this.groupBox1.Controls.Add(this.btn_subnew);
            this.groupBox1.Controls.Add(this.pycb_babyfish);
            this.groupBox1.Controls.Add(this.pycb_friendfish);
            this.groupBox1.Controls.Add(this.pycb_myfish);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加新的交配信息";
            // 
            // cb_babyfishisZhen
            // 
            this.cb_babyfishisZhen.AutoSize = true;
            this.cb_babyfishisZhen.Location = new System.Drawing.Point(436, 44);
            this.cb_babyfishisZhen.Name = "cb_babyfishisZhen";
            this.cb_babyfishisZhen.Size = new System.Drawing.Size(60, 16);
            this.cb_babyfishisZhen.TabIndex = 6;
            this.cb_babyfishisZhen.Text = "真系列";
            this.cb_babyfishisZhen.UseVisualStyleBackColor = true;
            // 
            // cb_friendfishisZhen
            // 
            this.cb_friendfishisZhen.AutoSize = true;
            this.cb_friendfishisZhen.Location = new System.Drawing.Point(262, 44);
            this.cb_friendfishisZhen.Name = "cb_friendfishisZhen";
            this.cb_friendfishisZhen.Size = new System.Drawing.Size(60, 16);
            this.cb_friendfishisZhen.TabIndex = 4;
            this.cb_friendfishisZhen.Text = "真系列";
            this.cb_friendfishisZhen.UseVisualStyleBackColor = true;
            // 
            // cb_myfishisZhen
            // 
            this.cb_myfishisZhen.AutoSize = true;
            this.cb_myfishisZhen.Location = new System.Drawing.Point(76, 44);
            this.cb_myfishisZhen.Name = "cb_myfishisZhen";
            this.cb_myfishisZhen.Size = new System.Drawing.Size(60, 16);
            this.cb_myfishisZhen.TabIndex = 2;
            this.cb_myfishisZhen.Text = "真系列";
            this.cb_myfishisZhen.UseVisualStyleBackColor = true;
            // 
            // btn_subnew
            // 
            this.btn_subnew.Location = new System.Drawing.Point(563, 16);
            this.btn_subnew.Name = "btn_subnew";
            this.btn_subnew.Size = new System.Drawing.Size(91, 23);
            this.btn_subnew.TabIndex = 7;
            this.btn_subnew.Text = "添加新的记录";
            this.btn_subnew.UseVisualStyleBackColor = true;
            this.btn_subnew.Click += new System.EventHandler(this.btn_subnew_Click);
            // 
            // pycb_babyfish
            // 
            this.pycb_babyfish.FormattingEnabled = true;
            this.pycb_babyfish.Location = new System.Drawing.Point(436, 18);
            this.pycb_babyfish.Name = "pycb_babyfish";
            this.pycb_babyfish.Size = new System.Drawing.Size(121, 20);
            this.pycb_babyfish.TabIndex = 5;
            // 
            // pycb_friendfish
            // 
            this.pycb_friendfish.FormattingEnabled = true;
            this.pycb_friendfish.Location = new System.Drawing.Point(262, 18);
            this.pycb_friendfish.Name = "pycb_friendfish";
            this.pycb_friendfish.Size = new System.Drawing.Size(121, 20);
            this.pycb_friendfish.TabIndex = 3;
            // 
            // pycb_myfish
            // 
            this.pycb_myfish.FormattingEnabled = true;
            this.pycb_myfish.Location = new System.Drawing.Point(76, 18);
            this.pycb_myfish.Name = "pycb_myfish";
            this.pycb_myfish.Size = new System.Drawing.Size(121, 20);
            this.pycb_myfish.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "鱼宝宝";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "好友的鱼";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自己的鱼";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.SelfFishKind,
            this.FriendFishKind,
            this.BabyFishKind,
            this.InsertDate});
            this.dataGridView1.Location = new System.Drawing.Point(12, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(760, 447);
            this.dataGridView1.TabIndex = 2;
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(660, 16);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 8;
            this.btn_del.Text = "删除记录";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // SelfFishKind
            // 
            this.SelfFishKind.DataPropertyName = "SelfFishName";
            this.SelfFishKind.HeaderText = "自己的鱼";
            this.SelfFishKind.Name = "SelfFishKind";
            this.SelfFishKind.ReadOnly = true;
            // 
            // FriendFishKind
            // 
            this.FriendFishKind.DataPropertyName = "FriendFishName";
            this.FriendFishKind.HeaderText = "好友的鱼";
            this.FriendFishKind.Name = "FriendFishKind";
            this.FriendFishKind.ReadOnly = true;
            // 
            // BabyFishKind
            // 
            this.BabyFishKind.DataPropertyName = "BabyFishName";
            this.BabyFishKind.HeaderText = "Baby鱼";
            this.BabyFishKind.Name = "BabyFishKind";
            this.BabyFishKind.ReadOnly = true;
            // 
            // InsertDate
            // 
            this.InsertDate.DataPropertyName = "InsertDate";
            this.InsertDate.HeaderText = "记录添加日期";
            this.InsertDate.Name = "InsertDate";
            this.InsertDate.ReadOnly = true;
            this.InsertDate.Width = 150;
            // 
            // UnderWaterMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UnderWaterMainForm";
            this.Text = "海底总动员收藏";
            this.Load += new System.EventHandler(this.UnderWaterMainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MyUsefulTools.MyControl.PinyinSearchComboBox pycb_babyfish;
        private MyUsefulTools.MyControl.PinyinSearchComboBox pycb_friendfish;
        private MyUsefulTools.MyControl.PinyinSearchComboBox pycb_myfish;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_subnew;
        private System.Windows.Forms.CheckBox cb_myfishisZhen;
        private System.Windows.Forms.CheckBox cb_babyfishisZhen;
        private System.Windows.Forms.CheckBox cb_friendfishisZhen;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelfFishKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendFishKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn BabyFishKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn InsertDate;
    }
}