namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymenttransactionsector : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentTransactions", "Sector", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentTransactions", "Sector");
        }
    }
}
