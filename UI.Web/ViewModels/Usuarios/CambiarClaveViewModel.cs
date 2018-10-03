using Foolproof;
using System.ComponentModel.DataAnnotations;
using Modelos;

namespace UI.Web.ViewModels.Usuarios
{
    public class CambiarClaveViewModel
    {
        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50, ErrorMessage = "{0} debe ser una cadena con una longitud máxima de 50")]
        public string Password { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "{0} es Requerido")]
        [StringLength(50, ErrorMessage = "{0} debe ser una cadena con una longitud máxima de 50")]
        [EqualTo("Password", ErrorMessage = "{0} debe coincidir con el campo Nueva Contraseña")]
        public string RepetirPassword { get; set; }

        public Usuario Mapear()
        {
            var Usuario = new Usuario();
            Usuario.Password = Password;
            return Usuario;
        }
    }
}