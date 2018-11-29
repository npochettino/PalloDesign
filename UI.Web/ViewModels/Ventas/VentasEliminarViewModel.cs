using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Web.ViewModels.Ventas
{
    public class VentasEliminarViewModel
    {
        public VentasEliminarViewModel()
        {
            Pagos = new List<Pago>();
            Cliente = new Cliente();
            Items = new List<VentaItem>();
        }

        public List<VentaItem> Items { get; set; }

        public int Id { get; set; }
        public bool Anulado { get; set; }

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
                return Pagos.Sum(a => a.Monto);
            }
        }

        public VentasEliminarViewModel(Venta venta)
        {
            Items = venta.VentaItem;
            Cliente = venta.Cliente;
            Pagos = venta.Pagos;
            Id = venta.Id;
        }
    }
}