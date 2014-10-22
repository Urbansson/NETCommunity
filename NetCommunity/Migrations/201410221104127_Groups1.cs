namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Logins", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Logins", new[] { "Group_Id" });
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Group_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Group_Id);
            
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            DropColumn("dbo.Logins", "Group_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "Group_Id", c => c.Int());
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.ApplicationUserGroups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserGroups", new[] { "Group_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserGroups");
            CreateIndex("dbo.Logins", "Group_Id");
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            AddForeignKey("dbo.Logins", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
