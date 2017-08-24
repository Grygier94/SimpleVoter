namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateComputedColumsInDailyStatistics : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyStatistics", "Users", c => c.Int());
            AlterColumn("dbo.DailyStatistics", "PublicPolls", c => c.Int());
            AlterColumn("dbo.DailyStatistics", "PrivatePolls", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyStatistics", "PrivatePolls", c => c.Int(nullable: false));
            AlterColumn("dbo.DailyStatistics", "PublicPolls", c => c.Int(nullable: false));
            AlterColumn("dbo.DailyStatistics", "Users", c => c.Int(nullable: false));
        }
    }
}
