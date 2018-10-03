using System.Web.Mvc;
using Modelos;
using Servicios;
using UI.Web.ViewModels.MovimientosEfectivo;

namespace UI.Web.Controllers
{
    public class MovimientosEfectivoController : BaseController
    {
        private MovimientosEfectivoServicios _movimientosServicios;
        private TipoMovimientosEfectivoServicios _tipoMovimientosServicios;
        //private Usuario usr;

        public MovimientosEfectivoController()
        {
            _movimientosServicios = new MovimientosEfectivoServicios();
            _tipoMovimientosServicios = new TipoMovimientosEfectivoServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Proveedores
        public ActionResult Index(string msj)
        {
           if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            //Traigo todos los proveedores
            var Movimientos = _movimientosServicios.GetAll();
            MovimientoEfectivoIndexViewModel MovimientoVM = new MovimientoEfectivoIndexViewModel();
            foreach (var movimiento in Movimientos)
            {
                MovimientoVM.Movimientos.Add(movimiento);
            }
            return View(MovimientoVM);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1,3)) return RedirectToAction("ErrorPermisos", "Base");

            MovimientoEfectivoAgregarViewModel MovimientoVM = new MovimientoEfectivoAgregarViewModel();
            ViewBag.TiposMovimientos = _tipoMovimientosServicios.GetAll();
            return View(MovimientoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(MovimientoEfectivoAgregarViewModel movimientoVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _movimientosServicios.Add(movimientoVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Movimiento se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Movimiento, por favor vuelva a intentarlo.";
                    ViewBag.TiposMovimientos = _tipoMovimientosServicios.GetAll();
                    return View(movimientoVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Movimiento, por favor vuelva a intentarlo.";
                ViewBag.TiposMovimientos = _tipoMovimientosServicios.GetAll();
                return View(movimientoVM);
            }

        }

    }
}