namespace MyUsefulTools.Forms.Android
{
    partial class ViewApkInfo
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txt_filePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_chooseFile = new System.Windows.Forms.Button();
            this.rtb_info = new System.Windows.Forms.RichTextBox();
            this.btn_analyse = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txt_filePath
            // 
            this.txt_filePath.AllowDrop = true;
            this.txt_filePath.Location = new System.Drawing.Point(90, 16);
            this.txt_filePath.Name = "txt_filePath";
            this.txt_filePath.Size = new System.Drawing.Size(352, 21);
            this.txt_filePath.TabIndex = 0;
            this.txt_filePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_filePath_DragDrop);
            this.txt_filePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_filePath_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择apk文件";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_analyse);
            this.panel1.Controls.Add(this.btn_chooseFile);
            this.panel1.Controls.Add(this.txt_filePath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 51);
            this.panel1.TabIndex = 2;
            // 
            // btn_chooseFile
            // 
            this.btn_chooseFile.Location = new System.Drawing.Point(448, 14);
            this.btn_chooseFile.Name = "btn_chooseFile";
            this.btn_chooseFile.Size = new System.Drawing.Size(75, 23);
            this.btn_chooseFile.TabIndex = 2;
            this.btn_chooseFile.Text = "选择文件";
            this.btn_chooseFile.UseVisualStyleBackColor = true;
            this.btn_chooseFile.Click += new System.EventHandler(this.btn_chooseFile_Click);
            // 
            // rtb_info
            // 
            this.rtb_info.Location = new System.Drawing.Point(12, 88);
            this.rtb_info.Name = "rtb_info";
            this.rtb_info.Size = new System.Drawing.Size(598, 333);
            this.rtb_info.TabIndex = 3;
            this.rtb_info.Text = "";
            // 
            // btn_analyse
            // 
            this.btn_analyse.Location = new System.Drawing.Point(529, 14);
            this.btn_analyse.Name = "btn_analyse";
            this.btn_analyse.Size = new System.Drawing.Size(54, 23);
            this.btn_analyse.TabIndex = 3;
            this.btn_analyse.Text = "分析";
            this.btn_analyse.UseVisualStyleBackColor = true;
            this.btn_analyse.Click += new System.EventHandler(this.btn_analyse_Click);
            // 
            // ViewApkInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 486);
            this.Controls.Add(this.rtb_info);
            this.Controls.Add(this.panel1);
            this.Name = "ViewApkInfo";
            this.Text = "ViewApkInfo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txt_filePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_chooseFile;
        private System.Windows.Forms.RichTextBox rtb_info;
        private System.Windows.Forms.Button btn_analyse;
    }
}