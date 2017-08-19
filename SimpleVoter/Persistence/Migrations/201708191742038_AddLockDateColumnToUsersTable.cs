namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLockDateColumnToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccountLockExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccountLockExpirationDate");
        }
    }
}
