namespace Modelos
{
    public class FormaDePago:Base
    {
        public string Nombre { get; set; }
        public decimal Recargo { get; set; }
        public string NombreRecargo
        {
            get
            {
                if (Recargo > 1)
                {
                    var nro = ((Recargo - 1) * 100).ToString();
                    return Nombre + " (" + nro + "% Recargo)";
                }
                else if (Recargo == 0)
                {
                    return Nombre;
                }
                else if (Recargo < 1)
                {
                    var nro = ((1 - Recargo) * 100).ToString();
                    return Nombre + " (" + nro + "% Descuento)";
                }
               
                else
                {
                    return Nombre;
                }
            }
            
        }
    }
}
