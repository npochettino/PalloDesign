namespace DAL.Migrations
{
    using Contexto;
    using Modelos;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Contexto.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Configuraciones.AddOrUpdate(
                a => new { a.Id, a.FechaFinPeriodoPrueba, a.SaldoInicialTurnoMañana, a.LongitudNombreEtiqueta },
                new Modelos.Configuracion { Id = 1, FechaFinPeriodoPrueba = DateTime.Now.AddMonths(12), SaldoInicialTurnoMañana = 500M, LongitudNombreEtiqueta = 15 }
                );
            context.SaveChanges();

            context.Usuarios.AddOrUpdate(
               a => new { a.Id, a.Apellido, a.DNI, a.Nombre, a.Password, a.Habilitado },
               new Modelos.Usuario { Id = 1, DNI = "admin", Apellido = "admin", Nombre = "admin", Password = "admin", Habilitado = true },
                new Modelos.Usuario { Id = 2, DNI = "32542761", Apellido = "Battista", Nombre = "Franco", Password = "101010", Habilitado = true },
                new Modelos.Usuario { Id = 3, DNI = "vend1", Apellido = "vend1", Nombre = "vend1", Password = "vend1", Habilitado = true },
                new Modelos.Usuario { Id = 4, DNI = "vend2", Apellido = "vend2", Nombre = "vend2", Password = "vend2", Habilitado = true },
                new Modelos.Usuario { Id = 5, DNI = "resp1", Apellido = "resp1", Nombre = "resp1", Password = "resp1", Habilitado = true },
                new Usuario { Id = 6, DNI = "resp2", Apellido = "resp2", Nombre = "resp2", Password = "resp2", Habilitado = true },
                new Modelos.Usuario { Id = 7, DNI = "deposito", Apellido = "deposito", Nombre = "deposito", Password = "deposito", Habilitado = true }
               );
            context.SaveChanges();

            context.Proveedores.AddOrUpdate(
               a => new { a.Id, a.RazonSocial, a.Nombre, a.Apellido, a.DNI, a.Calle, a.Numero, a.Piso, a.Dpto, a.Bis, a.Telefono, a.Email, a.Habilitado },
               new Proveedor { Id = 1, RazonSocial = "D&R Asociados", Nombre = "Diego", Apellido = "Roca", DNI = "25874125", Calle = "Las Heras", Numero = "1147", Piso = "5", Dpto = "A", Bis = false, Telefono = "3415789471", Email = "diego@roca.com.ar", Habilitado = true },
               new Proveedor { Id = 2, RazonSocial = "J&G Asociados", Nombre = "Jorge", Apellido = "Garcia", DNI = "28987548", Calle = "Moreno", Numero = "325", Piso = "2", Dpto = "C", Bis = false, Telefono = "3412586958", Email = "jorge@garcia.com.ar", Habilitado = true }
               );
            context.SaveChanges();

            context.Turnos.AddOrUpdate(
              a => new { a.Id, a.Nombre, a.HoraDesde, a.HoraHasta },
              new Turno { Id = 1, Nombre = "Mañana", HoraDesde = 6, HoraHasta = 16 },
              new Turno { Id = 2, Nombre = "Tarde", HoraDesde = 16, HoraHasta = 25 }
              );
            context.SaveChanges();

            context.Sucursales.AddOrUpdate(
               a => new { a.Id, a.Nombre, a.Descripcion },
               new Modelos.Sucursal { Id = 1, Nombre = "Depósito", Descripcion = "Desc. de Depósito" },
               new Modelos.Sucursal { Id = 2, Nombre = "Sucursal 1", Descripcion = "Desc. de Sucursal 1" },
               new Modelos.Sucursal { Id = 3, Nombre = "Sucursal 2", Descripcion = "Desc. de Sucursal 2" }
               );
            context.SaveChanges();

            context.CategoriasMovimientoEfectivo.AddOrUpdate(
             a => new { a.Id, a.Nombre },
             new Modelos.CategoriaMovimientoEfectivo { Id = 1, Nombre = "Proveedores" },
             new Modelos.CategoriaMovimientoEfectivo { Id = 2, Nombre = "Sueldos" },
             new Modelos.CategoriaMovimientoEfectivo { Id = 3, Nombre = "Varios" }
             );
            context.SaveChanges();

            context.TiposMovimientosEfectivo.AddOrUpdate(
               a => new { a.Id, a.Nombre, a.Caja, a.Suma, a.Habilitado },
               new Modelos.TipoMovimientoEfectivo { Id = 1, Nombre = "Pago Sueldo Vendedor ", CategoriaMovimientoEfectivoID = 2, Caja = true, Suma = false, Habilitado = true },
               new Modelos.TipoMovimientoEfectivo { Id = 2, Nombre = "Pago Sueldo Encargado ", CategoriaMovimientoEfectivoID = 2, Caja = false, Suma = false, Habilitado = true },
               new Modelos.TipoMovimientoEfectivo { Id = 3, Nombre = "Pago Alquiler", CategoriaMovimientoEfectivoID = 3, Caja = false, Suma = false, Habilitado = true },
               new Modelos.TipoMovimientoEfectivo { Id = 4, Nombre = "Ingreso Efectivo a Caja", CategoriaMovimientoEfectivoID = 3, Caja = true, Suma = true, Habilitado = true },
               new Modelos.TipoMovimientoEfectivo { Id = 5, Nombre = "Retiro Efectivo de Caja ", CategoriaMovimientoEfectivoID = 3, Caja = true, Suma = false, Habilitado = true },
               new Modelos.TipoMovimientoEfectivo { Id = 6, Nombre = "Pago a Proveedor", CategoriaMovimientoEfectivoID = 1, Caja = true, Suma = false, Habilitado = true }
               );
            context.SaveChanges();

            context.TiposMovimientosStock.AddOrUpdate(
               a => new { a.Id, a.Nombre, a.Suma },
               new Modelos.TipoMovimientoStock { Id = 1, Nombre = "Venta", Suma = false },
               new Modelos.TipoMovimientoStock { Id = 2, Nombre = "Reposición", Suma = true },
               new Modelos.TipoMovimientoStock { Id = 3, Nombre = "Devolución", Suma = true },
               new Modelos.TipoMovimientoStock { Id = 4, Nombre = "Extravío", Suma = false }
               );
            context.SaveChanges();

            context.FormasDePago.AddOrUpdate(
              a => new { a.Id, a.Nombre, a.Recargo },
              new FormaDePago { Id = 1, Nombre = "Efectivo", Recargo = 1 },
              new Modelos.FormaDePago { Id = 2, Nombre = "Tarjeta Débito", Recargo = 1 },
              new Modelos.FormaDePago { Id = 3, Nombre = "Tarjeta Crédito", Recargo = decimal.Parse("1,2") },
              new Modelos.FormaDePago { Id = 4, Nombre = "Devolución", Recargo = 1 }
              );
            context.SaveChanges();

            context.Rubros.AddOrUpdate(
               a => new { a.Id, a.Nombre },
               new Modelos.Rubro { Id = 1, Nombre = "Bazar", Descripcion = "Desc. de Bazar", Habilitado = true },
               new Modelos.Rubro { Id = 2, Nombre = "Juguetería", Descripcion = "", Habilitado = true },
               new Modelos.Rubro { Id = 3, Nombre = "Regalería", Descripcion = "Desc. de Regalería", Habilitado = true },
               new Modelos.Rubro { Id = 4, Nombre = "Varios", Descripcion = "Varios", Habilitado = true }
               );
            context.SaveChanges();

            context.TipoHistoricosPrecios.AddOrUpdate(
               a => new { a.Id, a.Nombre },
               new Modelos.TipoHistoricoPrecio { Id = 1, Nombre = "Compra" },
               new Modelos.TipoHistoricoPrecio { Id = 2, Nombre = "Venta" }
               );
            context.SaveChanges();

            context.Articulos.AddOrUpdate(
               a => new { a.Id, a.Nombre, a.NombreEtiqueta, a.Codigo, a.PrecioActualCompra, a.PrecioActualVenta, a.StockMinimo, a.StockMaximo, a.RubroID, a.Habilitado },
               new Modelos.Articulo { Id = 1, Codigo = "1000000000016", Nombre = "Juego de Mesa", NombreEtiqueta = "Juego de Mesa", PrecioActualCompra = 500, PrecioActualVenta = 599, StockMinimo = 2, StockMaximo = 10, RubroID = 3, Habilitado = true },
               new Modelos.Articulo { Id = 2, Codigo = "1000000000023", Nombre = "Set Asado", NombreEtiqueta = "Set Asado", PrecioActualCompra = 120, PrecioActualVenta = 159, StockMinimo = 3, StockMaximo = 8, RubroID = 1, Habilitado = true },
               new Modelos.Articulo { Id = 3, Codigo = "1000000000030", Nombre = "Set Vino", NombreEtiqueta = "Set Vino", PrecioActualCompra = 180, PrecioActualVenta = 219, StockMinimo = 3, StockMaximo = 7, RubroID = 1, Habilitado = true },
               new Modelos.Articulo { Id = 4, Codigo = "1000000000047", Nombre = "Reloj Pulsera", NombreEtiqueta = "Reloj Pulsera", PrecioActualCompra = 59, PrecioActualVenta = 89, StockMinimo = 30, StockMaximo = 50, RubroID = 3, Habilitado = true },
               new Modelos.Articulo { Id = 5, Codigo = "1000000000054", Nombre = "Vaso Térmico", NombreEtiqueta = "Vaso Térmico", PrecioActualCompra = 79, PrecioActualVenta = 99, StockMinimo = 15, StockMaximo = 30, RubroID = 3, Habilitado = true }
               );
            context.SaveChanges();

            context.HistoricosPrecios.AddOrUpdate(
               a => new { a.Id, a.FechaDesde, a.Precio, a.ArticuloID, a.TipoHistoricoPrecioID },
               new Modelos.HistoricoPrecio { Id = 1, FechaDesde = new DateTime(2015, 11, 1), Precio = 500, ArticuloID = 1, TipoHistoricoPrecioID = 1 },
               new Modelos.HistoricoPrecio { Id = 2, FechaDesde = new DateTime(2015, 11, 1), Precio = 120, ArticuloID = 2, TipoHistoricoPrecioID = 1 },
               new Modelos.HistoricoPrecio { Id = 3, FechaDesde = new DateTime(2015, 11, 1), Precio = 180, ArticuloID = 3, TipoHistoricoPrecioID = 1 },
               new Modelos.HistoricoPrecio { Id = 4, FechaDesde = new DateTime(2015, 11, 1), Precio = 59, ArticuloID = 4, TipoHistoricoPrecioID = 1 },
               new Modelos.HistoricoPrecio { Id = 5, FechaDesde = new DateTime(2015, 11, 1), Precio = 79, ArticuloID = 5, TipoHistoricoPrecioID = 1 },
               new Modelos.HistoricoPrecio { Id = 6, FechaDesde = new DateTime(2015, 11, 1), Precio = 599, ArticuloID = 1, TipoHistoricoPrecioID = 2 },
               new Modelos.HistoricoPrecio { Id = 7, FechaDesde = new DateTime(2015, 11, 1), Precio = 159, ArticuloID = 2, TipoHistoricoPrecioID = 2 },
               new Modelos.HistoricoPrecio { Id = 8, FechaDesde = new DateTime(2015, 11, 1), Precio = 219, ArticuloID = 3, TipoHistoricoPrecioID = 2 },
               new Modelos.HistoricoPrecio { Id = 9, FechaDesde = new DateTime(2015, 11, 1), Precio = 89, ArticuloID = 4, TipoHistoricoPrecioID = 2 },
               new Modelos.HistoricoPrecio { Id = 10, FechaDesde = new DateTime(2015, 11, 1), Precio = 99, ArticuloID = 5, TipoHistoricoPrecioID = 2 }
               );
            context.SaveChanges();

            context.Clientes.AddOrUpdate(
               a => new { a.Id, a.Nombre, a.Apellido, a.DNI, a.Calle, a.Numero, a.Piso, a.Dpto, a.Bis, a.Telefono, a.Email, a.Habilitado },
               new Cliente { Id = 1, Nombre = "Efectivo", Apellido = "Venta", DNI = "", Calle = "", Numero = "", Piso = "", Dpto = "", Bis = false, Telefono = "", Email = "", Habilitado = false },
               new Cliente { Id = 2, Nombre = "Marcos", Apellido = "Lazo", DNI = "25985748", Calle = "Paz", Numero = "256", Piso = "2", Dpto = "C", Bis = false, Telefono = "3412574891", Email = "marcos@lazo.com.ar", Habilitado = true },
               new Cliente { Id = 3, Nombre = "Pedro", Apellido = "Miller", DNI = "31524785", Calle = "Colon", Numero = "1274", Piso = "3", Dpto = "A", Bis = false, Telefono = "3412569857", Email = "pedro@miller.com.ar", Habilitado = true }
               );

            context.StocksArticuloSucursal.AddOrUpdate(
               a => new { a.Id, a.StockActual, a.ArticuloID, a.SucursalID },
               new StockArticuloSucursal { Id = 1, StockActual = 10, ArticuloID = 1, SucursalID = 1 },
               new StockArticuloSucursal { Id = 2, StockActual = 10, ArticuloID = 2, SucursalID = 1 },
               new StockArticuloSucursal { Id = 3, StockActual = 10, ArticuloID = 3, SucursalID = 1 },
               new StockArticuloSucursal { Id = 4, StockActual = 10, ArticuloID = 4, SucursalID = 1 },
               new StockArticuloSucursal { Id = 5, StockActual = 10, ArticuloID = 5, SucursalID = 1 },
               new StockArticuloSucursal { Id = 6, StockActual = 7, ArticuloID = 1, SucursalID = 2 },
               new StockArticuloSucursal { Id = 7, StockActual = 5, ArticuloID = 2, SucursalID = 2 },
               new StockArticuloSucursal { Id = 8, StockActual = 4, ArticuloID = 3, SucursalID = 2 },
               new StockArticuloSucursal { Id = 9, StockActual = 35, ArticuloID = 4, SucursalID = 2 },
               new StockArticuloSucursal { Id = 10, StockActual = 25, ArticuloID = 5, SucursalID = 2 },
               new StockArticuloSucursal { Id = 11, StockActual = 7, ArticuloID = 1, SucursalID = 3 },
               new StockArticuloSucursal { Id = 12, StockActual = 5, ArticuloID = 2, SucursalID = 3 },
               new StockArticuloSucursal { Id = 13, StockActual = 4, ArticuloID = 3, SucursalID = 3 },
               new StockArticuloSucursal { Id = 14, StockActual = 35, ArticuloID = 4, SucursalID = 3 },
               new StockArticuloSucursal { Id = 15, StockActual = 25, ArticuloID = 5, SucursalID = 3 }
               );

            context.Roles.AddOrUpdate(
        a => new { a.Id, a.Nombre },
        new Modelos.Rol { Id = 1, Nombre = "Administrador" },
        new Modelos.Rol { Id = 2, Nombre = "Responsable" },
        new Modelos.Rol { Id = 3, Nombre = "Vendedor" },
        new Modelos.Rol { Id = 4, Nombre = "Adm. de Stock" }
        );
            context.SaveChanges();

            context.UsuariosRoles.AddOrUpdate(
      a => new { a.Id, a.RolID, a.SucursalID, a.UsuarioID },
      new Modelos.UsuarioRol { Id = 1, RolID = 1, UsuarioID = 1, SucursalID = 1 },
      new Modelos.UsuarioRol { Id = 2, RolID = 1, UsuarioID = 1, SucursalID = 2 },
      new Modelos.UsuarioRol { Id = 3, RolID = 1, UsuarioID = 1, SucursalID = 3 },
      new Modelos.UsuarioRol { Id = 4, RolID = 2, UsuarioID = 2, SucursalID = 2 },
      new Modelos.UsuarioRol { Id = 5, RolID = 3, UsuarioID = 2, SucursalID = 3 },
      new Modelos.UsuarioRol { Id = 6, RolID = 3, UsuarioID = 3, SucursalID = 2 },
      new Modelos.UsuarioRol { Id = 7, RolID = 3, UsuarioID = 4, SucursalID = 3 },
      new Modelos.UsuarioRol { Id = 8, RolID = 2, UsuarioID = 5, SucursalID = 2 },
      new Modelos.UsuarioRol { Id = 9, RolID = 2, UsuarioID = 6, SucursalID = 3 },
      new Modelos.UsuarioRol { Id = 10, RolID = 1, UsuarioID = 7, SucursalID = 1 },
      new Modelos.UsuarioRol { Id = 11, RolID = 1, UsuarioID = 7, SucursalID = 2 },
      new UsuarioRol { Id = 12, RolID = 1, UsuarioID = 7, SucursalID = 3 },
      new UsuarioRol { Id = 13, RolID = 4, UsuarioID = 2, SucursalID = 2 }
      );
            context.SaveChanges();
        }
    }
}