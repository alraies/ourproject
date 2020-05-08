namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEvaluationForm1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvaluationForms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        year = c.DateTime(nullable: false),
                        iscurent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EvaluationForms");
        }
    }
}
