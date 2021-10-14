namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestepHeader : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StepsHeaders", "ServiceType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StepsHeaders", "ServiceType", c => c.String());
        }
    }
}
