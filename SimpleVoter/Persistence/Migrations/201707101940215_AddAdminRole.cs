namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (1, 'Administrator')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Id = 1");
        }
    }
}
