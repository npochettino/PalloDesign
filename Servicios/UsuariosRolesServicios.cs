using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class UsuariosRolesServicios
    {
        private UsuariosRolesRepositorio _usuariosRolesRepositorio;
        private UsuariosRepositorio _usuariosRepositorios;

        public UsuariosRolesServicios()
        {
            _usuariosRolesRepositorio = new UsuariosRolesRepositorio();
            _usuariosRepositorios = new UsuariosRepositorio();
        }

        public List<UsuarioRol> GetAll()
        {
            return _usuariosRolesRepositorio.GetAll();
        }

        public List<UsuarioRol> GetAllByUsuario(int idUsuario)
        {
            return _usuariosRolesRepositorio.GetAllByUsuario(idUsuario);
        }

        public UsuarioRol GetOne(int id)
        {
            return _usuariosRolesRepositorio.GetOne(id);
        }

        public bool Add(UsuarioRol usuarioRol)
        {
            return _usuariosRolesRepositorio.Add(usuarioRol);
        }

        public List<Usuario> GetUsuariosByRolYSucursal(int sucID, int rolID)
        {
            List<Usuario> Usuarios = new List<Usuario>();
            var UsuariosRoles = _usuariosRolesRepositorio.GetAll();

            foreach (var usuarioRol in UsuariosRoles)
            {
                if (usuarioRol.SucursalID == sucID && usuarioRol.RolID == rolID)
                    Usuarios.Add(_usuariosRepositorios.GetOne(usuarioRol.UsuarioID));
                
                    
            }

            return Usuarios;

        }

        public bool Delete(UsuarioRol usuarioRol)
        {
            return _usuariosRolesRepositorio.Delete(usuarioRol);
        }
        
    }
}
