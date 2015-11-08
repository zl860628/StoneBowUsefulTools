namespace MyUsefulTools.Forms.UnderWater
{
    partial class FishKindManager
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
            this.dataGridView_fishKind = new System.Windows.Forms.DataGridView();
            this.btn_saveUpdate = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KindName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyNeedLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LifeLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MakeCoinInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_fishKind)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_fishKind
            // 
            this.dataGridView_fishKind.AllowUserToOrderColumns = true;
            this.dataGridView_fishKind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_fishKind.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.KindName,
            this.Level,
            this.BuyNeedLevel,
            this.LifeLength,
            this.MakeCoinInterval});
            this.dataGridView_fishKind.Location = new System.Drawing.Point(12, 88);
            this.dataGridView_fishKind.Name = "dataGridView_fishKind";
            this.dataGridView_fishKind.RowTemplate.Height = 23;
            this.dataGridView_fishKind.Size = new System.Drawing.Size(699, 412);
            this.dataGridView_fishKind.TabIndex = 0;
            // 
            // btn_saveUpdate
            // 
            this.btn_saveUpdate.Location = new System.Drawing.Point(12, 59);
            this.btn_saveUpdate.Name = "btn_saveUpdate";
            this.btn_saveUpdate.Size = new System.Drawing.Size(75, 23);
            this.btn_saveUpdate.TabIndex = 1;
            this.btn_saveUpdate.Text = "保存更新";
            this.btn_saveUpdate.UseVisualStyleBackColor = true;
            this.btn_saveUpdate.Click += new System.EventHandler(this.btn_saveUpdate_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // KindName
            // 
            this.KindName.DataPropertyName = "KindName";
            this.KindName.HeaderText = "鱼类别名";
            this.KindName.Name = "KindName";
            // 
            // Level
            // 
            this.Level.DataPropertyName = "Level";
            this.Level.HeaderText = "自定义级别";
            this.Level.Name = "Level";
            // 
            // BuyNeedLevel
            // 
            this.BuyNeedLevel.DataPropertyName = "BuyNeedLevel";
            this.BuyNeedLevel.HeaderText = "购买需要等级";
            this.BuyNeedLevel.Name = "BuyNeedLevel";
            this.BuyNeedLevel.Width = 120;
            // 
            // LifeLength
            // 
            this.LifeLength.DataPropertyName = "LifeLength";
            this.LifeLength.HeaderText = "成年寿命（天）";
            this.LifeLength.Name = "LifeLength";
            this.LifeLength.Width = 120;
            // 
            // MakeCoinInterval
            // 
            this.MakeCoinInterval.DataPropertyName = "MakeCoinInterval";
            this.MakeCoinInterval.HeaderText = "产金币间隔（分钟）";
            this.MakeCoinInterval.Name = "MakeCoinInterval";
            // 
            // FishKindManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 512);
            this.Controls.Add(this.btn_saveUpdate);
            this.Controls.Add(this.dataGridView_fishKind);
            this.Name = "FishKindManager";
            this.Text = "FishKindManager";
            this.Load += new System.EventHandler(this.FishKindManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_fishKind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_fishKind;
        private System.Windows.Forms.Button btn_saveUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn KindName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyNeedLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn LifeLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn MakeCoinInterval;
    }
}