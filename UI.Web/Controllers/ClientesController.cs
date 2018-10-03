using System.Web.Mvc;
using Modelos;
using Servicios;
using System.Linq;
using UI.Web.ViewModels.Clientes;

namespace UI.Web.Controllers
{
    public class ClientesController : BaseController
    {
        private ClientesServicios _clientesServicios;
      //  private Usuario usr;

        public ClientesController()
        {
            _clientesServicios = new ClientesServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Clientes
        public ActionResult Index(string msj)
        {
            //if (usr == null) return RedirectToAction("Login", "Account");

            ViewBag.Informacion = msj;

            //Traigo todos los clientes
            var Clientes = _clientesServicios.GetAllHabilitados();
            ClienteIndexViewModel ClienteVM = new ClienteIndexViewModel();
            foreach (var cliente in Clientes)
            {
                ClienteVM.Clientes.Add(cliente);
            }
            return View(ClienteVM);
        }

        public ActionResult Agregar()
        {
            //if (usr == null) return RedirectToAction("Login", "Account");

            ClienteAgregarViewModel ClienteVM = new ClienteAgregarViewModel();
            return View(ClienteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(ClienteAgregarViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                var cliente = clienteVM.Mapear();

                bool bandera = _clientesServicios.Add(cliente);
                if (bandera)
                {
                    var mensaje = "El Cliente se registró correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Cliente, por favor vuelva a intentarlo.";
                    return View(clienteVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Cliente, por favor vuelva a intentarlo.";
                return View(clienteVM);
            }

        }

        public ActionResult Eliminar(int id)
        {
            //if (usr == null) return RedirectToAction("Login", "Account");

            ViewBag.Alerta = "Se eliminará el Cliente";
            ClienteEliminarViewModel clienteVM = new ClienteEliminarViewModel(_clientesServicios.GetOne(id));
            return View(clienteVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(ClienteEliminarViewModel clienteVM)
        {
            if (clienteVM.Id != 0)
            {
                var bandera = _clientesServicios.Delete(_clientesServicios.GetOne(clienteVM.Id));
                if (bandera)
                {
                    var mensaje = "El Cliente se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Cliente no se ha eliminado, vuelva a intentarlo.";
                    ClienteEliminarViewModel cVM = new ClienteEliminarViewModel(_clientesServicios.GetOne(clienteVM.Id));
                    return View(cVM);
                }

            }
            else
            {
                ViewBag.Error = "El Cliente no se ha eliminado, vuelva a intentarlo.";
                ClienteEliminarViewModel cVM = new ClienteEliminarViewModel(_clientesServicios.GetOne(clienteVM.Id));
                return View(cVM);
            }
        }

        public ActionResult Editar(int id)
        {
            //if (usr == null) return RedirectToAction("Login", "Account");

            var ClienteVM = new ClienteEditarViewModel(_clientesServicios.GetOne(id));

            return View(ClienteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ClienteEditarViewModel clienteVM)
        {
            if (clienteVM != null)
            {
                var bandera = _clientesServicios.Update(clienteVM.Mapear());
                if (bandera)
                {
                    var mensaje = "El Cliente se ha actualizado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Cliente no se ha actualizado, vuelva a intentarlo.";
                    return View("Editar", clienteVM);
                }
            }
            else
            {
                ViewBag.Error = "El Cliente no se ha actualizado, vuelva a intentarlo.";
                return View("Editar", clienteVM);
            }
        }

        public ActionResult Detalles(int id)
        {
            //if (usr == null) return RedirectToAction("Login", "Account");

            ClienteDetallesViewModel ClienteVM = new ClienteDetallesViewModel(_clientesServicios.GetOne(id));
            return View(ClienteVM);

        }

        public ActionResult BusquedaClientes(string term)
        {
            var clientes = _clientesServicios.GetByNameOrDNI(term);
            var cliente = (from obj in clientes select new { Id = obj.Id, Nombre = obj.NombreCompleto + " ("+obj.DNI+")" });
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }
    }
}