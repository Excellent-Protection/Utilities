﻿namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsTestPayment : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.PaymentTransactions", "IsTest", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentTransactions", "IsTest");
        }
    }
}
