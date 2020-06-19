namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentToTopicEV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicEVs", "Nameproved", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopicEVs", "Nameproved");
        }
    }
}
