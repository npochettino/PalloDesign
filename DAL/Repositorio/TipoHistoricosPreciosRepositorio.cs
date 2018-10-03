using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class TipoHistoricosPreciosRepositorio : Base
    {
        public TipoHistoricosPreciosRepositorio()
            : base()
        {
        }

        public List<TipoHistoricoPrecio> GetAll()
        {
            return _applicationDbContext.TipoHistoricosPrecios.ToList();
        }

        public TipoHistoricoPrecio GetOne(int id)
        {
            return _applicationDbContext.TipoHistoricosPrecios.Find(id);
        }

        public bool Add(TipoHistoricoPrecio tipoHistoricoPrecio)
        {
            try
            {
                _applicationDbContext.TipoHistoricosPrecios.Add(tipoHistoricoPrecio);
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
