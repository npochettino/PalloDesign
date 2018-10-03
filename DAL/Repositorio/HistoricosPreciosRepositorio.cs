using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class HistoricosPreciosRepositorio : Base
    {
        public HistoricosPreciosRepositorio()
            : base()
        {
        }

        public List<HistoricoPrecio> GetAll()
        {
            return _applicationDbContext.HistoricosPrecios.ToList();
        }

        public HistoricoPrecio GetOne(int id)
        {
            return _applicationDbContext.HistoricosPrecios.Find(id);
        }

        public bool Add(HistoricoPrecio historicoPrecio)
        {
            try
            {
                _applicationDbContext.Entry(historicoPrecio).State = EntityState.Added;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}