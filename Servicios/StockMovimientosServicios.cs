using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class StockMovimientosServicios
    {
        private StockMovimientosRepositorio _stockMovimientosRepositorio;
        private TipoMovimientosStockServicios _tipoMovimientosStockServicios;
        private ArticulosServicios _articulosServicios;
        private StockArticuloSucursalServicios _stockArticuloSucursalServicios;

        public StockMovimientosServicios()
        {
            _stockMovimientosRepositorio = new StockMovimientosRepositorio();
            _articulosServicios = new ArticulosServicios();
            _stockArticuloSucursalServicios = new StockArticuloSucursalServicios();
            _tipoMovimientosStockServicios = new TipoMovimientosStockServicios();
        }

        public List<StockMovimiento> GetAll()
        {
            return _stockMovimientosRepositorio.GetAll();
        }

        public List<StockMovimiento> GetAllByDate(int sucursalID, DateTime fechaDesde, DateTime fechaHasta)
        {
            var Movimientos = _stockMovimientosRepositorio.GetAllByDate(sucursalID, fechaDesde, fechaHasta);

            //var StockLista = _stockArticuloSucursalServicios.GetAllBySucursal(sucursalID);

            //foreach(var mov in Movimientos)
            //{
            //    foreach(var stock in StockLista)
            //    {
            //        if (mov.ArticuloID == stock.ArticuloID && mov.SucursalID == stock.SucursalID)
            //        {
            //            mov.StockActual = stock.StockActual;
            //            break;
            //        }
            //    }
            //}
            
            return Movimientos;
        }

        public StockMovimiento GetOne(int id)
        {
            return _stockMovimientosRepositorio.GetOne(id);
        }

        public bool Add(StockMovimiento stockMovimiento)
        {
            return _stockMovimientosRepositorio.Add(stockMovimiento);
        }

        public bool Agregar(StockMovimiento stockMovimiento, int sucursalID)
        {
            //Si ModificarStock no falla agregamos el StockMovimiento
            var suma = _tipoMovimientosStockServicios.GetAll().Where(a => a.Id == stockMovimiento.TipoMovimientoStockID).FirstOrDefault().Suma;
            if (!suma)
            {
                stockMovimiento.Cantidad = (stockMovimiento.Cantidad * -1);
            }

            if (ModificarStock(stockMovimiento.ArticuloID, stockMovimiento.Cantidad, sucursalID))
            {
                stockMovimiento.SucursalID = sucursalID;
                var stockArticuloSucursal = _stockArticuloSucursalServicios.GetOneBySucursal(stockMovimiento.ArticuloID, sucursalID);
                stockMovimiento.StockActual = stockArticuloSucursal.StockActual;
                if (Add(stockMovimiento))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            return false;
        }

        private bool ModificarStock(int articuloID, int cantidad, int sucursalID)
        {
            try
            {
                var stockArticulo = _stockArticuloSucursalServicios.GetOneBySucursal(articuloID, sucursalID);
                //Valido que haya Stock suficiente
                //Se debe corregir para que la actualizacion de stock se haga sobre la tabla StockArticuloSucursal
                //if (cantidad < 0 && Articulo.StockActual <= (-cantidad)) return false;
                //Articulo.StockActual += cantidad;
                if (cantidad > 0)
                {
                    stockArticulo.StockActual = stockArticulo.StockActual + cantidad;
                    _stockArticuloSucursalServicios.Update(stockArticulo);
                    return true;
                }

                if (stockArticulo.StockActual >= cantidad)
                {
                    stockArticulo.StockActual = stockArticulo.StockActual + cantidad;
                    _stockArticuloSucursalServicios.Update(stockArticulo);
                    return true;
                }
                else
                {
                    return false;
                }              
            }
            catch
            {
                return false;
            }
        }

        public bool AgregarMovimientoVentas(List<VentaItem> ventaItem, int sucID, int usuID)
        {
            try
            {
                var tipos = _tipoMovimientosStockServicios.GetAll();
                var tipoID = tipos.Where(a => a.Nombre == "Venta").FirstOrDefault().Id;
                foreach (var item in ventaItem)
                {
                    StockMovimiento sm = new StockMovimiento();
                    sm.ArticuloID = item.ArticuloID;
                    sm.Cantidad = item.Cantidad * -1;
                    sm.Fecha = DateTime.Now;
                    sm.SucursalID = sucID;
                    sm.TipoMovimientoStockID = tipoID;
                    sm.UsuarioID = usuID;
                    var stockArticuloSucursal = _stockArticuloSucursalServicios.GetOneBySucursal(item.ArticuloID, sucID);
                    sm.StockActual = stockArticuloSucursal.StockActual;
                    if (!Add(sm))
                    {
                        return false;
                    }                    
                }

                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

    }
}
