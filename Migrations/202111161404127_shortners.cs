namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shortners : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UrlShorteners", new[] { "ShortUr" });
            AddColumn("dbo.UrlShorteners", "ShortUrl", c => c.String(maxLength: 100));
            AddColumn("dbo.UrlShorteners", "LongUrl", c => c.String());
            AddColumn("dbo.UrlShorteners", "NoOfVisits", c => c.Int(nullable: false));
            CreateIndex("dbo.UrlShorteners", "ShortUrl", unique: true);
            DropColumn("dbo.UrlShorteners", "ShortUr");
            DropColumn("dbo.UrlShorteners", "LongUr");
            DropColumn("dbo.UrlShorteners", "NoOfVisit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UrlShorteners", "NoOfVisit", c => c.Int(nullable: false));
            AddColumn("dbo.UrlShorteners", "LongUr", c => c.String());
            AddColumn("dbo.UrlShorteners", "ShortUr", c => c.String(maxLength: 100));
            DropIndex("dbo.UrlShorteners", new[] { "ShortUrl" });
            DropColumn("dbo.UrlShorteners", "NoOfVisits");
            DropColumn("dbo.UrlShorteners", "LongUrl");
            DropColumn("dbo.UrlShorteners", "ShortUrl");
            CreateIndex("dbo.UrlShorteners", "ShortUr", unique: true);
        }
    }
}
