using Modelos;
using System.Collections.Generic;

namespace UI.Web.ViewModels.Stock
{

    public class LineaAsignarStockViewModel
    {
        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }
        public List<StockArticuloSucursalViewModel> StockArticuloSucursal { get; set; }

        public LineaAsignarStockViewModel()
        {
            StockArticuloSucursal = new List<StockArticuloSucursalViewModel>();
        }
    }
}