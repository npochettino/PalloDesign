using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace UI.Web.ViewModels.Ventas
{
    public class VentasAgregarPagoViewModel
    {
        [Display(Name = "Agregar Forma de Pago")]
        public int FormaDePagoID { get; set; }
        public SelectList FormasDePago { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Cliente es requerido")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Cliente es requerido")]
        public int ClienteID { get; set; }

        public int FormaPagoIDEliminar { get; set; }
        public List<Pago> Pagos { get; set; }

        public List<VentaItem> Items { get; set; }

        public VentasAgregarPagoViewModel()
        {
            Pagos = new List<Pago>();
            Items = new List<VentaItem>();
        }

        public decimal Total
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

        public Venta MapearVenta()
        {
            Venta v = new Venta();
            v.ClienteID = ClienteID;
            v.FechaVenta = DateTime.Now;
            try
            {
                //La forma de pago 4 (devolucion) no se suma porque ya fue sumada en otra venta anterior
                v.TotalVenta = Pagos.Where(a => a.FormaDePagoID != 4).Sum(a => a.MontoRecargo);
            }
            catch
            {
                v.TotalVenta = Total;
            }

            var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            v.UsuarioID = usuario.Id;
            foreach (var p in Pagos)
            {
                p.Monto = p.MontoRecargo;
            }
            v.Pagos = Pagos;
            v.VentaItem = Items;
            /*SACAR CUANDO ESTE ARREGLADO*/

            var sucursal = (int)System.Web.HttpContext.Current.Session["SucursalActual"];
            v.SucursalID = sucursal;
            return v;

        }
    }
}