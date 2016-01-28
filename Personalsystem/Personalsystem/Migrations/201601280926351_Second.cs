namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Departments", new[] { "Cid" });
            DropIndex("dbo.Groups", new[] { "DId" });
            DropIndex("dbo.PrivateMessages", new[] { "Uid" });
            DropIndex("dbo.AspNetUsers", new[] { "Cid" });
            DropIndex("dbo.BlogPosts", new[] { "Cid" });
            DropIndex("dbo.Vacancies", new[] { "Cid" });
            DropIndex("dbo.Vacancies", new[] { "Did" });
            CreateIndex("dbo.Departments", "cId");
            CreateIndex("dbo.Groups", "dId");
            CreateIndex("dbo.PrivateMessages", "uId");
            CreateIndex("dbo.AspNetUsers", "cId");
            CreateIndex("dbo.BlogPosts", "cId");
            CreateIndex("dbo.Vacancies", "cId");
            CreateIndex("dbo.Vacancies", "dId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vacancies", new[] { "dId" });
            DropIndex("dbo.Vacancies", new[] { "cId" });
            DropIndex("dbo.BlogPosts", new[] { "cId" });
            DropIndex("dbo.AspNetUsers", new[] { "cId" });
            DropIndex("dbo.PrivateMessages", new[] { "uId" });
            DropIndex("dbo.Groups", new[] { "dId" });
            DropIndex("dbo.Departments", new[] { "cId" });
            CreateIndex("dbo.Vacancies", "Did");
            CreateIndex("dbo.Vacancies", "Cid");
            CreateIndex("dbo.BlogPosts", "Cid");
            CreateIndex("dbo.AspNetUsers", "Cid");
            CreateIndex("dbo.PrivateMessages", "Uid");
            CreateIndex("dbo.Groups", "DId");
            CreateIndex("dbo.Departments", "Cid");
        }
    }
}
