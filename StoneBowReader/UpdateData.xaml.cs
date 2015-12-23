using EFDataAccess.Entities;
using StoneBowReader.InfoParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StoneBowReader
{
    /// <summary>
    /// UpdateData.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateData : Window
    {
        public UpdateData()
        {
            InitializeComponent();
        }

        private void btn_update_CnBeta_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(UpdateCnBetaNews);
            task.Start();
        }

        private void UpdateCnBetaNews()
        {
            int lastestId = 456555;
            // Read database and get the lastest news id
            using (ReaderInfoContext context = new ReaderInfoContext())
            {
                CnBetaInfo lastestNews = context.CnBetaInfos.OrderByDescending(d => d.Id).First();
                lastestId = Int32.Parse(lastestNews.Id.Replace("CnBeta", ""));
                tb_message.Dispatcher.Invoke(() => { tb_message.AppendText(String.Format("数据库中最近新闻为: Id ({0}), Date ({1})\n", lastestNews.Id, lastestNews.PubTime)); });
            }

            CnBetaNewsParser parser = new CnBetaNewsParser();

            int errorCount = 0, succCount = 0;
            while (errorCount < 20)
            {
                string url = "http://www.cnbeta.com/articles/" + lastestId.ToString() + ".htm";
                CnBetaInfo cnBetaInfo = parser.ParseCnBetaInfo(url);
                if (cnBetaInfo != null)
                {
                    using (ReaderInfoContext context = new ReaderInfoContext())
                    {
                        try
                        {
                            context.CnBetaInfos.Add(cnBetaInfo);
                            context.SaveChanges();
                            succCount++;
                            tb_message.Dispatcher.Invoke(() => { tb_message.AppendText(String.Format("Record successfully for : {0}\n", url)); });
                        }
                        catch (Exception ex)
                        {
                            tb_message.Dispatcher.Invoke(() => { tb_message.AppendText(String.Format("Record error for : {0}\n", url)); });
                        }
                    }
                    errorCount = 0;
                }
                else
                {
                    errorCount++;
                }
                lastestId += 2;
            }

            tb_message.Dispatcher.Invoke(() => { tb_message.AppendText(String.Format("Totally add records count is {0}\n", succCount)); });
        }
    }
}
