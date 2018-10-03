using System.Web.Mvc;
using UI.Web.ViewModels.Sueldos;
using Servicios;
using System;
using System.Linq;

namespace UI.Web.Controllers
{
    public class SueldosController : BaseController
    {
        private MovimientosEfectivoServicios _movimientoEfectivoServicios;
        private UsuariosRolesServicios _usuariosRolesServicios;

        public SueldosController()
        {
            _movimientoEfectivoServicios = new MovimientosEfectivoServicios();
            _usuariosRolesServicios = new UsuariosRolesServicios();
        }

        public ActionResult Index(string msj)
        {

            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            SueldoIndexViewModel sueldoVM = new SueldoIndexViewModel();

            sueldoVM.Sueldos = _movimientoEfectivoServicios.GetSueldosByFecha(DateTime.Now, DateTime.Now);
            sueldoVM.FechaDesde = DateTime.Now;
            sueldoVM.FechaHasta = DateTime.Now;

            return View(sueldoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SueldoIndexViewModel sueldoVM)
        {
            if (ModelState.IsValid)
            {
                sueldoVM.Sueldos = _movimientoEfectivoServicios.GetSueldosByFecha(sueldoVM.FechaDesde, sueldoVM.FechaHasta);
            }
            else
            {
                ViewBag.Error = "No pudo realizarse la búsqueda, vuelva a intentarlo.";
            }
            return View(sueldoVM);
        }

        public ActionResult Vendedor(string msj)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            SueldoVendedorViewModel sueldoVM = new SueldoVendedorViewModel();
            CargarVendedores();

            return View(sueldoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendedor(SueldoVendedorViewModel sueldoVM)
        {
            if (ModelState.IsValid && !ExisteCalculoAnterior(sueldoVM.Vendedor))
            {
                bool bandera = _movimientoEfectivoServicios.Add(sueldoVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", "MovimientosEfectivo", new { msj = "La liquidación de sueldo del Vendedor se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido liquidar el sueldo del Vendedor, por favor vuelva a intentarlo.";
                    CargarVendedores();
                    return View(sueldoVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido liquidar el sueldo del Vendedor, ya fue realizado antes.";
                CargarVendedores();
                return View(sueldoVM);
            }

        }

        public ActionResult Encargado(string msj)
        {
            //if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            SueldoEncargadoViewModel sueldoVM = new SueldoEncargadoViewModel();
            CargarEncargados();

            return View(sueldoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Encargado(SueldoEncargadoViewModel sueldoVM)
        {
            if (ModelState.IsValid && !ExisteCalculoAnterior(sueldoVM.Encargado))
            {
                bool bandera = _movimientoEfectivoServicios.Add(sueldoVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", "MovimientosEfectivo", new { msj = "La liquidación de sueldo del Vendedor se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido liquidar el sueldo del Vendedor, por favor vuelva a intentarlo.";
                    CargarVendedores();
                    return View(sueldoVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido liquidar el sueldo del Vendedor, ya fue realizado antes.";
                CargarVendedores();
                return View(sueldoVM);
            }

        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var sueldo = _movimientoEfectivoServicios.GetOne(id);
            var sueldoVM = new SueldoEditarViewModel(sueldo);

            return View(sueldoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(SueldoEditarViewModel sueldoVM)
        {
            if (ModelState.IsValid)
            {
                var bandera = _movimientoEfectivoServicios.Update(sueldoVM.Mapear());
                if (bandera)
                {
                    var mensaje = "La liquidación de sueldo se ha actualizado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "La liquidación de sueldo no se ha actualizado, vuelva a intentarlo.";
                    return View("Editar", sueldoVM);
                }
            }
            else
            {
                ViewBag.Error = "La liquidación de sueldo no se ha actualizado, vuelva a intentarlo.";
                return View("Editar", sueldoVM);
            }
        }

        private void CargarVendedores()
        {
            //Traer los vendedores de la sucursal
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            ViewBag.Vendedores = _usuariosRolesServicios.GetUsuariosByRolYSucursal(sucID, 3); //El 3 es Vendedor
        }

        private void CargarEncargados()
        {
            //Traer los encargados de la sucursal
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            ViewBag.Vendedores = _usuariosRolesServicios.GetUsuariosByRolYSucursal(sucID, 2); //El 2 es Encargado
        }

        private bool ExisteCalculoAnterior(string persona)
        {
            var desde = DateTime.Now.Date;
            var hasta = DateTime.Now.AddDays(1).Date;
            var anterior = _movimientoEfectivoServicios.GetAll();
            if ((anterior.Any(a => a.Descripcion.Contains(persona) && a.Fecha >= desde && a.Fecha <= hasta)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}