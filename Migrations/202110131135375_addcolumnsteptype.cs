namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnsteptype : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.StepsDetails", "StepType", c => c.Int(nullable: false));
            //AlterColumn("dbo.StepsHeaders", "ServiceType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StepsHeaders", "ServiceType", c => c.String());
            DropColumn("dbo.StepsDetails", "StepType");
        }
    }
}
