namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.String(nullable: false, maxLength: 128),
                        Vid = c.Int(nullable: false),
                        CoverLetter = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Uid, cascadeDelete: true)
                .ForeignKey("dbo.Vacancies", t => t.Vid, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Uid)
                .Index(t => t.Vid)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Salary = c.Double(nullable: false),
                        CVurl = c.String(),
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
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
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
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        SenderId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Company_Id);
            
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
                "dbo.Vacancies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Did = c.Int(nullable: false),
                        Cid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Cid, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Did, cascadeDelete: true)
                .Index(t => t.Did)
                .Index(t => t.Cid);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
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
            DropForeignKey("dbo.Messages", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Departments", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Applications", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Applications", "Vid", "dbo.Vacancies");
            DropForeignKey("dbo.Vacancies", "Did", "dbo.Departments");
            DropForeignKey("dbo.Groups", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Vacancies", "Cid", "dbo.Companies");
            DropForeignKey("dbo.Applications", "Uid", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Groups", new[] { "Department_Id" });
            DropIndex("dbo.Departments", new[] { "Company_Id" });
            DropIndex("dbo.Vacancies", new[] { "Cid" });
            DropIndex("dbo.Vacancies", new[] { "Did" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Messages", new[] { "Company_Id" });
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.Applications", new[] { "Company_Id" });
            DropIndex("dbo.Applications", new[] { "Vid" });
            DropIndex("dbo.Applications", new[] { "Uid" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Groups");
            DropTable("dbo.Departments");
            DropTable("dbo.Vacancies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Applications");
            DropTable("dbo.Companies");
        }
    }
}
