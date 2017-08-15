namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpirationDateColumnToPollsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Polls", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Polls", "ExpirationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Polls", "DateAdded");
            DropColumn("dbo.Polls", "DateUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Polls", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Polls", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Polls", "ExpirationDate");
            DropColumn("dbo.Polls", "UpdateDate");
            DropColumn("dbo.Polls", "CreationDate");
        }
    }
}
