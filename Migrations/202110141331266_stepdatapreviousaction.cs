namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stepdatapreviousaction : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.StepDatas", "PreviousAction", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StepDatas", "PreviousAction");
        }
    }
}
