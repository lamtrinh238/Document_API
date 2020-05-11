namespace Document_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_document_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "Content");
        }
    }
}
