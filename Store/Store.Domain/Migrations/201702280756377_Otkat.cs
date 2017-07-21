namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Otkat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");   
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Products");
            DropColumn("dbo.Categories", "Id");
            DropColumn("dbo.Products", "Id");
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_CategoryId");
            RenameIndex(table: "dbo.Products", name: "IX_CategoryId", newName: "IX_Category_CategoryId");
            AddColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Products", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddPrimaryKey("dbo.Products", "ProductId");
            AddForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Categories", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Products");
            DropPrimaryKey("dbo.Categories");
            DropColumn("dbo.Products", "ProductId");
            DropColumn("dbo.Categories", "CategoryId");
            AddPrimaryKey("dbo.Products", "Id");
            AddPrimaryKey("dbo.Categories", "Id");
            RenameIndex(table: "dbo.Products", name: "IX_Category_CategoryId", newName: "IX_CategoryId");
            RenameColumn(table: "dbo.Products", name: "Category_CategoryId", newName: "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
