using Modelos;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositorio
{
    public class ConfiguracionRepositorio : Base
    {
        public ConfiguracionRepositorio()
            : base()
        {
        }

        public List<Configuracion> GetAll()
        {
            return _applicationDbContext.Configuraciones.ToList();
        }
    }
}
