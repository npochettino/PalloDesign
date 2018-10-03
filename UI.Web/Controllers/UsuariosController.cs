using System.Web.Mvc;
using Modelos;
using Servicios;
using UI.Web.ViewModels.Usuarios;


namespace UI.Web.Controllers
{
    public class UsuariosController : BaseController
    {
        private UsuariosServicios _usuariosServicios;
        private SucursalesServicios _sucursalesServicios;
        private RolesServicios _rolesServicios;
        

        public UsuariosController()
        {
            _usuariosServicios = new UsuariosServicios();
            _sucursalesServicios = new SucursalesServicios();
            _rolesServicios = new RolesServicios();
        }

        // GET: Usuario
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            var Usuarios = _usuariosServicios.GetAll();
            UsuarioIndexViewModel UsuarioVM = new UsuarioIndexViewModel();
            foreach (var usuario in Usuarios)
            {
                UsuarioVM.Usuarios.Add(usuario);
            }
            return View(UsuarioVM);

        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            LLenarDropDownList();
            UsuarioViewModel UsuarioVM = new UsuarioViewModel();
            return View(UsuarioVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarUsuario")]
        public ActionResult AgregarUsuario(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                bool bandera = _usuariosServicios.Agregar(usuarioVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Usuario se registró correctamente!" });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Usuario, por favor vuelva a intentarlo.";
                    return View(usuarioVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Usuario, por favor vuelva a intentarlo.";
                return View(usuarioVM);
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarRol")]
        public ActionResult AgregarRol(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                if (usuarioVM.RolIdFromVista == 0 || usuarioVM.SucursalIdFromVista == 0)
                {
                    ViewBag.Error = "No pudo agregarse el Rol, vuelva a intentarlo.";
                }
                else if (usuarioVM.Roles.Exists(a => a.Rol.Id == usuarioVM.RolIdFromVista && a.Sucursal.Id == usuarioVM.SucursalIdFromVista))
                {
                    ViewBag.Error = "El Rol seleccionado ya existe.";
                }
                else
                {
                    //Creo un Usuario Rol y lo agrego a la lista usuarioVM.Roles
                    UsuarioRol UsuarioRol = new UsuarioRol();
                    UsuarioRol.RolID = usuarioVM.RolIdFromVista;
                    UsuarioRol.Rol = _rolesServicios.GetOne(usuarioVM.RolIdFromVista);
                    UsuarioRol.SucursalID = usuarioVM.SucursalIdFromVista;
                    UsuarioRol.Sucursal = _sucursalesServicios.GetOne(usuarioVM.SucursalIdFromVista);
                    usuarioVM.Roles.Add(UsuarioRol);
                }
            }
            else
            {
                ViewBag.Error = "No pudo agregarse el Rol, vuelva a intentarlo.";
            }
            LLenarDropDownList();
            return View("Agregar", usuarioVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EliminarRol")]
        public ActionResult EliminarRol(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                if (usuarioVM.RolIdFromVista == 0 || usuarioVM.SucursalIdFromVista == 0)
                {
                    ViewBag.Error = "No pudo eliminarse el Rol, vuelva a intentarlo.";
                }
                else
                {
                    //Elimino el Rol de la lista usuarioVM.Roles
                    ModelState.Clear();
                    UsuarioRol UsuarioRol = usuarioVM.Roles.Find(a => a.Rol.Id == usuarioVM.RolIdFromVista && a.Sucursal.Id == usuarioVM.SucursalIdFromVista);
                    usuarioVM.Roles.Remove(UsuarioRol);
                }

            }
            else
            {
                ViewBag.Error = "No pudo eliminarse el Rol, vuelva a intentarlo.";
            }
            LLenarDropDownList();
            return View("Agregar", usuarioVM);

        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            var UsuarioVM = new UsuarioViewModel(_usuariosServicios.GetOne(id));
            UsuarioVM.RolIdFromVista = 1;
            UsuarioVM.SucursalIdFromVista = 1;

            LLenarDropDownList();
            return View(UsuarioVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EditarUsuario")]
        public ActionResult EditarUsuario(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                var bandera = _usuariosServicios.Editar(usuarioVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Usuario se ha actualizado correctamente!" });
                }
                else
                {
                    ViewBag.Error = "El Usuario no se ha actualizado, vuelva a intentarlo.";
                    return View("Editar", usuarioVM);
                }
            }
            else
            {
                ViewBag.Error = "El Usuario no se ha actualizado, vuelva a intentarlo.";
                return View("Editar", usuarioVM);
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarRolEnEditar")]
        public ActionResult AgregarRolEnEditar(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                if (usuarioVM.RolIdFromVista == 0 || usuarioVM.SucursalIdFromVista == 0)
                {
                    ViewBag.Error = "No pudo agregarse el Rol, vuelva a intentarlo.";
                }
                else if (usuarioVM.Roles.Exists(a => a.Rol.Id == usuarioVM.RolIdFromVista && a.Sucursal.Id == usuarioVM.SucursalIdFromVista))
                {
                    ViewBag.Error = "El Rol seleccionado ya existe.";
                }
                else
                {
                    //Creo un Usuario Rol y lo agrego a la lista usuarioVM.Roles
                    UsuarioRol UsuarioRol = new UsuarioRol();
                    UsuarioRol.RolID = usuarioVM.RolIdFromVista;
                    UsuarioRol.Rol = _rolesServicios.GetOne(usuarioVM.RolIdFromVista);
                    UsuarioRol.SucursalID = usuarioVM.SucursalIdFromVista;
                    UsuarioRol.Sucursal = _sucursalesServicios.GetOne(usuarioVM.SucursalIdFromVista);
                    usuarioVM.Roles.Add(UsuarioRol);
                }
            }
            else
            {
                ViewBag.Error = "No pudo agregarse el Rol, vuelva a intentarlo.";
            }
            LLenarDropDownList();
            return View("Editar", usuarioVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EliminarRolEnEditar")]
        public ActionResult EliminarRolEnEditar(UsuarioViewModel usuarioVM)
        {
            if (ModelState.IsValid)
            {
                if (usuarioVM.RolIdFromVista == 0 || usuarioVM.SucursalIdFromVista == 0)
                {
                    ViewBag.Error = "No pudo eliminarse el Rol, vuelva a intentarlo.";
                }
                else
                {
                    //Elimino el Rol de la lista usuarioVM.Roles
                    ModelState.Clear();
                    UsuarioRol UsuarioRol = usuarioVM.Roles.Find(a => a.Rol.Id == usuarioVM.RolIdFromVista && a.Sucursal.Id == usuarioVM.SucursalIdFromVista);
                    usuarioVM.Roles.Remove(UsuarioRol);
                }

            }
            else
            {
                ViewBag.Error = "No pudo eliminarse el Rol, vuelva a intentarlo.";
            }
            LLenarDropDownList();
            return View("Editar", usuarioVM);

        }

        public ActionResult Eliminar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = "Se eliminará el Usuario";
            UsuarioViewModel usuarioVM = new UsuarioViewModel(_usuariosServicios.GetOne(id));
            return View(usuarioVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(UsuarioViewModel usuarioVM)
        {
            if (usuarioVM.Id != 0)
            {
                var bandera = _usuariosServicios.Eliminar(_usuariosServicios.GetOne(usuarioVM.Id));
                if (bandera)
                {
                    var mensaje = "El Usuario se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Usuario no se ha eliminado, vuelva a intentarlo.";
                    UsuarioViewModel uVM = new UsuarioViewModel(_usuariosServicios.GetOne(usuarioVM.Id));
                    return View(uVM);
                }

            }
            else
            {
                ViewBag.Error = "El Usuario no se ha eliminado, vuelva a intentarlo.";
                UsuarioViewModel uVM = new UsuarioViewModel(_usuariosServicios.GetOne(usuarioVM.Id));
                return View(uVM);
            }
        }

        public ActionResult CambiarClave()
        {
            CambiarClaveViewModel usuarioVM = new CambiarClaveViewModel();

            return View(usuarioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarClave(CambiarClaveViewModel usuarioVM)
        {
            if(ModelState.IsValid)
            {

                //Obtener el usuario logueado, cambiarle el password y hacer el update
                Usuario UsuarioActual = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
                var Usuario = _usuariosServicios.GetOne(UsuarioActual.Id);

                Usuario.Password = usuarioVM.Password;

                var bandera = _usuariosServicios.Update(Usuario);
                if(bandera)
                {
                    var msj = "Su contraseña ha sido cambiada!";
                    return RedirectToAction("Agregar", "Ventas", new { mensaje = msj });
                }
                else
                {
                    return View(usuarioVM);
                }
                
            }
            else
            {
                return View(usuarioVM);
            }

            
        }

        private void LLenarDropDownList()
        {
            ViewBag.Roles = _rolesServicios.GetAll();
            ViewBag.Sucursales = _sucursalesServicios.GetAll();

        }
        
    }
}