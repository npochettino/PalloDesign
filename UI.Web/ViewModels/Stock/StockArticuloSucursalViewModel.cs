using Modelos;

namespace UI.Web.ViewModels.Stock
{

    public class StockArticuloSucursalViewModel
    {
        public int ArticuloID { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public int SucursalID { get; set; }
        public int StockActual { get; set; }
        public int StockAgregar { get; set; }
    }
}