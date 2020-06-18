namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvaluationToTopicEV2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "Id", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "Id" });
            DropPrimaryKey("dbo.Documents");
            AlterColumn("dbo.Documents", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Documents", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Documents");
            AlterColumn("dbo.Documents", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Documents", "Id");
            CreateIndex("dbo.Documents", "Id");
            AddForeignKey("dbo.Documents", "Id", "dbo.TopicEVs", "Id");
        }
    }
}
