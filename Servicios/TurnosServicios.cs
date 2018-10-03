using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class TurnosServicios
    {
        private TurnosRepositorio _turnosServicios;

        public TurnosServicios()
        {
            _turnosServicios = new TurnosRepositorio();
            
        }
        public List<Turno> GetAll()
        {
            return _turnosServicios.GetAll();
        }
    }
}
