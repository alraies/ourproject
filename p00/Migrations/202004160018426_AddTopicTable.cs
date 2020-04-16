namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Pid = c.Int(nullable: false, identity: true),
                        TopicName = c.String(nullable: false),
                        Points = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        ReqDoc = c.Boolean(nullable: false),
                        PointForDoc = c.Int(nullable: false),
                        Approvingly = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Topics");
        }
    }
}
