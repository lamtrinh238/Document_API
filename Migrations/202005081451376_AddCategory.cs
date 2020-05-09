namespace Document_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
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
            
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "FirstName", c => c.String(maxLength: 255));
            AlterColumn("dbo.User", "LastName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "LastName", c => c.String());
            AlterColumn("dbo.User", "FirstName", c => c.String());
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false));
            DropTable("dbo.Category");
        }
    }
}
