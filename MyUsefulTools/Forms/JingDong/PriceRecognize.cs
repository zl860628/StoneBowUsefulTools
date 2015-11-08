using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.DAO;
using MyUsefulTools.Utility;

namespace MyUsefulTools.Forms.JingDong
{
    public partial class PriceRecognize : Form
    {
        private static int RecordID = 0;

        public PriceRecognize()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获得下一个可用的京东记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_getNextSample_Click(object sender, EventArgs e)
        {
            JingDongNewGoodsDAO item = null;
            //找到下一个有效的item
            while (true)
            {
                RecordID++;
                item = new JingDongNewGoodsDAO(RecordID);
                if (item.IsRecord)
                {
                    break;
                }
            }
            lb_recordID.Text = item.ID.ToString();
            pb_priceImage.Image = Utility.CSharpUtility.GetImageFromByteArray(item.PriceImg);
            //进行图像处理，获得独立字符二值图像
            GraphicRecognize gr = new GraphicRecognize((Bitmap)pb_priceImage.Image);
            gr.GrayByPixels(); //灰度处理
            gr.GetPicValidByValue(128, 1); //得到有效空间
            Bitmap pic = gr.GetBitmapFromBoolArray(gr.GetSingleBmpBoolArray(gr.bmpobj, 128));
            List<Bitmap> charPics = gr.GetSplitPics_FitHeight(pic);
            //二值图像字符化
            StringBuilder sbd = new StringBuilder();
            for (int i = 0; i < charPics.Count; i++)
            {
                Bitmap charPic = gr.ResizeBmp(charPics[i], 10, 12);
                for (int j = 0; j < charPic.Height; j++)
                {
                    for (int k = 0; k < charPic.Width; k++)
                    {
                        if (charPic.GetPixel(k, j).R == 0)
                        {
                            sbd.Append('*');
                        }
                        else sbd.Append('.');
                    }
                    sbd.Append("\n");
                }
                sbd.Append("\n");
            }
            richTextBox1.Text = sbd.ToString();
        }
    }
}
