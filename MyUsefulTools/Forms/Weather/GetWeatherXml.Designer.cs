namespace MyUsefulTools.Forms.Weather
{
    partial class GetWeatherXml
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_getFromFile = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_viewDataUrl = new System.Windows.Forms.Button();
            this.btn_openConfigFile = new System.Windows.Forms.Button();
            this.btn_getFromWeb = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_remark = new System.Windows.Forms.RichTextBox();
            this.tb_xml = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(573, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_remark);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(565, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "其他添加";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_getFromFile);
            this.groupBox2.Controls.Add(this.btn_download);
            this.groupBox2.Location = new System.Drawing.Point(8, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(549, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "天气数据文件管理";
            // 
            // btn_getFromFile
            // 
            this.btn_getFromFile.Location = new System.Drawing.Point(115, 20);
            this.btn_getFromFile.Name = "btn_getFromFile";
            this.btn_getFromFile.Size = new System.Drawing.Size(93, 23);
            this.btn_getFromFile.TabIndex = 1;
            this.btn_getFromFile.Text = "文件数据保存";
            this.btn_getFromFile.UseVisualStyleBackColor = true;
            this.btn_getFromFile.Click += new System.EventHandler(this.btn_getFromFile_Click);
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(6, 20);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(103, 23);
            this.btn_download.TabIndex = 0;
            this.btn_download.Text = "网络数据下载";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_viewDataUrl);
            this.groupBox1.Controls.Add(this.btn_openConfigFile);
            this.groupBox1.Controls.Add(this.btn_getFromWeb);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 60);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "获取天气信息";
            // 
            // btn_viewDataUrl
            // 
            this.btn_viewDataUrl.Location = new System.Drawing.Point(214, 20);
            this.btn_viewDataUrl.Name = "btn_viewDataUrl";
            this.btn_viewDataUrl.Size = new System.Drawing.Size(75, 23);
            this.btn_viewDataUrl.TabIndex = 2;
            this.btn_viewDataUrl.Text = "查看链接";
            this.btn_viewDataUrl.UseVisualStyleBackColor = true;
            this.btn_viewDataUrl.Click += new System.EventHandler(this.btn_viewDataUrl_Click);
            // 
            // btn_openConfigFile
            // 
            this.btn_openConfigFile.Location = new System.Drawing.Point(6, 20);
            this.btn_openConfigFile.Name = "btn_openConfigFile";
            this.btn_openConfigFile.Size = new System.Drawing.Size(103, 23);
            this.btn_openConfigFile.TabIndex = 1;
            this.btn_openConfigFile.Text = "打开配置文件";
            this.btn_openConfigFile.UseVisualStyleBackColor = true;
            this.btn_openConfigFile.Click += new System.EventHandler(this.btn_openConfigFile_Click);
            // 
            // btn_getFromWeb
            // 
            this.btn_getFromWeb.Location = new System.Drawing.Point(115, 20);
            this.btn_getFromWeb.Name = "btn_getFromWeb";
            this.btn_getFromWeb.Size = new System.Drawing.Size(93, 23);
            this.btn_getFromWeb.TabIndex = 0;
            this.btn_getFromWeb.Text = "网络数据获取";
            this.btn_getFromWeb.UseVisualStyleBackColor = true;
            this.btn_getFromWeb.Click += new System.EventHandler(this.btn_getFromWeb_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tb_xml);
            this.tabPage1.Controls.Add(this.btn_save);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(565, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文本添加";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(245, 373);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_remark
            // 
            this.txt_remark.Location = new System.Drawing.Point(8, 130);
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.Size = new System.Drawing.Size(549, 91);
            this.txt_remark.TabIndex = 4;
            this.txt_remark.Text = "";
            // 
            // tb_xml
            // 
            this.tb_xml.Location = new System.Drawing.Point(8, 6);
            this.tb_xml.Name = "tb_xml";
            this.tb_xml.Size = new System.Drawing.Size(551, 350);
            this.tb_xml.TabIndex = 2;
            this.tb_xml.Text = "";
            // 
            // GetWeatherXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 448);
            this.Controls.Add(this.tabControl1);
            this.Name = "GetWeatherXml";
            this.Text = "GetWeatherXml";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_getFromWeb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_openConfigFile;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_getFromFile;
        private System.Windows.Forms.Button btn_viewDataUrl;
        private System.Windows.Forms.RichTextBox txt_remark;
        private System.Windows.Forms.RichTextBox tb_xml;
    }
}