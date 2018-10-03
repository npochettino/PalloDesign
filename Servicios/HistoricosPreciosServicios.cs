using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class HistoricosPreciosServicios
    {
        private HistoricosPreciosRepositorio _historicosPreciosRepositorio;

        public HistoricosPreciosServicios()
        {
            _historicosPreciosRepositorio = new HistoricosPreciosRepositorio();
        }

        public List<HistoricoPrecio> GetAll()
        {
            return _historicosPreciosRepositorio.GetAll();
        }

        public HistoricoPrecio GetOne(int id)
        {
            return _historicosPreciosRepositorio.GetOne(id);
        }

        public bool Add(HistoricoPrecio HistoricoPrecio)
        {
            return _historicosPreciosRepositorio.Add(HistoricoPrecio);
        }
        
    }
}
