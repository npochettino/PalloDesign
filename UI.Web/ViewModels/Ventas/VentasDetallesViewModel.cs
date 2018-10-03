using Modelos;
using System.Collections.Generic;
using System.Linq;

namespace UI.Web.ViewModels.Ventas
{
    public class VentasDetallesViewModel
    {
        public List<VentaItem> Items { get; set; }

        public int VentaID { get; set; }

        public Cliente Cliente { get; set; }
        public int ClienteID { get; set; }

        public List<Pago> Pagos { get; set; }

        public decimal TotalVentaItems
        {
            get
            {
                var total = 0m;
                foreach (var item in Items)
                {
                    total += item.TotalItem;
                }

                return total;
            }
        }

        public decimal Total
        {
            get
            {            
                return Pagos.Sum(a=>a.Monto);
            }
        }

        public VentasDetallesViewModel(Venta venta)
        {
            Items = venta.VentaItem;
            Cliente = venta.Cliente;
            Pagos = venta.Pagos;
            VentaID = venta.Id;
        }
    }
}