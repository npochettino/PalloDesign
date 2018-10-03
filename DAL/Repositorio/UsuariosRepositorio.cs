using Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class UsuariosRepositorio : Base
    {
        public UsuariosRepositorio()
            : base()
        {
        }

        public List<Usuario> GetAllHabilitados()
        {
            return _applicationDbContext.Usuarios.
                Where(a => a.Habilitado == true).
                OrderBy(c => c.Apellido).ToList();
        }

        public Usuario GetOne(int id)
        {
            return _applicationDbContext.Usuarios.Find(id);
        }

        public bool Add(Usuario usuario)
        {
            try
            {
                _applicationDbContext.Entry(usuario).State = EntityState.Added;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Usuario> GetAll()
        {
            return _applicationDbContext.Usuarios.
                Where(x => x.Habilitado == true).
                OrderBy(c => c.Apellido).ToList();
        }

        public bool Delete(Usuario usuario)
        {
            try
            {
                //Lo deshabilito al usuario. No lo elimino.
                _applicationDbContext.Entry(usuario).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<UsuarioRol> GetSucursalesByUsuario(int usuarioID)
        {
            return _applicationDbContext.UsuariosRoles.Where(a => a.UsuarioID == usuarioID).ToList();
        }

        public bool Update(Usuario usuario)
        {
            try
            {
                _applicationDbContext.Entry(usuario).State = EntityState.Modified;
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
