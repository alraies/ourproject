namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPrmaryKeyinTableDucment1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicEVs", "Document_Id", "dbo.Documents");
            DropIndex("dbo.TopicEVs", new[] { "Document_Id" });
            DropColumn("dbo.TopicEVs", "Document_Id");
            DropColumn("dbo.Documents", "IdTopicEV");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "IdTopicEV", c => c.Int(nullable: false));
            AddColumn("dbo.TopicEVs", "Document_Id", c => c.Int());
            CreateIndex("dbo.TopicEVs", "Document_Id");
            AddForeignKey("dbo.TopicEVs", "Document_Id", "dbo.Documents", "Id");
        }
    }
}
