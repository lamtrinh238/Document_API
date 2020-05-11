namespace Document_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocument : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.OwnerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "OwnerID", "dbo.User");
            DropForeignKey("dbo.Document", "CategoryID", "dbo.Category");
            DropIndex("dbo.Document", new[] { "OwnerID" });
            DropIndex("dbo.Document", new[] { "CategoryID" });
            DropTable("dbo.Document");
        }
    }
}
