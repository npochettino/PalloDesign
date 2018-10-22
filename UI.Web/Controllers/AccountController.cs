using Modelos;
using Servicios;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.Login;

namespace UI.Web.Controllers
{
    public class AccountController : Controller
    {
        private ConfiguracionServicios _configServicios;
        private UsuariosServicios _usuariosServicios;

        public AccountController()
        {
            _configServicios = new ConfiguracionServicios();
            _usuariosServicios = new UsuariosServicios();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel usuario)
        {
            //Inicializo la variable de sesión "RolActual"
            System.Web.HttpContext.Current.Session["RolActual"] = "";

            System.Web.HttpContext.Current.Session["SaldoAFavor"] = null;

            if (!_configServicios.IsPeriodoDePrueba())
            {
                ViewBag.Error = "Período de prueba finalizado, para continuar utilizando el sistema contactarse con ADWEB. Muchas gracias!";
                return View();
            }

            var usuarioEnBase = _usuariosServicios.GetAll().Where(a => a.DNI.Trim().ToLower() == usuario.NombreUsuario.Trim().ToLower() && a.Password.Trim().ToLower() == usuario.Password.Trim().ToLower()).FirstOrDefault();
            if (usuarioEnBase != null)
            {
                System.Web.HttpContext.Current.Session["UsuarioActual"] = usuarioEnBase;
                System.Web.HttpContext.Current.Session["DatosUsuario"] = usuarioEnBase.NombreCompleto;
                System.Web.HttpContext.Current.Session["DatosSucursalActual"] = "";
                System.Web.HttpContext.Current.Session["SucursalIDAgregar"] = 2;
                System.Web.HttpContext.Current.Session["SucursalActual"] = 2;


                /*Redirect to New Venta*/
                int rolAdministrador = 1, rolResponsable = 2, rolAdmStock = 4;
                int sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
                string rolActual = "";

                UsuarioRol UsuarioRol = usuarioEnBase.Roles.Where(x => x.RolID == rolAdministrador && x.SucursalID == sucID).FirstOrDefault();
                
                if (UsuarioRol != null) rolActual = "administrador";
                else
                {
                    UsuarioRol = usuarioEnBase.Roles.Where(x => x.RolID == rolResponsable && x.SucursalID == sucID).FirstOrDefault();
                    if (UsuarioRol != null) rolActual = "responsable";
                    else
                    {
                        UsuarioRol = usuarioEnBase.Roles.Where(x => x.RolID == rolAdmStock && x.SucursalID == sucID).FirstOrDefault();
                        if (UsuarioRol != null) rolActual = "adm. de stock";
                        else
                        {
                            rolActual = "vendedor";
                        }
                    }
                }

                System.Web.HttpContext.Current.Session["RolActual"] = rolActual;


                //RedirectToAction("SeleccionarSucursal", "Home");
                return RedirectToAction("Agregar", "Ventas");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

        }
    }
}