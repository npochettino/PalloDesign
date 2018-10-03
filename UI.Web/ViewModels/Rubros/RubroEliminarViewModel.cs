using Modelos;

namespace UI.Web.ViewModels.Rubros
{
    public class RubroEliminarViewModel
    {
        public RubroEliminarViewModel()
        {
        }

        public RubroEliminarViewModel(Rubro rubro)
        {
            Id = rubro.Id;
            Nombre = rubro.Nombre;
            Descripcion = rubro.Descripcion;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}