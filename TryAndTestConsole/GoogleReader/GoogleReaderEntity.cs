using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.GoogleReaderEntity
{
    public class GoogleFeed
    {
        public string FeedName;
        public int UnreadCount;

        public GoogleFeed(string _feedName, int _unreadCount)
        {
            FeedName = _feedName;
            UnreadCount = _unreadCount;
        }
    }
}