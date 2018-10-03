using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;
using System;

namespace DAL.Repositorio
{
    public class ArticulosRepositorio:Base
    {
        public ArticulosRepositorio()
            :base()
        {
        }

        public List<Articulo> GetAll()
        {
            return _applicationDbContext.Articulos.Where(a => a.Habilitado == true).ToList();
        }

        public List<Articulo> GetAllByRubro(int rubroID)
        {
            return _applicationDbContext.Articulos.Where(a => a.Habilitado == true && a.RubroID == rubroID).ToList();
        }

        public Articulo GetOne(Int64 id)
        {
            return _applicationDbContext.Articulos.Find(id);
        }

        public bool Add (Articulo articulo)
        {
            try
            {
                _applicationDbContext.Entry(articulo).State = EntityState.Added;
                Guardar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Articulo articulo)
        {
            try
            {
                _applicationDbContext.Articulos.Remove(articulo);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Articulo articulo)
        {
            try 
            {
                //_applicationDbContext.Entry(articulo).State = EntityState.Modified;
                var ArticuloBD = GetOne(articulo.Id);
                ArticuloBD.Codigo = articulo.Codigo;
                ArticuloBD.Nombre = articulo.Nombre;
                ArticuloBD.NombreEtiqueta = articulo.NombreEtiqueta;
                ArticuloBD.PrecioActualCompra = articulo.PrecioActualCompra;
                ArticuloBD.PrecioActualVenta = articulo.PrecioActualVenta;
                ArticuloBD.StockMinimo = articulo.StockMinimo;
                ArticuloBD.StockMaximo = articulo.StockMaximo;
                ArticuloBD.RubroID = articulo.RubroID;
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
