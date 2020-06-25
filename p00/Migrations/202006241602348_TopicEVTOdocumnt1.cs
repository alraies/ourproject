namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicEVTOdocumnt1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "TopicEV_Id", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "TopicEV_Id" });
            RenameColumn(table: "dbo.Documents", name: "TopicEV_Id", newName: "TopicEVId");
            AlterColumn("dbo.Documents", "TopicEVId", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "TopicEVId");
            AddForeignKey("dbo.Documents", "TopicEVId", "dbo.TopicEVs", "Id", cascadeDelete: true);
            DropColumn("dbo.Documents", "IdTopicEV");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "IdTopicEV", c => c.Int(nullable: false));
            DropForeignKey("dbo.Documents", "TopicEVId", "dbo.TopicEVs");
            DropIndex("dbo.Documents", new[] { "TopicEVId" });
            AlterColumn("dbo.Documents", "TopicEVId", c => c.Int());
            RenameColumn(table: "dbo.Documents", name: "TopicEVId", newName: "TopicEV_Id");
            CreateIndex("dbo.Documents", "TopicEV_Id");
            AddForeignKey("dbo.Documents", "TopicEV_Id", "dbo.TopicEVs", "Id");
        }
    }
}
