namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvaluationToTopicEV3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms");
            DropForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics");
            DropIndex("dbo.TopicEVs", new[] { "EvaluationFormId" });
            DropIndex("dbo.TopicEVs", new[] { "SectionsId" });
            DropIndex("dbo.TopicEVs", new[] { "TopicsId" });
            DropIndex("dbo.TopicEVs", new[] { "TeacherId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TopicEVs", "TeacherId");
            CreateIndex("dbo.TopicEVs", "TopicsId");
            CreateIndex("dbo.TopicEVs", "SectionsId");
            CreateIndex("dbo.TopicEVs", "EvaluationFormId");
            AddForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms", "id", cascadeDelete: true);
        }
    }
}
