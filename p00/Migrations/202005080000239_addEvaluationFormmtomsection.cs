namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEvaluationFormmtomsection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvaluaationFormtoSections",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EvaluationFormID = c.Int(nullable: false),
                        SectionsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EvaluationForms", t => t.EvaluationFormID, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionsID, cascadeDelete: true)
                .Index(t => t.EvaluationFormID)
                .Index(t => t.SectionsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvaluaationFormtoSections", "SectionsID", "dbo.Sections");
            DropForeignKey("dbo.EvaluaationFormtoSections", "EvaluationFormID", "dbo.EvaluationForms");
            DropIndex("dbo.EvaluaationFormtoSections", new[] { "SectionsID" });
            DropIndex("dbo.EvaluaationFormtoSections", new[] { "EvaluationFormID" });
            DropTable("dbo.EvaluaationFormtoSections");
        }
    }
}
