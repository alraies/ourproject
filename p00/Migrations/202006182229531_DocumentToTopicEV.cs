namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentToTopicEV : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Documents");
            AddColumn("dbo.Documents", "Name", c => c.String());
            AlterColumn("dbo.Documents", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Documents", "Id");
            CreateIndex("dbo.Documents", "Id");
            AddForeignKey("dbo.Documents", "Id", "dbo.TopicEVs", "Id");
            DropColumn("dbo.Documents", "Approved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Approved", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Documents", "Id", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "Id" });
            DropPrimaryKey("dbo.Documents");
            AlterColumn("dbo.Documents", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Documents", "Name");
            AddPrimaryKey("dbo.Documents", "Id");
        }
    }
}
