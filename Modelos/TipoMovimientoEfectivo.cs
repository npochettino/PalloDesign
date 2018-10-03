namespace Modelos
{
    public class TipoMovimientoEfectivo : Base
    {
        public string Nombre { get; set; }
        public bool Caja { get; set; }
        public bool Suma { get; set; }
        public int CategoriaMovimientoEfectivoID { get; set; }
        public virtual CategoriaMovimientoEfectivo Categoria { get; set; }
        public bool Habilitado { get; set; }
    }
}
