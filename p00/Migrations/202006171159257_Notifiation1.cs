namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifiation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "issee", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "issee");
        }
    }
}
