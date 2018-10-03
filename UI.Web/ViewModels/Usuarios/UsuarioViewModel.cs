using Modelos;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using System.Collections.Generic;
using System;

namespace UI.Web.ViewModels.Usuarios

{

    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Roles = new List<UsuarioRol>();
        }

        public UsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            DNI = usuario.DNI;
            Password = usuario.Password;
            RepetirPassword = usuario.Password; //Sólo para facilitar el Testing, luego hay que sacarlo....
            Roles = usuario.Roles;

        }

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe ser una cadena con una longitud máxima de 50")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Apellido es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Apellido debe ser una cadena con una longitud máxima de 50")]
        public string Apellido { get; set; }

        [Display(Name = "DNI")]
        [Required(ErrorMessage = "DNI es Requerido")]
        [StringLength(50, ErrorMessage = "El campo DNI debe ser una cadena con una longitud máxima de 50")]
        public string DNI { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Password debe ser una cadena con una longitud máxima de 50")]
        public string Password { get; set; }

        [Display(Name = "Repetir Password")]
        [Required(ErrorMessage = "Repetir Password es Requerido")]
        [StringLength(50, ErrorMessage = "El campo Repetir Password debe ser una cadena con una longitud máxima de 50")]
        [EqualTo("Password", ErrorMessage = "El campo Repetir Password debe coincidir con el campo Password")]
        public string RepetirPassword { get; set; }

        [Display(Name = "Apellido y Nombre")]
        public string NombreCompleto
        {
            get
            {
                return String.Format("{0}, {1}", Apellido, Nombre);
            }
        }

        //----------Roles ---------- 
        [Display(Name = "Rol")]
        public int RolIdFromVista { get; set; }

        [Display(Name = "Sucursal")]
        public int SucursalIdFromVista { get; set; }

        public List<UsuarioRol> Roles { get; set; }

        public Usuario Mapear()
        {
            Usuario Usuario = new Usuario();

            Usuario.Id = Id;
            Usuario.Nombre = Nombre;
            Usuario.Apellido = Apellido;
            Usuario.DNI = DNI;
            Usuario.Password = Password;
            Usuario.Roles = Roles;
            Usuario.Habilitado = true;
            
            return Usuario;
        }
    }
}