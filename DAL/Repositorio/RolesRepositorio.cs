using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class RolesRepositorio : Base
    {
        public RolesRepositorio()
            : base()
        {
        }

        public List<Rol> GetAll()
        {
            return _applicationDbContext.Roles.ToList();
        }

        public Rol GetOne(int id)
        {
            return _applicationDbContext.Roles.Find(id);
        }
        
    }
}
