using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Web.ViewModels.Devoluciones;
using Servicios;
using Modelos;

namespace UI.Web.Controllers
{
    public class DevolucionesController : Controller
    {
        private VentasServicios _ventaServicios;
        private VentaItemsServicios _ventaItemsServicios;
        private StockMovimientosServicios _stockMovimientosServicios;

        public DevolucionesController()
        {
            _ventaServicios = new VentasServicios();
            _ventaItemsServicios = new VentaItemsServicios();
            _stockMovimientosServicios = new StockMovimientosServicios();
        }

        // GET: Devoluciones
        public ActionResult Agregar()
        {
            DevolucionAgregarViewModel devolucionVM = new DevolucionAgregarViewModel();

            return View(devolucionVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "BuscarVenta")]
        public ActionResult BuscarVenta(DevolucionAgregarViewModel devolucionVM)
        {
            //Limpio la lista de items
            devolucionVM.Items.Clear();
            if (ModelState.IsValid)
            {
                var Venta = _ventaServicios.GetOne(devolucionVM.VentaID);
                if (Venta != null)
                {
                    foreach (VentaItem ventaItem in Venta.VentaItem)
                    {
                        if (ventaItem.Devuelto == false)
                        {
                            DevolucionItemViewModel itemVM = new DevolucionItemViewModel(ventaItem.ArticuloID, ventaItem.Id, ventaItem.Articulo.Nombre, ventaItem.Articulo.Codigo, ventaItem.Cantidad, ventaItem.Precio);
                            devolucionVM.Items.Add(itemVM);
                        }

                    }
                }
                else
                {
                    ViewBag.Error = "No se pudo encontrar la Venta, vuelva a intentarlo.";
                }
            }
            else
            {
                ViewBag.Error = "No se pudo encontrar la Venta, vuelva a intentarlo.";
            }

            return View("Agregar", devolucionVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ConfirmarDevolucion")]
        public ActionResult ConfirmarDevolucion(DevolucionAgregarViewModel devolucionVM)
        {
            if (ModelState.IsValid)
            {
                if (ValidarCantidades(devolucionVM.Items))
                {
                    decimal montoARestar = 0;
                    foreach (var item in devolucionVM.Items)
                    {
                        if (item.CantidadADevolver > 0)
                        {
                            montoARestar += item.CantidadADevolver * item.PrecioDeVenta;
                            //Resto la CantidadADevolver al registro correspondiente
                            VentaItem VentaItem = _ventaItemsServicios.GetOne(item.VentaItemID);
                            VentaItem.Cantidad -= item.CantidadADevolver;
                            _ventaItemsServicios.Update(VentaItem);
                            //Creo un nuevo registro con la CantidadADevolver y Devuelto = true
                            VentaItem.Cantidad = item.CantidadADevolver;
                            VentaItem.Devuelto = true;
                            _ventaItemsServicios.Add(VentaItem);

                            //Si afecta el stock aumento el stock y agrego un movimiento de stock del tipo "devolucion"
                            if (item.VuelveAlStock)
                            {
                                StockMovimiento StockMovimiento = new StockMovimiento();

                                StockMovimiento.Fecha = DateTime.Now;
                                StockMovimiento.Cantidad = item.CantidadADevolver;
                                StockMovimiento.TipoMovimientoStockID = 3; //Es la devolucion
                                StockMovimiento.ArticuloID = item.ArticuloID;
                                var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
                                StockMovimiento.UsuarioID = usuario.Id;

                                _stockMovimientosServicios.Agregar(StockMovimiento, (int)System.Web.HttpContext.Current.Session["SucursalActual"]);

                            }
                        }
                    }

                    //Actualiza el monto total de la venta
                    //_ventaServicios.ActualizarMontoDeVenta(devolucionVM.VentaID, montoARestar);

                    ViewBag.Informacion = "Devolución registrada con éxito";
                    devolucionVM.Items.Clear();
                    devolucionVM.VentaID = 0;
                    return View("Agregar", devolucionVM);

                }
                else
                {
                    ViewBag.Error = "Las cantidades a devolver no pueden ser mayor a las cantidades vendidas.";
                }

            }
            else
            {
                ViewBag.Error = "No se pudo generar la devolución, vuelva a intentarlo.";
            }
            return View("Agregar", devolucionVM);

        }

        private bool ValidarCantidades(List<DevolucionItemViewModel> itemsVM)
        {
            foreach (DevolucionItemViewModel item in itemsVM)
            {
                if (item.CantidadADevolver > item.CantidadVendida)
                    return false;
            }

            return true;
        }
    }
}