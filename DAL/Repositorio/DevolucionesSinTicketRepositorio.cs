using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class DevolucionesSinTicketRepositorio : Base
    {
        public DevolucionesSinTicketRepositorio()
            : base()
        {
        }

        public List<DevolucionSinTicket> GetAll()
        {
            return _applicationDbContext.DevolucionesSinTicket.ToList();
        }

        public DevolucionSinTicket GetOne(int id)
        {
            return _applicationDbContext.DevolucionesSinTicket.Find(id);
        }

        public bool Add(DevolucionSinTicket devolucionSinTicket)
        {
            try
            {
                _applicationDbContext.DevolucionesSinTicket.Add(devolucionSinTicket);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(DevolucionSinTicket devolucionSinTicket)
        {
            try
            {
                _applicationDbContext.DevolucionesSinTicket.Remove(devolucionSinTicket);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(DevolucionSinTicket devolucionSinTicket)
        {
            try
            {
                _applicationDbContext.Entry(devolucionSinTicket).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<DevolucionSinTicket> GetByDate(DateTime desde, DateTime hasta)
        {
            var d = desde.Date;
            var h = hasta.Date.AddDays(1);
            return _applicationDbContext.DevolucionesSinTicket.Where(a => a.Fecha >= d && a.Fecha <= h).ToList();
        }
    }
}

