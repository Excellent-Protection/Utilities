namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isauthoriz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StepsDetails", "IsAuthorized", c => c.Boolean(nullable: true));

        }

        public override void Down()
        {
        }
    }
}
