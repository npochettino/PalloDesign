using Modelos;

namespace UI.Web.ViewModels.Articulos
{
    public class ArticuloGenerarEtiquetasViewModel
    {
        public ArticuloGenerarEtiquetasViewModel()
        {
        }

        public ArticuloGenerarEtiquetasViewModel(Articulo articulo)
        {
            Id = articulo.Id;
            Codigo = articulo.Codigo;
            Nombre = articulo.Nombre;
            NombreEtiqueta = articulo.NombreEtiqueta;
            Precio = articulo.PrecioActualVenta;
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreEtiqueta { get; set; }
        public decimal Precio { get; set; }



        //[Display(Name = "Cantidad de Etiquetas")]
        //[Required(ErrorMessage = "Cantidad de Etiquetas es Requerido")]
        //[Range(1, 100, ErrorMessage = "Debe ingresar un numero entre 1 y 100")]
        //public int CantidadEtiquetas { get; set; }

    }
}