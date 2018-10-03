using Modelos;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositorio
{
    public class TipoMovimientosStockRepositorio : Base
    {
        public List<TipoMovimientoStock> GetAll()
        {
            return _applicationDbContext.TiposMovimientosStock.ToList();
        }
    }
}
