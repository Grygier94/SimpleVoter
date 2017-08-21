namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserPollsRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Polls", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Polls", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polls", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Polls", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
