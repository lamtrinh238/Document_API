namespace Document_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_document_table_ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "CoverFileExtension", c => c.String(maxLength: 255));
            AddColumn("dbo.Document", "ContentFileExtension", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "ContentFileExtension");
            DropColumn("dbo.Document", "CoverFileExtension");
        }
    }
}
