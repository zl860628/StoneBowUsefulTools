using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Entities
{
    public class CnBetaInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime PubTime { get; set; }
        public string Source { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
    }

    public class ViewInfo
    {
        [Key]
        public string ViewId { get; set; }
        public bool IsViewed { get; set; }
        public DateTime FirstViewTime { get; set; }
        public DateTime FinishViewTime { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
    }

    public class ReaderInfoContext : DbContext
    {      
        public DbSet<CnBetaInfo> CnBetaInfos { get; set; }
        public DbSet<ViewInfo> ViewInfoes { get; set; }

        public ReaderInfoContext() : base("TryAndTestConsole.Properties.Settings.CnBetaNewsConnectionString")
        {

        }

    }
}
