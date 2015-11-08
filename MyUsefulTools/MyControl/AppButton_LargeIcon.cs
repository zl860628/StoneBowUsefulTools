using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyUsefulTools.MyControl
{   
    public partial class AppButton_LargeIcon : UserControl
    {
        protected enum MouseStatus
        { 
            MouseIn, MouseOut
        }
        
        private string text = "";
        private Image iconImage = null;
        private Color originBackColor;
        private Color mouseOverBackColor = Color.FromArgb(128, 218, 233, 248);//鼠标经过控件时的背景色
        private MouseStatus mouseStatus = MouseStatus.MouseOut;//初始鼠标状态为鼠标在控件外
        
        /// <summary>
        /// 图标显示名称
        /// </summary>
        [Description("图标显示名称")]　//显示在属性设计视图中的描述
        [DefaultValue("")]//给予初始值
        public string Text
        {
            get { return text; }
            set 
            {
                text = value;
                lb_appName.Text = text;
            }
        }
        public Image IconImage
        {
            get { return iconImage; }
            set 
            {
                iconImage = value;
                pb_icon.Image = iconImage;
            }
        }
        /// <summary>
        /// 鼠标经过控件时的背景色
        /// </summary>
        public Color MouseOverBackColor
        {
            get { return mouseOverBackColor; }
            set { mouseOverBackColor = value; }
        }
        /// <summary>
        /// 控件的背景色
        /// </summary>
        public Color ControlBackColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        
        public AppButton_LargeIcon()
        {
            InitializeComponent();
            //为控件添加事件
            this.MouseEnter += new EventHandler(Control_MouseEnter);
            this.MouseLeave += new EventHandler(Control_MouseLeave);
            foreach (Control control in this.Controls)
            {
                control.MouseEnter += new EventHandler(Control_MouseEnter);
                control.MouseLeave += new EventHandler(Control_MouseLeave);
                control.Click += new EventHandler(delegate(object sender, EventArgs e)
                {
                    this.OnClick(e);
                });
            }
        }
        //鼠标进入控件时需要完成的操作
        private void MyMouseEnter()
        {
            originBackColor = this.BackColor;
            this.BackColor = mouseOverBackColor;
            this.Cursor = Cursors.Hand;
        }
        //鼠标离开控件时需要完成的操作
        private void MyMouseLeave()
        {
            this.BackColor = originBackColor;
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 判断鼠标的状态，分辨鼠标当前是否在容器控件中
        /// </summary>
        private MouseStatus JudgeMouseStatus()
        {
            Point mouseposi = Control.MousePosition;
            Point p2c = this.PointToClient(mouseposi);
            if (p2c.X >= this.Width || p2c.X < 0 || p2c.Y >= this.Height || p2c.Y < 0)
            { //鼠标在控件外
                return MouseStatus.MouseOut;
            }
            else
            { 
                return MouseStatus.MouseIn;
            }
        }
      
        /// <summary>
        /// 当鼠标进入整个控件的时候发生
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (this.mouseStatus != MouseStatus.MouseIn)
            { //当鼠标当前状态不在整个控件内时
                if (JudgeMouseStatus() == MouseStatus.MouseIn)
                {//原来鼠标在外，现在在内，执行鼠标进入事件
                    this.mouseStatus = MouseStatus.MouseIn;
                    MyMouseEnter();
                }
            }
        }
        /// <summary>
        /// 当鼠标进入整个控件的时候发生
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (this.mouseStatus != MouseStatus.MouseOut)
            { //当鼠标当前状态在整个控件内时
                if (JudgeMouseStatus() == MouseStatus.MouseOut)
                {//原来鼠标在内，现在在外，执行鼠标离开事件
                    this.mouseStatus = MouseStatus.MouseOut;
                    MyMouseLeave();
                }
            }
        }

    }
}
