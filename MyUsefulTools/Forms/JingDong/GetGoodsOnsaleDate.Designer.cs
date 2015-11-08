namespace MyUsefulTools.Forms.JingDong
{
    partial class GetGoodsOnsaleDate
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
            this.timer_refreshForm = new System.Windows.Forms.Timer(this.components);
            this.lb_getOnsaleCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_stopGetDate = new System.Windows.Forms.Button();
            this.btn_getAllDate = new System.Windows.Forms.Button();
            this.bgwork_getOnsaleDate = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_refreshForm
            // 
            this.timer_refreshForm.Tick += new System.EventHandler(this.timer_refreshForm_Tick);
            // 
            // lb_getOnsaleCount
            // 
            this.lb_getOnsaleCount.AutoSize = true;
            this.lb_getOnsaleCount.Font = new System.Drawing.Font("Algerian", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_getOnsaleCount.Location = new System.Drawing.Point(225, 12);
            this.lb_getOnsaleCount.Name = "lb_getOnsaleCount";
            this.lb_getOnsaleCount.Size = new System.Drawing.Size(32, 32);
            this.lb_getOnsaleCount.TabIndex = 0;
            this.lb_getOnsaleCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 18F);
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "已获取上架日期数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_stopGetDate);
            this.groupBox1.Controls.Add(this.btn_getAllDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lb_getOnsaleCount);
            this.groupBox1.Location = new System.Drawing.Point(28, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 135);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btn_stopGetDate
            // 
            this.btn_stopGetDate.Location = new System.Drawing.Point(426, 17);
            this.btn_stopGetDate.Name = "btn_stopGetDate";
            this.btn_stopGetDate.Size = new System.Drawing.Size(75, 23);
            this.btn_stopGetDate.TabIndex = 1;
            this.btn_stopGetDate.Text = "停止获取";
            this.btn_stopGetDate.UseVisualStyleBackColor = true;
            this.btn_stopGetDate.Click += new System.EventHandler(this.btn_stopGetDate_Click);
            // 
            // btn_getAllDate
            // 
            this.btn_getAllDate.Location = new System.Drawing.Point(345, 17);
            this.btn_getAllDate.Name = "btn_getAllDate";
            this.btn_getAllDate.Size = new System.Drawing.Size(75, 23);
            this.btn_getAllDate.TabIndex = 1;
            this.btn_getAllDate.Text = "获取全部";
            this.btn_getAllDate.UseVisualStyleBackColor = true;
            this.btn_getAllDate.Click += new System.EventHandler(this.btn_getAll_Click);
            // 
            // bgwork_getOnsaleDate
            // 
            this.bgwork_getOnsaleDate.WorkerSupportsCancellation = true;
            this.bgwork_getOnsaleDate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwork_getOnsaleDate_DoWork);
            // 
            // GetGoodsOnsaleDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 496);
            this.Controls.Add(this.groupBox1);
            this.Name = "GetGoodsOnsaleDate";
            this.Text = "获取商品上架日期";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_refreshForm;
        private System.Windows.Forms.Label lb_getOnsaleCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_getAllDate;
        private System.ComponentModel.BackgroundWorker bgwork_getOnsaleDate;
        private System.Windows.Forms.Button btn_stopGetDate;
    }
}