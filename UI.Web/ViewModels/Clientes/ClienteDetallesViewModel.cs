using Modelos;
using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Clientes
{
    public class ClienteDetallesViewModel
    {
        public ClienteDetallesViewModel()
        {
        }
        public ClienteDetallesViewModel(Cliente cliente)
        {
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
        }

        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public bool Bis { get; set; }
        public string Piso { get; set; }
        public string Dpto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        [Display(Name = "Apellido y Nombre")]
        public string NombreCompleto
        {
            get
            {
                return String.Format("{0}, {1}", Apellido, Nombre);
            }
        }

        [Display(Name = "Domicilio")]
        public string DomicilioCompleto
        {
            get { return String.Format("{0} {1} {2} {3} {4}", Calle, Numero, Bis ? "Bis" : "", Piso, Dpto); }
        }
    }
}