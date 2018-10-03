using Modelos;
using System.Collections.Generic;

namespace UI.Web.ViewModels.Sucursales
{
    public class SucursalSeleccionarViewModel
    {
        public List<Sucursal> Sucursales { get; set; }
        public int SucursalIDAgregar { get; set; }

        public SucursalSeleccionarViewModel()
        {
            Sucursales = new List<Sucursal>();
        }
    }
}