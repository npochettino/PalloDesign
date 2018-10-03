using Modelos;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositorio
{

    public class TurnosRepositorio : Base
    {
        public TurnosRepositorio()
                : base()
        {
        }

        public List<Turno> GetAll()
        {
            return _applicationDbContext.Turnos.ToList();
        }
    }
}
