namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChques_Referencias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Referencia", c => c.String());
            AddColumn("dbo.Proveedores", "Referencia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proveedores", "Referencia");
            DropColumn("dbo.Clientes", "Referencia");
        }
    }
}
