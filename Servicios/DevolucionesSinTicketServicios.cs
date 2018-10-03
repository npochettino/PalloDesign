using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;

namespace Servicios
{
    public class DevolucionesSinTicketServicios
    {
        private DevolucionesSinTicketRepositorio _devolucionesSinTicketRepositorio;

        public DevolucionesSinTicketServicios()
        {
            _devolucionesSinTicketRepositorio = new DevolucionesSinTicketRepositorio();
        }

        public List<DevolucionSinTicket> GetAll()
        {
            return _devolucionesSinTicketRepositorio.GetAll();
        }

        public DevolucionSinTicket GetOne(int id)
        {
            return _devolucionesSinTicketRepositorio.GetOne(id);
        }

        public bool Add(DevolucionSinTicket devolucionSinTicket)
        {
            return _devolucionesSinTicketRepositorio.Add(devolucionSinTicket);
        }

        public bool Delete(DevolucionSinTicket devolucionSinTicket)
        {
            return _devolucionesSinTicketRepositorio.Delete(devolucionSinTicket);
        }

        public bool Update(DevolucionSinTicket devolucionSinTicket)
        {
            return _devolucionesSinTicketRepositorio.Update(devolucionSinTicket);
        }

        public List<DevolucionSinTicket> GetByDate(DateTime desde, DateTime hasta)
        {
            return _devolucionesSinTicketRepositorio.GetByDate(desde, hasta);
        }
    }
}

