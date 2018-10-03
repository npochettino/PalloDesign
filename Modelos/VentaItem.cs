using System.ComponentModel;

namespace Modelos
{
    public class VentaItem:Base
    {
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [DefaultValue(0)]         
        public decimal Descuento { get; set; }

        [DefaultValue(false)]
        public bool Devuelto { get; set; }
        
        public int VentaID { get; set; }
        public virtual Venta Venta { get; set; }

        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }

        public decimal TotalItem
        {
            get
            {
                if (Descuento > 0)
                {
                    var total = Precio * Cantidad;
                    return (total - ((Descuento / 100m) * total));
                }
                else
                {
                    return Precio * Cantidad;
                }

            }
        }
    }
}
