namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "uId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Applications", "uId");
            AddForeignKey("dbo.Applications", "uId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "uId", "dbo.AspNetUsers");
            DropIndex("dbo.Applications", new[] { "uId" });
            DropColumn("dbo.Applications", "uId");
        }
    }
}
