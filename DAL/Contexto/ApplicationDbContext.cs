using Modelos;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<CierreCaja> CierresCaja { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<MovimientoEfectivo> MovimientosEfectivo { get; set; }
        public DbSet<FormaDePago> FormasDePago { get; set; }
        public DbSet<HistoricoPrecio> HistoricosPrecios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<StockMovimiento> StockMovimientos { get; set; }
        public DbSet<TipoMovimientoEfectivo> TiposMovimientosEfectivo { get; set; }
        public DbSet<TipoHistoricoPrecio> TipoHistoricosPrecios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaItem> VentaItems { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<StockArticuloSucursal> StocksArticuloSucursal { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }
        public DbSet<TipoMovimientoStock> TiposMovimientosStock { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<CategoriaMovimientoEfectivo> CategoriasMovimientoEfectivo { get; set; }
        public DbSet<DevolucionSinTicket> DevolucionesSinTicket { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Entity<Articulo>().ToTable("Articulos");
            modelBuilder.Entity<CierreCaja>().ToTable("CierresCaja");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<MovimientoEfectivo>().ToTable("MovimientosEfectivo");
            modelBuilder.Entity<FormaDePago>().ToTable("FormasDePago");
            modelBuilder.Entity<HistoricoPrecio>().ToTable("HistoricosPrecios");
            modelBuilder.Entity<Pago>().ToTable("Pagos");
            modelBuilder.Entity<Proveedor>().ToTable("Proveedores");
            modelBuilder.Entity<StockMovimiento>().ToTable("StockMovimientos");
            modelBuilder.Entity<TipoMovimientoEfectivo>().ToTable("TiposMovimientosEfectivo");
            modelBuilder.Entity<TipoHistoricoPrecio>().ToTable("TipoHistoricosPrecios");
            modelBuilder.Entity<Venta>().ToTable("Ventas");
            modelBuilder.Entity<VentaItem>().ToTable("VentaItems");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Configuracion>().ToTable("Configuraciones");
            modelBuilder.Entity<Sucursal>().ToTable("Sucursales");
            modelBuilder.Entity<Rubro>().ToTable("Rubros");
            modelBuilder.Entity<StockArticuloSucursal>().ToTable("StockArticuloSucursal");
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<UsuarioRol>().ToTable("UsuariosRoles");
            modelBuilder.Entity<TipoMovimientoStock>().ToTable("TiposMovimientosStock");
            modelBuilder.Entity<Turno>().ToTable("Turnos");
            modelBuilder.Entity<CategoriaMovimientoEfectivo>().ToTable("CategoriasMovimientoEfectivo");
            modelBuilder.Entity<DevolucionSinTicket>().ToTable("DevolucionesSinTicket");

        }

        private void FixEfProvider()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }


}
