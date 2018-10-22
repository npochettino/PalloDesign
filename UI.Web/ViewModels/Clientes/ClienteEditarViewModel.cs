using Modelos;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace UI.Web.ViewModels.Clientes
{
    public class ClienteEditarViewModel
    {
        public ClienteEditarViewModel()
        {
        }

        public ClienteEditarViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            Apellido = cliente.Apellido;
            Nombre = cliente.Nombre;
            DNI = cliente.DNI;
            Calle = cliente.Calle;
            Numero = cliente.Numero;
            Bis = cliente.Bis;
            Piso = cliente.Piso;
            Dpto = cliente.Dpto;
            Telefono = cliente.Telefono;
            Email = cliente.Email;
            Referencia = cliente.Referencia;
        }

        public int Id { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Apellido es Requerido")]
        public string Apellido { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre es Requerido")]
        public string Nombre { get; set; }

        [Display(Name = "DNI")]
        //[Required(ErrorMessage = "DNI es Requerido")]
        public string DNI { get; set; }

        [Display(Name = "Calle")]
        //[RequiredIfNotEmpty("Numero", ErrorMessage = "Calle es Requerido si Número no está vacío")]
        public string Calle { get; set; }

        [Display(Name = "Número")]
        //[RequiredIfNotEmpty("Calle", ErrorMessage = "Número es Requerido si Calle no está vacío")]
        public string Numero { get; set; }

        public bool Bis { get; set; }

        public string Piso { get; set; }

        public string Dpto { get; set; }

        public string Referencia { get; set; }

        [Display(Name = "Teléfono")]
        //[Required(ErrorMessage = "Teléfono es Requerido")]
        public string Telefono { get; set; }


        //[EmailAddress(ErrorMessage = "Email debe ser una dirección de corre electrónico válida")]
        public string Email { get; set; }

        public Cliente Mapear()
        {
            Cliente Cliente = new Cliente();

            Cliente.Id = Id;
            Cliente.Apellido = Apellido;
            Cliente.Nombre = Nombre;
            Cliente.DNI = DNI;
            Cliente.Calle = Calle;
            Cliente.Numero = Numero;
            Cliente.Bis = Bis;
            Cliente.Piso = Piso;
            Cliente.Dpto = Dpto;
            Cliente.Telefono = Telefono;
            Cliente.Email = Email;
            Cliente.Habilitado = true;
            Cliente.Referencia = Referencia;

            return Cliente;
        }


    }
}