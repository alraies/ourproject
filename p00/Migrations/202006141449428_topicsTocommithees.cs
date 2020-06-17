namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topicsTocommithees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "CommitHeesID", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "CommitHeesID");
            AddForeignKey("dbo.Topics", "CommitHeesID", "dbo.CommitHees", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "CommitHeesID", "dbo.CommitHees");
            DropIndex("dbo.Topics", new[] { "CommitHeesID" });
            DropColumn("dbo.Topics", "CommitHeesID");
        }
    }
}
