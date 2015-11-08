namespace MyUsefulTools.Forms.JingDong
{
    partial class PriceRecognize
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb_priceImage = new System.Windows.Forms.PictureBox();
            this.btn_getNextSample = new System.Windows.Forms.Button();
            this.lb_recordID = new System.Windows.Forms.Label();
            this.pb_numChars = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_priceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_numChars)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.pb_numChars);
            this.groupBox1.Controls.Add(this.lb_recordID);
            this.groupBox1.Controls.Add(this.btn_getNextSample);
            this.groupBox1.Controls.Add(this.pb_priceImage);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样本获取实验";
            // 
            // pb_priceImage
            // 
            this.pb_priceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_priceImage.Location = new System.Drawing.Point(104, 16);
            this.pb_priceImage.Name = "pb_priceImage";
            this.pb_priceImage.Size = new System.Drawing.Size(115, 36);
            this.pb_priceImage.TabIndex = 0;
            this.pb_priceImage.TabStop = false;
            // 
            // btn_getNextSample
            // 
            this.btn_getNextSample.Location = new System.Drawing.Point(269, 25);
            this.btn_getNextSample.Name = "btn_getNextSample";
            this.btn_getNextSample.Size = new System.Drawing.Size(75, 23);
            this.btn_getNextSample.TabIndex = 1;
            this.btn_getNextSample.Text = "下一个样本";
            this.btn_getNextSample.UseVisualStyleBackColor = true;
            this.btn_getNextSample.Click += new System.EventHandler(this.btn_getNextSample_Click);
            // 
            // lb_recordID
            // 
            this.lb_recordID.AutoSize = true;
            this.lb_recordID.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_recordID.Location = new System.Drawing.Point(5, 20);
            this.lb_recordID.Name = "lb_recordID";
            this.lb_recordID.Size = new System.Drawing.Size(75, 28);
            this.lb_recordID.TabIndex = 2;
            this.lb_recordID.Text = "样本号";
            // 
            // pb_numChars
            // 
            this.pb_numChars.Location = new System.Drawing.Point(10, 58);
            this.pb_numChars.Name = "pb_numChars";
            this.pb_numChars.Size = new System.Drawing.Size(334, 50);
            this.pb_numChars.TabIndex = 3;
            this.pb_numChars.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(360, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(193, 96);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // PriceRecognize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 506);
            this.Controls.Add(this.groupBox1);
            this.Name = "PriceRecognize";
            this.Text = "价格图片识别测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_priceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_numChars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_recordID;
        private System.Windows.Forms.Button btn_getNextSample;
        private System.Windows.Forms.PictureBox pb_priceImage;
        private System.Windows.Forms.PictureBox pb_numChars;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}