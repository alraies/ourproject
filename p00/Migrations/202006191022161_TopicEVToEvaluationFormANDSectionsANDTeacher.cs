namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicEVToEvaluationFormANDSectionsANDTeacher : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TopicEVs", "EvaluationFormId");
            CreateIndex("dbo.TopicEVs", "SectionsId");
            CreateIndex("dbo.TopicEVs", "TeacherId");
            AddForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms", "id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms");
            DropIndex("dbo.TopicEVs", new[] { "TeacherId" });
            DropIndex("dbo.TopicEVs", new[] { "SectionsId" });
            DropIndex("dbo.TopicEVs", new[] { "EvaluationFormId" });
        }
    }
}
