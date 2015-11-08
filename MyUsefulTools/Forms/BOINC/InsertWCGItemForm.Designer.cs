namespace MyUsefulTools.Forms.BOINC
{
    partial class InsertWCGItemForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ItemName = new System.Windows.Forms.TextBox();
            this.txt_ReceiveTime = new System.Windows.Forms.TextBox();
            this.txt_UpTime = new System.Windows.Forms.TextBox();
            this.txt_CPUTime = new System.Windows.Forms.TextBox();
            this.txt_Credit = new System.Windows.Forms.TextBox();
            this.cbb_ComputerID = new System.Windows.Forms.ComboBox();
            this.cbb_Application = new System.Windows.Forms.ComboBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label_warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "机器编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "项目接收时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "项目提交时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "项目CPU用时（小时）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "获得积分";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "程序";
            // 
            // txt_ItemName
            // 
            this.txt_ItemName.Location = new System.Drawing.Point(149, 52);
            this.txt_ItemName.Name = "txt_ItemName";
            this.txt_ItemName.Size = new System.Drawing.Size(270, 21);
            this.txt_ItemName.TabIndex = 0;
            this.txt_ItemName.TextChanged += new System.EventHandler(this.txt_ItemName_TextChanged);
            // 
            // txt_ReceiveTime
            // 
            this.txt_ReceiveTime.Location = new System.Drawing.Point(149, 124);
            this.txt_ReceiveTime.Name = "txt_ReceiveTime";
            this.txt_ReceiveTime.Size = new System.Drawing.Size(270, 21);
            this.txt_ReceiveTime.TabIndex = 2;
            // 
            // txt_UpTime
            // 
            this.txt_UpTime.Location = new System.Drawing.Point(149, 160);
            this.txt_UpTime.Name = "txt_UpTime";
            this.txt_UpTime.Size = new System.Drawing.Size(270, 21);
            this.txt_UpTime.TabIndex = 3;
            // 
            // txt_CPUTime
            // 
            this.txt_CPUTime.Location = new System.Drawing.Point(149, 196);
            this.txt_CPUTime.Name = "txt_CPUTime";
            this.txt_CPUTime.Size = new System.Drawing.Size(270, 21);
            this.txt_CPUTime.TabIndex = 4;
            // 
            // txt_Credit
            // 
            this.txt_Credit.Location = new System.Drawing.Point(149, 232);
            this.txt_Credit.Name = "txt_Credit";
            this.txt_Credit.Size = new System.Drawing.Size(270, 21);
            this.txt_Credit.TabIndex = 5;
            // 
            // cbb_ComputerID
            // 
            this.cbb_ComputerID.FormattingEnabled = true;
            this.cbb_ComputerID.Location = new System.Drawing.Point(149, 88);
            this.cbb_ComputerID.Name = "cbb_ComputerID";
            this.cbb_ComputerID.Size = new System.Drawing.Size(270, 20);
            this.cbb_ComputerID.TabIndex = 1;
            // 
            // cbb_Application
            // 
            this.cbb_Application.FormattingEnabled = true;
            this.cbb_Application.Location = new System.Drawing.Point(149, 263);
            this.cbb_Application.Name = "cbb_Application";
            this.cbb_Application.Size = new System.Drawing.Size(270, 20);
            this.cbb_Application.TabIndex = 6;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(178, 322);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_submit.TabIndex = 7;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label_warning
            // 
            this.label_warning.AutoSize = true;
            this.label_warning.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_warning.ForeColor = System.Drawing.Color.Red;
            this.label_warning.Location = new System.Drawing.Point(147, 21);
            this.label_warning.Name = "label_warning";
            this.label_warning.Size = new System.Drawing.Size(0, 22);
            this.label_warning.TabIndex = 11;
            // 
            // InsertWCGItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 383);
            this.Controls.Add(this.label_warning);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.cbb_Application);
            this.Controls.Add(this.cbb_ComputerID);
            this.Controls.Add(this.txt_Credit);
            this.Controls.Add(this.txt_CPUTime);
            this.Controls.Add(this.txt_UpTime);
            this.Controls.Add(this.txt_ReceiveTime);
            this.Controls.Add(this.txt_ItemName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InsertWCGItemForm";
            this.Text = "手工添加World Community Grid记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ItemName;
        private System.Windows.Forms.TextBox txt_ReceiveTime;
        private System.Windows.Forms.TextBox txt_UpTime;
        private System.Windows.Forms.TextBox txt_CPUTime;
        private System.Windows.Forms.TextBox txt_Credit;
        private System.Windows.Forms.ComboBox cbb_ComputerID;
        private System.Windows.Forms.ComboBox cbb_Application;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label_warning;
    }
}