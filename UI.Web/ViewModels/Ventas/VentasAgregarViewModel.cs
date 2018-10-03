using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Ventas
{
    public class VentasAgregarViewModel
    {
        public Int64 ArticuloIdAgregar { get; set; }

        public List<VentaItem> Items { get; set; }

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

        [Display(Name = "Saldo a Favor: ")]
        public decimal SaldoAFavor { get; set; }

        public VentasAgregarViewModel()
        {
            Items = new List<VentaItem>();     
        }
      
    }
}