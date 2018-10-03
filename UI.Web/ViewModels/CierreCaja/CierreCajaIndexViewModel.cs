using System;

namespace UI.Web.ViewModels.CierreCaja
{
    public class CierreCajaIndexViewModel
    {
        public int CierreCajaID { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal TotalMañana { get; set; }
        public decimal TotalTarde { get; set; }
        public string Sucursal { get; set; }
        public string Usuario { get; set; }

        public decimal Total
        {
            get
            {
                return TotalMañana + TotalTarde;
            }
        }


        public CierreCajaIndexViewModel()
        {
           
        }
    }
}