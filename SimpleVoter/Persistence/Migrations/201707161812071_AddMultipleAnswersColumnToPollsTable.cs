namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultipleAnswersColumnToPollsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "AllowMultipleAnswers", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Polls", "AllowMultipleAnswers");
        }
    }
}
