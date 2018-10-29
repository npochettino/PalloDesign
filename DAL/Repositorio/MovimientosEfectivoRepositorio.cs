using System.Collections.Generic;
using System.Linq;
using Modelos;
using System;

namespace DAL.Repositorio
{
    public class MovimientosEfectivoRepositorio : Base
    {
        public MovimientosEfectivoRepositorio()
            : base()
        {
        }

        public List<MovimientoEfectivo> GetAll()
        {
            return _applicationDbContext.MovimientosEfectivo.ToList();
        }

        public MovimientoEfectivo GetOne(int id)
        {
            return _applicationDbContext.MovimientosEfectivo.Find(id);
        }

        public List<MovimientoEfectivo> GetSueldosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            var desde = fechaDesde.Date;
            var hasta = fechaHasta.Date.AddDays(1);

            return _applicationDbContext.MovimientosEfectivo
                .Where(x => x.Descripcion.Contains("Sueldo") && x.Fecha >= desde && x.Fecha <= hasta)
                .ToList(); ;
        }

        public bool Add(MovimientoEfectivo movimiento)
        {
            try
            {
                _applicationDbContext.MovimientosEfectivo.Add(movimiento);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<MovimientoEfectivo> GetAllBySucursal(int sucID)
        {
            return _applicationDbContext.MovimientosEfectivo.Where(a => a.SucursalID == sucID).ToList();
        }

        public List<MovimientoEfectivo> GetAllBySucursalRangoFecha(int sucID, DateTime fechaDesde, DateTime fechaHasta)
        {
            return _applicationDbContext.MovimientosEfectivo.Where(x => x.SucursalID == sucID && x.Fecha >= fechaDesde && x.Fecha <= fechaHasta).ToList();
        }

        public bool Delete(MovimientoEfectivo movimientoEfectivo)
        {
            try
            {
                _applicationDbContext.MovimientosEfectivo.Remove(movimientoEfectivo);
                Guardar();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(MovimientoEfectivo movimiento)
        {
            try
            {
                var MovimientoBD = GetOne(movimiento.Id);
                MovimientoBD.Id = movimiento.Id;
                MovimientoBD.Descripcion = movimiento.Descripcion;
                MovimientoBD.Monto = movimiento.Monto;
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
