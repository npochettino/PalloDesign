namespace Modelos
{
    public class UsuarioRol : Base
    {
        public int SucursalID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual Rol Rol { get; set; }
        public int RolID { get; set; }
        public int UsuarioID { get; set; }

    }
}
