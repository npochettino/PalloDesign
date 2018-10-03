using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Rubros

{
    public class RubroIndexViewModel
    {
        public RubroIndexViewModel()
        {
            Rubros = new List<Rubro>();
        }

        public List<Rubro> Rubros { get; set; }

    }
}