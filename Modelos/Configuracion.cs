using System;

namespace Modelos
{
    public class Configuracion : Base
    {
        public DateTime FechaFinPeriodoPrueba { get; set; }
        public decimal SaldoInicialTurnoMañana { get; set;}
        public int LongitudNombreEtiqueta { get; set; }
    }
}
