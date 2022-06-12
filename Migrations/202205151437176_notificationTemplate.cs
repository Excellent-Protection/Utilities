namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class notificationTemplate : DbMigration
    {
        public override void Up()
        {
    //        CreateTable(
    //"dbo.NotificationTemplates",
    //c => new
    //{
    //    Id = c.Guid(nullable: false),
    //    CreatedBy = c.String(maxLength: 250),
    //    CreatedOn = c.DateTime(),
    //    ModifiedBy = c.String(maxLength: 250),
    //    ModifiedOn = c.DateTime(),
    //    DeletedOn = c.DateTime(),
    //    DeletedBy = c.String(maxLength: 250),
    //    IsDeleted = c.Boolean(nullable: false),
    //    IsDeactivated = c.Boolean(nullable: false),
    //    OwnerId = c.String(maxLength: 128),
    //    Name = c.String(maxLength: 250),
    //    Code = c.String(maxLength: 250),
    //    Text = c.String(maxLength: 500),
    //    Type = c.Int(nullable: false),
    //    SqlQuery = c.String()
    //})
    //.PrimaryKey(t => t.Id)
    //.ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
    //.Index(t => t.OwnerId);

    //        AddColumn("dbo.UserNotifications", "NotificationTemplateId", c => c.String());
        }

        public override void Down()
        {
        }
    }
}
