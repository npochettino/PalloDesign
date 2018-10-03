using Modelos;
using System;
using System.ComponentModel.DataAnnotations;


namespace UI.Web.ViewModels.StockMovimientos

{
    public class StockMovimientoAgregarViewModel
    {
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Cantidad es Requerido")]
        public int Cantidad { get; set; }

        [Display(Name = "Motivo")]
        [Required(ErrorMessage = "Motivo es Requerido")]
        public int TipoMovimientoStockID { get; set; }
        public virtual TipoMovimientoStock Motivo { get; set; }

        [Display(Name = "Artículo")]
        [Required(ErrorMessage = "Articulo es Requerido")]
        public int ArticuloID { get; set; }

        public StockMovimiento Mapear()
        {
            StockMovimiento StockMovimiento = new StockMovimiento();

            StockMovimiento.Fecha = DateTime.Now;
            StockMovimiento.Cantidad = Cantidad;
            StockMovimiento.TipoMovimientoStockID = TipoMovimientoStockID;
            StockMovimiento.ArticuloID = ArticuloID;
            var usuario = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
            StockMovimiento.UsuarioID = usuario.Id;

            return StockMovimiento;
        }
    }
}