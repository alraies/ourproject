namespace p00.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommHeeMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        CommHeeid = c.Int(nullable: false),
                        Teacherid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommHees", t => t.CommHeeid, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacherid, cascadeDelete: true)
                .Index(t => t.CommHeeid)
                .Index(t => t.Teacherid);
            
            CreateTable(
                "dbo.CommHees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AcdYea = c.String(),
                        head = c.String(),
                        CommitHeesid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CommitHees", t => t.CommitHeesid, cascadeDelete: true)
                .Index(t => t.CommitHeesid);
            
            CreateTable(
                "dbo.CommitHees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        comitname = c.String(),
                        comitDecipt = c.String(),
                        comitpriod = c.String(),
                        comittype = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TotalPoints = c.Int(nullable: false),
                        ReqDoc = c.Boolean(nullable: false),
                        DocPoints = c.Int(nullable: false),
                        CommitHeesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommitHees", t => t.CommitHeesID, cascadeDelete: true)
                .Index(t => t.CommitHeesID);
            
            CreateTable(
                "dbo.SectionstoTopics",
                c => new
                    {
                        SectionstoTopicsID = c.Int(nullable: false, identity: true),
                        SectionsID = c.Int(nullable: false),
                        TopicsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionstoTopicsID)
                .ForeignKey("dbo.Sections", t => t.SectionsID, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicsID, cascadeDelete: true)
                .Index(t => t.SectionsID)
                .Index(t => t.TopicsID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TotalPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EvaluaationFormtoSections",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EvaluationFormID = c.Int(nullable: false),
                        SectionsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EvaluationForms", t => t.EvaluationFormID, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionsID, cascadeDelete: true)
                .Index(t => t.EvaluationFormID)
                .Index(t => t.SectionsID);
            
            CreateTable(
                "dbo.EvaluationForms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        year = c.DateTime(nullable: false),
                        iscurent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TopicEVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EvaluationFormId = c.Int(nullable: false),
                        SectionsId = c.Int(nullable: false),
                        TopicsId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Nameproved = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EvaluationForms", t => t.EvaluationFormId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionsId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicsId, cascadeDelete: true)
                .Index(t => t.EvaluationFormId)
                .Index(t => t.SectionsId)
                .Index(t => t.TopicsId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        IdDocument = c.Int(nullable: false, identity: true),
                        TopicEVId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdDocument)
                .ForeignKey("dbo.TopicEVs", t => t.TopicEVId, cascadeDelete: true)
                .Index(t => t.TopicEVId);
            
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
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RecipientID = c.Int(nullable: false),
                        AccountontID = c.Int(nullable: false),
                        Messagee = c.String(),
                        AddedOn = c.DateTime(nullable: false),
                        issee = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AccountType = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SectionstoTopics", "TopicsID", "dbo.Topics");
            DropForeignKey("dbo.SectionstoTopics", "SectionsID", "dbo.Sections");
            DropForeignKey("dbo.EvaluaationFormtoSections", "SectionsID", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "TopicsId", "dbo.Topics");
            DropForeignKey("dbo.TopicEVs", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CommHeeMembers", "Teacherid", "dbo.Teachers");
            DropForeignKey("dbo.TopicEVs", "SectionsId", "dbo.Sections");
            DropForeignKey("dbo.TopicEVs", "EvaluationFormId", "dbo.EvaluationForms");
            DropForeignKey("dbo.Documents", "TopicEVId", "dbo.TopicEVs");
            DropForeignKey("dbo.EvaluaationFormtoSections", "EvaluationFormID", "dbo.EvaluationForms");
            DropForeignKey("dbo.Topics", "CommitHeesID", "dbo.CommitHees");
            DropForeignKey("dbo.CommHees", "CommitHeesid", "dbo.CommitHees");
            DropForeignKey("dbo.CommHeeMembers", "CommHeeid", "dbo.CommHees");
            DropIndex("dbo.UserToTeachers", new[] { "TeacherID" });
            DropIndex("dbo.UserToTeachers", new[] { "UserID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Documents", new[] { "TopicEVId" });
            DropIndex("dbo.TopicEVs", new[] { "TeacherId" });
            DropIndex("dbo.TopicEVs", new[] { "TopicsId" });
            DropIndex("dbo.TopicEVs", new[] { "SectionsId" });
            DropIndex("dbo.TopicEVs", new[] { "EvaluationFormId" });
            DropIndex("dbo.EvaluaationFormtoSections", new[] { "SectionsID" });
            DropIndex("dbo.EvaluaationFormtoSections", new[] { "EvaluationFormID" });
            DropIndex("dbo.SectionstoTopics", new[] { "TopicsID" });
            DropIndex("dbo.SectionstoTopics", new[] { "SectionsID" });
            DropIndex("dbo.Topics", new[] { "CommitHeesID" });
            DropIndex("dbo.CommHees", new[] { "CommitHeesid" });
            DropIndex("dbo.CommHeeMembers", new[] { "Teacherid" });
            DropIndex("dbo.CommHeeMembers", new[] { "CommHeeid" });
            DropTable("dbo.UserToTeachers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.Teachers");
            DropTable("dbo.Documents");
            DropTable("dbo.TopicEVs");
            DropTable("dbo.EvaluationForms");
            DropTable("dbo.EvaluaationFormtoSections");
            DropTable("dbo.Sections");
            DropTable("dbo.SectionstoTopics");
            DropTable("dbo.Topics");
            DropTable("dbo.CommitHees");
            DropTable("dbo.CommHees");
            DropTable("dbo.CommHeeMembers");
        }
    }
}
