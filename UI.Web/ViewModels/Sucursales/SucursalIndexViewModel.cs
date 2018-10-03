using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Sucursales

{
    public class SucursalIndexViewModel
    {
        public SucursalIndexViewModel()
        {
            Sucursales = new List<Sucursal>();
        }

        public List<Sucursal> Sucursales { get; set; }

    }
}