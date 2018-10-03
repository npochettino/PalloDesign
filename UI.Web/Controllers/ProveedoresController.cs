using System.Web.Mvc;
using Modelos;
using Servicios;
using UI.Web.ViewModels.Proveedores;

namespace UI.Web.Controllers
{
    public class ProveedoresController : BaseController
    {
        private ProveedoresServicios _proveedorServicio;
      //  private Usuario usr;

        public ProveedoresController()
        {
            _proveedorServicio = new ProveedoresServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Proveedores
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            //Traigo todos los proveedores
            var Proveedores = _proveedorServicio.GetAll();
            ProveedorIndexViewModel ProveedorVM = new ProveedorIndexViewModel();
            foreach (var proveedor in Proveedores)
            {
                ProveedorVM.Proveedores.Add(proveedor);
            }
            return View(ProveedorVM);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ProveedorAgregarViewModel ProveedorVM = new ProveedorAgregarViewModel();
            return View(ProveedorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(ProveedorAgregarViewModel proveedorVM)
        {
            if (ModelState.IsValid)
            {
                var proveedor = proveedorVM.Mapear();
                
                bool bandera = _proveedorServicio.Add(proveedor);
                if (bandera)
                {
                    var mensaje = "El Proveedor se registró correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Proveedor, por favor vuelva a intentarlo.";
                    return View(proveedorVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Proveedor, por favor vuelva a intentarlo.";
                return View(proveedorVM);
            }
            
        }

        public ActionResult Eliminar(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = "Se eliminará el Proveedor";
            ProveedorEliminarViewModel proveedorVM = new ProveedorEliminarViewModel(_proveedorServicio.GetOne(id));
            return View(proveedorVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(ProveedorEliminarViewModel proveedorVM)
        {
            if (proveedorVM.Id != 0)
            {
                var bandera = _proveedorServicio.Delete(_proveedorServicio.GetOne(proveedorVM.Id));
                if (bandera)
                {
                    var mensaje = "El Proveedor se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Proveedor no se ha eliminado, vuelva a intentarlo.";
                    ProveedorEliminarViewModel pVM = new ProveedorEliminarViewModel(_proveedorServicio.GetOne(proveedorVM.Id));
                    return View(pVM);
                }

            }
            else
            {
                ViewBag.Error = "El Proveedor no se ha eliminado, vuelva a intentarlo.";
                ProveedorEliminarViewModel pVM = new ProveedorEliminarViewModel(_proveedorServicio.GetOne(proveedorVM.Id));
                return View(pVM);
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var ProveedorVM = new ProveedorEditarViewModel(_proveedorServicio.GetOne(id));

            return View(ProveedorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProveedorEditarViewModel proveedorVM)
        {
            if (proveedorVM != null)
            {
                var bandera = _proveedorServicio.Update(proveedorVM.Mapear());
                if (bandera)
                {
                    var mensaje = "El Proveedor se ha actualizado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Proveedor no se ha actualizado, vuelva a intentarlo.";
                    return View("Editar", proveedorVM);
                }
            }
            else
            {
                ViewBag.Error = "El Proveedor no se ha actualizado, vuelva a intentarlo.";
                return View("Editar", proveedorVM);
            }
        }

        public ActionResult Detalles(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ProveedorDetallesViewModel proveedorVM = new ProveedorDetallesViewModel(_proveedorServicio.GetOne(id));
            return View(proveedorVM);

        }

    }
}