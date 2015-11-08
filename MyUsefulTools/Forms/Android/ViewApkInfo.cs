using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SevenZip;
using System.IO;
using System.Diagnostics;

namespace MyUsefulTools.Forms.Android
{
    public partial class ViewApkInfo : Form
    {
        public ViewApkInfo()
        {
            InitializeComponent();
        }

        private void btn_chooseFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "APK(*.apk)|*.apk";
            openFileDialog1.ShowDialog();
            txt_filePath.Text = openFileDialog1.FileName;
        }

        private void btn_analyse_Click(object sender, EventArgs e)
        {
            SaveFileFromApk(txt_filePath.Text, "AndroidManifest.xml", "AndroidManifest.xml");
            string xmlString = BinaryXmlToText("AndroidManifest.xml");
            rtb_info.Text = xmlString;
        }
        #region 算法相关
        /// <summary>
        /// 从apk文件中提取并保存指定的文件，保存在指定的目录下
        /// </summary>
        /// <param name="_apkFullName"></param>
        /// <param name="_fileName"></param>
        /// <param name="_saveFullName"></param>
        public void SaveFileFromApk(string _apkFullName, string _fileName, string _saveFullName)
        {
            SevenZipExtractor extractor = new SevenZipExtractor(_apkFullName);
            FileStream fileStream = new FileStream(_saveFullName, FileMode.Create);
            extractor.ExtractFile(_fileName, fileStream);
            fileStream.Close();
            extractor.Dispose();
        }
        /// <summary>
        /// 利用AXMLPrinter2.jar程序将二进制xml文件转化为标准xml文本
        /// </summary>
        /// <param name="_sourceFullName"></param>
        /// <param name="_targetFullName"></param>
        public string BinaryXmlToText(string _sourceFullName)
        {
            Process p = new Process();
            p.StartInfo.FileName = "java";
            p.StartInfo.Arguments = string.Format("-jar AXMLPrinter2.jar \"{0}\"", _sourceFullName);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            StreamReader myStreamReader = p.StandardOutput;
            string myString = myStreamReader.ReadToEnd();
            p.Close();
            myStreamReader.Close();
            return myString;
        }
        #endregion

        private void txt_filePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txt_filePath_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (a != null)
                {
                    string s = a.GetValue(0).ToString();
                    this.Activate();
                    txt_filePath.Text = s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in DragDrop function: " + ex.Message);
            }
        }

    }
}
