namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentmethodbysector : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.paymentMethods", "Authorization", c => c.String(maxLength: 250));
            AddColumn("dbo.paymentMethods", "CheckOutUrl", c => c.String(maxLength: 250));
            AddColumn("dbo.paymentMethods", "QueryUrl", c => c.String(maxLength: 250));
            AddColumn("dbo.paymentMethods", "IsTest", c => c.Boolean());
            DropColumn("dbo.paymentMethods", "EntityIdTest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.paymentMethods", "EntityIdTest", c => c.String(maxLength: 250));
            DropColumn("dbo.paymentMethods", "IsTest");
            DropColumn("dbo.paymentMethods", "QueryUrl");
            DropColumn("dbo.paymentMethods", "CheckOutUrl");
            DropColumn("dbo.paymentMethods", "Authorization");
        }
    }
}
