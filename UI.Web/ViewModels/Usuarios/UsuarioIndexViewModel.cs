using System.Collections.Generic;
using Modelos;

namespace UI.Web.ViewModels.Usuarios

{
    public class UsuarioIndexViewModel
    {
        public UsuarioIndexViewModel()
        {
            Usuarios = new List<Usuario>();
        }

        public List<Usuario> Usuarios { get; set; }

    }
}