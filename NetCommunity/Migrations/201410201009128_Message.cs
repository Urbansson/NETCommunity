namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Messages", name: "Reciver_Id", newName: "Sender_Id");
            RenameIndex(table: "dbo.Messages", name: "IX_Reciver_Id", newName: "IX_Sender_Id");
            DropColumn("dbo.Messages", "Title");
            DropColumn("dbo.Messages", "Content");
            DropColumn("dbo.Messages", "IsRead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Messages", "Title", c => c.String(nullable: false));
            RenameIndex(table: "dbo.Messages", name: "IX_Sender_Id", newName: "IX_Reciver_Id");
            RenameColumn(table: "dbo.Messages", name: "Sender_Id", newName: "Reciver_Id");
        }
    }
}
