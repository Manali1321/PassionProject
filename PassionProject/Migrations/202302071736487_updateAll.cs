namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groceries", "OrderID", "dbo.Orders");
            DropIndex("dbo.Groceries", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "StoreId" });
            AddColumn("dbo.Orders", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "StoreID");
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Groceries", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Groceries", "OrderID");
            DropColumn("dbo.Orders", "OrderId");
            DropColumn("dbo.Orders", "Upc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Upc", c => c.String());
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Groceries", "OrderID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Groceries");
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "StoreID" });
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "OrderNumber");
            CreateIndex("dbo.Orders", "StoreId");
            CreateIndex("dbo.Groceries", "OrderID");
            AddForeignKey("dbo.Groceries", "OrderID", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
