using Foolproof;
using Modelos;
using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Cheques
{
    public class ChequeAgregarViewModel
    {
        [Required(ErrorMessage ="El Numero del cheque es requerido")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El Nombre del Banco es requerido")]
        public string Banco { get; set; }

        [Required(ErrorMessage = "Fecha de Vencimiento requerida")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage ="Monto Requerido")]
        public decimal Monto { get; set; }

        public string Firmante { get; set; }

        public bool Cobrado { get; set; }

        public Cheque Mapear()
        {
            Cheque Cheque = new Cheque();

            Cheque.Numero = Numero;
            Cheque.Banco = Banco;
            Cheque.FechaVencimiento = FechaVencimiento;
            Cheque.Monto = Monto;
            Cheque.Firmante = Firmante;
            Cheque.Cobrado = Cobrado;

            return Cheque;
        }
    }
}