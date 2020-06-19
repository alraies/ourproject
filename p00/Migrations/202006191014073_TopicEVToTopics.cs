namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicEVToTopics : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TopicEVs", "TopicsId");
            AddForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics");
            DropIndex("dbo.TopicEVs", new[] { "TopicsId" });
        }
    }
}
