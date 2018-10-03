using System.Linq;
using System.Web.Mvc;
using Servicios;
using UI.Web.ViewModels.StockMovimientos;
using Modelos;
using System;

namespace UI.Web.Controllers
{
    public class StockMovimientosController : BaseController
    {
        private StockMovimientosServicios _stockMovimientosServicios;
        private TipoMovimientosStockServicios _tiposStockMovimientosServicios;
        private SucursalesServicios _sucursalesServicios;
        //private Usuario usr;
        //private int sucID;

        public StockMovimientosController()
        {
            _stockMovimientosServicios = new StockMovimientosServicios();
            _tiposStockMovimientosServicios = new TipoMovimientosStockServicios();
            _sucursalesServicios = new SucursalesServicios();
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Proveedores
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            //Traigo los movimientos de stock con los filtros correspondientes.
            CargarSucursales();
            StockMovimientoIndexViewModel StockMovimientoVM = new StockMovimientoIndexViewModel();
            StockMovimientoVM.FechaDesde = DateTime.Now;
            StockMovimientoVM.FechaHasta = DateTime.Now;
            StockMovimientoVM.SucursalID = sucID;
            StockMovimientoVM.StockMovimientos = _stockMovimientosServicios.GetAllByDate(sucID, DateTime.Now, DateTime.Now);

            return View(StockMovimientoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StockMovimientoIndexViewModel StockMovimientoVM)
        {
            if (ModelState.IsValid)
            {
                StockMovimientoVM.StockMovimientos = _stockMovimientosServicios.GetAllByDate(
                    StockMovimientoVM.SucursalID, StockMovimientoVM.FechaDesde, StockMovimientoVM.FechaHasta);
            }
            else
            {
                ViewBag.Error = "No pudo realizarse la búsqueda, vuelva a intentarlo.";
            }
            CargarSucursales();
            return View(StockMovimientoVM);
        }

        public ActionResult Agregar()
        {

            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var tipos = _tiposStockMovimientosServicios.GetAll();
            ViewBag.TiposMovimientosStock = tipos.ToList();
            StockMovimientoAgregarViewModel StockMovimientoVM = new StockMovimientoAgregarViewModel();
            return View(StockMovimientoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(StockMovimientoAgregarViewModel stockMovimientoVM)
        {

            ViewBag.TiposMovimientosStock = _tiposStockMovimientosServicios.GetAll();
            if (ModelState.IsValid && stockMovimientoVM.ArticuloID != 0)
            {
                var StockMovimiento = stockMovimientoVM.Mapear();
                StockMovimiento.ArticuloID = stockMovimientoVM.ArticuloID;
                bool bandera = _stockMovimientosServicios.Agregar(StockMovimiento, sucID);
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Movimiento de Stock se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Movimiento de Stock, por favor vuelva a intentarlo.";
                    return View(stockMovimientoVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Movimiento de Stock, por favor vuelva a intentarlo.";
                return View(stockMovimientoVM);
            }

        }

        private void CargarSucursales()
        {
            ViewBag.Sucursales = _sucursalesServicios.GetAll();
        }
    }
}