namespace Readit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUpAndDownVotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UpCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "DownCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DownCount");
            DropColumn("dbo.Posts", "UpCount");
        }
    }
}
