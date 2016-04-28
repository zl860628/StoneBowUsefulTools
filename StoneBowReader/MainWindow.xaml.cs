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
        CnBetaInfo _selectedCnBetaInfo = null;

        public MainWindow()
        {
            InitializeComponent();

            ShowNewsFromLaserRead();
            //ShowLastestNews(1000);
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
                                                 Title = newsInfo.Title + " | 字数：" + newsInfo.Content.Length,
                                                 Introduce = newsInfo.Introduction,
                                                 IsViewed = joinedInfo == null || !joinedInfo.IsViewed ? false : true,
                                                 NewsInfo = newsInfo
                                             };
                newSummaryItems = lastestUnreadNewsQuery.Take(newCount).ToList();
            }

            listbox_summary.ItemsSource = newSummaryItems;
        }
        private void ShowNewsFromLaserRead()
        {
            List<NewsSummaryItem> newSummaryItems = null;

            using (ReaderInfoContext context = new ReaderInfoContext())
            {
                ViewInfo lastReadNewsInfo = context.ViewInfoes.Where(d => d.IsViewed == true).OrderByDescending(d => d.ViewId).First();

                var lastestUnreadNewsQuery = from newsInfo in context.CnBetaInfos where String.Compare(newsInfo.Id, lastReadNewsInfo.ViewId) > 0
                                             join viewInfo in context.ViewInfoes on newsInfo.Id equals viewInfo.ViewId into joinedInfoes
                                             from joinedInfo in joinedInfoes.DefaultIfEmpty()
                                             where joinedInfo == null || joinedInfo.IsViewed == false
                                             orderby newsInfo.PubTime ascending
                                             select new NewsSummaryItem
                                             {
                                                 Title = newsInfo.Title + " | 字数：" + newsInfo.Content.Length,
                                                 Introduce = newsInfo.Introduction,
                                                 IsViewed = joinedInfo == null || !joinedInfo.IsViewed ? false : true,
                                                 NewsInfo = newsInfo
                                             };
                newSummaryItems = lastestUnreadNewsQuery.ToList();
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
            _selectedCnBetaInfo = item.NewsInfo;

            wb_content.NavigateToString(GenerateBrowseContent(_selectedCnBetaInfo));

            using (ReaderInfoContext context = new ReaderInfoContext())
            {
                ViewInfo viewInfo = null;
                try
                {
                    viewInfo = context.ViewInfoes.Find(_selectedCnBetaInfo.Id);
                }
                catch (Exception ex)
                {
                }
                if (viewInfo == null)
                {
                    viewInfo = new ViewInfo()
                    {
                        ViewId = _selectedCnBetaInfo.Id,
                        FirstViewTime = DateTime.Now,
                        FinishViewTime = DateTime.Now,
                        IsViewed = false
                    };
                    context.ViewInfoes.Add(viewInfo);
                    context.SaveChanges();
                }
            }

            checkbox_AlreadyRead.IsChecked = false;
        }

        private void wb_content_LoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            mshtml.HTMLDocument htmlDoc_content = browser.Document as mshtml.HTMLDocument;
            htmlDoc_content.focus();
            htmlDoc_content.focus();
        }

        private void ChangeAlreadyRead(bool hasAlreadyRead)
        {
            using (ReaderInfoContext context = new ReaderInfoContext())
            {
                ViewInfo viewInfo = null;
                try
                {
                    viewInfo = context.ViewInfoes.Find(_selectedCnBetaInfo.Id);
                }
                catch (Exception ex)
                {
                }
                if (viewInfo == null)
                {
                    viewInfo = new ViewInfo()
                    {
                        ViewId = _selectedCnBetaInfo.Id,
                        FirstViewTime = DateTime.Now,
                        FinishViewTime = DateTime.Now,
                        IsViewed = hasAlreadyRead
                    };
                    context.ViewInfoes.Add(viewInfo);
                }
                else
                {
                    viewInfo.FinishViewTime = DateTime.Now;
                    viewInfo.IsViewed = hasAlreadyRead;
                }

                context.SaveChanges();
            }

            NewsSummaryItem item = (NewsSummaryItem)(listbox_summary.SelectedItem);
            item.IsViewed = hasAlreadyRead;
        }
        private void checkbox_AlreadyRead_Checked(object sender, RoutedEventArgs e)
        {
            ChangeAlreadyRead(true);
        }

        private void checkbox_AlreadyRead_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeAlreadyRead(false);
        }

        private void toolBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeAlreadyRead(true);
        }
    }
}
