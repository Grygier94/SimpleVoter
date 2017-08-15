namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVisibilityColumnToPollTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "Visibility", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "Visibility");
        }
    }
}
