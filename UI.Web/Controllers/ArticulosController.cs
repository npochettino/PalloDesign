using BarcodeLib;
using Modelos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Web.Util;
using UI.Web.ViewModels.Articulos;

namespace UI.Web.Controllers
{
    public class ArticulosController : BaseController
    {
        private ArticulosServicios _articulosServicios;
        private RubrosServicios _rubrosServicios;
        private ConfiguracionServicios _configuracionesServicios;
        private SucursalesServicios _sucServicios;
        private StockArticuloSucursalServicios _stockArticuloSucursalServicios;
        private ConfiguracionServicios _configuracionServicios;

        // private Usuario usr;

        public ArticulosController()
        {
            _articulosServicios = new ArticulosServicios();
            _rubrosServicios = new RubrosServicios();
            _configuracionesServicios = new ConfiguracionServicios();
            _sucServicios = new SucursalesServicios();
            _stockArticuloSucursalServicios = new StockArticuloSucursalServicios();
            _configuracionServicios = new ConfiguracionServicios();

            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Articulos
        public ActionResult Index(string msj)
        {
            if (!ValidarUsuario(1, 4)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Informacion = msj;

            ArticuloIndexViewModel ArticuloVM = new ArticuloIndexViewModel();
            // Traigo todos los articulos
            //var Articulos = _articulosServicios.GetAll();

            //foreach (var articulo in Articulos)
            //{
            //    ArticuloVM.Articulos.Add(articulo);
            //}

            //Traigo las sucursales
            ArticuloVM.Sucursales = _sucServicios.GetAll();

            return View(ArticuloVM);
        }

        [HttpPost]
        public ActionResult Index(ArticuloIndexViewModel vm)
        {
            if (String.IsNullOrEmpty(vm.Filtro) && vm.Filtro.Length < 4)
            {
                ViewBag.Informacion = "Debe ingresar más de 3 letras del nombre del articulo a buscar.";
                return View(vm);
            }
            // Traigo todos los articulos
            var Articulos = GetArticulosByName(vm.Filtro.ToLower().Trim());
            vm.Articulos = new List<Articulo>();
            foreach (var articulo in Articulos)
            {
                vm.Articulos.Add(articulo);
            }

            //Traigo las sucursales
            vm.Sucursales = new List<Sucursal>();
            vm.Sucursales = _sucServicios.GetAll();

            return View(vm);
        }

        public ActionResult Agregar()
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            //LLeno el ddl de rubros
            ViewBag.Rubros = _rubrosServicios.GetAll();
            ArticuloAgregarViewModel ArticuloVM = new ArticuloAgregarViewModel();
            return View(ArticuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(ArticuloAgregarViewModel articuloVM)
        {
            if (ModelState.IsValid)
            {
                var StockInicial = articuloVM.StockInicial;
                var articulo = articuloVM.Mapear();

                bool bandera = _articulosServicios.Agregar(articulo, StockInicial);
                if (bandera)
                {
                    var mensaje = "El Artículo se registró correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el Artículo, por favor vuelva a intentarlo.";
                    ViewBag.Rubros = _rubrosServicios.GetAll();
                    return View(articuloVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Artículo, por favor vuelva a intentarlo.";
                ViewBag.Rubros = _rubrosServicios.GetAll();
                return View(articuloVM);
            }
        }

        public ActionResult Eliminar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            ViewBag.Alerta = "Se eliminará el Artículo y sus correspondientes Stocks";
            ArticuloEliminarViewModel ArticuloVM = new ArticuloEliminarViewModel(_articulosServicios.GetOne(id));
            return View(ArticuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(ArticuloEliminarViewModel articuloVM)
        {
            if (articuloVM.Id != 0)
            {
                var bandera = _articulosServicios.Eliminar(articuloVM.Id);
                if (bandera)
                {
                    var mensaje = "El Artículo se ha eliminado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Artículo no se ha eliminado, vuelva a intentarlo.";
                    ArticuloEliminarViewModel aVM = new ArticuloEliminarViewModel(_articulosServicios.GetOne(articuloVM.Id));
                    return View(aVM);
                }
            }
            else
            {
                ViewBag.Error = "El Artículo no se ha eliminado, vuelva a intentarlo.";
                ArticuloEliminarViewModel aVM = new ArticuloEliminarViewModel(_articulosServicios.GetOne(articuloVM.Id));
                return View(aVM);
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ValidarUsuario(1)) return RedirectToAction("ErrorPermisos", "Base");

            //LLeno el ddl de rubros
            ViewBag.Rubros = _rubrosServicios.GetAll();
            var ArticuloVM = new ArticuloEditarViewModel(_articulosServicios.GetOne(id));

            return View(ArticuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ArticuloEditarViewModel articuloVM)
        {
            if (articuloVM != null && ModelState.IsValid)
            {
                //Valido que la longitud de Nombre Etiqueta sea correcta
                var LongitudNombreEtiqueta = _configuracionesServicios.GetLongitudNombreEtiqueta();
                if (articuloVM.NombreEtiqueta.Length > LongitudNombreEtiqueta)
                {
                    ViewBag.Error = "Longitud Máxima del campo Nombre Etqueta " + LongitudNombreEtiqueta + " caracteres";
                    ViewBag.Rubros = _rubrosServicios.GetAll();
                    return View("Editar", articuloVM);
                }

                var bandera = _articulosServicios.Editar(articuloVM.Mapear());
                if (bandera)
                {
                    var mensaje = "El Artículo se ha actualizado correctamente!";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "El Artículo no se ha actualizado, vuelva a intentarlo.";
                    ViewBag.Rubros = _rubrosServicios.GetAll();
                    return View("Editar", articuloVM);
                }
            }
            else
            {
                ViewBag.Error = "El Artículo no se ha actualizado, vuelva a intentarlo.";
                ViewBag.Rubros = _rubrosServicios.GetAll();
                return View("Editar", articuloVM);
            }
        }

        public ActionResult BusquedaArticulo(string term)
        {
            var articulos = _articulosServicios.GetByNameOrCode(term);
            var articulo = (from obj in articulos select new { Id = obj.Id, Nombre = obj.Nombre + " (" + obj.Codigo + ")", Codigo = obj.Codigo });
            return Json(articulo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Importar()
        {
            if (!ValidarUsuario(1, 4)) return RedirectToAction("ErrorPermisos", "Base");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Importar(HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    //string filePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads"), Path.GetFileName(upload.FileName));
                    string file = Path.GetFileName(upload.FileName);
                    //string path = WebConfigurationManager.AppSettings["CarpetaUpload"].ToString();
                    //string filePath = @"" + path + file;
                    string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/"), file);
                    //finalFileNameWithPath = string.Format("{0}.xlsx", currentDirectorypath);

                    upload.SaveAs(filePath);
                    DataSet ds = new DataSet();

                    string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";
                    //  string ConnectionString = WebConfigurationManager.AppSettings["ExcelConn"].ToString();

                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            string query = "SELECT * FROM [" + sheetName + "]";
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            adapter.Fill(ds, "Items");
                            if (ds.Tables.Count > 0)
                            {
                                List<Articulo> la = new List<Articulo>();
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        //TODO: POR SI HAY QUE ACTUALIZAR PRECIOS
                                        //var idArt = 0;
                                        var nombreArt = "";
                                        var precioCompra = 0m;
                                        var precioVenta = 0m;
                                        var rubroId = 0;
                                        var stockMax = 0;
                                        var stockMin = 0;
                                        var stockAct = 0;
                                        var stockSuc1 = 0;
                                        var stockSuc2 = 0;

                                        //try { idProd = int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()); }
                                        //catch { }
                                        try { nombreArt = ds.Tables[0].Rows[i].ItemArray[0].ToString(); }
                                        catch { }
                                        try { precioCompra = decimal.Parse(ds.Tables[0].Rows[i].ItemArray[1].ToString()); }
                                        catch { }
                                        try { precioVenta = decimal.Parse(ds.Tables[0].Rows[i].ItemArray[2].ToString()); }
                                        catch { }
                                        try { rubroId = int.Parse(ds.Tables[0].Rows[i].ItemArray[3].ToString()); }
                                        catch { }
                                        try { stockMax = int.Parse(ds.Tables[0].Rows[i].ItemArray[4].ToString()); }
                                        catch { }
                                        try { stockMin = int.Parse(ds.Tables[0].Rows[i].ItemArray[5].ToString()); }
                                        catch { }
                                        try { stockAct = int.Parse(ds.Tables[0].Rows[i].ItemArray[6].ToString()); }
                                        catch { }
                                        try { stockSuc1 = int.Parse(ds.Tables[0].Rows[i].ItemArray[7].ToString()); }
                                        catch { }
                                        try { stockSuc2 = int.Parse(ds.Tables[0].Rows[i].ItemArray[8].ToString()); }
                                        catch { }

                                        Articulo a = new Articulo();
                                        a.Nombre = nombreArt;
                                        //Lleno el Nombre etiqueta. Si es necesario lo corto
                                        var LongitudNombreEtiqueta = _configuracionServicios.GetLongitudNombreEtiqueta();
                                        var LongitudNombre = a.Nombre.Length;
                                        if (LongitudNombre > LongitudNombreEtiqueta)
                                            a.NombreEtiqueta = a.Nombre.Remove(LongitudNombreEtiqueta, LongitudNombre - LongitudNombreEtiqueta);
                                        else
                                            a.NombreEtiqueta = a.Nombre;
                                        a.PrecioActualCompra = precioCompra;
                                        a.PrecioActualVenta = precioVenta;
                                        a.RubroID = rubroId;
                                        a.StockMaximo = stockMax;
                                        a.StockMinimo = stockMin;
                                        a.Habilitado = true;
                                        //TODO (Agregar sucursales dinámicamente)
                                        //Esto es Harcodeo del bueno!! Ssssabor! Azúca!
                                        a.Stock = new List<StockArticuloSucursal>();
                                        //Stock deposito
                                        a.Stock = AsignoStockASucursales(stockAct, stockSuc1, stockSuc2);
                                        la.Add(a);
                                    }
                                }
                                _articulosServicios.ImportarArticulos(la);
                                ViewBag.Informacion = "Los articulos se han importado correctamente!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al intentar importar los precios. Revise el archivo .xlsx" + ex;
            }
            return View();
        }

        private List<StockArticuloSucursal> AsignoStockASucursales(int stockAct, int stockSuc1, int stockSuc2)
        {
            List<StockArticuloSucursal> lista = new List<StockArticuloSucursal>();
            StockArticuloSucursal deposito = new StockArticuloSucursal();
            deposito.StockActual = (stockAct - stockSuc1 - stockSuc2);
            deposito.SucursalID = 1;
            lista.Add(deposito);
            StockArticuloSucursal Suc1 = new StockArticuloSucursal();
            Suc1.StockActual = (stockSuc1);
            Suc1.SucursalID = 2;
            lista.Add(Suc1);
            StockArticuloSucursal Suc2 = new StockArticuloSucursal();
            Suc2.StockActual = (stockSuc2);
            Suc2.SucursalID = 3;
            lista.Add(Suc2);
            return lista;
        }

        public ActionResult Exportar()
        {
            if (!ValidarUsuario(1, 4)) return RedirectToAction("ErrorPermisos", "Base");

            var listRubro = _rubrosServicios.GetAll();
            var pathFile = FuncionesComunes.GenerateExcel(FuncionesComunes.ListToDataTable<Rubro>(listRubro), "ImportarArticulos", "Articulos");

            if (!string.IsNullOrEmpty(pathFile))
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;
                FuncionesComunes.ShowFile(pathFile, response);
            }

            return RedirectToAction("Importar");
        }

        public ActionResult ExportarReporte(ReporteViewModel rVM)
        {
            var sucursales = _sucServicios.GetAll();
            var articulos = new List<StockArticuloSucursal>();
            var cabecera = "";
            var stockCabecera = "";
            if (rVM.StockCero)
            {
                stockCabecera = " (Incluidos art. sin stock.)";
            }
            else
            {
                stockCabecera = " (Sólo art. con stock disponible.)";
            }

            var suc = "";
            foreach (var s in rVM.Sucursales)
            {
                s.Sucursal = sucursales.Where(a => a.Id == s.Sucursal.Id).First();
                if (s.Checked)
                {
                    suc = suc + s.Sucursal.Nombre + " | ";
                    var artSucursal = new List<StockArticuloSucursal>();
                    artSucursal = _stockArticuloSucursalServicios.GetBySucursal(s.Sucursal.Id, rVM.StockCero);
                    articulos.AddRange(artSucursal);
                }
            }

            rVM.CabeceraReporte = cabecera + suc + stockCabecera;
            rVM.Articulos = articulos;

            var pathFile = FuncionesComunes.GenerateExcelReporteArticulos(FuncionesComunes.ListToDataTable<StockArticuloSucursal>(rVM.Articulos), "StockArticulos", rVM.CabeceraReporte);

            if (!string.IsNullOrEmpty(pathFile))
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;
                FuncionesComunes.ShowFile(pathFile, response);
            }

            return View("Reporte", rVM);
            //  return View(rVM);
        }

        public ActionResult GenerarEtiquetas(int id)
        {
            if (!ValidarUsuario(1, 4)) return RedirectToAction("ErrorPermisos", "Base");

            var ArticuloVM = new ArticuloGenerarEtiquetasViewModel(_articulosServicios.GetOne(id));

            return View(ArticuloVM);
        }

        public ActionResult Etiqueta(int id)
        {
            //A partir del id genero 12 números para luego crear el EAN-13
            string codigo = id.ToString();
            var cantCeros = 11 - id.ToString().Count();
            for (int i = 0; i < cantCeros; i++)
            {
                codigo = "0" + codigo;
            }
            codigo = "1" + codigo;

            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
            {
                IncludeLabel = false,
                Alignment = AlignmentPositions.CENTER,
                Width = 300,
                Height = 120,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };

            System.Drawing.Image img = barcode.Encode(TYPE.EAN13, codigo); //Cuando le ponemos muchos nros toma los 12 primeros.

            using (var streak = new MemoryStream())
            {
                img.Save(streak, ImageFormat.Png);

                return File(streak.ToArray(), "image/png");
            }
        }

        public ActionResult Reporte()
        {
            var sucursales = _sucServicios.GetAll();
            ReporteViewModel rVM = new ReporteViewModel();
            List<SucursalReporteViewModel> lista = new List<SucursalReporteViewModel>();
            foreach (var s in sucursales)
            {
                SucursalReporteViewModel srVM = new SucursalReporteViewModel();
                srVM.Sucursal = s;
                srVM.Checked = false;
                lista.Add(srVM);
            }
            rVM.Sucursales = lista;
            rVM.StockCero = false;
            return View(rVM);
        }

        [HttpPost]
        public ActionResult Reporte(ReporteViewModel rVM)
        {
            var sucursales = _sucServicios.GetAll();
            var articulos = new List<StockArticuloSucursal>();
            var cabecera = "";
            var stockCabecera = "";
            if (rVM.StockCero)
            {
                stockCabecera = " (Incluidos art. sin stock.)";
            }
            else
            {
                stockCabecera = " (Sólo art. con stock disponible.)";
            }

            var suc = "";
            foreach (var s in rVM.Sucursales)
            {
                s.Sucursal = sucursales.Where(a => a.Id == s.Sucursal.Id).First();
                if (s.Checked)
                {
                    suc = suc + s.Sucursal.Nombre + " | ";
                    var artSucursal = new List<StockArticuloSucursal>();
                    artSucursal = _stockArticuloSucursalServicios.GetBySucursal(s.Sucursal.Id, rVM.StockCero);
                    articulos.AddRange(artSucursal);
                }
            }

            rVM.CabeceraReporte = cabecera + suc + stockCabecera;
            rVM.Articulos = articulos;
            return View("ReportePrint", rVM);
            //  return View(rVM);
        }

        [HttpPost]
        public ActionResult ReportePrint(ReporteViewModel rVM)
        {
            return View(rVM);
        }

        public List<Articulo> GetArticulosByName(string name)
        {
            var art = _articulosServicios.GetAll().Where(a => a.Nombre.ToLower().Contains(name)).ToList();
            return art;
        }
    }
}