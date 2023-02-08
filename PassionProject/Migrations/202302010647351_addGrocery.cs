namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGrocery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groceries",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Upc = c.String(),
                        Name = c.String(),
                        Weight = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groceries", "OrderID", "dbo.Orders");
            DropIndex("dbo.Groceries", new[] { "OrderID" });
            DropTable("dbo.Groceries");
        }
    }
}
