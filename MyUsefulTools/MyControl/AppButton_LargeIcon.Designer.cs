namespace MyUsefulTools.MyControl
{
    partial class AppButton_LargeIcon
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
            this.pb_icon = new System.Windows.Forms.PictureBox();
            this.lb_appName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(10, 0);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(60, 60);
            this.pb_icon.TabIndex = 0;
            this.pb_icon.TabStop = false;
            // 
            // lb_appName
            // 
            this.lb_appName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_appName.Location = new System.Drawing.Point(5, 60);
            this.lb_appName.Margin = new System.Windows.Forms.Padding(0);
            this.lb_appName.Name = "lb_appName";
            this.lb_appName.Size = new System.Drawing.Size(70, 20);
            this.lb_appName.TabIndex = 1;
            this.lb_appName.Text = "程序1";
            this.lb_appName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AppButton_LargeIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_appName);
            this.Controls.Add(this.pb_icon);
            this.Name = "AppButton_LargeIcon";
            this.Size = new System.Drawing.Size(80, 80);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label lb_appName;
    }
}
