using EFDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoneBowReader
{
    public class NewsSummaryItem : INotifyPropertyChanged
    {
        private bool _isViewed;

        public event PropertyChangedEventHandler PropertyChanged;

        virtual internal protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Title { get; set; }
        public FontWeight TitleFontWeight { get; set; }
        public string Introduce { get; set; }
        public CnBetaInfo NewsInfo { get; set; }
        public bool IsViewed
        {
            get
            {
                return _isViewed;
            }
            set
            {
                _isViewed = value;
                if (_isViewed)
                {
                    TitleFontWeight = FontWeights.Regular;
                }
                else
                {
                    TitleFontWeight = FontWeights.Bold;
                }
                OnPropertyChanged("TitleFontWeight");
            }
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ShowLastestNews(20);
        }

        private void ShowLastestNews(int newCount)
        {
            List<NewsSummaryItem> newSummaryItems = null;

            using (ReaderInfoContext context = new ReaderInfoContext())
            {
                var lastestUnreadNewsQuery = from newsInfo in context.CnBetaInfos
                                             join viewInfo in context.ViewInfoes on newsInfo.Id equals viewInfo.ViewId into joinedInfoes
                                             from joinedInfo in joinedInfoes.DefaultIfEmpty()
                                             where joinedInfo == null || joinedInfo.IsViewed == false
                                             orderby newsInfo.PubTime descending
                                             select new NewsSummaryItem
                                             {
                                                 Title = newsInfo.Title,
                                                 Introduce = newsInfo.Introduction,
                                                 IsViewed = joinedInfo == null || !joinedInfo.IsViewed ? false : true,
                                                 NewsInfo = newsInfo
                                             };
                newSummaryItems = lastestUnreadNewsQuery.Take(newCount).ToList();
            }

            listbox_summary.ItemsSource = newSummaryItems;
        }

        private String GenerateBrowseContent(CnBetaInfo cnBetaInfo)
        {
            StringBuilder sbdContent = new StringBuilder();
            sbdContent.AppendLine("<html>");
            sbdContent.AppendLine("<meta charset=\"utf-8\">");
            sbdContent.AppendLine("<style>body{font-family: 微软雅黑,宋体}</style>");
            sbdContent.AppendFormat("<h2 align=\"center\" font-size=\"18px\" font-weight=\"bold\">{0}</h2>\n", cnBetaInfo.Title);
            sbdContent.AppendFormat("<p align=\"center\">{0} | From : {1}</p>\n", cnBetaInfo.PubTime, cnBetaInfo.Source);
            sbdContent.AppendFormat("<p>{0}</p>\n", cnBetaInfo.Introduction);
            sbdContent.AppendLine("</html>");
            sbdContent.AppendLine(cnBetaInfo.Content);

            return sbdContent.ToString();
        }

        private void menu_main_data_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateData window_UpdateData = new UpdateData();
            window_UpdateData.ShowDialog();
        }

        private void listbox_summary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            NewsSummaryItem item = (NewsSummaryItem)(listbox.SelectedItem);
            CnBetaInfo cnBetaInfo = item.NewsInfo;
            wb_content.NavigateToString(GenerateBrowseContent(cnBetaInfo));
            mshtml.HTMLDocument htmlDoc_content = wb_content.Document as mshtml.HTMLDocument;
            htmlDoc_content.focus();
            htmlDoc_content.parentWindow.execScript("document.onmousedown=function(e) { alert('a'); }", "javascript");

            WebBrowser browser_content = new WebBrowser();
            browser_content.NavigateToString(GenerateBrowseContent(cnBetaInfo));
            mshtml.HTMLDocument htmlDoc = browser_content.Document as mshtml.HTMLDocument;
            htmlDoc.parentWindow.execScript("document.onmousedown=function(e) { alert('a'); }", "javascript");

            frame_webContent.Content = browser_content;

            //using (ReaderInfoContext context = new ReaderInfoContext())
            //{
            //    ViewInfo viewInfo = null;
            //    try
            //    {
            //        viewInfo = context.ViewInfoes.Find(cnBetaInfo.Id);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    if (viewInfo == null)
            //    {
            //        viewInfo = new ViewInfo()
            //        {
            //            ViewId = cnBetaInfo.Id,
            //            FirstViewTime = DateTime.Now,
            //            FinishViewTime = DateTime.Now,
            //            IsViewed = true
            //        };
            //        context.ViewInfoes.Add(viewInfo);
            //        context.SaveChanges();
            //    }
            //}

            if (!item.IsViewed)
            {
                item.IsViewed = true;
            }
        }

        private void frame_webContent_MouseEnter(object sender, MouseEventArgs e)
        {
            if (frame_webContent.Content is WebBrowser)
            {
                WebBrowser browser_content = frame_webContent.Content as WebBrowser;
                mshtml.HTMLDocument htmlDoc = browser_content.Document as mshtml.HTMLDocument;
                //htmlDoc.parentWindow.execScript("alert('a')", "javascript");
                htmlDoc.focus();
            }
            else
            {
                Console.WriteLine("Mouse enter when frame's content is not WebBrowser");
            }
        }

        private void wb_content_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument htmlDoc_content = wb_content.Document as mshtml.HTMLDocument;
            htmlDoc_content.focus();
        }
    }
}
