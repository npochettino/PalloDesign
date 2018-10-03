using System;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.DevolucionesSinTicket;
using Servicios;
using Modelos;

namespace UI.Web.Controllers
{
    public class DevolucionesSinTicketController : BaseController
    {
        DevolucionesSinTicketServicios _devolucionesSinTicketServicios;
        ArticulosServicios _articulosServicios;
        StockArticuloSucursalServicios _stockArticuloSucursalServicios;

        public DevolucionesSinTicketController()
        {
            _devolucionesSinTicketServicios = new DevolucionesSinTicketServicios();
            _articulosServicios = new ArticulosServicios();
            _stockArticuloSucursalServicios = new StockArticuloSucursalServicios();
        }

        // GET: DevolucionesSinTicket
        public ActionResult Index(string msj)
        {
           // if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = msj;

            DevolucionSinTicketIndexViewModel DevolucionSinTicketVM = new DevolucionSinTicketIndexViewModel();
            DevolucionSinTicketVM.FechaDesde = DateTime.Now;
            DevolucionSinTicketVM.FechaHasta = DateTime.Now;

            var devolucionesSinTicket = _devolucionesSinTicketServicios.GetByDate(DevolucionSinTicketVM.FechaDesde, DevolucionSinTicketVM.FechaHasta);
            var sucursalID = int.Parse(System.Web.HttpContext.Current.Session["SucursalActual"].ToString());
            DevolucionSinTicketVM.DevolucionesSinTicket = devolucionesSinTicket.Where(a => a.SucursalID == sucursalID).ToList();

            return View(DevolucionSinTicketVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DevolucionSinTicketIndexViewModel DevolucionSinTicketVM)
        {
            if (ModelState.IsValid)
            {
                var devolucionesSinTicket = _devolucionesSinTicketServicios.GetByDate(DevolucionSinTicketVM.FechaDesde, DevolucionSinTicketVM.FechaHasta);
                var sucursalID = int.Parse(System.Web.HttpContext.Current.Session["SucursalActual"].ToString());
                DevolucionSinTicketVM.DevolucionesSinTicket = devolucionesSinTicket.Where(a => a.SucursalID == sucursalID).ToList();
            }
            else
            {
                ViewBag.Error = "No pudo realizarse la búsqueda, vuelva a intentarlo.";
            }
            return View(DevolucionSinTicketVM);
        }

        public ActionResult Agregar()
        {
            DevolucionSinTicketAgregarViewModel DevolucionSinTicketVM = new DevolucionSinTicketAgregarViewModel();
            DevolucionSinTicketVM.Cantidad = 1;

            return View(DevolucionSinTicketVM);

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CalcularMonto")]
        public ActionResult CalcularMonto(DevolucionSinTicketAgregarViewModel devolucionSinTicketVM)
        {
            if(devolucionSinTicketVM.ArticuloID == 0)
            {
                ViewBag.Error = "Debe seleccionar un Artículo";
                return View("Agregar", devolucionSinTicketVM);
            }

            var Articulo = _articulosServicios.GetOne(devolucionSinTicketVM.ArticuloID);
            devolucionSinTicketVM.Monto = Articulo.PrecioActualVenta * devolucionSinTicketVM.Cantidad;
            devolucionSinTicketVM.Articulo = Articulo.Nombre;
            return View("Agregar", devolucionSinTicketVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarDevolucionSinTicket")]
        public ActionResult AgregarDevolucionSinTicket(DevolucionSinTicketAgregarViewModel devolucionSinTicketVM)
        {
            if (devolucionSinTicketVM.ArticuloID == 0)
            {
                ViewBag.Error = "Debe seleccionar un Artículo";
                return View("Agregar", devolucionSinTicketVM);
            }
            //Vuelvo a calcular el monto, porque el usuario pudo haber cambiado los datos luego de 
            //presionar el boton "calcular monto"
            var Articulo = _articulosServicios.GetOne(devolucionSinTicketVM.ArticuloID);
            devolucionSinTicketVM.Monto = Articulo.PrecioActualVenta * devolucionSinTicketVM.Cantidad;

            var DevolucionSinTicket = devolucionSinTicketVM.Mapear();

            //Registrar la DevolucionSinTicket
            if (!_devolucionesSinTicketServicios.Add(DevolucionSinTicket))
            {
                ViewBag.Error = "No se pudo agregar la Nota de Crédito, vuelva a intentarlo";
                return View("Agregar", devolucionSinTicketVM);
            }

            //Sumar al Stock si corresponde
            if(devolucionSinTicketVM.RegresaAlStock)
            {
                int SucursalID = int.Parse(System.Web.HttpContext.Current.Session["SucursalActual"].ToString());
                var StockArticuloSucursal = _stockArticuloSucursalServicios.GetOneBySucursal(devolucionSinTicketVM.ArticuloID, SucursalID);

                StockArticuloSucursal.StockActual += devolucionSinTicketVM.Cantidad;

                _stockArticuloSucursalServicios.Update(StockArticuloSucursal);
            }

            //Registrar el monto en una variable de sesion
            System.Web.HttpContext.Current.Session["SaldoAFavor"] = devolucionSinTicketVM.Monto;

            return RedirectToAction("Agregar", "Ventas");
        }

        public ActionResult Eliminar (int id)
        {
             if(id != 0)
            {
                DevolucionSinTicket DevolucionSinTicket = _devolucionesSinTicketServicios.GetOne(id);

                //Actualizo el Stock si corresponde
                if (DevolucionSinTicket.RegresaAlStock)
                {
                    var StockArticuloSucursal = _stockArticuloSucursalServicios.GetOneBySucursal(DevolucionSinTicket.ArticuloID, DevolucionSinTicket.SucursalID);

                    StockArticuloSucursal.StockActual -= DevolucionSinTicket.Cantidad;

                    _stockArticuloSucursalServicios.Update(StockArticuloSucursal);
                }

                //Elimino
                if (_devolucionesSinTicketServicios.Delete(DevolucionSinTicket))
                {
                    return RedirectToAction("Index", new { msj = "La Nota de Crédito se eliminó correctamente." });
                }
                else
                {
                    return RedirectToAction("Index", new { msj = "No se pudo eliminar la Nota de Crédito, vuelva a intentarlo." });
                }
            }
            else
            {
                return RedirectToAction("Index", new { msj = "No se pudo eliminar la Nota de Crédito, vuelva a intentarlo." });
            }

        }

    }
}