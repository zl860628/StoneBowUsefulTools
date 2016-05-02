using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EFDataAccess.Entities
{
    public class WebsiteNewsInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime PubTime { get; set; }
        public string Source { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
    }

    public class WebsiteViewInfo
    {
        [Key]
        public string ViewId { get; set; }
        public bool IsViewed { get; set; }
        public bool IsReadLater { get; set; }
        public DateTime FirstViewTime { get; set; }
        public DateTime FinishViewTime { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
    }

    public class WebsiteReaderInfoContext : DbContext
    {
        public DbSet<WebsiteNewsInfo> WebsiteNewsInfoes { get; set; }
        public DbSet<WebsiteViewInfo> WebsiteViewInfoes { get; set; }

        public WebsiteReaderInfoContext() : base("TryAndTestConsole.Properties.Settings.Azure_LeoDataBase1ConnectionString")
        {

        }
    }
}
