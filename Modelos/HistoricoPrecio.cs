using System;

namespace Modelos
{
    public class HistoricoPrecio:Base
    {
        public DateTime FechaDesde { get; set; }
        public decimal Precio { get; set; }

        public int ArticuloID { get; set; }
        public Articulo Articulo { get; set; }

        public int TipoHistoricoPrecioID { get; set; }
        public TipoHistoricoPrecio TipoHistoricoPrecio { get; set; }

    }
}
