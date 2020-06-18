namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserToTeacher1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserToTeachers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.TeacherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToTeachers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToTeachers", "TeacherID", "dbo.Teachers");
            DropIndex("dbo.UserToTeachers", new[] { "TeacherID" });
            DropIndex("dbo.UserToTeachers", new[] { "UserID" });
            DropTable("dbo.UserToTeachers");
        }
    }
}
