namespace Document_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        OwnerID = c.Int(nullable: false),
                        Cover = c.Binary(),
                        Description = c.String(maxLength: 255),
                        CoverFileExtension = c.String(maxLength: 255),
                        ContentFileExtension = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "OwnerID", "dbo.User");
            DropForeignKey("dbo.Document", "CategoryID", "dbo.Category");
            DropIndex("dbo.Document", new[] { "OwnerID" });
            DropIndex("dbo.Document", new[] { "CategoryID" });
            DropTable("dbo.User");
            DropTable("dbo.Document");
            DropTable("dbo.Category");
        }
    }
}
