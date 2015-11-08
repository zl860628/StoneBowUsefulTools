namespace MyUsefulTools.Forms.JingDong
{
    partial class Dialog_GetNewData
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_pageInfo = new System.Windows.Forms.Label();
            this.lb_newcount = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lb_beginSaleDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_downflux = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_downspeed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前页码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "新商品数";
            // 
            // lb_pageInfo
            // 
            this.lb_pageInfo.AutoSize = true;
            this.lb_pageInfo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_pageInfo.Location = new System.Drawing.Point(131, 24);
            this.lb_pageInfo.Name = "lb_pageInfo";
            this.lb_pageInfo.Size = new System.Drawing.Size(26, 26);
            this.lb_pageInfo.TabIndex = 2;
            this.lb_pageInfo.Text = "~";
            // 
            // lb_newcount
            // 
            this.lb_newcount.AutoSize = true;
            this.lb_newcount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_newcount.Location = new System.Drawing.Point(131, 55);
            this.lb_newcount.Name = "lb_newcount";
            this.lb_newcount.Size = new System.Drawing.Size(24, 26);
            this.lb_newcount.TabIndex = 2;
            this.lb_newcount.Text = "0";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lb_beginSaleDate
            // 
            this.lb_beginSaleDate.AutoSize = true;
            this.lb_beginSaleDate.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_beginSaleDate.Location = new System.Drawing.Point(131, 86);
            this.lb_beginSaleDate.Name = "lb_beginSaleDate";
            this.lb_beginSaleDate.Size = new System.Drawing.Size(26, 26);
            this.lb_beginSaleDate.TabIndex = 4;
            this.lb_beginSaleDate.Text = "~";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "当前上架时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "下载流量";
            // 
            // lb_downflux
            // 
            this.lb_downflux.AutoSize = true;
            this.lb_downflux.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_downflux.Location = new System.Drawing.Point(131, 118);
            this.lb_downflux.Name = "lb_downflux";
            this.lb_downflux.Size = new System.Drawing.Size(26, 26);
            this.lb_downflux.TabIndex = 4;
            this.lb_downflux.Text = "~";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "下载速度";
            // 
            // lb_downspeed
            // 
            this.lb_downspeed.AutoSize = true;
            this.lb_downspeed.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_downspeed.Location = new System.Drawing.Point(131, 145);
            this.lb_downspeed.Name = "lb_downspeed";
            this.lb_downspeed.Size = new System.Drawing.Size(26, 26);
            this.lb_downspeed.TabIndex = 4;
            this.lb_downspeed.Text = "~";
            // 
            // Dialog_GetNewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 262);
            this.Controls.Add(this.lb_downspeed);
            this.Controls.Add(this.lb_downflux);
            this.Controls.Add(this.lb_beginSaleDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_pageInfo);
            this.Controls.Add(this.lb_newcount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_GetNewData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "从京东网站获取新的数据";
            this.Shown += new System.EventHandler(this.Dialog_GetNewData_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dialog_GetNewData_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_pageInfo;
        private System.Windows.Forms.Label lb_newcount;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_beginSaleDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_downflux;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_downspeed;
    }
}