namespace Modelos
{
    public class Cliente : Base
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Dpto { get; set; }
        public bool Bis { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Habilitado { get; set; }

        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }
    }
}
