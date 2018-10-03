using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.CierreCaja
{
    public class CierreCajaDetallesViewModel
    {
        [Display(Name = "Fecha de Cierre")]
        public DateTime FechaCierre { get; set; }  
        public string Sucursal { get; set; }     
        [Display(Name = "Total Caja")]
        public decimal TotalCaja { get; set; }
        public List<Modelos.CierreCaja> Cierres { get; set; }

        public CierreCajaDetallesViewModel()
        {
            Cierres = new List<Modelos.CierreCaja>();
        }
    }
}