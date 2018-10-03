using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class RolesServicios
    {
        private RolesRepositorio _rolesRepositorio;

        public RolesServicios()
        {
            _rolesRepositorio = new RolesRepositorio();
        }

        public List<Rol> GetAll()
        {
            return _rolesRepositorio.GetAll();
        }

        public Rol GetOne(int id)
        {
            return _rolesRepositorio.GetOne(id);
        }
    }
}

