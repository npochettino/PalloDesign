namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDecimalQuantity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockArticuloSucursal", "StockActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VentaItems", "Cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockMovimientos", "Cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockMovimientos", "StockActual", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockMovimientos", "StockActual", c => c.Int(nullable: false));
            AlterColumn("dbo.StockMovimientos", "Cantidad", c => c.Int(nullable: false));
            AlterColumn("dbo.VentaItems", "Cantidad", c => c.Int(nullable: false));
            AlterColumn("dbo.StockArticuloSucursal", "StockActual", c => c.Int(nullable: false));
        }
    }
}
