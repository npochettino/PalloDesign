using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Web.ViewModels.Cheques
{
    public class ChequeEditarViewModel
    {
        public ChequeEditarViewModel()
        { }

        public int Id { get; set; }

        public ChequeEditarViewModel(Cheque cheque)
        {
            Id = cheque.Id;
            Numero = cheque.Numero;
            Banco = cheque.Banco;
            FechaVencimiento = cheque.FechaVencimiento;
            Monto = cheque.Monto;
            Firmante = cheque.Firmante;
            Cobrado = cheque.Cobrado;
        }

        [Required(ErrorMessage = "El Numero del cheque es requerido")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El Nombre del Banco es requerido")]
        public string Banco { get; set; }

        [Required(ErrorMessage = "Fecha de Vencimiento requerida")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "Monto Requerido")]
        public decimal Monto { get; set; }

        public string Firmante { get; set; }

        public bool Cobrado { get; set; }

        public Cheque Mapear()
        {
            Cheque Cheque = new Cheque();

            Cheque.Id = Id;
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