using Modelos;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Articulos
{
    public class ArticuloEliminarViewModel
    {
        public ArticuloEliminarViewModel()
        {
        }
        public ArticuloEliminarViewModel(Articulo articulo)
        {
            Id = articulo.Id;
            Nombre = articulo.Nombre;
            StockMinimo = articulo.StockMinimo;
            StockMaximo = articulo.StockMaximo;
            NombreRubro = articulo.Rubro.Nombre;
            
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        [Display(Name = "Rubro")]
        public string NombreRubro { get; set; }
        public bool Habilitado { get; set; }

    }
}