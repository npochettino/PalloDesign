using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class UsuariosRolesRepositorio : Base
    {
        public UsuariosRolesRepositorio()
            : base()
        {
        }

        public List<UsuarioRol> GetAll()
        {
            return _applicationDbContext.UsuariosRoles.ToList();
        }

        public List<UsuarioRol> GetAllByUsuario(int idUsuario)
        {
            return _applicationDbContext.UsuariosRoles.Where(a => a.UsuarioID == idUsuario).ToList();
        }

        public UsuarioRol GetOne(int id)
        {
            return _applicationDbContext.UsuariosRoles.Find(id);
        }

        public bool Add(UsuarioRol usuarioRol)
        {
            try
            {
                _applicationDbContext.UsuariosRoles.Add(usuarioRol);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(UsuarioRol usuarioRol)
        {
            try
            {
                _applicationDbContext.UsuariosRoles.Remove(usuarioRol);
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
