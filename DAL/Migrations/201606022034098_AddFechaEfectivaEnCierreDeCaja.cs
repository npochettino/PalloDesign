namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFechaEfectivaEnCierreDeCaja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CierresCaja", "FechaCierreEfectiva", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CierresCaja", "FechaCierreEfectiva");
        }
    }
}
