using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Clientes
{
    public class ClienteIndexViewModel
    {
        public ClienteIndexViewModel()
        {
            Clientes = new List<Cliente>();
        }

        public List<Cliente> Clientes { get; set; }

    }
}