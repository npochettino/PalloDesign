using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;
using System;

namespace DAL.Repositorio
{
    public class StockArticuloSucursalRepositorio : Base
    {
        public StockArticuloSucursalRepositorio()
            : base()
        {
        }

        public List<StockArticuloSucursal> GetAll()
        {
            return _applicationDbContext.StocksArticuloSucursal.ToList();
        }

        public StockArticuloSucursal GetOne(int id)
        {
            return _applicationDbContext.StocksArticuloSucursal.Find(id);
        }

        public bool Add(StockArticuloSucursal stockArticuloSucursal)
        {
            try
            {
                _applicationDbContext.StocksArticuloSucursal.Add(stockArticuloSucursal);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(StockArticuloSucursal stockArticuloSucursal)
        {
            try
            {
                _applicationDbContext.Entry(stockArticuloSucursal).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<StockArticuloSucursal> GetAllBySucursal(int sucursalID)
        {
            return _applicationDbContext.StocksArticuloSucursal.Include(a => a.Articulo).Where(b => b.SucursalID == sucursalID).ToList();
        }

        public bool DescontarStock(Venta venta)
        {
            try
            {
                var stockItems = _applicationDbContext.StocksArticuloSucursal.Where(a => a.SucursalID == venta.SucursalID);
                foreach (var item in venta.VentaItem)
                {
                    var stock = stockItems.Where(a => a.ArticuloID == item.ArticuloID).FirstOrDefault();
                    if (stock.StockActual >= item.Cantidad)
                    {
                        stock.StockActual = stock.StockActual - item.Cantidad;
                    }
                }
                Guardar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public StockArticuloSucursal GetOneBySucursal(Int64 articuloID, int sucursalID)
        {
            return _applicationDbContext.StocksArticuloSucursal.Include(a => a.Articulo).Where(b => b.SucursalID == sucursalID && b.ArticuloID == articuloID).FirstOrDefault();
        }
    }
}
