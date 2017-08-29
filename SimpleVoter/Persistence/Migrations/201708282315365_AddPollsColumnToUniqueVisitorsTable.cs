namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPollsColumnToUniqueVisitorsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "UniqueVisitor_Id", c => c.Int());
            CreateIndex("dbo.Polls", "UniqueVisitor_Id");
            AddForeignKey("dbo.Polls", "UniqueVisitor_Id", "dbo.UniqueVisitors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polls", "UniqueVisitor_Id", "dbo.UniqueVisitors");
            DropIndex("dbo.Polls", new[] { "UniqueVisitor_Id" });
            DropColumn("dbo.Polls", "UniqueVisitor_Id");
        }
    }
}
