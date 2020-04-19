namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommHees", "CommHee_id", "dbo.CommHees");
            DropIndex("dbo.CommHees", new[] { "CommHee_id" });
            CreateTable(
                "dbo.CommHeeMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        CommHeeid = c.Int(nullable: false),
                        Teacherid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommHees", t => t.CommHeeid, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacherid, cascadeDelete: true)
                .Index(t => t.CommHeeid)
                .Index(t => t.Teacherid);
            
            DropColumn("dbo.CommHees", "CommHee_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommHees", "CommHee_id", c => c.Int());
            DropForeignKey("dbo.CommHeeMembers", "Teacherid", "dbo.Teachers");
            DropForeignKey("dbo.CommHeeMembers", "CommHeeid", "dbo.CommHees");
            DropIndex("dbo.CommHeeMembers", new[] { "Teacherid" });
            DropIndex("dbo.CommHeeMembers", new[] { "CommHeeid" });
            DropTable("dbo.CommHeeMembers");
            CreateIndex("dbo.CommHees", "CommHee_id");
            AddForeignKey("dbo.CommHees", "CommHee_id", "dbo.CommHees", "id");
        }
    }
}
