using Modelos;
using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Proveedores
{
    public class ProveedorDetallesViewModel
    {
        public ProveedorDetallesViewModel()
        {
        }
        public ProveedorDetallesViewModel(Proveedor proveedor)
        {
            RazonSocial = proveedor.RazonSocial;
            Apellido = proveedor.Apellido;
            Nombre = proveedor.Nombre;
            DNI = proveedor.DNI;
            Calle = proveedor.Calle;
            Numero = proveedor.Numero;
            Bis = proveedor.Bis;
            Piso = proveedor.Piso;
            Dpto = proveedor.Dpto;
            Telefono = proveedor.Telefono;
            Email = proveedor.Email;
            Referencia = proveedor.Referencia;
        }

        public string RazonSocial { get; set; }
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
        public string Referencia { get; set; }

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