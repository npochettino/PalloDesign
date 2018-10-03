namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockActualEnMovimientoDeStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockMovimientos", "StockActual", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockMovimientos", "StockActual");
        }
    }
}
