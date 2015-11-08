using MyUsefulTools.BLL.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUsefulTools.Forms
{
    public partial class SystemInitForm : Form
    {
        public SystemInitForm()
        {
            InitializeComponent();
        }

        private void SystemInit_Load(object sender, EventArgs e)
        {
            //输出配置文件状态
            ConfigInit ci = new ConfigInit();
            
            if (!ci.HasLocalInitFile())
            {
                ci.CreateLocalInitFile();
            }

            string configinfo = ci.GetInitFileInfo();
            txt_Info.Text = configinfo;
        }
    }
}
