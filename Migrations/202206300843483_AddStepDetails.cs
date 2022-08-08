namespace Utilities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStepDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepsDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StepHeaderId = c.Guid(),
                        StepOrder = c.Int(),
                        IsAvailable = c.Boolean(),
                        DBResourceName = c.String(),
                        DBResourceFieldName = c.String(),
                        IsAuthorized = c.Boolean(nullable: false),
                        source = c.Int(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                        IconClass = c.String(),
                        PreviousStepAction = c.String(),
                        NextStepAction = c.String(),
                        StepKeyword = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsDeactivated = c.Boolean(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        StepType = c.Int(nullable: false),
                        Name = c.String(),
                        IsVisible = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.StepsHeaders", t => t.StepHeaderId)
                .Index(t => t.StepHeaderId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.StepsHeaders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        Description = c.String(),
                        IsAvailable = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsDeactivated = c.Boolean(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepsDetails", "StepHeaderId", "dbo.StepsHeaders");
            DropForeignKey("dbo.StepsHeaders", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StepsDetails", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.StepsHeaders", new[] { "OwnerId" });
            DropIndex("dbo.StepsDetails", new[] { "OwnerId" });
            DropIndex("dbo.StepsDetails", new[] { "StepHeaderId" });
            DropTable("dbo.StepsHeaders");
            DropTable("dbo.StepsDetails");
        }
    }
}
