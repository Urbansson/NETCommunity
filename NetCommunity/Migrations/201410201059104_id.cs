namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Messages", name: "Sender_Id", newName: "SenderId");
            RenameIndex(table: "dbo.Messages", name: "IX_Sender_Id", newName: "IX_SenderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Messages", name: "IX_SenderId", newName: "IX_Sender_Id");
            RenameColumn(table: "dbo.Messages", name: "SenderId", newName: "Sender_Id");
        }
    }
}
