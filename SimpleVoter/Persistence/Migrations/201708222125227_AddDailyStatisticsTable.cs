namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDailyStatisticsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date", defaultValueSql: "GETUTCDATE()"),
                        NewUsers = c.Int(nullable: false),
                        DeletedUsers = c.Int(nullable: false),
                        TotalUsers = c.Int(nullable: false),
                        NewPublicPolls = c.Int(nullable: false),
                        DeletedPublicPolls = c.Int(nullable: false),
                        PublicPolls = c.Int(nullable: false),
                        NewPrivatePolls = c.Int(nullable: false),
                        DeletedPrivatePolls = c.Int(nullable: false),
                        PrivatePolls = c.Int(nullable: false),
                        UniqueVisitors = c.Int(nullable: false),
                        PageViews = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyStatistics");
        }
    }
}
