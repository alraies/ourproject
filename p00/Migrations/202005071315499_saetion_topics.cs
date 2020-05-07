namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saetion_topics : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "SectionsId", "dbo.Sections");
            DropIndex("dbo.Topics", new[] { "SectionsId" });
            CreateTable(
                "dbo.SectionstoTopics",
                c => new
                    {
                        SectionstoTopicsID = c.Int(nullable: false, identity: true),
                        SectionsID = c.Int(nullable: false),
                        TopicsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionstoTopicsID)
                .ForeignKey("dbo.Sections", t => t.SectionsID, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicsID, cascadeDelete: true)
                .Index(t => t.SectionsID)
                .Index(t => t.TopicsID);
            
            DropColumn("dbo.Topics", "SectionsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "SectionsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SectionstoTopics", "TopicsID", "dbo.Topics");
            DropForeignKey("dbo.SectionstoTopics", "SectionsID", "dbo.Sections");
            DropIndex("dbo.SectionstoTopics", new[] { "TopicsID" });
            DropIndex("dbo.SectionstoTopics", new[] { "SectionsID" });
            DropTable("dbo.SectionstoTopics");
            CreateIndex("dbo.Topics", "SectionsId");
            AddForeignKey("dbo.Topics", "SectionsId", "dbo.Sections", "Id", cascadeDelete: true);
        }
    }
}
