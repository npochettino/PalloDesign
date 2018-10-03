using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Stock

{
    public class StockIndexViewModel
    {
        public StockIndexViewModel()
        {
            Articulos = new List<Articulo>();
            Sucursales = new List<Sucursal>();
            TotalesStock = new List<int>();
        }

        public List<Articulo> Articulos { get; set; }
        public List<int> TotalesStock { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public bool MostrarSoloAlertas { get; set; }

    }
}