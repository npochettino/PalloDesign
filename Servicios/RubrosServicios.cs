using DAL.Repositorio;
using Modelos;
using System.Collections.Generic;

namespace Servicios
{
    public class RubrosServicios
    {
        private RubrosRepositorio _rubrosRepositorio;

        public RubrosServicios()
        {
            _rubrosRepositorio = new RubrosRepositorio();
        }

        public List<Rubro> GetAll()
        {
            return _rubrosRepositorio.GetAll();
        }

        public Rubro GetOne(int id)
        {
            return _rubrosRepositorio.GetOne(id);
        }

        public bool Add(Rubro rubro)
        {
            return _rubrosRepositorio.Add(rubro);
        }

        public bool Delete(Rubro rubro)
        {
            return _rubrosRepositorio.Delete(rubro);
        }

        public bool Update(Rubro rubro)
        {
            return _rubrosRepositorio.Update(rubro);
        }
    }
}
