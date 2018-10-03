using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class VentasServicios
    {
        private VentasRepositorio _ventasRepositorio;

        public VentasServicios()
        {
            _ventasRepositorio = new VentasRepositorio();
        }

        public List<Venta> GetAll()
        {
            return _ventasRepositorio.GetAll();
        }

        public Venta GetOne(int id)
        {
            return _ventasRepositorio.GetOne(id);
        }

        public bool Add(Venta venta)
        {
            return _ventasRepositorio.Add(venta);
        }

        public bool Delete(Venta venta)
        {
            return _ventasRepositorio.Delete(venta);
        }

        public bool Update(Venta venta)
        {
            return _ventasRepositorio.Update(venta);
        }

        public List<Venta> GetByDate(DateTime desde, DateTime hasta)
        {
            return _ventasRepositorio.GetByDate(desde, hasta);
        }

        internal List<Venta> GetAllByDateAndSucursal(DateTime desde, DateTime hasta, int sucID)
        {
            return _ventasRepositorio.GetByTurno(desde, hasta).Where(a=>a.SucursalID == sucID).ToList();
        }

        public void ActualizarMontoDeVenta(int idVenta, decimal montoARestar)
        {
            var venta = this.GetOne(idVenta);

            venta.TotalVenta -= montoARestar;

            this.Update(venta);
        }
    }
}
