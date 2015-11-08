using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyUsefulTools.Forms.Weather
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获取天气文件
        /// </summary>
        public void menuItem_GetWeatherXml_Click(object sender, EventArgs e)
        {
            Forms.Weather.GetWeatherXml getWeatherXmlForm = new GetWeatherXml();
            getWeatherXmlForm.ShowDialog();
        }
    }
}
