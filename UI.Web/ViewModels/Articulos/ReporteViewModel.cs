using Modelos;
using System.Collections.Generic;

namespace UI.Web.ViewModels.Articulos
{
    public class ReporteViewModel
    {
        public List<SucursalReporteViewModel> Sucursales { get; set; }      

        public bool StockCero { get; set; }

        public string CabeceraReporte { get; set; }

        public List<StockArticuloSucursal> Articulos { get; set; }

        public ReporteViewModel()
        {
            Sucursales = new List<SucursalReporteViewModel>();
            Articulos = new List<StockArticuloSucursal>();
        }


    }
}