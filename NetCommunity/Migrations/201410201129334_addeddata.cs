namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Title", c => c.String());
            DropColumn("dbo.Messages", "Time");
            DropColumn("dbo.Messages", "IsRead");
            DropColumn("dbo.Messages", "Content");
        }
    }
}
