using System.Collections.Generic;
using Modelos;
using DAL.Repositorio;

namespace Servicios
{
    public class CategoriasTiposMovimientosEfectivoServicios
    {
        private CategoriasTiposMovimientosEfectivoRepositorio _categoriasTiposMovimientosEfectivoRepositorio;

        public CategoriasTiposMovimientosEfectivoServicios()
        {
            _categoriasTiposMovimientosEfectivoRepositorio = new CategoriasTiposMovimientosEfectivoRepositorio();
        }

        public List<CategoriaMovimientoEfectivo> GetAll()
        {
            return _categoriasTiposMovimientosEfectivoRepositorio.GetAll();
        }

        public CategoriaMovimientoEfectivo GetOne(int id)
        {
            return _categoriasTiposMovimientosEfectivoRepositorio.GetOne(id);
        }
    }
}
