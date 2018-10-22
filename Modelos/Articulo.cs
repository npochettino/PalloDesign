using System.Collections.Generic;

namespace Modelos
{
    public class Articulo:Base
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreEtiqueta { get; set; }
        public decimal PrecioActualCompra { get; set; }
        public decimal PrecioActualVenta { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        public bool Habilitado { get; set; }

        public int RubroID { get; set; }
        public virtual Rubro Rubro { get; set; }

        public virtual List<StockArticuloSucursal> Stock { get; set; }
        
    }
}
