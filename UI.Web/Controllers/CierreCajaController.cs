using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.CierreCaja;
using Modelos;

namespace UI.Web.Controllers
{
    public class CierreCajaController : BaseController
    {

        private CierresCajasServicios _cierresCajaServicios;
        private SucursalesServicios _sucursalesServicios;
        private TurnosServicios _turnosServicios;
        private MovimientosEfectivoServicios _movimientosEfectivoServicios;
        private PagosServicios _pagosServicios;

        public CierreCajaController()
        {
            _cierresCajaServicios = new CierresCajasServicios();
            _sucursalesServicios = new SucursalesServicios();
            _turnosServicios = new TurnosServicios();
            _movimientosEfectivoServicios = new MovimientosEfectivoServicios();
            _pagosServicios = new PagosServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
        }

        // GET: CierreCaja
        public ActionResult Index(string mensaje)
        {
            //if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            if (mensaje != null && mensaje != "")
            {
                ViewBag.Informacion = mensaje;
            }
            List<CierreCajaIndexViewModel> lcVM = new List<CierreCajaIndexViewModel>();
            var sucursales = _sucursalesServicios.GetAll().Select(a => a.Id).Distinct().ToList();
            foreach (var suc in sucursales)
            {
                var cierres = _cierresCajaServicios.GetAll().Where(b => b.SucursalID == suc).GroupBy(a => a.FechaCierreCaja).ToList();
                foreach (var c in cierres)
                {
                    CierreCajaIndexViewModel cVM = ArmarIndexCierre(c);
                    lcVM.Add(cVM);
                }
            }
            return View(lcVM);
        }

        private CierreCajaIndexViewModel ArmarIndexCierre(IGrouping<DateTime, CierreCaja> c)
        {
            CierreCajaIndexViewModel cVM = new CierreCajaIndexViewModel();
            try
            {
                cVM.CierreCajaID = c.Where(a => a.Turno.Nombre == "Mañana").FirstOrDefault().Id;
            }
            catch { cVM.CierreCajaID = 0; }

            try
            {
                if (cVM.CierreCajaID == 0)
                { cVM.CierreCajaID = c.Where(a => a.Turno.Nombre == "Tarde").FirstOrDefault().Id; }
            }
            catch { }

            try { cVM.TotalMañana = c.Where(a => a.Turno.Nombre == "Mañana").FirstOrDefault().TotalCaja; }
            catch { cVM.TotalMañana = 0; }
            try { cVM.TotalTarde = c.Where(a => a.Turno.Nombre == "Tarde").FirstOrDefault().TotalCaja; }
            catch { cVM.TotalTarde = 0; }
            try { cVM.FechaCierre = c.FirstOrDefault().FechaCierreCaja; }
            catch { cVM.FechaCierre = new DateTime(1900, 1, 1); }
            try { cVM.Sucursal = c.FirstOrDefault().Sucursal.Nombre; }
            catch { cVM.Sucursal = "Sin Sucursal"; }
            try { cVM.Usuario = c.FirstOrDefault().Usuario.NombreCompleto; }
            catch { cVM.Usuario = "Sin Usuario"; }
            return cVM;
        }

