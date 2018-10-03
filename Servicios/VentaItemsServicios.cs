using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;

namespace Servicios
{
    public class VentaItemsServicios
    {
        private VentaItemsRepositorio _ventaItemsRepositorio;
        private ArticulosRepositorio _articulosRepositorio;

        public VentaItemsServicios()
        {
            _ventaItemsRepositorio = new VentaItemsRepositorio();
            _articulosRepositorio = new ArticulosRepositorio();
        }

        public List<VentaItem> GetAll()
        {
            return _ventaItemsRepositorio.GetAll();
        }

        public VentaItem GetOne(int id)
        {
            return _ventaItemsRepositorio.GetOne(id);
        }

        public bool Add(VentaItem ventaItem)
        {
            return _ventaItemsRepositorio.Add(ventaItem);
        }

        public bool Delete(VentaItem ventaItem)
        {
            return _ventaItemsRepositorio.Delete(ventaItem);
        }

        public bool Update(VentaItem ventaItem)
        {
            return _ventaItemsRepositorio.Update(ventaItem);
        }

        public VentaItem AgregarArticuloEnVenta(Int64 articuloID)
        {
            var ventaItem = new VentaItem();
            ventaItem.Articulo = _articulosRepositorio.GetOne(articuloID);
            ventaItem.Cantidad = 1;
            ventaItem.ArticuloID = ventaItem.Articulo.Id;
            ventaItem.Descuento = 0;
            ventaItem.Precio = ventaItem.Articulo.PrecioActualVenta;
            return ventaItem;
        }
    }
}
