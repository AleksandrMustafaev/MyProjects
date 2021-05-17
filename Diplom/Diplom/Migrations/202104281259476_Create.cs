namespace Diplom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.String(),
                        AcademicTitle = c.String(),
                        AcademicDegree = c.String(),
                        AffiliatedOrganization = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GroupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                        ConferenceName = c.String(),
                        SqopusLink = c.String(),
                        WebOfScienceLink = c.String(),
                        Annotation = c.String(),
                        Name = c.String(),
                        Text = c.String(),
                        Language = c.String(),
                        UserId = c.String(nullable: false),
                        PublicationTypeId = c.Int(nullable: false),
                        JournalId = c.Int(),
                        PublisherId = c.Int(),
                        ConferenceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConferenceModels", t => t.ConferenceId)
                .ForeignKey("dbo.JournalModels", t => t.JournalId)
                .ForeignKey("dbo.PublicationTypeModels", t => t.PublicationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PublisherModels", t => t.PublisherId)
                .Index(t => t.PublicationTypeId)
                .Index(t => t.JournalId)
                .Index(t => t.PublisherId)
                .Index(t => t.ConferenceId);
            
            CreateTable(
                "dbo.ConferenceModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        ConferenceNumber = c.String(),
                        County = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationDescriptionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Annotation = c.String(),
                        Name = c.String(),
                        PublicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PublicationModels", t => t.PublicationId, cascadeDelete: true)
                .Index(t => t.PublicationId);
            
            CreateTable(
                "dbo.JournalModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KeyWordModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationTypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublisherModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        MiddleName = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.GroupModelsAuthorModels",
                c => new
                    {
                        GroupModels_Id = c.Int(nullable: false),
                        AuthorModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupModels_Id, t.AuthorModels_Id })
                .ForeignKey("dbo.GroupModels", t => t.GroupModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.AuthorModels", t => t.AuthorModels_Id, cascadeDelete: true)
                .Index(t => t.GroupModels_Id)
                .Index(t => t.AuthorModels_Id);
            
            CreateTable(
                "dbo.PublicationModelsAuthorModels",
                c => new
                    {
                        PublicationModels_Id = c.Int(nullable: false),
                        AuthorModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PublicationModels_Id, t.AuthorModels_Id })
                .ForeignKey("dbo.PublicationModels", t => t.PublicationModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.AuthorModels", t => t.AuthorModels_Id, cascadeDelete: true)
                .Index(t => t.PublicationModels_Id)
                .Index(t => t.AuthorModels_Id);
            
            CreateTable(
                "dbo.KeyWordModelsPublicationModels",
                c => new
                    {
                        KeyWordModels_Id = c.Int(nullable: false),
                        PublicationModels_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.KeyWordModels_Id, t.PublicationModels_Id })
                .ForeignKey("dbo.KeyWordModels", t => t.KeyWordModels_Id, cascadeDelete: true)
                .ForeignKey("dbo.PublicationModels", t => t.PublicationModels_Id, cascadeDelete: true)
                .Index(t => t.KeyWordModels_Id)
                .Index(t => t.PublicationModels_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AuthorModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PublicationModels", "PublisherId", "dbo.PublisherModels");
            DropForeignKey("dbo.PublicationModels", "PublicationTypeId", "dbo.PublicationTypeModels");
            DropForeignKey("dbo.KeyWordModelsPublicationModels", "PublicationModels_Id", "dbo.PublicationModels");
            DropForeignKey("dbo.KeyWordModelsPublicationModels", "KeyWordModels_Id", "dbo.KeyWordModels");
            DropForeignKey("dbo.PublicationModels", "JournalId", "dbo.JournalModels");
            DropForeignKey("dbo.PublicationDescriptionModels", "PublicationId", "dbo.PublicationModels");
            DropForeignKey("dbo.PublicationModels", "ConferenceId", "dbo.ConferenceModels");
            DropForeignKey("dbo.PublicationModelsAuthorModels", "AuthorModels_Id", "dbo.AuthorModels");
            DropForeignKey("dbo.PublicationModelsAuthorModels", "PublicationModels_Id", "dbo.PublicationModels");
            DropForeignKey("dbo.GroupModelsAuthorModels", "AuthorModels_Id", "dbo.AuthorModels");
            DropForeignKey("dbo.GroupModelsAuthorModels", "GroupModels_Id", "dbo.GroupModels");
            DropIndex("dbo.KeyWordModelsPublicationModels", new[] { "PublicationModels_Id" });
            DropIndex("dbo.KeyWordModelsPublicationModels", new[] { "KeyWordModels_Id" });
            DropIndex("dbo.PublicationModelsAuthorModels", new[] { "AuthorModels_Id" });
            DropIndex("dbo.PublicationModelsAuthorModels", new[] { "PublicationModels_Id" });
            DropIndex("dbo.GroupModelsAuthorModels", new[] { "AuthorModels_Id" });
            DropIndex("dbo.GroupModelsAuthorModels", new[] { "GroupModels_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PublicationDescriptionModels", new[] { "PublicationId" });
            DropIndex("dbo.PublicationModels", new[] { "ConferenceId" });
            DropIndex("dbo.PublicationModels", new[] { "PublisherId" });
            DropIndex("dbo.PublicationModels", new[] { "JournalId" });
            DropIndex("dbo.PublicationModels", new[] { "PublicationTypeId" });
            DropIndex("dbo.AuthorModels", new[] { "UserId" });
            DropTable("dbo.KeyWordModelsPublicationModels");
            DropTable("dbo.PublicationModelsAuthorModels");
            DropTable("dbo.GroupModelsAuthorModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PublisherModels");
            DropTable("dbo.PublicationTypeModels");
            DropTable("dbo.KeyWordModels");
            DropTable("dbo.JournalModels");
            DropTable("dbo.PublicationDescriptionModels");
            DropTable("dbo.ConferenceModels");
            DropTable("dbo.PublicationModels");
            DropTable("dbo.GroupModels");
            DropTable("dbo.AuthorModels");
        }
    }
}
