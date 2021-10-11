namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authorization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StepsDetails", "IsAuthorized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StepsDetails", "IsAuthorized");
        }
    }
}
