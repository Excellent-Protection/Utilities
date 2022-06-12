namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getNotifications : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.NotificationTemplates",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            CreatedBy = c.String(),
            //            CreatedOn = c.DateTime(),
            //            ModifiedBy = c.String(),
            //            ModifiedOn = c.DateTime(),
            //            DeletedOn = c.DateTime(),
            //            DeletedBy = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            IsDeactivated = c.Boolean(nullable: false),
            //            OwnerId = c.String(maxLength: 128),
            //            Name = c.String(),
            //            Code = c.String(),
            //            Text = c.String(),
            //            Type = c.Int(nullable: false),
            //            SqlQuery = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
            //    .Index(t => t.OwnerId);
            
            AddColumn("dbo.UserNotifications", "UserId", c => c.String());
            AddColumn("dbo.UserNotifications", "Readed", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.NotificationTemplates", "OwnerId", "dbo.AspNetUsers");
            //DropIndex("dbo.NotificationTemplates", new[] { "OwnerId" });
            //DropColumn("dbo.UserNotifications", "Readed");
            //DropColumn("dbo.UserNotifications", "UserId");
            //DropTable("dbo.NotificationTemplates");
        }
    }
}
