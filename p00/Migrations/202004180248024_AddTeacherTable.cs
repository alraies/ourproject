namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        University = c.String(),
                        College = c.String(),
                        Department = c.String(),
                        Certificate = c.String(nullable: false),
                        C_Date = c.DateTime(nullable: false),
                        C_Doner = c.String(),
                        GeneralSpecialization = c.String(),
                        Specialization = c.String(),
                        ScientificTitle = c.String(),
                        ST_Date = c.DateTime(nullable: false),
                        ST_Doner = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
        }
    }
}
