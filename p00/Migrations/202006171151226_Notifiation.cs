namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifiation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    RecipientID = c.Int(nullable: false),
                    AccountontID = c.Int(nullable: false),
                    Messagee = c.String(),
                    AddedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id);

        }

        public override void Down()
        {
            DropTable("dbo.Notifications");
        }
    }
}
