using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;

namespace Servicios
{
    public class PagosServicios
    {
        private PagosRepositorio _pagosRepositorio;

        public PagosServicios()
        {
            _pagosRepositorio = new PagosRepositorio();
        }

        public List<Pago> GetAll()
        {
            return _pagosRepositorio.GetAll();
        }

        public List<Pago> GetAllBySucursalRangoFechas(int sucID, DateTime fechaDesde, DateTime fechaHasta)
        {
            return _pagosRepositorio.GetAllBySucursalRangoFechas(sucID, fechaDesde, fechaHasta);
        }

        public Pago GetOne(int id)
        {
            return _pagosRepositorio.GetOne(id);
        }

        public bool Add(Pago pago)
        {
            return _pagosRepositorio.Add(pago);
        }

        public bool Delete(Pago pago)
        {
            return _pagosRepositorio.Delete(pago);
        }

        public bool Update(Pago pago)
        {
            return _pagosRepositorio.Update(pago);
        }
    }
}
