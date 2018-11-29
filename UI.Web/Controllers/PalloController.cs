using Modelos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.Web.ViewModels.Articulos;
using UI.Web.ViewModels.CierreCaja;
using UI.Web.ViewModels.Stock;

namespace UI.Web.Controllers
{
    public class PalloController : ApiController
    {
        private VentasServicios _ventasServicios;
        private ProveedoresServicios _proveedorServicio;
        private ArticulosServicios _articulosServicios;
        private MovimientosEfectivoServicios _movimientosEfectivoServicios;
        private PagosServicios _pagosServicios;
        private ClientesServicios _clientesServicios;
        private FormasDePagoServicios _formasDePagoServicios;

        public PalloController()
        {
            _ventasServicios = new VentasServicios();
            _clientesServicios = new ClientesServicios();
            _proveedorServicio = new ProveedoresServicios();
            _articulosServicios = new ArticulosServicios();
            _movimientosEfectivoServicios = new MovimientosEfectivoServicios();
            _pagosServicios = new PagosServicios();
            _formasDePagoServicios = new FormasDePagoServicios();
        }

        public IHttpActionResult GetAllFormasDePago() {
            IList<FormaDePago> formasDePago = null;

            formasDePago = _formasDePagoServicios.GetAll().ToList();
            if (formasDePago.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(formasDePago);
            }
        }

        public IHttpActionResult GetAllPagos() {
            IList<Pago> pagos = null;

            pagos = _pagosServicios.GetAll().ToList();
            if (pagos.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(pagos);
            }
        }

        public IHttpActionResult GetAllClientes()
        {
            IList<Cliente> clientes = null;
            clientes = _clientesServicios.GetAll().ToList();

            if (clientes.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        public IHttpActionResult GetAllVentas()
        {
            IList<Venta> ventas = null;
            ventas = _ventasServicios.GetAll().Where(a => a.Anulado == false).ToList();
            ventas = ventas.Where(v => v.Id == 9897).ToList();
            if (ventas.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(ventas);
            }
        }

        public IHttpActionResult GetStock()
        {
            IList<Articulo> articulos = null;
            articulos = _articulosServicios.GetAllArticulos().ToList();

            if (articulos.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(articulos);
            }
        }

        public IHttpActionResult GetMovimientos()
        {
            ReporteGananciasViewModel vm = new ReporteGananciasViewModel();

            var MovimientosDeEfectivoDelPeriodo = _movimientosEfectivoServicios.GetAll();
            var PagosDelPeriodo = _pagosServicios.GetAll();

            vm.Detalles = ArmarDetalleGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);
            vm.Resumen = ArmarResumenGanancias(MovimientosDeEfectivoDelPeriodo, PagosDelPeriodo);

            return Ok(vm);
        }

        private ReporteGananciasResumenViewModel ArmarResumenGanancias(List<MovimientoEfectivo> movimientosDeEfectivoDelPeriodo, List<Pago> pagosDelPeriodo)
        {
            ReporteGananciasResumenViewModel vm = new ReporteGananciasResumenViewModel();

            //vm.TotalGastos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Categoria.Nombre == "Proveedores" || x.TipoMovimiento.Categoria.Nombre == "Varios" && x.TipoMovimiento.Suma == false).Sum(x => x.Monto);
            vm.TotalGastos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Suma == false && x.TipoMovimiento.Categoria.Nombre != "Sueldos").Sum(x => x.Monto);
            vm.TotalIngresos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Suma == true).Sum(x => x.Monto);
            vm.TotalSueldos = movimientosDeEfectivoDelPeriodo.Where(x => x.TipoMovimiento.Categoria.Nombre == "Sueldos").Sum(x => x.Monto);
            vm.TotalVentasEfectivo = pagosDelPeriodo.Where(x => x.FormaDePago.Nombre == "Efectivo").Sum(x => x.Monto);
            vm.TotalVentasCC = pagosDelPeriodo.Where(x => x.FormaDePago.Nombre == "Cuenta Corriente").Sum(x => x.Monto);
            vm.TotalVentasCheque = pagosDelPeriodo.Where(x => x.FormaDePago.Nombre == "Cheque").Sum(x => x.Monto);
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
            return rVM.OrderByDescending(a => a.Fecha).ToList();
        }
    }
}
