namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePollExpirationDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Polls", "ExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Polls", "ExpirationDate", c => c.DateTime(nullable: false));
        }
    }
}
