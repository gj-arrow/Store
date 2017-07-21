namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Picture", c => c.String());
            DropColumn("dbo.Products", "ImageData");
            DropColumn("dbo.Products", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageMimeType", c => c.String());
            AddColumn("dbo.Products", "ImageData", c => c.Binary());
            DropColumn("dbo.Products", "Picture");
        }
    }
}
