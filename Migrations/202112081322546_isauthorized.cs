namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isauthorized : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StepsDetails", "IsAuthorized", c => c.Boolean(nullable: true));

        }

        public override void Down()
        {
        }
    }
}
