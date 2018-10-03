using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;
using System;

namespace Servicios
{
    public class MovimientosEfectivoServicios
    {
        private MovimientosEfectivoRepositorio _movimientosRepositorio;

        public MovimientosEfectivoServicios()
        {
            _movimientosRepositorio = new MovimientosEfectivoRepositorio();
        }

        public List<MovimientoEfectivo> GetAll()
        {
            return _movimientosRepositorio.GetAll();
        }

        public List<MovimientoEfectivo> GetSueldosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            return _movimientosRepositorio.GetSueldosByFecha(fechaDesde, fechaHasta);
        }

        public List<MovimientoEfectivo> GetAllBySucursal(int sucID)
        {
            return _movimientosRepositorio.GetAllBySucursal(sucID);
        }

        public List<MovimientoEfectivo> GetAllBySucursalRangoFecha(int sucID, DateTime fechaDesde, DateTime fechaHasta)
        {
            return _movimientosRepositorio.GetAllBySucursalRangoFecha(sucID, fechaDesde, fechaHasta);
        }

        public MovimientoEfectivo GetOne(int id)
        {
            return _movimientosRepositorio.GetOne(id);
        }

        public bool Add(MovimientoEfectivo movimiento)
        {
            return _movimientosRepositorio.Add(movimiento);
        }

        public bool Update(MovimientoEfectivo movimiento)
        {
            return _movimientosRepositorio.Update(movimiento);
        }

    }
}
