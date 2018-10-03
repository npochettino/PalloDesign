using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class TipoMovimientosEfectivoRepositorio:Base
    {
        public List<TipoMovimientoEfectivo> GetAll()
        {
            return _applicationDbContext.TiposMovimientosEfectivo.Where(a => a.Habilitado == true).ToList();
        }

        public TipoMovimientoEfectivo GetOne(int id)
        {
            return _applicationDbContext.TiposMovimientosEfectivo.Find(id);
        }

        public bool Add(TipoMovimientoEfectivo tipoMovimientoEfectivo)
        {
            try
            {
                _applicationDbContext.TiposMovimientosEfectivo.Add(tipoMovimientoEfectivo);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TipoMovimientoEfectivo tipoMovimientoEfectivo)
        {
            try
            {
                _applicationDbContext.TiposMovimientosEfectivo.Remove(tipoMovimientoEfectivo);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(TipoMovimientoEfectivo tipoMovimientoEfectivo)
        {
            try
            {
                _applicationDbContext.Entry(tipoMovimientoEfectivo).State = EntityState.Modified;
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
