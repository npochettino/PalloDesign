using System.Collections.Generic;

namespace Modelos
{
    public class Sucursal:Base
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual List<StockArticuloSucursal> Stock { get; set; }

        public virtual List<UsuarioRol> Usuarios { get; set; }
    }
}
