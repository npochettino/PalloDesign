using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class CategoriasTiposMovimientosEfectivoRepositorio:Base
    {
        public CategoriasTiposMovimientosEfectivoRepositorio()
            : base()
        {
        }

        public List<CategoriaMovimientoEfectivo> GetAll()
        {
            return _applicationDbContext.CategoriasMovimientoEfectivo.ToList();
        }

        public CategoriaMovimientoEfectivo GetOne(int id)
        {
            return _applicationDbContext.CategoriasMovimientoEfectivo.Find(id);
        }
    }
}
