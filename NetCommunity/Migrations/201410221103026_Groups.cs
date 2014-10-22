namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Logins", "Group_Id", c => c.Int());
            CreateIndex("dbo.Logins", "Group_Id");
            AddForeignKey("dbo.Logins", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Logins", new[] { "Group_Id" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Logins", "Group_Id");
            DropTable("dbo.Groups");
        }
    }
}
