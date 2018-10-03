using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class VentasRepositorio : Base
    {
        public VentasRepositorio()
            : base()
        {
        }

        public List<Venta> GetAll()
        {
            return _applicationDbContext.Ventas.ToList();
        }

        public Venta GetOne(int id)
        {
            return _applicationDbContext.Ventas.Include(a => a.Cliente).Where(a => a.Id == id).FirstOrDefault();
        }

        public bool Add(Venta venta)
        {
            try
            {
                foreach (var item in venta.VentaItem)
                {
                    item.Articulo = _applicationDbContext.Articulos.Find(item.ArticuloID);
                }
                foreach (var pago in venta.Pagos)
                {
                    pago.FormaDePago = _applicationDbContext.FormasDePago.Find(pago.FormaDePagoID);
                }
                _applicationDbContext.Ventas.Add(venta);
                Guardar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Venta venta)
        {
            try
            {
                _applicationDbContext.Ventas.Remove(venta);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Venta venta)
        {
            try
            {
                _applicationDbContext.Entry(venta).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<Venta> GetByDate(DateTime desde, DateTime hasta)
        {
            var d = desde.Date;
            var h = hasta.Date.AddDays(1);
            return _applicationDbContext.Ventas.Where(a => a.FechaVenta >= d && a.FechaVenta <= h).ToList();
        }

        public List<Venta> GetByTurno(DateTime desde, DateTime hasta)
        {
            return _applicationDbContext.Ventas.Where(a => a.FechaVenta >= desde && a.FechaVenta <= hasta).ToList();
        }
    }
}
