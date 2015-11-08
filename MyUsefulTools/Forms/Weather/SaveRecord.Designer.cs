namespace MyUsefulTools.Forms.Weather
{
    partial class SaveRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_save = new System.Windows.Forms.Button();
            this.城市名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.记录时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.温度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.相对湿度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.降水 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.风力 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.风向 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.气压 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.城市名称,
            this.记录时间,
            this.温度,
            this.相对湿度,
            this.降水,
            this.风力,
            this.风向,
            this.气压});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(849, 411);
            this.dataGridView1.TabIndex = 0;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(143, 427);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(59, 42);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存到数据库";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // 城市名称
            // 
            this.城市名称.DataPropertyName = "城市名称";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.城市名称.DefaultCellStyle = dataGridViewCellStyle2;
            this.城市名称.HeaderText = "城市名称";
            this.城市名称.Name = "城市名称";
            // 
            // 记录时间
            // 
            this.记录时间.DataPropertyName = "记录时间";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.记录时间.DefaultCellStyle = dataGridViewCellStyle3;
            this.记录时间.HeaderText = "记录时间";
            this.记录时间.Name = "记录时间";
            // 
            // 温度
            // 
            this.温度.DataPropertyName = "温度";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.温度.DefaultCellStyle = dataGridViewCellStyle4;
            this.温度.HeaderText = "温度";
            this.温度.Name = "温度";
            // 
            // 相对湿度
            // 
            this.相对湿度.DataPropertyName = "相对湿度";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.相对湿度.DefaultCellStyle = dataGridViewCellStyle5;
            this.相对湿度.HeaderText = "相对湿度";
            this.相对湿度.Name = "相对湿度";
            // 
            // 降水
            // 
            this.降水.DataPropertyName = "降水";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.降水.DefaultCellStyle = dataGridViewCellStyle6;
            this.降水.HeaderText = "降水";
            this.降水.Name = "降水";
            // 
            // 风力
            // 
            this.风力.DataPropertyName = "风力";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.风力.DefaultCellStyle = dataGridViewCellStyle7;
            this.风力.HeaderText = "风力";
            this.风力.Name = "风力";
            // 
            // 风向
            // 
            this.风向.DataPropertyName = "风向";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.风向.DefaultCellStyle = dataGridViewCellStyle8;
            this.风向.HeaderText = "风向";
            this.风向.Name = "风向";
            // 
            // 气压
            // 
            this.气压.DataPropertyName = "气压";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.气压.DefaultCellStyle = dataGridViewCellStyle9;
            this.气压.HeaderText = "气压";
            this.气压.Name = "气压";
            // 
            // SaveRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 483);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SaveRecord";
            this.Text = "SaveRecord";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn 城市名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 记录时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 温度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 相对湿度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 降水;
        private System.Windows.Forms.DataGridViewTextBoxColumn 风力;
        private System.Windows.Forms.DataGridViewTextBoxColumn 风向;
        private System.Windows.Forms.DataGridViewTextBoxColumn 气压;

    }
}