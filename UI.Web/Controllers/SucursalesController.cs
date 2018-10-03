using System.Web.Mvc;
using Modelos;
using Servicios;
using UI.Web.ViewModels.Sucursales;

namespace UI.Web.Controllers
{
    public class SucursalesController : BaseController
    {
        private SucursalesServicios _sucursalesServicios;
       // private Usuario usr;

        public SucursalesController()
        {
            _sucursalesServicios = new SucursalesServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Sucursales
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            var Sucursales = _sucursalesServicios.GetAll();
            SucursalIndexViewModel SucursalVM = new SucursalIndexViewModel();
            foreach (var sucursal in Sucursales)
            {
                SucursalVM.Sucursales.Add(sucursal);
            }
            return View(SucursalVM);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            SucursalAgregarViewModel SucursalVM = new SucursalAgregarViewModel();
            return View(SucursalVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(SucursalAgregarViewModel sucursalVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _sucursalesServicios.Add(sucursalVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "La Sucursal se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar la Sucursal, por favor vuelva a intentarlo.";
                    return View(sucursalVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar la Sucursal, por favor vuelva a intentarlo.";
                return View(sucursalVM);
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            var SucursalVM = new SucursalEditarViewModel(_sucursalesServicios.GetOne(id));

            return View(SucursalVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(SucursalEditarViewModel sucursalVM)
        {
            if (sucursalVM != null)
            {
                var bandera = _sucursalesServicios.Update(sucursalVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "La Sucursal se ha actualizado correctamente!" });
                }
                else
                {
                    ViewBag.Error = "La Sucursal no se ha actualizado, vuelva a intentarlo.";
                    return View("Editar", sucursalVM);
                }
            }
            else
            {
                ViewBag.Error = "La Sucursal no se ha actualizado, vuelva a intentarlo.";
                return View("Editar", sucursalVM);
            }
        }
    }
}