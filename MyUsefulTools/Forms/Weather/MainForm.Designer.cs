﻿namespace MyUsefulTools.Forms.Weather
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
            this.常用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_GetWeatherXml = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常用ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 常用ToolStripMenuItem
            // 
            this.常用ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_GetWeatherXml});
            this.常用ToolStripMenuItem.Name = "常用ToolStripMenuItem";
            this.常用ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.常用ToolStripMenuItem.Text = "常用";
            // 
            // menuItem_GetWeatherXml
            // 
            this.menuItem_GetWeatherXml.Name = "menuItem_GetWeatherXml";
            this.menuItem_GetWeatherXml.Size = new System.Drawing.Size(152, 22);
            this.menuItem_GetWeatherXml.Text = "获取天气文件";
            this.menuItem_GetWeatherXml.Click += new System.EventHandler(this.menuItem_GetWeatherXml_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "天气实况";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 常用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_GetWeatherXml;
    }
}