namespace MyUsefulTools.MyControl
{
    partial class PageSelectControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_first = new System.Windows.Forms.Button();
            this.btn_last = new System.Windows.Forms.Button();
            this.btn_pre = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.lb_total = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxt_now = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // btn_first
            // 
            this.btn_first.Location = new System.Drawing.Point(3, 4);
            this.btn_first.Name = "btn_first";
            this.btn_first.Size = new System.Drawing.Size(45, 23);
            this.btn_first.TabIndex = 0;
            this.btn_first.Text = "首页";
            this.btn_first.UseVisualStyleBackColor = true;
            this.btn_first.Click += new System.EventHandler(this.btn_first_Click);
            // 
            // btn_last
            // 
            this.btn_last.Location = new System.Drawing.Point(323, 4);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(45, 23);
            this.btn_last.TabIndex = 0;
            this.btn_last.Text = "末页";
            this.btn_last.UseVisualStyleBackColor = true;
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // btn_pre
            // 
            this.btn_pre.Location = new System.Drawing.Point(53, 4);
            this.btn_pre.Name = "btn_pre";
            this.btn_pre.Size = new System.Drawing.Size(70, 23);
            this.btn_pre.TabIndex = 0;
            this.btn_pre.Text = "上一页&P";
            this.btn_pre.UseVisualStyleBackColor = true;
            this.btn_pre.Click += new System.EventHandler(this.btn_pre_Click);
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(247, 4);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(70, 23);
            this.btn_next.TabIndex = 0;
            this.btn_next.Text = "下一页&N";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_total.Location = new System.Drawing.Point(200, 7);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(17, 16);
            this.lb_total.TabIndex = 1;
            this.lb_total.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(185, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "/";
            // 
            // mtxt_now
            // 
            this.mtxt_now.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtxt_now.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtxt_now.Location = new System.Drawing.Point(133, 6);
            this.mtxt_now.Name = "mtxt_now";
            this.mtxt_now.Size = new System.Drawing.Size(49, 19);
            this.mtxt_now.TabIndex = 2;
            this.mtxt_now.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PageSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtxt_now);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_total);
            this.Controls.Add(this.btn_last);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.btn_pre);
            this.Controls.Add(this.btn_first);
            this.Name = "PageSelectControl";
            this.Size = new System.Drawing.Size(371, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_first;
        private System.Windows.Forms.Button btn_last;
        private System.Windows.Forms.Button btn_pre;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtxt_now;
    }
}
