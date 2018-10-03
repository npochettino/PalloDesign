using System;

namespace Modelos
{
    public class DevolucionSinTicket:Base
    {
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public bool RegresaAlStock { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }

        public int SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }
    }
}
