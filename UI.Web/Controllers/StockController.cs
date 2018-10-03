using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Modelos;
using UI.Web.ViewModels.Stock;
using Servicios;

namespace UI.Web.Controllers
{
    public class StockController : BaseController
    {
        private ArticulosServicios _articulosServicios;
        private SucursalesServicios _sucursalesServicios;
        private StockArticuloSucursalServicios _stockArticuloSucursalServicios;
        private StockMovimientosServicios _stockMovimientosServicios;
        private TipoMovimientosStockServicios _tipoMovimientosStockServicios;

        public StockController()
        {
            _articulosServicios = new ArticulosServicios();
            _sucursalesServicios = new SucursalesServicios();
            _stockArticuloSucursalServicios = new StockArticuloSucursalServicios();
            _stockMovimientosServicios = new StockMovimientosServicios();
            _tipoMovimientosStockServicios = new TipoMovimientosStockServicios();


        }

        // GET: Stock
        public ActionResult Index(string msj)
        {
            ViewBag.Informacion = msj;

            StockIndexViewModel StockVM = new StockIndexViewModel();

            //Articulos
            var Articulos = _articulosServicios.GetAll();
            foreach (var articulo in Articulos)
            {
                StockVM.Articulos.Add(articulo);
                StockVM.TotalesStock.Add(articulo.Stock.Sum(a => a.StockActual));
            }

            //Sucursales
            var Sucursales = _sucursalesServicios.GetAll();
            foreach (var sucursal in Sucursales)
            {
                StockVM.Sucursales.Add(sucursal);
            }

            //Por defecto sólo muestra las alertas de stock
            StockVM.MostrarSoloAlertas = false;

            return View(StockVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StockIndexViewModel StockVM)
        {
            //Articulos
            var Articulos = _articulosServicios.GetAll();
            foreach (var articulo in Articulos)
            {
                StockVM.Articulos.Add(articulo);
                StockVM.TotalesStock.Add(articulo.Stock.Sum(a => a.StockActual));
            }

            //Sucursales
            var Sucursales = _sucursalesServicios.GetAll();
            foreach (var sucursal in Sucursales)
            {
                StockVM.Sucursales.Add(sucursal);
            }

            return View(StockVM);
        }

        public ActionResult AsignarStock(AsignarStockViewModel AsignarVM, string mensaje)
        {
            if (!ValidarUsuario(1,2,4)) return RedirectToAction("ErrorPermisos", "Base");

            if (AsignarVM == null)
            {
                AsignarVM = new AsignarStockViewModel();            
            }

            if (mensaje != "" && mensaje != null)
            {
                ViewBag.Informacion = mensaje;
            }

            AsignarVM.Sucursales = _sucursalesServicios.GetAll();

            Session["listaStock"] = null;
           
            return View(AsignarVM);
        }

        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "AgregarArticulo")]
        //public ActionResult AgregarArticulo(AsignarStockViewModel aVM, List<LineaAsignarStockViewModel> lista)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //VentasAgregarViewModel aVM = new VentasAgregarViewModel();
        //        if (aVM.ArticuloID == 0)
        //        {
        //            var items = System.Web.HttpContext.Current.Session["ListaItemsStock"];
        //            aVM.ListaArticulosAsignarStock = (List<LineaAsignarStockViewModel>)items;
        //            if (aVM.ListaArticulosAsignarStock.Count < 1)
        //            {
        //                ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
        //            }
        //        }
        //        else
        //        {

        //            //var items = System.Web.HttpContext.Current.Session["ListaItemsStock"];
        //            //aVM.ListaArticulosAsignarStock = (List<LineaAsignarStockViewModel>)items;

        //            LineaAsignarStockViewModel item = new LineaAsignarStockViewModel();
        //            item.Articulo = _articulosServicios.GetOne(aVM.ArticuloID);
        //            item.ArticuloID = item.Articulo.Id;
        //            item.StockArticuloSucursal = MapearStockArticuloSucursales(item.Articulo);
        //            aVM.ListaArticulosAsignarStock.Add(item);
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
        //    }
        //    aVM.Sucursales = _sucursalesServicios.GetAll();
        //    return View("AsignarStock", aVM);
        //}

