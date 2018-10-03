using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;
using System;
using System.Linq;
using BarcodeLib;

namespace Servicios
{
    public class ArticulosServicios
    {
        private ArticulosRepositorio _articulosRepositorio;
        private StockArticuloSucursalRepositorio _stockArticuloSucursalRepositorio;
        private SucursalesRepositorio _sucursalRepositorio;
        private HistoricosPreciosServicios _historicosPreciosServicios;
        private ConfiguracionServicios _configuracionServicios;

        public ArticulosServicios()
        {
            _articulosRepositorio = new ArticulosRepositorio();
            _stockArticuloSucursalRepositorio = new StockArticuloSucursalRepositorio();
            _sucursalRepositorio = new SucursalesRepositorio();
            _historicosPreciosServicios = new HistoricosPreciosServicios();
            _configuracionServicios = new ConfiguracionServicios();
        }

        public List<Articulo> GetAll()
        {
            return _articulosRepositorio.GetAll();
        }

        public Articulo GetOne(int id)
        {
            return _articulosRepositorio.GetOne(id);
        }

        public bool Add(Articulo articulo)
        {
            return _articulosRepositorio.Add(articulo);
        }

        public bool ActualizarPrecioPorRubro(int rubroID, int porcentajeActualizacion)
        {
            decimal PorcentajeActualizacion = decimal.Parse(porcentajeActualizacion.ToString()) / 100;

            try
            {
                var ArticulosDelRubro = _articulosRepositorio.GetAllByRubro(rubroID);

                foreach (var articulo in ArticulosDelRubro)
                {
                    //Articulo
                    articulo.PrecioActualVenta *= (1 + PorcentajeActualizacion);
                    _articulosRepositorio.Update(articulo);

                    //Historico Precio Venta
                    HistoricoPrecio HistoricoPrecio = new HistoricoPrecio();
                    HistoricoPrecio.FechaDesde = DateTime.Now;
                    HistoricoPrecio.Precio = articulo.PrecioActualVenta;
                    HistoricoPrecio.ArticuloID = articulo.Id;
                    HistoricoPrecio.TipoHistoricoPrecioID = 2; //Venta
                    _historicosPreciosServicios.Add(HistoricoPrecio);
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(Articulo articulo)
        {
            return _articulosRepositorio.Delete(articulo);
        }

        public IQueryable<Articulo> GetByName(string term)
        {
            return GetAll().Where(a => a.Nombre.ToLower().Contains(term)).AsQueryable();
        }

        public IQueryable<Articulo> GetByNameOrCode(string term)
        {
            return GetAll().Where(a => a.Nombre.ToLower().Contains(term) || a.Codigo.ToString().Contains(term)).AsQueryable();
        }

        public bool Update(Articulo articulo)
        {
            return _articulosRepositorio.Update(articulo);
        }

        public bool Editar(Articulo articulo)
        {
            var ArticuloBD = _articulosRepositorio.GetOne(articulo.Id);
            if (articulo.PrecioActualCompra != ArticuloBD.PrecioActualCompra)
            {
                //Inserto un nuevo registro en HistoricoPrecio del tipo Compra
                HistoricoPrecio HistoricoPrecio = new HistoricoPrecio();
                HistoricoPrecio.ArticuloID = articulo.Id;
                HistoricoPrecio.FechaDesde = DateTime.Now;
                HistoricoPrecio.Precio = articulo.PrecioActualCompra;
                HistoricoPrecio.TipoHistoricoPrecioID = 1;
                _historicosPreciosServicios.Add(HistoricoPrecio);
            }
            if (articulo.PrecioActualVenta != ArticuloBD.PrecioActualVenta)
            {
                //Inserto un nuevo registro en HistoricoPrecio del tipo Venta
                HistoricoPrecio HistoricoPrecio = new HistoricoPrecio();
                HistoricoPrecio.ArticuloID = articulo.Id;
                HistoricoPrecio.FechaDesde = DateTime.Now;
                HistoricoPrecio.Precio = articulo.PrecioActualVenta;
                HistoricoPrecio.TipoHistoricoPrecioID = 2;
                _historicosPreciosServicios.Add(HistoricoPrecio);
            }
            var bandera = Update(articulo);
            if (bandera) return true;

            return false;
        }

        //Agrego el Artículo,  el stock en todas las sucursales y el HistoricoPrecio
        public bool Agregar(Articulo articulo, int StockInicial)
        {
            //Si NombreEtiqueta es NULL, lo lleno.
            if (articulo.NombreEtiqueta == null)
            {
                var LongitudNombreEtiqueta = _configuracionServicios.GetLongitudNombreEtiqueta();
                var LongitudNombre = articulo.Nombre.Length;
                if (LongitudNombre > LongitudNombreEtiqueta)
                    articulo.NombreEtiqueta = articulo.Nombre.Remove(LongitudNombreEtiqueta, LongitudNombre - LongitudNombreEtiqueta);
                else
                    articulo.NombreEtiqueta = articulo.Nombre;
            }
            if (Add(articulo))
            {
                AgregarStockArticuloSucursal(articulo.Id, StockInicial);
                AgregarHistoricosPrecios(articulo);
                AgregarCodigo(articulo);
                return true;
            }
            return false;
        }

        //El stock inicial lo agrego al deposito luego agrego Stock "cero" en las sucursales
        public void AgregarStockArticuloSucursal(int idArticulo, int StockInicial)
        {
            var cantidadSucursales = _sucursalRepositorio.Count();

            //Stock Deposito
            StockArticuloSucursal stockArticuloDeposito = new StockArticuloSucursal();
            stockArticuloDeposito.ArticuloID = idArticulo;
            stockArticuloDeposito.SucursalID = 1;
            stockArticuloDeposito.StockActual = StockInicial;
            _stockArticuloSucursalRepositorio.Add(stockArticuloDeposito);

            //Stock Sucursales (pongo cero) 
            for (int i = 2; i <= cantidadSucursales; i++)
            {
                StockArticuloSucursal stockArticuloSucursal = new StockArticuloSucursal();
                stockArticuloSucursal.ArticuloID = idArticulo;
                stockArticuloSucursal.SucursalID = i;
                stockArticuloSucursal.StockActual = 0;
                _stockArticuloSucursalRepositorio.Add(stockArticuloSucursal);
            }
        }

        public bool AgregarStockArticuloSucursal(Articulo art)
        {
            //Si el NombreEtiqueta es null, lo agrego...
            if (Add(art))
            {
                AgregarHistoricosPrecios(art);
                AgregarCodigo(art);
                return true;
            }
            return false;
        }

        //Agrego Precio de Compra y Precio de Venta al Histórico Precios
        public void AgregarHistoricosPrecios(Articulo articulo)
        {
            //Agrego Precio de Compra
            HistoricoPrecio HistoricoPrecioCompra = new HistoricoPrecio();
            HistoricoPrecioCompra.FechaDesde = DateTime.Now;
            HistoricoPrecioCompra.Precio = articulo.PrecioActualCompra;
            HistoricoPrecioCompra.ArticuloID = articulo.Id;
            HistoricoPrecioCompra.TipoHistoricoPrecioID = 1;
            _historicosPreciosServicios.Add(HistoricoPrecioCompra);

            //Agrego Precio de Venta
            HistoricoPrecio HistoricoPrecioVenta = new HistoricoPrecio();
            HistoricoPrecioVenta.FechaDesde = DateTime.Now;
            HistoricoPrecioVenta.Precio = articulo.PrecioActualVenta;
            HistoricoPrecioVenta.ArticuloID = articulo.Id;
            HistoricoPrecioVenta.TipoHistoricoPrecioID = 2;
            _historicosPreciosServicios.Add(HistoricoPrecioVenta);
        }

        //Completo el campo código
        private void AgregarCodigo(Articulo articulo)
        {
            //A partir del id genero 12 números para luego crear el EAN-13
            string codigo = articulo.Id.ToString();
            var cantCeros = 11 - articulo.Id.ToString().Count();
            for (int i = 0; i < cantCeros; i++)
            {
                codigo = "0" + codigo;
            }
            codigo = "1" + codigo;

            //Genero el codigo EAN13
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();

            barcode.Encode(TYPE.EAN13, codigo);

            articulo.Codigo = barcode.RawData;

            //Insertar el codigo en la tabla Articulos
            Editar(articulo);

        }

        //Deshabilito el artículo
        public bool Eliminar(int idArticulo)
        {
            var Articulo = _articulosRepositorio.GetOne(idArticulo);
            Articulo.Habilitado = false;
            if (_articulosRepositorio.Update(Articulo))
                return true;
            return false;
        }

        public void ImportarArticulos(List<Articulo> la)
        {
            if (la.Count > 0)
            {
                foreach (var a in la)
                {
                    if (a.Nombre.Trim() != "" && a.RubroID != 0)
                    {
                        AgregarStockArticuloSucursal(a);
                    }
                }
            }
        }

       
    }
}
