namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageCounters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TotalMessages", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ReadMessages", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RemovedMessages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RemovedMessages");
            DropColumn("dbo.AspNetUsers", "ReadMessages");
            DropColumn("dbo.AspNetUsers", "TotalMessages");
        }
    }
}
