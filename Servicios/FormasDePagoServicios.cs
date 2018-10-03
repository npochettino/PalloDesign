using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class FormasDePagoServicios
    {
        private FormasDePagoRepositorio _formasDePagoRepositorio;

        public FormasDePagoServicios()
        {
            _formasDePagoRepositorio = new FormasDePagoRepositorio();
        }

        public List<FormaDePago> GetAll()
        {
            return _formasDePagoRepositorio.GetAll();
        }

        public FormaDePago GetOne(int id)
        {
            return _formasDePagoRepositorio.GetOne(id);
        }

    }
}
