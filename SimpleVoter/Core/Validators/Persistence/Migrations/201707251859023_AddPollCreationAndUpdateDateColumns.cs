namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPollCreationAndUpdateDateColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Polls", "DateUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "DateUpdated");
            DropColumn("dbo.Polls", "DateAdded");
        }
    }
}
