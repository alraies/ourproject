namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPrmaryKeyinTableDucment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "IdDocument", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "IdDocument");
        }
    }
}
