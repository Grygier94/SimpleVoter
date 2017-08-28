namespace SimpleVoter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueVisitorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UniqueVisitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAdress = c.String(maxLength: 45),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UniqueVisitors");
        }
    }
}
