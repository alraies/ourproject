namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolleFieldVaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Vacation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Vacation");
        }
    }
}
