namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shortner : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UrlShorteners", new[] { "ShortUrl" });
            AddColumn("dbo.UrlShorteners", "ShortUr", c => c.String(maxLength: 100));
            AddColumn("dbo.UrlShorteners", "LongUr", c => c.String());
            AddColumn("dbo.UrlShorteners", "NoOfVisit", c => c.Int(nullable: false));
            CreateIndex("dbo.UrlShorteners", "ShortUr", unique: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.UrlShorteners", "NoOfVisits", c => c.Int(nullable: false));
            AddColumn("dbo.UrlShorteners", "LongUrl", c => c.String());
            AddColumn("dbo.UrlShorteners", "ShortUrl", c => c.String(maxLength: 100));
            DropIndex("dbo.UrlShorteners", new[] { "ShortUr" });
            DropColumn("dbo.UrlShorteners", "NoOfVisit");
            DropColumn("dbo.UrlShorteners", "LongUr");
            DropColumn("dbo.UrlShorteners", "ShortUr");
            CreateIndex("dbo.UrlShorteners", "ShortUrl", unique: true);
        }
    }
}
