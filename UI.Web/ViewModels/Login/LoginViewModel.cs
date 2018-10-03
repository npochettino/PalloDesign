using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Login
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario: ")]
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}