namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Title", c => c.String());
            AddColumn("dbo.BlogPosts", "postedBy", c => c.String());
            AddColumn("dbo.BlogPosts", "publicPost", c => c.Boolean(nullable: false));
            DropColumn("dbo.BlogPosts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPosts", "Name", c => c.String());
            DropColumn("dbo.BlogPosts", "publicPost");
            DropColumn("dbo.BlogPosts", "postedBy");
            DropColumn("dbo.BlogPosts", "Title");
        }
    }
}
