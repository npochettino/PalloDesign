using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class StockMovimientosRepositorio : Base
    {
        public StockMovimientosRepositorio()
            : base()
        {
        }

        public List<StockMovimiento> GetAll()
        {
            return _applicationDbContext.StockMovimientos.ToList();
        }

        public List<StockMovimiento> GetAllByDate(int sucursalID, DateTime fechaDesde, DateTime fechaHasta)
        {
            var desde = fechaDesde.Date;
            var hasta = fechaHasta.Date.AddDays(1);
            
            return _applicationDbContext.StockMovimientos
                .Where(x => x.SucursalID == sucursalID && x.Fecha >= desde && x.Fecha <= hasta)
                .ToList();
        }

        public StockMovimiento GetOne(int id)
        {
            return _applicationDbContext.StockMovimientos.Find(id);
        }

        public bool Add(StockMovimiento stockMovimiento)
        {
            try
            {
                _applicationDbContext.StockMovimientos.Add(stockMovimiento);
                Guardar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
