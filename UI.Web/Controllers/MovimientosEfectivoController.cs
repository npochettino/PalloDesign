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
        private FormasDePagoServicios _formasDePagoServicios;
        //private Usuario usr;

        public MovimientosEfectivoController()
        {
            _movimientosServicios = new MovimientosEfectivoServicios();
            _tipoMovimientosServicios = new TipoMovimientosEfectivoServicios();
            _formasDePagoServicios = new FormasDePagoServicios();
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
            ViewBag.FormasDePago = _formasDePagoServicios.GetAll();
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

        public ActionResult Eliminar(int id)
        {
            ViewBag.Alert = "Se eliminara el Movimiento Efectivo";
            MovimientoEfectivoEliminarViewModel MovimientoVM = new MovimientoEfectivoEliminarViewModel(_movimientosServicios.GetOne(id));
            return View(MovimientoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(MovimientoEfectivoEliminarViewModel movimientoVM)
        {
            if (movimientoVM != null)
            {
                bool bandera = _movimientosServicios.Delete(_movimientosServicios.GetOne(movimientoVM.Id));
                if (bandera)
                {
                    var mensaje = "El Movimiento fue eliminado correctamente";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se ha podido eliminar el movimiento, por favor vuelva a intentar.";
                    MovimientoEfectivoEliminarViewModel mVM = new MovimientoEfectivoEliminarViewModel(_movimientosServicios.GetOne(movimientoVM.Id));
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido eliminar el movimiento, por favor vuelva a intentar.";
                MovimientoEfectivoEliminarViewModel mVM = new MovimientoEfectivoEliminarViewModel(_movimientosServicios.GetOne(movimientoVM.Id));
                return View();
            }
        }
    }
}