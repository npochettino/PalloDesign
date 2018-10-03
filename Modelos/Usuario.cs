using System.Collections.Generic;

namespace Modelos
{
    public class Usuario : Base
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public bool Habilitado { get; set; }
        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }
       
        public virtual List<UsuarioRol> Roles { get; set; }

    }
}
