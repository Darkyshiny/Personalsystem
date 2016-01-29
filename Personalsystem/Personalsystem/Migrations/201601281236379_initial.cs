namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PrivateMessages", new[] { "Uid" });
            AddColumn("dbo.Vacancies", "dId", c => c.Int());
            CreateIndex("dbo.PrivateMessages", "uId");
            CreateIndex("dbo.Vacancies", "dId");
            AddForeignKey("dbo.Vacancies", "dId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacancies", "dId", "dbo.Departments");
            DropIndex("dbo.Vacancies", new[] { "dId" });
            DropIndex("dbo.PrivateMessages", new[] { "uId" });
            DropColumn("dbo.Vacancies", "dId");
            CreateIndex("dbo.PrivateMessages", "Uid");
        }
    }
}
