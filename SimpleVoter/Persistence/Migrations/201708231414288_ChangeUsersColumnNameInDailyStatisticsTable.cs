namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUsersColumnNameInDailyStatisticsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyStatistics", "Users", c => c.Int(nullable: false));
            DropColumn("dbo.DailyStatistics", "TotalUsers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyStatistics", "TotalUsers", c => c.Int(nullable: false));
            DropColumn("dbo.DailyStatistics", "Users");
        }
    }
}
