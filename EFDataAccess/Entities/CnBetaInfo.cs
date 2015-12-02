using System;
using System.Collections.Generic;
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
        public string Content { get; set; }
        public string URL { get; set; }
    }

    public class CnBetaInfoContext : DbContext
    {
        public DbSet<CnBetaInfo> CnBetaInfos { get; set; }
    }
}
