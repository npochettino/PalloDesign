using System.Web.Mvc;
using Servicios;
using UI.Web.ViewModels.TiposMovimientosEfectivo;

namespace UI.Web.Controllers
{
    public class TiposMovimientosEfectivoController : BaseController
    {
        TipoMovimientosEfectivoServicios _tipoMovimientosCajaServicios;
        CategoriasTiposMovimientosEfectivoServicios _categoriasTiposMovimientosEfectivoServicios;

        public TiposMovimientosEfectivoController()
        {
            _tipoMovimientosCajaServicios = new TipoMovimientosEfectivoServicios();
            _categoriasTiposMovimientosEfectivoServicios = new CategoriasTiposMovimientosEfectivoServicios();
        }

        // GET: TipoMovimientosCaja
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            var TiposMovimientosCaja = _tipoMovimientosCajaServicios.GetAll();
            TipoMovimientoEfectivoIndexViewModel TipoMovimientoCajaVM = new TipoMovimientoEfectivoIndexViewModel();
            foreach (var tipoMovimientoCaja in TiposMovimientosCaja)
            {
                TipoMovimientoCajaVM.TiposMovimientosCaja.Add(tipoMovimientoCaja);
            }
            return View(TipoMovimientoCajaVM);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");
            LlenarDDL();
            TipoMovimientoEfectivoAgregarViewModel TipoMovimientoCajaVM = new TipoMovimientoEfectivoAgregarViewModel();
            return View(TipoMovimientoCajaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(TipoMovimientoEfectivoAgregarViewModel TipoMovimientoCajaVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _tipoMovimientosCajaServicios.Add(TipoMovimientoCajaVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Tipo de Movimiento de Efectivo se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Tipo de Movimiento de Efectivo, por favor vuelva a intentarlo.";
                    LlenarDDL();
                    return View(TipoMovimientoCajaVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Tipo de Movimiento de Efectivo, por favor vuelva a intentarlo.";
                LlenarDDL();
                return View(TipoMovimientoCajaVM);
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            LlenarDDL();

            var TipoMovimientoCajaVM = new TipoMovimientoEfectivoEditarViewModel(_tipoMovimientosCajaServicios.GetOne(id));

            return View(TipoMovimientoCajaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(TipoMovimientoEfectivoEditarViewModel TipoMovimientoCajaVM)
        {
            if (TipoMovimientoCajaVM != null)
            {
                var bandera = _tipoMovimientosCajaServicios.Update(TipoMovimientoCajaVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Tipo de Movimiento de Efectivo se ha actualizado correctamente!" });
                }
                else
                {
                    ViewBag.Error = "El Tipo de Movimiento de Efectivo no se ha actualizado, vuelva a intentarlo.";
                    LlenarDDL();
                    return View(TipoMovimientoCajaVM);
                }
            }
            else
            {
                ViewBag.Error = "El Tipo de Movimiento de Efectivo no se ha actualizado, vuelva a intentarlo.";
                LlenarDDL();
                return View(TipoMovimientoCajaVM);
            }
        }

        public ActionResult Eliminar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = "Se eliminará el Tipo de Movimiento de Efectivo";
            var TipoMovimientoCajaVM = new TipoMovimientoEfectivoEliminarViewModel(_tipoMovimientosCajaServicios.GetOne(id));
            return View(TipoMovimientoCajaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(TipoMovimientoEfectivoEliminarViewModel TipoMovimientoCajaVM)
        {
            if (TipoMovimientoCajaVM.Id != 0)
            {
                var TipoMovimientoCaja = _tipoMovimientosCajaServicios.GetOne(TipoMovimientoCajaVM.Id);
                TipoMovimientoCaja.Habilitado = false;
                var bandera = _tipoMovimientosCajaServicios.Update(TipoMovimientoCaja);
                if (bandera)
                {
                    var mensaje = "El Tipo de Movimiento de Efectivo se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Tipo de Movimiento de Efectivo no se ha eliminado, vuelva a intentarlo.";
                    TipoMovimientoEfectivoEliminarViewModel tmcVM = new TipoMovimientoEfectivoEliminarViewModel(_tipoMovimientosCajaServicios.GetOne(TipoMovimientoCajaVM.Id));
                    return View(tmcVM);
                }

            }
            else
            {
                ViewBag.Error = "El Tipo de Movimiento de Efectivo no se ha eliminado, vuelva a intentarlo.";
                TipoMovimientoEfectivoEliminarViewModel tmcVM = new TipoMovimientoEfectivoEliminarViewModel(_tipoMovimientosCajaServicios.GetOne(TipoMovimientoCajaVM.Id));
                return View(tmcVM);
            }
        }

        private void LlenarDDL()
        {
            ViewBag.Categorias = _categoriasTiposMovimientosEfectivoServicios.GetAll();
        }
    }
}