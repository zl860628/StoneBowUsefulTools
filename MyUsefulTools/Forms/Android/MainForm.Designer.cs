namespace MyUsefulTools.Forms.Android
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.常用功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看apk安装包信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常用功能ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(791, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 常用功能ToolStripMenuItem
            // 
            this.常用功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看apk安装包信息ToolStripMenuItem});
            this.常用功能ToolStripMenuItem.Name = "常用功能ToolStripMenuItem";
            this.常用功能ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.常用功能ToolStripMenuItem.Text = "常用功能";
            // 
            // 查看apk安装包信息ToolStripMenuItem
            // 
            this.查看apk安装包信息ToolStripMenuItem.Name = "查看apk安装包信息ToolStripMenuItem";
            this.查看apk安装包信息ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.查看apk安装包信息ToolStripMenuItem.Text = "查看apk安装包信息";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 532);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 常用功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看apk安装包信息ToolStripMenuItem;
    }
}