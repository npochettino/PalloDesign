using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Devoluciones
{
    public class DevolucionItemViewModel
    {
        public DevolucionItemViewModel()
        {
        }

        public DevolucionItemViewModel(int articuloID, int ventaItemID,string nombreArticulo, string codigoArticulo, decimal cantidadVendida, decimal precioDeVenta)
        {
            ArticuloID = articuloID;
            VentaItemID = ventaItemID;
            NombreArticulo = nombreArticulo;
            CodigoArticulo = codigoArticulo;
            CantidadVendida = cantidadVendida;
            PrecioDeVenta = precioDeVenta;
             
        }

        [Display(Name = "Cant. a Devolver")]
        [Range(0, 10000000000, ErrorMessage = "Debe ingresar un numero mayor o igual a cero")]
        public int CantidadADevolver { get; set; }

        public bool VuelveAlStock { get; set; }

        public int ArticuloID { get; set; }

        public int VentaItemID { get; set; }

        public string NombreArticulo { get; set; }

        public string CodigoArticulo { get; set; }

        public decimal CantidadVendida { get; set; }

        public decimal PrecioDeVenta { get; set; }

        public decimal Subtotal
        {
            get
            {
                return (CantidadVendida * PrecioDeVenta);
            }
            
        }

    }
}