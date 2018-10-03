using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Articulos
{
    public class ArticuloIndexViewModel
    {
        public ArticuloIndexViewModel()
        {
            Articulos = new List<Articulo>();
            Sucursales = new List<Sucursal>();
        }
        public string Filtro { get; set; }
        public List<Articulo> Articulos { get; set; }
        public List<Sucursal> Sucursales { get; set; }
    }
}