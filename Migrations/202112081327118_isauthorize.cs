namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isauthorize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StepsDetails", "IsAuthorized", c => c.Boolean(nullable: false));

        }
        
        public override void Down()
        {
        }
    }
}
