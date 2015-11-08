using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.MyControl;
using MyUsefulTools.Forms.UnderWater;
using MyUsefulTools.Test;
using MyUsefulTools.Forms.LiuWei;
using MyUsefulTools.Forms.JingDong;

namespace MyUsefulTools.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AppButton_LargeIcon appbtn = new AppButton_LargeIcon();
            appbtn.Text = "京东新商品";
            appbtn.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn);
            appbtn.Click += new EventHandler(appbtn_Click);
            AppButton_LargeIcon appbtn2 = new AppButton_LargeIcon();
            appbtn2.Text = "海底总动员";
            appbtn2.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn2);
            appbtn2.Location = new Point(100, 0);
            appbtn2.Click += new EventHandler(appbtn2_Click); 
            AppButton_LargeIcon appbtn3 = new AppButton_LargeIcon();
            appbtn3.Text = "Test";
            appbtn3.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn3);
            appbtn3.Location = new Point(200, 0);
            appbtn3.Click += new EventHandler(appbtn3_Click);
            AppButton_LargeIcon appbtn4 = new AppButton_LargeIcon();
            appbtn4.Text = "DAOGenerator";
            appbtn4.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn4);
            appbtn4.Location = new Point(300, 0);
            appbtn4.Click += new EventHandler(appbtn4_Click);
            AppButton_LargeIcon appbtn5 = new AppButton_LargeIcon();
            appbtn5.Text = "鱼类管理器";
            appbtn5.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn5);
            appbtn5.Location = new Point(400, 0);
            appbtn5.Click += new EventHandler(appbtn5_Click);
            AppButton_LargeIcon appbtn6 = new AppButton_LargeIcon();
            appbtn6.Text = "六维空间";
            appbtn6.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn6);
            appbtn6.Location = new Point(0, 100);
            appbtn6.Click += new EventHandler(appbtn6_Click);
            AppButton_LargeIcon appbtn7 = new AppButton_LargeIcon();
            appbtn7.Text = "天气实况";
            appbtn7.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn7);
            appbtn7.Location = new Point(100, 100);
            appbtn7.Click += new EventHandler(appbtn7_Click);
            AppButton_LargeIcon appbtn8 = new AppButton_LargeIcon();
            appbtn8.Text = "BOINC";
            appbtn8.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn8);
            appbtn8.Location = new Point(200, 100);
            appbtn8.Click += new EventHandler(appbtn8_Click);

            AppButton_LargeIcon appbtn9 = new AppButton_LargeIcon();
            appbtn9.Text = "ViewApkInfo";
            appbtn9.Tag = "ViewApkInfo";
            appbtn9.IconImage = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"res\image\AppIcon\JingDongIcon.png");
            panel2.Controls.Add(appbtn9);
            appbtn9.Location = new Point(300, 100);
            appbtn9.Click += new EventHandler(appbtn9_Click);
        }

        private void appbtn_Click(object sender, EventArgs e)
        {
            JingDongNewGoods app = new JingDongNewGoods();
            app.Show();
        }
        private void appbtn2_Click(object sender, EventArgs e)
        {
            UnderWaterMainForm app = new UnderWaterMainForm();
            app.Show();
        }
        private void appbtn3_Click(object sender, EventArgs e)
        {
            TestForm app = new TestForm();
            app.Show();
        }
        private void appbtn4_Click(object sender, EventArgs e)
        {
            DaoGenerateForm app = new DaoGenerateForm();
            app.Show();
        }
        private void appbtn5_Click(object sender, EventArgs e)
        {
            FishKindManager app = new FishKindManager();
            app.Show();
        }
        private void appbtn6_Click(object sender, EventArgs e)
        {
            LiuWeiMainForm app = new LiuWeiMainForm();
            app.Show();
        }
        private void appbtn7_Click(object sender, EventArgs e)
        {
            Forms.Weather.MainForm app = new Forms.Weather.MainForm();
            app.Show();
        }
        private void appbtn8_Click(object sender, EventArgs e)
        {
            Forms.BOINC.MainForm app = new Forms.BOINC.MainForm();
            app.Show();
        }
        private void appbtn9_Click(object sender, EventArgs e)
        {
            Forms.Android.ViewApkInfo app = new Forms.Android.ViewApkInfo();
            app.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string closeReason = e.CloseReason.ToString();
            if (closeReason.Equals("UserClosing"))
            {//用户点击窗口关闭按钮后，自动隐藏
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon_mainForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 系统初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemInitForm sysinfoForm = new SystemInitForm();
            sysinfoForm.ShowDialog();
        }
    }
}