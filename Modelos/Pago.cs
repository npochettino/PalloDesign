namespace Modelos
{
    public class Pago:Base
    {
        public decimal Monto { get; set; }

        public decimal MontoRecargo
        {
            get
            {
                try
                {
                    if (Monto == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return Monto * FormaDePago.Recargo;
                    }
                }
               catch { return Monto; }

            }
        }

        public int FormaDePagoID { get; set; }
        public virtual FormaDePago FormaDePago { get; set; }

        public int VentaID { get; set; }
        public virtual Venta Venta { get; set; }

    }
}
