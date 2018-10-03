using System.Collections.Generic;

namespace Modelos
{
    public class Rubro : Base
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Habilitado { get; set; }

        public virtual List<Articulo> Articulos { get; set; }
    }
}
