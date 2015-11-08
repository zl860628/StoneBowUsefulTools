using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessTools.Entities.GoogleRSS
{
    public class GoogleReaderSubscription
    {
        public string Title { get; set; }

        [PrimaryKey]
        public string FeedUrl { get; set; }
    }
    /// <summary>
    /// 用于表示Google Reader每个订阅中的一条信息
    /// </summary>
    public class GoogleReaderItem
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(GoogleReaderSubscription))]
        public string SubscriptionFeedUrl { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public bool HasRead { get; set; }
        public bool IsStar { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
