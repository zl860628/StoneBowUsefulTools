namespace EFDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewInfoes",
                c => new
                    {
                        ViewId = c.String(nullable: false, maxLength: 128),
                        IsViewed = c.Boolean(nullable: false),
                        FirstViewTime = c.DateTime(nullable: false),
                        FinishViewTime = c.DateTime(nullable: false),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ViewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ViewInfoes");
        }
    }
}
