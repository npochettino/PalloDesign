using System.Collections.Generic;
using DAL.Repositorio;
using Modelos;

namespace Servicios
{
    public class TipoMovimientosEfectivoServicios
    {
        private TipoMovimientosEfectivoRepositorio _tipoMovimientosRepositorio;

        public TipoMovimientosEfectivoServicios()
        {
            _tipoMovimientosRepositorio = new TipoMovimientosEfectivoRepositorio();
        }

        public List<TipoMovimientoEfectivo> GetAll()
        {
            return _tipoMovimientosRepositorio.GetAll();
        }

        public TipoMovimientoEfectivo GetOne(int id)
        {
            return _tipoMovimientosRepositorio.GetOne(id);
        }

        public bool Add(TipoMovimientoEfectivo tipoMovimientoCaja)
        {
            return _tipoMovimientosRepositorio.Add(tipoMovimientoCaja);
        }

        public bool Delete(TipoMovimientoEfectivo tipoMovimientoCaja)
        {
            return _tipoMovimientosRepositorio.Delete(tipoMovimientoCaja);
        }

        public bool Update(TipoMovimientoEfectivo tipoMovimientoCaja)
        {
            return _tipoMovimientosRepositorio.Update(tipoMovimientoCaja);
        }
        
    }
}
