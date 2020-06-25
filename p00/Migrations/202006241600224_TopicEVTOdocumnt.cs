namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicEVTOdocumnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "IdTopicEV", c => c.Int(nullable: false));
            AddColumn("dbo.Documents", "TopicEV_Id", c => c.Int());
            CreateIndex("dbo.Documents", "TopicEV_Id");
            AddForeignKey("dbo.Documents", "TopicEV_Id", "dbo.TopicEVs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "TopicEV_Id", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "TopicEV_Id" });
            DropColumn("dbo.Documents", "TopicEV_Id");
            DropColumn("dbo.Documents", "IdTopicEV");
        }
    }
}
