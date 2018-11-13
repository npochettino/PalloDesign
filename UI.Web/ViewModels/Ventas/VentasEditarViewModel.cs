using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Web.ViewModels.Ventas
{
    public class VentasEditarViewModel
    {
        public VentasEditarViewModel()
        {
            Pagos = new List<Pago>();
        }

        public VentasEditarViewModel(Venta venta)
        {
            Id = venta.Id;
            Items = venta.VentaItem;
            Cliente = venta.Cliente;
            Pagos = venta.Pagos;
            VentaID = venta.Id;
            FechaVenta = venta.FechaVenta;
            SucursalID = venta.SucursalID;
        }
        public int Id { get; set; }

        public int FormaDePagoID { get; set; }
        public SelectList FormasDePago { get; set; }

        public int SucursalID { get; set; }  

        public DateTime FechaVenta { get; set; }

        public List<VentaItem> Items { get; set; }

        public int VentaID { get; set; }

        public Cliente Cliente { get; set; }
        public int ClienteID { get; set; }

        public List<Pago> Pagos { get; set; }

        public decimal TotalVentaItems { get; set; }

        public decimal Total { get; set; }
        
    }
}