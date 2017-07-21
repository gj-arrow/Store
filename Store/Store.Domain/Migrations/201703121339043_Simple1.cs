namespace Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Simple1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String(nullable: false));
        }
    }
}
