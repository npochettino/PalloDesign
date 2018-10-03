using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.CierreCaja
{
    public class CierreCajaAgregarViewModel
    {
        [Display(Name ="Fecha de Cierre")]
        public DateTime FechaCierre { get; set; }
        [Display(Name = "Turno")]
        public int TurnoID { get; set; }
        [Display(Name = "Total Caja")]
        public decimal TotalCaja { get; set; }
        public List<Modelos.CierreCaja> Cierres { get; set; }

        public CierreCajaAgregarViewModel()
        {
            Cierres = new List<Modelos.CierreCaja>();
        }
    }
}