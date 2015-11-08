namespace MyUsefulTools.Forms.StoneReader
{
    partial class ReaderMain
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
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取订阅信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.软件初始化ToolStripMenuItem,
            this.获取订阅信息ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 获取订阅信息ToolStripMenuItem
            // 
            this.获取订阅信息ToolStripMenuItem.Name = "获取订阅信息ToolStripMenuItem";
            this.获取订阅信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.获取订阅信息ToolStripMenuItem.Text = "获取订阅信息";
            // 
            // 软件初始化ToolStripMenuItem
            // 
            this.软件初始化ToolStripMenuItem.Name = "软件初始化ToolStripMenuItem";
            this.软件初始化ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.软件初始化ToolStripMenuItem.Text = "软件初始化";
            // 
            // ReaderMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ReaderMain";
            this.Text = "ReaderMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取订阅信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 软件初始化ToolStripMenuItem;
    }
}