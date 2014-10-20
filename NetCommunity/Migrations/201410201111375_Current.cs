namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ReciverdId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ReciverdId");
        }
    }
}
