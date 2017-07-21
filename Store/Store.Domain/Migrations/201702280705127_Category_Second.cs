namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category_Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Products");
            DropColumn("dbo.Categories", "CategoryId");
            DropColumn("dbo.Products", "ProductId");
            RenameColumn(table: "dbo.Products", name: "Category_CategoryId", newName: "CategoryId");
            RenameIndex(table: "dbo.Products", name: "IX_Category_CategoryId", newName: "IX_CategoryId");
            AddColumn("dbo.Categories", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Products", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "Id");
            AddPrimaryKey("dbo.Products", "Id");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Products");
            DropPrimaryKey("dbo.Categories");
            DropColumn("dbo.Products", "Id");
            DropColumn("dbo.Categories", "Id");
            AddPrimaryKey("dbo.Products", "ProductId");
            AddPrimaryKey("dbo.Categories", "CategoryId");
            RenameIndex(table: "dbo.Products", name: "IX_CategoryId", newName: "IX_Category_CategoryId");
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_CategoryId");
            AddForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
