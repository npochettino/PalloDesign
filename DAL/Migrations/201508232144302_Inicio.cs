namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Inicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articulos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Nombre = c.String(),
                        NombreEtiqueta = c.String(),
                        PrecioActualCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioActualVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockMinimo = c.Int(nullable: false),
                        StockMaximo = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        RubroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubros", t => t.RubroID)
                .Index(t => t.RubroID);
            
            CreateTable(
                "dbo.Rubros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockArticuloSucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticuloID = c.Int(nullable: false),
                        SucursalID = c.Int(nullable: false),
                        StockActual = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloID)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .Index(t => t.ArticuloID)
                .Index(t => t.SucursalID);
            
            CreateTable(
                "dbo.Sucursales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuariosRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SucursalID = c.Int(nullable: false),
                        RolID = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RolID)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID)
                .Index(t => t.SucursalID)
                .Index(t => t.RolID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.String(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Password = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoriasMovimientoEfectivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CierresCaja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaCierreCaja = c.DateTime(nullable: false),
                        TotalVentasEfectivo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalProveedores = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSueldos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalVentasTarjetas = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalVarios = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TurnoID = c.Int(nullable: false),
                        SucursalID = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .ForeignKey("dbo.Turnos", t => t.TurnoID)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID)
                .Index(t => t.TurnoID)
                .Index(t => t.SucursalID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.Turnos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        HoraDesde = c.Int(nullable: false),
                        HoraHasta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Apellido = c.String(),
                        Nombre = c.String(),
                        DNI = c.String(),
                        Calle = c.String(),
                        Numero = c.String(),
                        Piso = c.String(),
                        Dpto = c.String(),
                        Bis = c.Boolean(nullable: false),
                        Telefono = c.String(),
                        Email = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configuraciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaFinPeriodoPrueba = c.DateTime(nullable: false),
                        SaldoInicialTurnoMañana = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LongitudNombreEtiqueta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DevolucionesSinTicket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Motivo = c.String(),
                        RegresaAlStock = c.Boolean(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SucursalID = c.Int(nullable: false),
                        ArticuloID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloID)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .Index(t => t.SucursalID)
                .Index(t => t.ArticuloID);
            
            CreateTable(
                "dbo.FormasDePago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoricosPrecios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaDesde = c.DateTime(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ArticuloID = c.Int(nullable: false),
                        TipoHistoricoPrecioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloID)
                .ForeignKey("dbo.TipoHistoricosPrecios", t => t.TipoHistoricoPrecioID)
                .Index(t => t.ArticuloID)
                .Index(t => t.TipoHistoricoPrecioID);
            
            CreateTable(
                "dbo.TipoHistoricosPrecios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovimientosEfectivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoMovimientoID = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                        SucursalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .ForeignKey("dbo.TiposMovimientosEfectivo", t => t.TipoMovimientoID)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID)
                .Index(t => t.TipoMovimientoID)
                .Index(t => t.UsuarioID)
                .Index(t => t.SucursalID);
            
            CreateTable(
                "dbo.TiposMovimientosEfectivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Caja = c.Boolean(nullable: false),
                        Suma = c.Boolean(nullable: false),
                        CategoriaMovimientoEfectivoID = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriasMovimientoEfectivo", t => t.CategoriaMovimientoEfectivoID)
                .Index(t => t.CategoriaMovimientoEfectivoID);
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaDePagoID = c.Int(nullable: false),
                        VentaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormasDePago", t => t.FormaDePagoID)
                .ForeignKey("dbo.Ventas", t => t.VentaID)
                .Index(t => t.FormaDePagoID)
                .Index(t => t.VentaID);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaVenta = c.DateTime(nullable: false),
                        TotalVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Anulado = c.Boolean(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                        ClienteID = c.Int(nullable: false),
                        SucursalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteID)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.ClienteID)
                .Index(t => t.SucursalID);
            
            CreateTable(
                "dbo.VentaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Devuelto = c.Boolean(nullable: false),
                        VentaID = c.Int(nullable: false),
                        ArticuloID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloID)
                .ForeignKey("dbo.Ventas", t => t.VentaID)
                .Index(t => t.VentaID)
                .Index(t => t.ArticuloID);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RazonSocial = c.String(),
                        Apellido = c.String(),
                        Nombre = c.String(),
                        DNI = c.String(),
                        Calle = c.String(),
                        Numero = c.String(),
                        Piso = c.String(),
                        Dpto = c.String(),
                        Bis = c.Boolean(nullable: false),
                        Telefono = c.String(),
                        Email = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockMovimientos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        TipoMovimientoStockID = c.Int(nullable: false),
                        ArticuloID = c.Int(nullable: false),
                        SucursalID = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulos", t => t.ArticuloID)
                .ForeignKey("dbo.TiposMovimientosStock", t => t.TipoMovimientoStockID)
                .ForeignKey("dbo.Sucursales", t => t.SucursalID)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID)
                .Index(t => t.TipoMovimientoStockID)
                .Index(t => t.ArticuloID)
                .Index(t => t.SucursalID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.TiposMovimientosStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Suma = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockMovimientos", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.StockMovimientos", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.StockMovimientos", "TipoMovimientoStockID", "dbo.TiposMovimientosStock");
            DropForeignKey("dbo.StockMovimientos", "ArticuloID", "dbo.Articulos");
            DropForeignKey("dbo.VentaItems", "VentaID", "dbo.Ventas");
            DropForeignKey("dbo.VentaItems", "ArticuloID", "dbo.Articulos");
            DropForeignKey("dbo.Ventas", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.Ventas", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.Pagos", "VentaID", "dbo.Ventas");
            DropForeignKey("dbo.Ventas", "ClienteID", "dbo.Clientes");
            DropForeignKey("dbo.Pagos", "FormaDePagoID", "dbo.FormasDePago");
            DropForeignKey("dbo.MovimientosEfectivo", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.MovimientosEfectivo", "TipoMovimientoID", "dbo.TiposMovimientosEfectivo");
            DropForeignKey("dbo.TiposMovimientosEfectivo", "CategoriaMovimientoEfectivoID", "dbo.CategoriasMovimientoEfectivo");
            DropForeignKey("dbo.MovimientosEfectivo", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.HistoricosPrecios", "TipoHistoricoPrecioID", "dbo.TipoHistoricosPrecios");
            DropForeignKey("dbo.HistoricosPrecios", "ArticuloID", "dbo.Articulos");
            DropForeignKey("dbo.DevolucionesSinTicket", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.DevolucionesSinTicket", "ArticuloID", "dbo.Articulos");
            DropForeignKey("dbo.CierresCaja", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.CierresCaja", "TurnoID", "dbo.Turnos");
            DropForeignKey("dbo.CierresCaja", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.UsuariosRoles", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosRoles", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.UsuariosRoles", "RolID", "dbo.Roles");
            DropForeignKey("dbo.StockArticuloSucursal", "SucursalID", "dbo.Sucursales");
            DropForeignKey("dbo.StockArticuloSucursal", "ArticuloID", "dbo.Articulos");
            DropForeignKey("dbo.Articulos", "RubroID", "dbo.Rubros");
            DropIndex("dbo.StockMovimientos", new[] { "UsuarioID" });
            DropIndex("dbo.StockMovimientos", new[] { "SucursalID" });
            DropIndex("dbo.StockMovimientos", new[] { "ArticuloID" });
            DropIndex("dbo.StockMovimientos", new[] { "TipoMovimientoStockID" });
            DropIndex("dbo.VentaItems", new[] { "ArticuloID" });
            DropIndex("dbo.VentaItems", new[] { "VentaID" });
            DropIndex("dbo.Ventas", new[] { "SucursalID" });
            DropIndex("dbo.Ventas", new[] { "ClienteID" });
            DropIndex("dbo.Ventas", new[] { "UsuarioID" });
            DropIndex("dbo.Pagos", new[] { "VentaID" });
            DropIndex("dbo.Pagos", new[] { "FormaDePagoID" });
            DropIndex("dbo.TiposMovimientosEfectivo", new[] { "CategoriaMovimientoEfectivoID" });
            DropIndex("dbo.MovimientosEfectivo", new[] { "SucursalID" });
            DropIndex("dbo.MovimientosEfectivo", new[] { "UsuarioID" });
            DropIndex("dbo.MovimientosEfectivo", new[] { "TipoMovimientoID" });
            DropIndex("dbo.HistoricosPrecios", new[] { "TipoHistoricoPrecioID" });
            DropIndex("dbo.HistoricosPrecios", new[] { "ArticuloID" });
            DropIndex("dbo.DevolucionesSinTicket", new[] { "ArticuloID" });
            DropIndex("dbo.DevolucionesSinTicket", new[] { "SucursalID" });
            DropIndex("dbo.CierresCaja", new[] { "UsuarioID" });
            DropIndex("dbo.CierresCaja", new[] { "SucursalID" });
            DropIndex("dbo.CierresCaja", new[] { "TurnoID" });
            DropIndex("dbo.UsuariosRoles", new[] { "UsuarioID" });
            DropIndex("dbo.UsuariosRoles", new[] { "RolID" });
            DropIndex("dbo.UsuariosRoles", new[] { "SucursalID" });
            DropIndex("dbo.StockArticuloSucursal", new[] { "SucursalID" });
            DropIndex("dbo.StockArticuloSucursal", new[] { "ArticuloID" });
            DropIndex("dbo.Articulos", new[] { "RubroID" });
            DropTable("dbo.TiposMovimientosStock");
            DropTable("dbo.StockMovimientos");
            DropTable("dbo.Proveedores");
            DropTable("dbo.VentaItems");
            DropTable("dbo.Ventas");
            DropTable("dbo.Pagos");
            DropTable("dbo.TiposMovimientosEfectivo");
            DropTable("dbo.MovimientosEfectivo");
            DropTable("dbo.TipoHistoricosPrecios");
            DropTable("dbo.HistoricosPrecios");
            DropTable("dbo.FormasDePago");
            DropTable("dbo.DevolucionesSinTicket");
            DropTable("dbo.Configuraciones");
            DropTable("dbo.Clientes");
            DropTable("dbo.Turnos");
            DropTable("dbo.CierresCaja");
            DropTable("dbo.CategoriasMovimientoEfectivo");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Roles");
            DropTable("dbo.UsuariosRoles");
            DropTable("dbo.Sucursales");
            DropTable("dbo.StockArticuloSucursal");
            DropTable("dbo.Rubros");
            DropTable("dbo.Articulos");
        }
    }
}
