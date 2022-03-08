namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class platformPayment : DbMigration
    {
        public override void Up()
        {
             AddColumn("dbo.paymentMethods", "Platforms", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.paymentMethods", "Platforms");
        }
    }
}
