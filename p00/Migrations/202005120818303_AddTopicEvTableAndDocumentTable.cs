namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicEvTableAndDocumentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicEVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EvaluationFormId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Topics_Id = c.Int(),
                        Sections_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.Topics_Id)
                .ForeignKey("dbo.Sections", t => t.Sections_Id)
                .ForeignKey("dbo.EvaluationForms", t => t.EvaluationFormId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.EvaluationFormId)
                .Index(t => t.TeacherId)
                .Index(t => t.Topics_Id)
                .Index(t => t.Sections_Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TopicEVs", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms");
            DropForeignKey("dbo.TopicEVs", "Sections_Id", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "Topics_Id", "dbo.Topics");
            DropForeignKey("dbo.Documents", "Id", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "Id" });
            DropIndex("dbo.TopicEVs", new[] { "Sections_Id" });
            DropIndex("dbo.TopicEVs", new[] { "Topics_Id" });
            DropIndex("dbo.TopicEVs", new[] { "TeacherId" });
            DropIndex("dbo.TopicEVs", new[] { "EvaluationFormId" });
            DropTable("dbo.Documents");
            DropTable("dbo.TopicEVs");
        }
    }
}
