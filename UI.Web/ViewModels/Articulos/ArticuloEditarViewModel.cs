using Modelos;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace UI.Web.ViewModels.Articulos
{
    public class ArticuloEditarViewModel
    {

        public ArticuloEditarViewModel()
        {
        }

        public ArticuloEditarViewModel(Articulo articulo)
        {
            Id = articulo.Id;
            Nombre = articulo.Nombre;
            NombreEtiqueta = articulo.NombreEtiqueta;
            Codigo = articulo.Codigo;
            StockMinimo = articulo.StockMinimo;
            StockMaximo = articulo.StockMaximo;
            RubroID = articulo.RubroID;
            PrecioActualVenta = articulo.PrecioActualVenta;
            PrecioActualCompra = articulo.PrecioActualCompra;

        }

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(50, ErrorMessage = "Longitud máxima de 50 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Etiqueta")]
        [Required(ErrorMessage = "Nombre Etiqueta es Requerido")]
        public string NombreEtiqueta { get; set; }

        public string Codigo { get; set; }

        [Display(Name = "Stock Mínimo")]
        [Required(ErrorMessage = "Stock Mínimo es Requerido")]
        public decimal StockMinimo { get; set; }

        [Display(Name = "Stock Máximo")]
        [Required(ErrorMessage = "Stock Máximo es Requerido")]
        [GreaterThan("StockMinimo", ErrorMessage = "El Stock Máximo debe ser mayor al Stock Mínimo")]
        public decimal StockMaximo { get; set; }

        [Display(Name = "Precio de Compra")]
        [Required(ErrorMessage = "Precio de Compra es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioActualCompra { get; set; }

        [Display(Name = "Precio de Venta")]
        [Required(ErrorMessage = "Precio de Venta es Requerido")]
        [Range(0.01, 100000000.00, ErrorMessage = "Debe ingresar un numero mayor a cero")]
        public decimal PrecioActualVenta { get; set; }

        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "Rubro es Requerido")]
        public int RubroID { get; set; }

        public bool Habilitado { get; set; }

        public Articulo Mapear()
        {
            Articulo Articulo = new Articulo();

            Articulo.Id = Id;
            Articulo.Nombre = Nombre;
            Articulo.NombreEtiqueta = NombreEtiqueta;
            Articulo.Codigo = Codigo;
            Articulo.StockMinimo = StockMinimo;
            Articulo.StockMaximo = StockMaximo;
            Articulo.PrecioActualCompra = PrecioActualCompra;
            Articulo.PrecioActualVenta = PrecioActualVenta;
            Articulo.RubroID = RubroID;
            Articulo.Habilitado = true;

            return Articulo;
        }


    }
}