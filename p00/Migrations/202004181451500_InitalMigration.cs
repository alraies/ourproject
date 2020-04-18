namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommHees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AcdYea = c.String(),
                        head = c.String(),
                        CommitHeesid = c.Int(nullable: false),
                        CommHee_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CommHees", t => t.CommHee_id)
                .ForeignKey("dbo.CommitHees", t => t.CommitHeesid, cascadeDelete: true)
                .Index(t => t.CommitHeesid)
                .Index(t => t.CommHee_id);
            
            CreateTable(
                "dbo.CommitHees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        comitname = c.String(),
                        comitDecipt = c.String(),
                        comitpriod = c.String(),
                        comittype = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommHees", "CommitHeesid", "dbo.CommitHees");
            DropForeignKey("dbo.CommHees", "CommHee_id", "dbo.CommHees");
            DropIndex("dbo.CommHees", new[] { "CommHee_id" });
            DropIndex("dbo.CommHees", new[] { "CommitHeesid" });
            DropTable("dbo.CommitHees");
            DropTable("dbo.CommHees");
        }
    }
}
