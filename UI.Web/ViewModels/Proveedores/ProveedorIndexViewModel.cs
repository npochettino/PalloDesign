using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Proveedores
{
    public class ProveedorIndexViewModel
    {
        public ProveedorIndexViewModel()
        {
            Proveedores = new List<Proveedor>();
        }

        public List<Proveedor> Proveedores { get; set; }

    }
}