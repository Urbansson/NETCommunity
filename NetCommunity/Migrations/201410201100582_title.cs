namespace NetCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Title");
        }
    }
}
