using Modelos;
using Servicios;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.Sucursales;

namespace UI.Web.Controllers
{
    public class HomeController : BaseController
    {

        private UsuariosServicios _usuariosServicios;
        private SucursalesServicios _sucursalesServicios;
        //private Usuario usr;

        public HomeController()
        {
            _usuariosServicios = new UsuariosServicios();
            _sucursalesServicios = new SucursalesServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }
        // GET: Home
        public ActionResult SeleccionarSucursal()
        {
            System.Web.HttpContext.Current.Session["DatosSucursalActual"] = "";
            System.Web.HttpContext.Current.Session["SucursalActual"] = "";
            var usuarioRoles = _usuariosServicios.GetSucursalesByUsuario(usr.Id);
            List<int> sucursalesID = usuarioRoles.Select(a => a.SucursalID).ToList();
            List<Sucursal> sucursales = _sucursalesServicios.GetAll().Where(a => sucursalesID.Contains(a.Id)).ToList();
            SucursalSeleccionarViewModel suc = new SucursalSeleccionarViewModel();
            suc.Sucursales = sucursales;
            return View(suc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarSucursal(SucursalSeleccionarViewModel suc)
        {
            if (ModelState.IsValid)
            {
                //var sucursal = _sucursalesServicios.GetOne(suc.SucursalIDAgregar);
                var sucursal = _sucursalesServicios.GetOne(2);
                var usuarioRoles = _usuariosServicios.GetSucursalesByUsuario(usr.Id);
                //System.Web.HttpContext.Current.Session["SucursalActual"] = suc.SucursalIDAgregar;
                System.Web.HttpContext.Current.Session["SucursalActual"] = 2;
                try {
                    //var rol = usuarioRoles.Where(a => a.UsuarioID == usr.Id && a.SucursalID == suc.SucursalIDAgregar).FirstOrDefault().Rol.Nombre;
                    var rol = usuarioRoles.Where(a => a.UsuarioID == usr.Id && a.SucursalID == 2).FirstOrDefault().Rol.Nombre;
                    System.Web.HttpContext.Current.Session["DatosSucursalActual"] = " | Rol: " + rol + " (" + sucursal.Nombre + ")";
                }
                catch {
                    var rol = "";
                    System.Web.HttpContext.Current.Session["DatosSucursalActual"] = " | Suc.: " + rol + " (" + sucursal.Nombre + ")";
                }
                System.Web.HttpContext.Current.Session["RolActual"] = GetRol();
                if(sucursal.Id == 1) return RedirectToAction("Index", "Ventas");
                return RedirectToAction("Agregar", "Ventas");
            }
            else
            {                
                ViewBag.Error = "Ocurrió un error, vuelva a intentarlo.";
                return View(suc);
            }

        }
    }
}