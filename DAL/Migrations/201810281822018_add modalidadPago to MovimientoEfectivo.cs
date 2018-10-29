namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodalidadPagotoMovimientoEfectivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientosEfectivo", "FormaDePagoID", c => c.Int(nullable: false));
            CreateIndex("dbo.MovimientosEfectivo", "FormaDePagoID");
            AddForeignKey("dbo.MovimientosEfectivo", "FormaDePagoID", "dbo.FormasDePago", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientosEfectivo", "FormaDePagoID", "dbo.FormasDePago");
            DropIndex("dbo.MovimientosEfectivo", new[] { "FormaDePagoID" });
            DropColumn("dbo.MovimientosEfectivo", "FormaDePagoID");
        }
    }
}
