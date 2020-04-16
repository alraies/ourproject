namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoulmnToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccountType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccountType");
        }
    }
}
