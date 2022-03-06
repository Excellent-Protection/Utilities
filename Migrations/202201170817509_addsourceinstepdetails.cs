namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsourceinstepdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StepsDetails", "source", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StepsDetails", "source");
        }
    }
}
