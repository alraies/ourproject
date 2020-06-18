namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvaluationToTopicEV : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicEVs", "Topics_Id", "dbo.Topics");
            DropForeignKey("dbo.TopicEVs", "Sections_Id", "dbo.Sections");
            DropIndex("dbo.TopicEVs", new[] { "Sections_Id" });
            DropIndex("dbo.TopicEVs", new[] { "Topics_Id" });
            RenameColumn(table: "dbo.TopicEVs", name: "Topics_Id", newName: "TopicsId");
            RenameColumn(table: "dbo.TopicEVs", name: "Sections_Id", newName: "SectionsId");
            AlterColumn("dbo.TopicEVs", "SectionsId", c => c.Int(nullable: false));
            AlterColumn("dbo.TopicEVs", "TopicsId", c => c.Int(nullable: false));
            CreateIndex("dbo.TopicEVs", "SectionsId");
            CreateIndex("dbo.TopicEVs", "TopicsId");
            AddForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections", "Id", cascadeDelete: true);
            DropColumn("dbo.TopicEVs", "SectionId");
            DropColumn("dbo.TopicEVs", "TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TopicEVs", "TopicId", c => c.Int(nullable: false));
            AddColumn("dbo.TopicEVs", "SectionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics");
            DropIndex("dbo.TopicEVs", new[] { "TopicsId" });
            DropIndex("dbo.TopicEVs", new[] { "SectionsId" });
            AlterColumn("dbo.TopicEVs", "TopicsId", c => c.Int());
            AlterColumn("dbo.TopicEVs", "SectionsId", c => c.Int());
            RenameColumn(table: "dbo.TopicEVs", name: "SectionsId", newName: "Sections_Id");
            RenameColumn(table: "dbo.TopicEVs", name: "TopicsId", newName: "Topics_Id");
            CreateIndex("dbo.TopicEVs", "Topics_Id");
            CreateIndex("dbo.TopicEVs", "Sections_Id");
            AddForeignKey("dbo.TopicEVs", "Sections_Id", "dbo.Sections", "Id");
            AddForeignKey("dbo.TopicEVs", "Topics_Id", "dbo.Topics", "Id");
        }
    }
}
