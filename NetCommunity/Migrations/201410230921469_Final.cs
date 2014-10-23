namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String(nullable: false));
        }
    }
}
