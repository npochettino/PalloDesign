using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class TipoMovimientosStockServicios
    {
        private TipoMovimientosStockRepositorio _tipoMovimientosStockRepositorio;

        public TipoMovimientosStockServicios()
        {
            _tipoMovimientosStockRepositorio = new TipoMovimientosStockRepositorio();
        }

        public List<TipoMovimientoStock> GetAll()
        {
            return _tipoMovimientosStockRepositorio.GetAll();
        }
    }
}
