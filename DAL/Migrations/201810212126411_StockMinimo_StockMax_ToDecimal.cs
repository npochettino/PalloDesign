namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockMinimo_StockMax_ToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articulos", "StockMinimo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Articulos", "StockMaximo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articulos", "StockMaximo", c => c.Int(nullable: false));
            AlterColumn("dbo.Articulos", "StockMinimo", c => c.Int(nullable: false));
        }
    }
}
