using System.Web.Mvc;
using UI.Web.ViewModels.Precios;
using Servicios;

namespace UI.Web.Controllers
{
    public class PreciosController : BaseController
    {
        RubrosServicios _rubrosServicios;
        ArticulosServicios _articulosServicios;

        public PreciosController()
        {
            _rubrosServicios = new RubrosServicios();
            _articulosServicios = new ArticulosServicios();
        }

        // GET: Precios
        public ActionResult ActualizarPorRubro(string msj)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            PrecioActualizarPorRubroViewModel ActualizaPorRubroVM = new PrecioActualizarPorRubroViewModel();

            ViewBag.Rubros = _rubrosServicios.GetAll();
            return View(ActualizaPorRubroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPorRubro(PrecioActualizarPorRubroViewModel ActualizaPorRubroVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _articulosServicios.ActualizarPrecioPorRubro(ActualizaPorRubroVM.RubroID, ActualizaPorRubroVM.PorcentajeActualizacion);
                if (bandera)
                {
                    var mensaje = "Los Precios se actualizaron correctamente!";
                    return RedirectToAction("Index", "Articulos", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se han podido actualizar los Precios, por favor vuelva a intentarlo.";
                    ViewBag.Rubros = _rubrosServicios.GetAll();
                    return View(ActualizaPorRubroVM);
                }

            }
            else
            {
                ViewBag.Error = "No se han podido actualizar los Precios, por favor vuelva a intentarlo.";
                ViewBag.Rubros = _rubrosServicios.GetAll();
                return View(ActualizaPorRubroVM);
            }

        }

    }
}