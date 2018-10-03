using Modelos;
using System.Linq;
using System.Web.Mvc;

namespace UI.Web.Controllers
{
    public class BaseController : Controller
    {
        internal Usuario usr;
        internal int sucID;

        public BaseController()
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // If the current user is authenticated, check to make sure the user's
            // profile has been loaded into session
            if (ValidateUser())
            {

            }
            else
            {
                filterContext.Result = new RedirectResult(@Url.Action("Login", "Account"));
                return;
            }
        }

        public ActionResult ValidarSesion()
        {
            if (!ValidateUser())
            {
                return View("Login", "Account");
            }

            if (!ValidateSucursal())
            {
                return View("SeleccionarSucursal", "Home");
            }

            return View();
        }

        public ActionResult ErrorPermisos()
        {
            return View();
        }

        public ActionResult ErrorPermisoNuevaVenta()
        {
            return View();
        }

        private bool ValidateUser()
        {
            try
            {
                usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
                if (usr == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch { return false; }
        }

        private bool ValidateSucursal()
        {
            try
            {
                sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
                if (sucID == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }

        }

        protected bool ValidarUsuario(int rol1)
        {
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            UsuarioRol UsuarioRol = usr.Roles.Where(x => x.RolID == rol1 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol != null) return true;

            return false;
        }

        protected bool ValidarUsuario(int rol1, int rol2)
        {
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            UsuarioRol UsuarioRol = usr.Roles.Where(x => x.RolID == rol1 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol == null) UsuarioRol = usr.Roles.Where(x => x.RolID == rol2 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol != null) return true;

            return false;
        }

        protected bool ValidarUsuario(int rol1, int rol2, int rol4)
        {
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            UsuarioRol UsuarioRol = usr.Roles.Where(x => x.RolID == rol1 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol == null) UsuarioRol = usr.Roles.Where(x => x.RolID == rol2 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol == null) UsuarioRol = usr.Roles.Where(x => x.RolID == rol4 && x.SucursalID == sucID).FirstOrDefault();

            if (UsuarioRol != null) return true;

            return false;
        }

        public string GetRol()
        {
            int rolAdministrador = 1, rolResponsable = 2, rolAdmStock = 4;

            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];

            UsuarioRol UsuarioRol = usr.Roles.Where(x => x.RolID == rolAdministrador && x.SucursalID == sucID).FirstOrDefault();
            if (UsuarioRol != null) return "administrador";
            else
            {
                UsuarioRol = usr.Roles.Where(x => x.RolID == rolResponsable && x.SucursalID == sucID).FirstOrDefault();
                if (UsuarioRol != null) return "responsable";
                else
                {
                    UsuarioRol = usr.Roles.Where(x => x.RolID == rolAdmStock && x.SucursalID == sucID).FirstOrDefault();
                    if (UsuarioRol != null) return "adm. de stock";
                    else
                    {
                        return "vendedor";
                    }
                }
            }
        }



    }
}