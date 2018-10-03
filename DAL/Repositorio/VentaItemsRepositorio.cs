using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace DAL.Repositorio
{
    public class VentaItemsRepositorio : Base
    {
        public VentaItemsRepositorio()
            : base()
        {
        }

        public List<VentaItem> GetAll()
        {
            return _applicationDbContext.VentaItems.ToList();
        }

        public VentaItem GetOne(int id)
        {
            return _applicationDbContext.VentaItems.Find(id);
        }

        public bool Add(VentaItem ventaItem)
        {
            try
            {
                _applicationDbContext.VentaItems.Add(ventaItem);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(VentaItem ventaItem)
        {
            try
            {
                _applicationDbContext.VentaItems.Remove(ventaItem);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(VentaItem ventaItem)
        {
            try
            {
                _applicationDbContext.Entry(ventaItem).State = EntityState.Modified;
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