        public ActionResult Agregar(string mensaje)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            if (mensaje != null && mensaje != "")
            {
                ViewBag.Error = mensaje;
            }
            var turnos = _turnosServicios.GetAll();
            ViewBag.Turnos = turnos;
            CierreCajaAgregarViewModel aVM = new CierreCajaAgregarViewModel();
            aVM.FechaCierre = DateTime.Now.Date;
            var cierresExistentes = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja == aVM.FechaCierre && a.SucursalID == sucID).ToList();
            var cierresCalculados = _cierresCajaServicios.Calcular(aVM.FechaCierre, sucID, DateTime.Now);
            List<CierreCaja> cierres = new List<CierreCaja>();
            if (cierresExistentes.Count > 0)
            {
                cierres.AddRange(cierresExistentes);
            }
            foreach (var cc in cierresCalculados)
            {
                if (!cierres.Any(a => a.TurnoID == cc.TurnoID))
                {
                    cierres.Add(cc);
                }
            }
            aVM.Cierres = cierres;
            return View(aVM);
        }

        [HttpPost]
        public ActionResult Agregar(int turnoID, string fechaCierre)
        {
            ViewBag.Turnos = _turnosServicios.GetAll();
            CierreCajaAgregarViewModel aVM = new CierreCajaAgregarViewModel();
            aVM.FechaCierre = DateTime.Parse(fechaCierre);
            //var cierres = new List<CierreCaja>();
            //cierres = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja == aVM.FechaCierre && sucID == a.SucursalID).ToList();
            //if (cierres.Count <= 0)
            //{
            //    cierres = _cierresCajaServicios.Calcular(aVM.FechaCierre, sucID);
            //}
            var cierresExistentes = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja == aVM.FechaCierre && a.SucursalID == sucID).ToList();
            var cierresCalculados = _cierresCajaServicios.Calcular(aVM.FechaCierre, sucID, DateTime.Now);
            List<CierreCaja> cierres = new List<CierreCaja>();
            if (cierresExistentes.Count > 0)
            {
                cierres.AddRange(cierresExistentes);
            }
            foreach (var cc in cierresCalculados)
            {
                if (!cierres.Any(a => a.TurnoID == cc.TurnoID))
                {
                    cierres.Add(cc);
                }
            }
            aVM.Cierres = cierres;

            if (turnoID == 0)
            {
                aVM.Cierres = cierres;
            }
            else
            {
                aVM.Cierres = cierres.Where(a => a.TurnoID == turnoID).ToList();
            }
            return PartialView("_CierreCajaTurno", aVM.Cierres);
        }

        public ActionResult Guardar(DateTime fechaCierre, int turnoID)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            //Valida que para realizar el cierre del turno tarde, se haya cerrado el turno mañana
            if(turnoID == 2)
            {
                bool valida = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja == fechaCierre && a.SucursalID == sucID && a.TurnoID == 1).ToList().Count == 1;

                if(!valida)
                {
                    return RedirectToAction("Agregar", new { mensaje = "Primero debe realizar el cierre de caja del turno mañana." });
                }
            }
            

            var cierres = _cierresCajaServicios.Calcular(fechaCierre, sucID, DateTime.Now);
            var cierre = cierres.Where(a => a.TurnoID == turnoID).FirstOrDefault();
            cierre.UsuarioID = usr.Id;
            bool bandera = _cierresCajaServicios.Add(cierre);
            if (bandera)
            {
                return RedirectToAction("Index", new { mensaje = "Cierre de caja realizado correctamente." });
            }
            else
            {
                ViewBag.Error = "No se pudo realizar el Cierre de caja, vuelva a intentarlo.";
                return View("Index");
            }
        }

        public ActionResult Recalcular(int cierreID)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var cierreViejo = _cierresCajaServicios.GetOne(cierreID);
            var fechaCierre = cierreViejo.FechaCierreCaja;
            var turnoID = cierreViejo.TurnoID;
            var cierres = _cierresCajaServicios.Calcular(fechaCierre, sucID, DateTime.Now);
            var cierre = cierres.Where(a => a.TurnoID == turnoID).FirstOrDefault();
            cierre.UsuarioID = usr.Id;
            bool bandera = _cierresCajaServicios.Update(cierre, cierreViejo.Id);
            if (bandera)
            {
                return RedirectToAction("Index", new { mensaje = "Cierre de caja recalculado correctamente." });
            }
            else
            {
                ViewBag.Error = "No se pudo recalcular el Cierre de caja, vuelva a intentarlo.";
                return View("Index");
            }
        }

        //[HttpPost]
        //public ActionResult ReporteGastosGeneralesPrint(ReporteGastosGeneralesViewModel vVM)
        //{
        //    var suc = _sucursalesServicios.GetOne(sucID);
        //    vVM.Sucursal = suc;
        //    vVM.CabeceraReporte = suc.Nombre + " (Desde: " + vVM.FechaDesde.ToShortDateString() + " Hasta: " + vVM.FechaHasta.ToShortDateString() + ")";
        //    var cierres = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja <= vVM.FechaHasta && a.FechaCierreCaja >= vVM.FechaDesde && a.SucursalID == sucID).ToList().GroupBy(b => b.FechaCierreCaja);
        //    foreach (var dia in cierres)
        //    {
        //        CierreCaja c = new CierreCaja();
        //        c.FechaCierreCaja = dia.FirstOrDefault().FechaCierreCaja;
        //        c.TotalProveedores = dia.Sum(a => a.TotalProveedores);
        //        c.TotalSueldos = dia.Sum(a => a.TotalSueldos);
        //        c.TotalVarios = dia.Sum(a => a.TotalVarios);
        //        c.TotalVentasEfectivo = dia.Sum(a => a.TotalVentasEfectivo);
        //        c.TotalVentasTarjetas = dia.Sum(a => a.TotalVentasTarjetas);
        //        vVM.Cierres.Add(c);
        //    }

        //    return View(vVM);
        //}

        //public ActionResult ReporteGastosGenerales()
        //{
        //    if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

        //    ReporteGastosGeneralesViewModel vVM = new ReporteGastosGeneralesViewModel();

        //    vVM.FechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    vVM.FechaHasta = vVM.FechaDesde.AddMonths(1).AddDays(-1);

        //    var cierres = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja <= vVM.FechaHasta && a.FechaCierreCaja >= vVM.FechaDesde && a.SucursalID == sucID).ToList().GroupBy(b=>b.FechaCierreCaja);
        //    var suc = _sucursalesServicios.GetOne(sucID);
        //    vVM.Sucursal = suc;
        //    vVM.CabeceraReporte = suc.Nombre + " (Desde: " + vVM.FechaDesde.ToShortDateString() + " Hasta: " + vVM.FechaHasta.ToShortDateString() + ")";
        //    foreach (var dia in cierres)
        //    {
        //        CierreCaja c = new CierreCaja();
        //        c.FechaCierreCaja = dia.FirstOrDefault().FechaCierreCaja;
        //        c.TotalProveedores = dia.Sum(a => a.TotalProveedores);
        //        c.TotalSueldos = dia.Sum(a => a.TotalSueldos);
        //        c.TotalVarios = dia.Sum(a => a.TotalVarios);
        //        c.TotalVentasEfectivo = dia.Sum(a => a.TotalVentasEfectivo);
        //        c.TotalVentasTarjetas = dia.Sum(a => a.TotalVentasTarjetas);
        //        vVM.Cierres.Add(c);
        //    }

        //    return View(vVM);
        //}

        public ActionResult ReporteGastosGenerales()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ReporteGastosGeneralesViewModel vVM = new ReporteGastosGeneralesViewModel();

            vVM.FechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            vVM.FechaHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vVM.FechaDesde, vVM.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vVM.FechaDesde, vVM.FechaHasta);

            //Por cada día instancio un detalleVM
            var dias = (vVM.FechaHasta - vVM.FechaDesde).Days;
            for (int i = 0; i < dias; i++)
            {
                ReporteGastosGeneralesDetalleViewModel detalleVM = new ReporteGastosGeneralesDetalleViewModel();
                detalleVM.Fecha = vVM.FechaDesde.AddDays(i);
                var MovimientosDeEfectivoDelDia = MovimientosDeEfectivoDelPeriodo.Where(x => x.Fecha.Date == detalleVM.Fecha.Date).ToList();
                var PagosDelDia = PagosDelPeriodo.Where(x => x.Venta.FechaVenta.Date == detalleVM.Fecha.Date).ToList();
                detalleVM.TotalGastos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Proveedores" || x.TipoMovimiento.Categoria.Nombre == "Varios" && x.TipoMovimiento.Suma == false).Sum(x => x.Monto);
                detalleVM.TotalSueldos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Sueldos").Sum(x => x.Monto);
                detalleVM.TotalVentasEfectivo = PagosDelDia.Where(x => x.FormaDePago.Nombre == "Efectivo").Sum(x => x.Monto);
                detalleVM.TotalIngresosPorTarjeta = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Ingresos por Tarjeta").Sum(x => x.Monto);

                vVM.Detalle.Add(detalleVM);

            }

            vVM.FechaHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return View(vVM);
        }

        [HttpPost]
        public ActionResult ReporteGastosGenerales(ReporteGastosGeneralesViewModel vVM)
        {
            vVM.FechaHasta = vVM.FechaHasta.AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vVM.FechaDesde, vVM.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vVM.FechaDesde, vVM.FechaHasta);

            //Por cada día instancio un detalleVM
            var dias = (vVM.FechaHasta - vVM.FechaDesde).Days;
            for (int i = 0; i < dias; i++)
            {
                ReporteGastosGeneralesDetalleViewModel detalleVM = new ReporteGastosGeneralesDetalleViewModel();
                detalleVM.Fecha = vVM.FechaDesde.AddDays(i);
                var MovimientosDeEfectivoDelDia = MovimientosDeEfectivoDelPeriodo.Where(x => x.Fecha.Date == detalleVM.Fecha.Date).ToList();
                var PagosDelDia = PagosDelPeriodo.Where(x => x.Venta.FechaVenta.Date == detalleVM.Fecha.Date).ToList();
                detalleVM.TotalGastos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Proveedores" || x.TipoMovimiento.Categoria.Nombre == "Varios" && x.TipoMovimiento.Suma == false).Sum(x => x.Monto);
                detalleVM.TotalSueldos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Sueldos").Sum(x => x.Monto);
                detalleVM.TotalVentasEfectivo = PagosDelDia.Where(x => x.FormaDePago.Nombre == "Efectivo").Sum(x => x.Monto);
                detalleVM.TotalIngresosPorTarjeta = PagosDelDia.Where(x => x.FormaDePago.Nombre.Contains("Tarjeta")).Sum(x => x.Monto);

                vVM.Detalle.Add(detalleVM);

            }

            vVM.FechaHasta = vVM.FechaHasta.AddDays(-1);
            return View(vVM);
        }

        [HttpPost]
        public ActionResult ReporteGastosGeneralesPrint(ReporteGastosGeneralesViewModel vVM)
        {
            vVM.FechaHasta = vVM.FechaHasta.AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vVM.FechaDesde, vVM.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vVM.FechaDesde, vVM.FechaHasta);

            //Por cada día instancio un detalleVM
            var dias = (vVM.FechaHasta - vVM.FechaDesde).Days;
            for (int i = 0; i < dias; i++)
            {
                ReporteGastosGeneralesDetalleViewModel detalleVM = new ReporteGastosGeneralesDetalleViewModel();
                detalleVM.Fecha = vVM.FechaDesde.AddDays(i);
                var MovimientosDeEfectivoDelDia = MovimientosDeEfectivoDelPeriodo.Where(x => x.Fecha.Date == detalleVM.Fecha.Date).ToList();
                var PagosDelDia = PagosDelPeriodo.Where(x => x.Venta.FechaVenta.Date == detalleVM.Fecha.Date).ToList();
                detalleVM.TotalGastos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Proveedores" || x.TipoMovimiento.Categoria.Nombre == "Varios" && x.TipoMovimiento.Suma == false).Sum(x => x.Monto);
                detalleVM.TotalSueldos = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Sueldos").Sum(x => x.Monto);
                detalleVM.TotalVentasEfectivo = PagosDelDia.Where(x => x.FormaDePago.Nombre == "Efectivo").Sum(x => x.Monto);
                detalleVM.TotalIngresosPorTarjeta = MovimientosDeEfectivoDelDia.Where(x => x.TipoMovimiento.Categoria.Nombre == "Ingresos por Tarjeta").Sum(x => x.Monto);

                vVM.Detalle.Add(detalleVM);

            }

            vVM.FechaHasta = vVM.FechaHasta.AddDays(-1);
            var suc = _sucursalesServicios.GetOne(sucID);
            vVM.CabeceraReporte = suc.Nombre + " (Desde: " + vVM.FechaDesde.ToShortDateString() + " Hasta: " + vVM.FechaHasta.ToShortDateString() + ")";
            return View(vVM);
        }

        public ActionResult ReporteGanancias()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ReporteGananciasViewModel vm = new ReporteGananciasViewModel();

            vm.FechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            vm.FechaHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vm.FechaDesde, vm.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vm.FechaDesde, vm.FechaHasta);

            vm.Detalles = ArmarDetalleGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);

            vm.Resumen = ArmarResumenGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);
            vm.Sucursal = _sucursalesServicios.GetOne(sucID);
            vm.FechaHasta = vm.FechaHasta.AddDays(-1);
            vm.CabeceraReporte = String.Format("Reporte Ganancias: {0} a {1}", vm.FechaDesde.ToShortDateString(), vm.FechaHasta.ToShortDateString());
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReporteGananciasPrint(ReporteGananciasViewModel vm)
        {
            vm.FechaHasta = vm.FechaHasta.AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vm.FechaDesde, vm.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vm.FechaDesde, vm.FechaHasta);

            vm.Detalles = ArmarDetalleGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);

            vm.Resumen = ArmarResumenGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);
            vm.Sucursal = _sucursalesServicios.GetOne(sucID);
            vm.FechaHasta = vm.FechaHasta.AddDays(-1);
            vm.CabeceraReporte = String.Format("Reporte Ganancias: {0} a {1}", vm.FechaDesde.ToShortDateString(), vm.FechaHasta.ToShortDateString());
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReporteGanancias(ReporteGananciasViewModel vm)
        {
            vm.FechaHasta = vm.FechaHasta.AddDays(1);

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAllBySucursalRangoFecha(sucID, vm.FechaDesde, vm.FechaHasta);
            var PagosDelPeriodo = _pagosServicios.GetAllBySucursalRangoFechas(sucID, vm.FechaDesde, vm.FechaHasta);

            vm.Detalles = ArmarDetalleGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);

            vm.Resumen = ArmarResumenGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);
            vm.Sucursal = _sucursalesServicios.GetOne(sucID);
            vm.FechaHasta = vm.FechaHasta.AddDays(-1);
            vm.CabeceraReporte = String.Format("Reporte Ganancias: {0} a {1}", vm.FechaDesde.ToShortDateString(), vm.FechaHasta.ToShortDateString());
            return View(vm);
        }

        private ReporteGananciasResumenViewModel ArmarResumenGanancias(List<MovimientoEfectivo> movimientosDeEfectivoDelPeriodo, List<Pago> pagosDelPeriodo)
        {
            ReporteGananciasResumenViewModel vm = new ReporteGananciasResumenViewModel();

            vm.TotalGastos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Categoria.Nombre == "Proveedores" || x.TipoMovimiento.Categoria.Nombre == "Varios" && x.TipoMovimiento.Suma == false).Sum(x => x.Monto);
            vm.TotalSueldos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Categoria.Nombre == "Sueldos").Sum(x => x.Monto);
            vm.TotalVentasEfectivo = pagosDelPeriodo.Where(x => x.FormaDePago.Nombre == "Efectivo").Sum(x => x.Monto);
            vm.TotalIngresosPorTarjeta = pagosDelPeriodo.Where(x => x.FormaDePago.Nombre.Contains("Tarjeta")).Sum(x => x.Monto);

            return vm;
        }

        private List<ReporteGananciasDetalleViewModel> ArmarDetalleGanancias(List<MovimientoEfectivo> movimientosDeEfectivoDelPeriodo, List<Pago> pagosDelPeriodo)
        {
            List<ReporteGananciasDetalleViewModel> rVM = new List<ReporteGananciasDetalleViewModel>();

            var movimientos = movimientosDeEfectivoDelPeriodo.GroupBy(x => new { x.Fecha.Date, x.TipoMovimiento.Nombre })
                    .Select(y => new ReporteGananciasDetalleViewModel()
                    {
                        Fecha = y.Key.Date,
                        Tipo = y.Key.Nombre,
                        Importe = y.Sum(a => a.Monto)
                    }
                    );

            foreach (var mov in movimientos)
            {
                ReporteGananciasDetalleViewModel vm = new ReporteGananciasDetalleViewModel();
                vm.Fecha = mov.Fecha.Date;
                vm.Tipo = mov.Tipo;
                vm.Importe = mov.Importe;
                rVM.Add(vm);
            }
            var pagos = pagosDelPeriodo.GroupBy(x => new { x.Venta.FechaVenta.Date, x.FormaDePago.Nombre })
                   .Select(y => new ReporteGananciasDetalleViewModel()
                   {
                       Fecha = y.Key.Date,
                       Tipo = y.Key.Nombre,
                       Importe = y.Sum(a => a.Monto)
                   }
                   );

            foreach (var p in pagos)
            {
                ReporteGananciasDetalleViewModel vm = new ReporteGananciasDetalleViewModel();
                vm.Fecha = p.Fecha.Date;
                vm.Tipo = "Venta " + p.Tipo;
                vm.Importe = p.Importe;
                rVM.Add(vm);
            }
            return rVM.OrderByDescending(a=>a.Fecha).ToList();
        }

        public ActionResult Detalles(int id)
        {
            //if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var cierre = _cierresCajaServicios.GetOne(id);
            var fecha = cierre.FechaCierreCaja.Date;
            CierreCajaDetallesViewModel cVM = new CierreCajaDetallesViewModel();
            var cierresExistentes = _cierresCajaServicios.GetAll().Where(a => a.FechaCierreCaja == fecha && a.SucursalID == sucID).ToList();
            cVM.Sucursal = cierre.Sucursal.Nombre;
            cVM.FechaCierre = fecha;
            cVM.Cierres = cierresExistentes;
            return View(cVM);
        }

    }
}