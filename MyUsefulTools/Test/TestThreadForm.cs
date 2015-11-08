using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MyUsefulTools.Test
{
    public partial class TestThreadForm : Form
    {
        delegate void deleShow(string _data);
        
        public TestThreadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //启动子线程
            backgroundWorker1.RunWorkerAsync();
            //主线程操作
            /*
            while (backgroundWorker1.IsBusy)
            {
                label2.Text = DateTime.Now.ToString();
                Thread.Sleep(500);
            }
            */
        }

        //将对控件的操作写到一个函数中
        private void showData(String para)
        {
            if (!label1.InvokeRequired)   //不需要唤醒，就是创建控件的线程
            //如果是创建控件的线程，直接正常操作
            {
                label1.Text = para;
            }
            else //非创建线程，用代理进行操作
            {
                deleShow ds = new deleShow(showData);
                //唤醒主线程，可以传递参数，也可以为null，即不传参数
                Invoke(ds, new object[] { para });
            }
        }

        private void showDataDirect(string para)
        {
            label1.Text = para;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            System.Diagnostics.Stopwatch MyWatch = new System.Diagnostics.Stopwatch();
            MyWatch.Start();
            for (int i = 1; i < 1000000000; i++)
            {
                int k = 123 * i * i + i;
                if (i % 10000 == 0)
                {
                    showData(backgroundWorker1.IsBusy.ToString());
                }
            }
            MyWatch.Stop();
            showData(MyWatch.ElapsedMilliseconds.ToString() + "ms");
            
        }

        private void TestThreadForm_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 判断后台线程的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "后台线程忙" + backgroundWorker1.IsBusy.ToString();
        }
    }
}
