using Modelos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.Ventas;

namespace UI.Web.Controllers
{
    public class VentasController : BaseController
    {
        private VentasServicios _ventasServicios;
        private VentaItemsServicios _ventaItemsServicios;
        private ArticulosServicios _articulosServicios;
        private FormasDePagoServicios _formasDePagoServicios;
        private StockArticuloSucursalServicios _stockArticuloSucursalServicios;
        private StockMovimientosServicios _stockMovimientosServicios;
        private UsuariosRolesServicios _usuariosRolesServicios;
        //private Usuario usr;
        //private int sucID;

        public VentasController()
        {
            _ventasServicios = new VentasServicios();
            _ventaItemsServicios = new VentaItemsServicios();
            _articulosServicios = new ArticulosServicios();
            _formasDePagoServicios = new FormasDePagoServicios();
            _stockArticuloSucursalServicios = new StockArticuloSucursalServicios();
            _stockMovimientosServicios = new StockMovimientosServicios();
            _usuariosRolesServicios = new UsuariosRolesServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
        }

        public ActionResult ReportePorRubro()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ReportePorRubroViewModel vVM = new ReportePorRubroViewModel();
            vVM.FechaDesde = DateTime.Now;
            vVM.FechaHasta = DateTime.Now;
            var ventas = _ventasServicios.GetByDate(vVM.FechaDesde, vVM.FechaHasta);         
            vVM.Detalles = ArmarDetalleReportePorRubro(ventas);
            vVM.CabeceraReporte = String.Format("Reporte por Rubro de {0} a {1}", vVM.FechaDesde.ToShortDateString(), vVM.FechaHasta.ToShortDateString());
            return View(vVM);
        }

        private List<ReportePorRubroDetalleViewModel> ArmarDetalleReportePorRubro(List<Venta> ventas)
        {
            List<ReportePorRubroDetalleViewModel> detalles = new List<ReportePorRubroDetalleViewModel>();
            List<VentaItem> items = new List<VentaItem>();
            foreach (var v in ventas)
            {
                items.AddRange(v.VentaItem);
            }

            foreach (var i in items.GroupBy(a => a.Articulo.Rubro.Nombre))
            {
                ReportePorRubroDetalleViewModel vm = new ReportePorRubroDetalleViewModel();
                vm.Nombre = i.Key;
                vm.Cantidad = i.Sum(a => a.Cantidad);
                vm.Total = items.Sum(a => a.Cantidad);
            
                detalles.Add(vm);
            }

            return detalles;
        }

