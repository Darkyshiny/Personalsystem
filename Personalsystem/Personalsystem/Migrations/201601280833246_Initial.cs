namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Cid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Cid, cascadeDelete: true)
                .Index(t => t.Cid);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DId, cascadeDelete: true)
                .Index(t => t.DId);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.String(maxLength: 128),
                        Content = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Uid)
                .Index(t => t.Uid);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Salary = c.Double(nullable: false),
                        CVurl = c.String(),
                        Cid = c.Int(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Cid)
                .Index(t => t.Cid);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cid = c.Int(nullable: false),
                        Content = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Cid, cascadeDelete: true)
                .Index(t => t.Cid);
            
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Cid = c.Int(nullable: false),
                        Did = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Cid, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Did)
                .Index(t => t.Cid)
                .Index(t => t.Did);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.Vacancies", "Did", "dbo.Departments");
            DropForeignKey("dbo.Vacancies", "Cid", "dbo.Companies");
            DropForeignKey("dbo.BlogPosts", "Cid", "dbo.Companies");
            DropForeignKey("dbo.PrivateMessages", "Uid", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Cid", "dbo.Companies");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "DId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Cid", "dbo.Companies");
            DropIndex("dbo.Vacancies", new[] { "Did" });
            DropIndex("dbo.Vacancies", new[] { "Cid" });
            DropIndex("dbo.BlogPosts", new[] { "Cid" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cid" });
            DropIndex("dbo.PrivateMessages", new[] { "Uid" });
            DropIndex("dbo.Groups", new[] { "DId" });
            DropIndex("dbo.Departments", new[] { "Cid" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Vacancies");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.Groups");
            DropTable("dbo.Departments");
            DropTable("dbo.Companies");
        }
    }
}
