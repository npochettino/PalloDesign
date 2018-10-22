using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelos
{
    public class Venta : Base
    {
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public bool Anulado { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Vendedor { get; set; }

        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual List<Pago> Pagos { get; set; }
        public virtual List<VentaItem> VentaItem { get; set; }

        public int SucursalID { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public decimal CantidadItems
        {
            get
            {
                decimal total = 0;
                try
                {
                    total = VentaItem.Sum(a => a.Cantidad);
                }
                catch { }

                return total;
            }
        }

        public decimal TotalVentasItem
        {
            get
            {
                return VentaItem.Sum(a => a.TotalItem);
            }
        }

        public decimal Total
        {
            get
            {
                return Pagos.Sum(a => a.Monto);
            }
        }
    }
}
