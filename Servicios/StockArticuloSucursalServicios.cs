using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;
using System;

namespace Servicios
{
    public class StockArticuloSucursalServicios
    {
        private StockArticuloSucursalRepositorio _stockArticuloSucursalRepositorio;

        public StockArticuloSucursalServicios()
        {
            _stockArticuloSucursalRepositorio = new StockArticuloSucursalRepositorio();
        }

        public List<StockArticuloSucursal> GetAll()
        {
            return _stockArticuloSucursalRepositorio.GetAll();
        }

        public StockArticuloSucursal GetOne(int id)
        {
            return _stockArticuloSucursalRepositorio.GetOne(id);
        }

        public bool Add(StockArticuloSucursal stockArticuloSucursal)
        {
            return _stockArticuloSucursalRepositorio.Add(stockArticuloSucursal);
        }

        public bool Update(StockArticuloSucursal stockArticuloSucursal)
        {
            return _stockArticuloSucursalRepositorio.Update(stockArticuloSucursal);
        }

        public List<StockArticuloSucursal> GetAllBySucursal(int sucursalID)
        {
            return _stockArticuloSucursalRepositorio.GetAllBySucursal(sucursalID);
        }

        public StockArticuloSucursal GetOneBySucursal(int ArticuloID, int SucursalID)
        {
            return _stockArticuloSucursalRepositorio.GetOneBySucursal(ArticuloID, SucursalID);
        }

        public bool DescontarStock(Venta venta)
        {
            return _stockArticuloSucursalRepositorio.DescontarStock(venta);
        }

        public decimal GetStock(Int64 articuloIdAgregar, int sucID)
        {
            try
            {
                var stock = _stockArticuloSucursalRepositorio.GetOneBySucursal(articuloIdAgregar, sucID);
                return stock.StockActual;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool DescontarStockDeposito(int art, decimal cant)
        {
            try
            {
                var artEnDeposito = GetOneBySucursal(art, 1);
                artEnDeposito.StockActual = artEnDeposito.StockActual - cant;
                if (Update(artEnDeposito))
                {
                    return true;
                }
                else
                { return false; }

            }
            catch (Exception ex)
            {
                return false;
            }
        }        

        public List<StockArticuloSucursal> GetBySucursal(int sucId, bool stockCero)
        {
            var lista = new List<StockArticuloSucursal>();
            lista = GetAllBySucursal(sucId);
            if (!stockCero)
            {
               
                var listaSinStock = lista.RemoveAll(a => a.StockActual < 1);
            }

            return lista;
        }
    }
}

