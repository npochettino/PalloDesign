using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class UsuariosServicios
    {
        private UsuariosRepositorio _usuariosRepositorio;
        private UsuariosRolesServicios _usuariosRolesServicios;

        public UsuariosServicios()
        {
            _usuariosRepositorio = new UsuariosRepositorio();
            _usuariosRolesServicios = new UsuariosRolesServicios();
        }

        public List<Usuario> GetAll()
        {
            return _usuariosRepositorio.GetAll();
        }

        public List<Usuario> GetAllHabilitados()
        {
            return _usuariosRepositorio.GetAllHabilitados();
        }

        public bool Add(Usuario usuario)
        {
            return _usuariosRepositorio.Add(usuario);
        }

        public bool Delete(Usuario usuario)
        {
            return _usuariosRepositorio.Delete(usuario);
        }

        public bool Update(Usuario usuario)
        {
            return _usuariosRepositorio.Update(usuario);
        }

        public List<UsuarioRol> GetSucursalesByUsuario(int UsuarioID)
        {
            return _usuariosRepositorio.GetSucursalesByUsuario(UsuarioID);
        }

        public Usuario GetOne(int id)
        {
            return _usuariosRepositorio.GetOne(id);
        }

        public bool Agregar(Usuario usuario)
        {
            //Agregar el Usuario
            var RolesBKP = usuario.Roles;
            usuario.Roles = null;
            if(Add(usuario))
            {
                //Agrego los Roles del Usuario en la tabla UsuariosRoles
                foreach(UsuarioRol usuarioRol in RolesBKP)
                {
                    usuarioRol.UsuarioID = usuario.Id;
                    usuarioRol.RolID = usuarioRol.Rol.Id;
                    usuarioRol.SucursalID = usuarioRol.Sucursal.Id;
                    usuarioRol.Rol = null;
                    usuarioRol.Sucursal = null;
                    _usuariosRolesServicios.Add(usuarioRol);
                }
                return true;
            }
            return false;



        }

        public bool Editar(Usuario usuario)
        {
            //Preparo la Lista usuario.Roles!!!
            foreach(var rol in usuario.Roles)
            {
                rol.RolID = rol.Rol.Id;
                rol.SucursalID = rol.Sucursal.Id;
                rol.Rol = null;
                rol.Sucursal = null;
            }

            //Update de todos los roles del usuario
            //Traigo los Roles del usuario que tiene en la BD
            var RolesBD = _usuariosRolesServicios.GetAllByUsuario(usuario.Id);

            //Recorro la lista "nueva" y lo que no está en RolesBD lo inserto en la BD
            foreach(var rol in usuario.Roles)
            {
                if (!RolesBD.Exists(a => a.RolID == rol.RolID && a.SucursalID == rol.SucursalID))
                {
                    rol.UsuarioID = usuario.Id;
                    _usuariosRolesServicios.Add(rol);
                }

            }
            //Recorro RolesBD y lo que no está en la lista "nueva" lo elimino de la BD
            foreach(var rolBD in RolesBD)
            {
                if(!usuario.Roles.Exists(a => a.RolID == rolBD.RolID && a.SucursalID == rolBD.SucursalID))
                {
                    _usuariosRolesServicios.Delete(rolBD);
                }
            }

            //Update del Usuario
            usuario.Roles = null;
            if(Update(usuario)) return true;
            return false;
            
        }

        public bool Eliminar(Usuario usuario)
        {
            usuario.Habilitado = false;
            if (Delete(usuario)) return true;
            return false;
        }
    }
}
