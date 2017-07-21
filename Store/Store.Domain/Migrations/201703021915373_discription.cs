namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
