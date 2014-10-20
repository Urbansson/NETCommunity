namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2keys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "SenderId" });
            AddColumn("dbo.Messages", "ReciverId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Messages", "SenderId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Messages", "SenderId");
            CreateIndex("dbo.Messages", "ReciverId");
            AddForeignKey("dbo.Messages", "ReciverId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Messages", "ReciverdId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ReciverdId", c => c.String());
            DropForeignKey("dbo.Messages", "ReciverId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ReciverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            AlterColumn("dbo.Messages", "SenderId", c => c.String(maxLength: 128));
            DropColumn("dbo.Messages", "ReciverId");
            CreateIndex("dbo.Messages", "SenderId");
        }
    }
}