        [HttpPost]
        public ActionResult ReportePorRubro(ReportePorRubroViewModel vVM)
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            var ventas = _ventasServicios.GetByDate(vVM.FechaDesde, vVM.FechaHasta);         
            vVM.Detalles = ArmarDetalleReportePorRubro(ventas);
            vVM.CabeceraReporte = String.Format("Reporte por Rubro de {0} a {1}", vVM.FechaDesde.ToShortDateString(), vVM.FechaHasta.ToShortDateString());
            return View(vVM);
        }

        private List<VentaItem> ArmarReportePorRubro(List<Venta> ventas)
        {
            List<VentaItem> items = new List<VentaItem>();
            foreach (var v in ventas)
            {
                items.AddRange(v.VentaItem);
            }
            return items;
        }

        public ActionResult ReportePorVendedor()
        {
            if (!ValidarUsuario(1, 2)) return RedirectToAction("ErrorPermisos", "Base");

            ReportePorVendedorViewModel vVM = new ReportePorVendedorViewModel();
            vVM.FechaDesde = DateTime.Now;
            vVM.FechaHasta = DateTime.Now;
            var ventas = _ventasServicios.GetByDate(vVM.FechaDesde, vVM.FechaHasta);
            vVM.Ventas = ventas.Where(a => a.SucursalID == sucID).ToList();
            ViewBag.Vendedores = _usuariosRolesServicios.GetUsuariosByRolYSucursal(sucID, 3); //El 3 es Vendedor

            return View(vVM);
        }

        [HttpPost]
        public ActionResult ReportePorVendedor(ReportePorVendedorViewModel rVM)
        {
            var ventas = _ventasServicios.GetByDate(rVM.FechaDesde, rVM.FechaHasta);
            rVM.Ventas = ventas.Where(a => a.SucursalID == sucID && a.Vendedor.Id == rVM.VendedorID).ToList();
            ViewBag.Vendedores = _usuariosRolesServicios.GetUsuariosByRolYSucursal(sucID, 3); //El 3 es Vendedor

            return View(rVM);
        }

        [HttpPost]
        public ActionResult ReportePorVendedorPrint(ReportePorVendedorViewModel rVM)
        {
            var ventas = _ventasServicios.GetByDate(rVM.FechaDesde, rVM.FechaHasta);
            rVM.Ventas = ventas.Where(a => a.SucursalID == sucID && a.Vendedor.Id == rVM.VendedorID).ToList();
            var v = _usuariosRolesServicios.GetUsuariosByRolYSucursal(sucID, 3); //El 3 es Vendedor
            var u = v.Where(a => a.Id == rVM.VendedorID).FirstOrDefault();
            rVM.CabeceraReporte = u.NombreCompleto + " (Desde: " + rVM.FechaDesde.ToShortDateString() + " Hasta: " + rVM.FechaHasta.ToShortDateString() + ")";

            return View(rVM);
        }


        // GET: Ventas
        public ActionResult Index()
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            VentasIndexViewModel vVM = new VentasIndexViewModel();
            vVM.FechaDesde = DateTime.Now;
            vVM.FechaHasta = DateTime.Now;
            var ventas = _ventasServicios.GetByDate(vVM.FechaDesde, vVM.FechaHasta);
            vVM.Ventas = ventas.Where(a => a.SucursalID == sucID).ToList();
            return View(vVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VentasIndexViewModel vVM)
        {
            if (ModelState.IsValid)
            {
                var ventas = _ventasServicios.GetByDate(vVM.FechaDesde, vVM.FechaHasta);
                vVM.Ventas = ventas.Where(a => a.SucursalID == sucID).ToList();
            }
            else
            {
                ViewBag.Error = "No pudo realizarse la búsqueda, vuelva a intentarlo.";
            }
            return View(vVM);
        }

        public ActionResult Agregar(VentasAgregarViewModel aVM, string mensaje)
        {
            //Valido que no se pueda realizar una venta cuando la sucursal es "Depósito"
            sucID = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            if (sucID == 1) return RedirectToAction("ErrorPermisoNuevaVenta", "Base");

            if (aVM == null)
            {
                aVM = new VentasAgregarViewModel();
            }

            if (mensaje != "" && mensaje != null)
            {
                ViewBag.Informacion = mensaje;
            }
            if (System.Web.HttpContext.Current.Session["SaldoAFavor"] != null)
            {
                aVM.SaldoAFavor = decimal.Parse(System.Web.HttpContext.Current.Session["SaldoAFavor"].ToString());
            }

            return View(aVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarPago")]
        public ActionResult AgregarPago(VentasAgregarPagoViewModel apVM)
        {
            apVM.FormasDePago = null;
            apVM.FormasDePago = new SelectList(_formasDePagoServicios.GetAll(), "Id", "Nombre", 1);
            if (apVM.FormaDePagoID != 0)
            {
                apVM.Items = (List<VentaItem>)System.Web.HttpContext.Current.Session["ListaItemsVentaActual"];
                var formaDePago = _formasDePagoServicios.GetOne(apVM.FormaDePagoID);
                if (apVM.Pagos.Any(a => a.FormaDePagoID == formaDePago.Id))
                {
                    ViewBag.Error = "La forma de pago ya está agregada.";
                    return View(apVM);
                }

                Pago pago = new Pago();
                pago.FormaDePago = _formasDePagoServicios.GetOne(formaDePago.Id);
                pago.FormaDePagoID = pago.FormaDePago.Id;
                apVM.Pagos.Add(pago);

                if (apVM.Pagos.Count == 1)
                {
                    apVM.Pagos.First().Monto = apVM.Total;
                }
            }
            else
            {
                Pago pago = new Pago();
                pago.FormaDePago = _formasDePagoServicios.GetOne(1);
                pago.FormaDePagoID = pago.FormaDePago.Id;
                apVM.Pagos.Add(pago);
                if (apVM.Pagos.Count == 1)
                {
                    apVM.Pagos.First().Monto = apVM.Total;
                }
                //Agregao la forma de pago Devolución
                if (System.Web.HttpContext.Current.Session["SaldoAFavor"] != null)
                {
                    decimal SaldoAFavor = decimal.Parse(System.Web.HttpContext.Current.Session["SaldoAFavor"].ToString());

                    Pago pagoDev = new Pago();
                    pagoDev.FormaDePago = _formasDePagoServicios.GetOne(4);
                    pagoDev.FormaDePagoID = pagoDev.FormaDePago.Id;
                    apVM.Pagos.Add(pagoDev);

                    apVM.Pagos[0].Monto = apVM.Total - SaldoAFavor;
                    apVM.Pagos[1].Monto = SaldoAFavor;

                }
                System.Web.HttpContext.Current.Session["ListaItemsVentaActual"] = apVM.Items;
            }

            return View(apVM);
        }

        //EliminarFormaPago
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EliminarFormaPago")]
        public ActionResult EliminarFormaPago(VentasAgregarPagoViewModel apVM)
        {

            apVM.FormasDePago = new SelectList(_formasDePagoServicios.GetAll(), "Id", "Nombre", 1);
            if (apVM.FormaDePagoID != 0)
            {
                apVM.Items = (List<VentaItem>)System.Web.HttpContext.Current.Session["ListaItemsVentaActual"];
                Pago pago = new Pago();
                ModelState.Clear();
                pago = apVM.Pagos.Where(a => a.FormaDePagoID == apVM.FormaPagoIDEliminar).FirstOrDefault();
                apVM.Pagos.Remove(pago);

                if (apVM.Pagos.Count == 1)
                {
                    apVM.Pagos.First().Monto = apVM.Total;
                }

                ViewBag.Informacion = "Forma de pago eliminada correctamente!";
            }


            return View("AgregarPago", apVM);
        }

        public ActionResult ConfirmarVenta(VentasAgregarPagoViewModel apVM)
        {
            if (apVM.ClienteID == 0)
            {
                apVM.ClienteID = 1;
            }
            apVM.Items = (List<VentaItem>)System.Web.HttpContext.Current.Session["ListaItemsVentaActual"];

            bool bandera = _ventasServicios.Add(apVM.MapearVenta());
            var msj = "";
            if (bandera)
            {
                msj = "Venta agregada correctamente!";
                var venta = _ventasServicios.GetOne(apVM.Pagos.FirstOrDefault().VentaID);
                _stockArticuloSucursalServicios.DescontarStock(venta);
                _stockMovimientosServicios.AgregarMovimientoVentas(venta.VentaItem, sucID, usr.Id);
                ViewBag.Informacion = "Venta generada correctamente!";
                return View("PrintVenta", venta);
                //apVM = new VentasAgregarPagoViewModel();
            }
            else
            {
                ViewBag.Error = "No se pudo agregar la venta. Vuelva a intentarlo.";
                return View("AgregarPago", apVM);
            }
            //return RedirectToAction("Agregar", new { mensaje = msj });
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AgregarArticulo")]
        public ActionResult AgregarArticulo(VentasAgregarViewModel aVM, string articulo)
        {

            if (aVM.ArticuloIdAgregar != 0)
            {
                if (articulo != "" && articulo != null)
                {
                    var art = _articulosServicios.GetByNameOrCode(articulo);
                    aVM.ArticuloIdAgregar = art.FirstOrDefault().Id; //Int64.Parse(articulo);
                }
            }


            if (ModelState.IsValid)
            {
                if (System.Web.HttpContext.Current.Session["SaldoAFavor"] != null)
                {
                    aVM.SaldoAFavor = decimal.Parse(System.Web.HttpContext.Current.Session["SaldoAFavor"].ToString());
                }
                //VentasAgregarViewModel aVM = new VentasAgregarViewModel();
                if (aVM.ArticuloIdAgregar == 0)
                {

                    var items = System.Web.HttpContext.Current.Session["ListaItemsVentaActual"];
                    aVM.Items = (List<VentaItem>)items;
                    try
                    {
                        if (aVM.Items.Count < 1)
                        {
                            ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
                        aVM.Items = new List<VentaItem>();
                    }

                }
                else
                {
                    int cantStock = _stockArticuloSucursalServicios.GetStock(aVM.ArticuloIdAgregar, sucID);
                    if (cantStock > 0)
                    {
                        if (aVM.Items.Any(a => a.ArticuloID == aVM.ArticuloIdAgregar))
                        {
                            ModelState.Clear();
                            var cantActual = aVM.Items.Find(a => a.ArticuloID == aVM.ArticuloIdAgregar).Cantidad;
                            if (cantActual >= cantStock)
                            {
                                ViewBag.Error = "No hay stock suficiente para el artículo seleccionado.";
                            }
                            else
                            {
                                aVM.Items.Find(a => a.ArticuloID == aVM.ArticuloIdAgregar).Cantidad = cantActual + 1;
                            }
                            aVM.ArticuloIdAgregar = 0;
                        }
                        else
                        {
                            aVM.Items.Add(_ventaItemsServicios.AgregarArticuloEnVenta(aVM.ArticuloIdAgregar));
                            ModelState.Clear();
                            aVM.ArticuloIdAgregar = 0;
                        }
                        System.Web.HttpContext.Current.Session["ListaItemsVentaActual"] = aVM.Items;
                    }
                    else
                    {
                        ViewBag.Error = "No hay stock suficiente para el artículo seleccionado.";
                    }

                }

            }
            else
            {
                ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
            }
            return View("Agregar", aVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "RestarArticulo")]
        public ActionResult RestarArticulo(VentasAgregarViewModel aVM)
        {
            if (ModelState.IsValid)
            {
                if (aVM.ArticuloIdAgregar == 0)
                {
                    ViewBag.Error = "No pudo restarse el artículo, vuelva a intentarlo.";
                }
                else
                {
                    if (aVM.Items.Any(a => a.ArticuloID == aVM.ArticuloIdAgregar))
                    {
                        var cantActual = aVM.Items.Find(a => a.ArticuloID == aVM.ArticuloIdAgregar).Cantidad;
                        if (cantActual <= 1)
                        {
                            ViewBag.Error = "La cantidad del artículo no puede ser 0.";
                        }
                        else
                        {
                            ModelState.Clear();
                            aVM.Items.Find(a => a.ArticuloID == aVM.ArticuloIdAgregar).Cantidad = cantActual - 1;
                            aVM.ArticuloIdAgregar = 0;
                        }

                    }
                    else
                    {
                        ViewBag.Error = "No pudo restarse el artículo, vuelva a intentarlo.";
                    }
                }

            }
            else
            {
                ViewBag.Error = "No pudo restarse el artículo, vuelva a intentarlo.";
            }
            return View("Agregar", aVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AplicarDescuento")]
        public ActionResult AplicarDescuento(VentasAgregarViewModel aVM)
        {
            if (ModelState.IsValid)
            {
                //if (aVM.ArticuloIdAgregar == null || aVM.ArticuloIdAgregar == 0)
                //{
                //    ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
                //}
                //else
                //{
                //    aVM.Items.Add(_ventaItemsServicios.AgregarArticuloEnVenta(aVM.ArticuloIdAgregar));
                //}

            }
            else
            {
                ViewBag.Error = "No pudo aplicarse el descuento, vuelva a intentarlo.";
            }
            return View("Agregar", aVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EliminarArticulo")]
        public ActionResult EliminarArticulo(VentasAgregarViewModel aVM)
        {
            if (ModelState.IsValid)
            {
                if (aVM.ArticuloIdAgregar == 0)
                {
                    ViewBag.Error = "No pudo eliminarse el artículo, vuelva a intentarlo.";
                }
                else
                {
                    if (aVM.Items.Any(a => a.ArticuloID == aVM.ArticuloIdAgregar))
                    {
                        ModelState.Clear();
                        var item = aVM.Items.Find(a => a.ArticuloID == aVM.ArticuloIdAgregar);
                        aVM.Items.Remove(item);
                        aVM.ArticuloIdAgregar = 0;

                    }
                    else
                    {
                        ViewBag.Error = "No pudo eliminarse el artículo, vuelva a intentarlo.";
                    }
                }

            }
            else
            {
                ViewBag.Error = "No pudo eliminarse el artículo, vuelva a intentarlo.";
            }
            return View("Agregar", aVM);
        }

        public ActionResult Detalles(int Id)
        {

            if (Id != 0)
            {
                var venta = _ventasServicios.GetOne(Id);

                VentasDetallesViewModel dVM = new VentasDetallesViewModel(venta);
                return View(dVM);
            }
            else
            {
                ViewBag.Error = "No se pudo ingresar a la venta, vuelva a intentarlo.";
                return View("Index");
            }
        }

    }
}