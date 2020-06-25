namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleletPrmaryKeyinTableDucment : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Documents");
            AddColumn("dbo.Documents", "IdDocument", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Documents", "IdDocument");
            DropColumn("dbo.Documents", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Documents");
            DropColumn("dbo.Documents", "IdDocument");
            AddPrimaryKey("dbo.Documents", "Id");
        }
    }
}
