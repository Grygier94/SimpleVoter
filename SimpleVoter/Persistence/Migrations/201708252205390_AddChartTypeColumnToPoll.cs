namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChartTypeColumnToPoll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "ChartType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "ChartType");
        }
    }
}
