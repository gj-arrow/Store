namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12345 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
