namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRenewingDateColumnToPollTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "RenewingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "RenewingDate");
        }
    }
}
