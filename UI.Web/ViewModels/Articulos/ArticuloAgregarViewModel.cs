using Foolproof;
using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Articulos

{
    public class ArticuloAgregarViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(50,ErrorMessage = "Longitud máxima de 50 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Precio de Compra")]
        [Required(ErrorMessage = "Precio de Compra es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioActualCompra { get; set; }

        [Display(Name = "Precio de Venta")]
        [Required(ErrorMessage = "Precio de Venta es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioActualVenta { get; set; }

        [Display(Name = "Stock Mínimo")]
        [Required(ErrorMessage = "Stock Mínimo es Requerido")]
        public int StockMinimo { get; set; }

        [Display(Name = "Stock Máximo")]
        [Required(ErrorMessage = "Stock Máximo es Requerido")]
        [GreaterThan("StockMinimo", ErrorMessage ="El Stock Máximo debe ser mayor al Stock Mínimo")]
        public int StockMaximo { get; set; }

        [Display(Name = "Stock Inicial")]
        [Required(ErrorMessage = "Stock Inicial es Requerido")]
        public int StockInicial { get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "Rubro es Requerido")]
        public int RubroID { get; set; }

        public bool Habilitado { get; set; }
        
        public Articulo Mapear()
        {
            Articulo Articulo = new Articulo();

            Articulo.Nombre = Nombre;
            Articulo.StockMinimo = StockMinimo;
            Articulo.StockMaximo = StockMaximo;
            Articulo.RubroID = RubroID;
            Articulo.Habilitado = true;
            Articulo.PrecioActualVenta = PrecioActualVenta;
            Articulo.PrecioActualCompra = PrecioActualCompra;

            return Articulo;
        }
    }
}