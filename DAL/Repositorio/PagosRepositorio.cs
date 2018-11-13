using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class PagosRepositorio : Base
    {
        public PagosRepositorio()
            : base()
        {
        }

        public List<Pago> GetAll()
        {
            return _applicationDbContext.Pagos.ToList();
        }

        public List<Pago> GetAllBySucursalRangoFechas(int sucID, DateTime fechaDesde, DateTime fechaHasta)
        {
            return _applicationDbContext.Pagos.Where(x => x.Venta.SucursalID == sucID && x.Venta.FechaVenta >= fechaDesde && x.Venta.FechaVenta <= fechaHasta).ToList();
        }

        public Pago GetOne(int id)
        {
            return _applicationDbContext.Pagos.Find(id);
        }

        public bool Add(Pago pago)
        {
            try
            {
                _applicationDbContext.Pagos.Add(pago);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Pago pago)
        {
            try
            {
                _applicationDbContext.Pagos.Remove(pago);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Pago pago)
        {
            try
            {
                //_applicationDbContext.Entry(pago).State = EntityState.Modified;
                var PagoBD = GetOne(pago.Id);
                PagoBD.Id = pago.Id;
                PagoBD.Monto = pago.Monto;
                PagoBD.FormaDePagoID = pago.FormaDePagoID;
                PagoBD.VentaID = pago.VentaID;
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
