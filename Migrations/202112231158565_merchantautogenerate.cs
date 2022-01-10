namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merchantautogenerate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentTransactions", "MerchantTransactionId", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newId()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentTransactions", "MerchantTransactionId", c => c.String(maxLength: 250));
        }
    }
}
