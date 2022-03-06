namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shortenerurl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UrlShorteners", "GeneratedURLCounter");
            DropColumn("dbo.UrlShorteners", "RegenerationDate");

        }
        
        public override void Down()
        {
        }
    }
}
