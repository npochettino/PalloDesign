using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class TipoHistoricosPreciosSevicios
    {
        private TipoHistoricosPreciosRepositorio _tipoHistoricosPreciosRepositorio;

        public TipoHistoricosPreciosSevicios()
        {
            _tipoHistoricosPreciosRepositorio = new TipoHistoricosPreciosRepositorio();
        }

        public List<TipoHistoricoPrecio> GetAll()
        {
            return _tipoHistoricosPreciosRepositorio.GetAll();
        }

        public TipoHistoricoPrecio GetOne(int id)
        {
            return _tipoHistoricosPreciosRepositorio.GetOne(id);
        }

        public bool Add(TipoHistoricoPrecio tipoHistoricoPrecio)
        {
            return _tipoHistoricosPreciosRepositorio.Add(tipoHistoricoPrecio);
        }

    }
}
