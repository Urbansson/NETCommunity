namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Groups", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Description", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String());
        }
    }
}
