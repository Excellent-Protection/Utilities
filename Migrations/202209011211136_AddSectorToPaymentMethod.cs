namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSectorToPaymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.paymentMethods", "Sector", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.paymentMethods", "Sector");
        }
    }
}
