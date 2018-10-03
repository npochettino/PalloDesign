using System.Web.Mvc;
using Modelos;
using UI.Web.ViewModels.Rubros;
using Servicios;

namespace UI.Web.Controllers
{
    public class RubrosController : BaseController
    {
        private RubrosServicios _rubrosServicios;
       // private Usuario usr;

        public RubrosController()
        {
            _rubrosServicios = new RubrosServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Rubros
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1,2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            var Rubros = _rubrosServicios.GetAll();
            RubroIndexViewModel RubroVM = new RubroIndexViewModel();
            foreach (var rubro in Rubros)
            {
                RubroVM.Rubros.Add(rubro);
            }
            return View(RubroVM);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            RubroAgregarViewModel RubroVM = new RubroAgregarViewModel();
            return View(RubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(RubroAgregarViewModel rubroVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _rubrosServicios.Add(rubroVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Rubro se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Rubro, por favor vuelva a intentarlo.";
                    return View(rubroVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Rubro, por favor vuelva a intentarlo.";
                return View(rubroVM);
            }
        }

        public ActionResult Eliminar(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = "Se eliminará el Rubro";
            var RubroVM = new RubroEliminarViewModel(_rubrosServicios.GetOne(id));
            return View(RubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(RubroEliminarViewModel rubroVM)
        {
            if (rubroVM.Id != 0)
            {
                var Rubro = _rubrosServicios.GetOne(rubroVM.Id);
                Rubro.Habilitado = false;
                var bandera = _rubrosServicios.Update(Rubro);
                if (bandera)
                {
                    var mensaje = "El Rubro se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Rubro no se ha eliminado, vuelva a intentarlo.";
                    RubroEliminarViewModel RubroVM = new RubroEliminarViewModel(_rubrosServicios.GetOne(rubroVM.Id));
                    return View(RubroVM);
                }

            }
            else
            {
                ViewBag.Error = "El Rubro no se ha eliminado, vuelva a intentarlo.";
                RubroEliminarViewModel RubroVM = new RubroEliminarViewModel(_rubrosServicios.GetOne(rubroVM.Id));
                return View(RubroVM);
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var RubroVM = new RubroEditarViewModel(_rubrosServicios.GetOne(id));

            return View(RubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(RubroEditarViewModel rubroVM)
        {
            if (rubroVM != null)
            {
                var bandera = _rubrosServicios.Update(rubroVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Rubro se ha actualizado correctamente!" });
                }
                else
                {
                    ViewBag.Error = "El Rubro no se ha actualizado, vuelva a intentarlo.";
                    return View(rubroVM);
                }
            }
            else
            {
                ViewBag.Error = "El Rubro no se ha actualizado, vuelva a intentarlo.";
                return View(rubroVM);
            }
        }

    }
}