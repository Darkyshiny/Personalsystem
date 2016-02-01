namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        uId = c.String(nullable: false, maxLength: 128),
                        vId = c.Int(nullable: false),
                        CoverLetter = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.uId, cascadeDelete: true)
                .ForeignKey("dbo.Vacancies", t => t.vId, cascadeDelete: true)
                .Index(t => t.uId)
                .Index(t => t.vId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "vId", "dbo.Vacancies");
            DropForeignKey("dbo.Applications", "uId", "dbo.AspNetUsers");
            DropIndex("dbo.Applications", new[] { "vId" });
            DropIndex("dbo.Applications", new[] { "uId" });
            DropTable("dbo.Applications");
        }
    }
}
