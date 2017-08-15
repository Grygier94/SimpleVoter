namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndedByOwnerColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "EndedByOwner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "EndedByOwner");
        }
    }
}