        [HttpPost]
        public ActionResult AgregarArticulo(int? articuloID, AsignarStockViewModel AsignarVM, string articulo)
        {
            ViewBag.Sucursales = _sucursalesServicios.GetAll();
            if (AsignarVM == null)
            {
                AsignarVM = new AsignarStockViewModel();
            }
           
            if (ModelState.IsValid)
            {
                int artID = 0;
                if (articuloID == 0)
                {
                    try
                    {
                        if (articulo != "" && articulo != null)
                        {
                            artID = _articulosServicios.GetByNameOrCode(articulo).First().Id;
                        }
                        else
                        {
                            artID = int.Parse(articuloID.ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
                else
                {
                    artID = int.Parse(articuloID.ToString());
                }

                

                if (artID == 0)
                {
                    ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
                }
                else
                {
                    var lista = new List<LineaAsignarStockViewModel>();
                    try
                    {
                        List<LineaAsignarStockViewModel> items = (List<LineaAsignarStockViewModel>)Session["listaStock"];
                        lista.AddRange(items);
                    }
                    catch { List<LineaAsignarStockViewModel> items = new List<LineaAsignarStockViewModel>(); }
                    if (lista.Any(a => a.ArticuloID == artID))
                    {
                        ViewBag.Error = "El artículo ya se encuentra en la lista.";
                        AsignarVM.ListaArticulosAsignarStock = lista;
                    }
                    else
                    {
                        LineaAsignarStockViewModel item = new LineaAsignarStockViewModel();                  
                       

                        item.Articulo = _articulosServicios.GetOne(artID);
                        item.ArticuloID = item.Articulo.Id;
                        item.StockArticuloSucursal = MapearStockArticuloSucursales(item.Articulo);
                        lista.Add(item);
                        AsignarVM.ListaArticulosAsignarStock = lista;
                        Session["listaStock"] = lista;
                    }                    
                }

            }
            else
            {
                ViewBag.Error = "No pudo agregarse el artículo, vuelva a intentarlo.";
            }

            return PartialView("_listaArticulosStock", AsignarVM.ListaArticulosAsignarStock);          
        }
       
        public ActionResult EliminarArticulo(int articuloID, List<LineaAsignarStockViewModel> lista)
        {
            ViewBag.Sucursales = _sucursalesServicios.GetAll();
            if (lista == null)
            {
                lista = new List<LineaAsignarStockViewModel>();
            }

            if (ModelState.IsValid)
            {
                if (articuloID == 0)
                {
                    ViewBag.Error = "No pudo eliminarse el artículo, vuelva a intentarlo.";
                }
                else
                {
                    try
                    {
                        List<LineaAsignarStockViewModel> items = (List<LineaAsignarStockViewModel>)Session["listaStock"];
                        lista.AddRange(items);
                    }
                    catch { List<LineaAsignarStockViewModel> items = new List<LineaAsignarStockViewModel>(); }
                    if (lista.Any(a => a.ArticuloID == articuloID))
                    {
                        var index = lista.Where(a => a.ArticuloID == articuloID).First();
                        lista.Remove(index);
                        Session["listaStock"] = lista;
                    }
                    else
                    {
                        ViewBag.Informacion = "El artículo se ha eliminado correctamente de la lista.";
                    }
                }

            }
            else
            {
                ViewBag.Error = "No pudo eliminarse el artículo, vuelva a intentarlo.";
            }

            return PartialView("_listaArticulosStock", lista);
        }

        private List<StockArticuloSucursalViewModel> MapearStockArticuloSucursales(Articulo art)
        {
            var sucursales = _sucursalesServicios.GetAll();
            List<StockArticuloSucursalViewModel> listaVM = new List<StockArticuloSucursalViewModel>();
            foreach (var suc in sucursales)
            {
                StockArticuloSucursalViewModel sVM = new StockArticuloSucursalViewModel();
                sVM.ArticuloID = art.Id;
                sVM.Articulo = art;
                sVM.Sucursal = suc;
                sVM.SucursalID = suc.Id;
                sVM.StockAgregar = 0;
                sVM.StockActual = _stockArticuloSucursalServicios.GetOneBySucursal(art.Id, suc.Id).StockActual;
                listaVM.Add(sVM);
            }

            return listaVM;
        }

        [HttpPost]     
        public ActionResult AsignarStock(FormCollection collection)
        {
            var cont = collection.GetValues(0).Count();
            var articulos = collection.GetValues(0);
            var sucursales = collection.GetValues(1);
            var cantidades = collection.GetValues(2);

            for (var i = 0; i < cont; i++)
            {
                var art = int.Parse(articulos[i].ToString());
                var suc = int.Parse(sucursales[i].ToString());
                var cant = int.Parse(cantidades[i].ToString());
                if (cant != 0)
                {
                    StockMovimiento sm = new StockMovimiento();
                    sm.ArticuloID = art;
                    sm.Cantidad = cant;
                    sm.Fecha = DateTime.Now;
                    sm.SucursalID = suc;
                    var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
                    sm.UsuarioID = usuario.Id;
                    //Si la sucursal NO es el depsósito, le descuento stock
                    if (suc != 1)
                    {
                        _stockArticuloSucursalServicios.DescontarStockDeposito(art, cant);
                    }
                    sm.TipoMovimientoStockID = _tipoMovimientosStockServicios.GetAll().Where(a => a.Nombre.Contains("Repos")).FirstOrDefault().Id;
                    bool bandera = _stockMovimientosServicios.Agregar(sm, suc);                   
                }
            }

            ViewBag.Sucursales = _sucursalesServicios.GetAll();

            Session["listaStock"] = null;
            string msj = "Stock asignado correctamente!";
            return RedirectToAction("AsignarStock", new { mensaje = msj } );
        }

    }
}