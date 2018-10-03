using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;

namespace DAL.Repositorio
{
    public class CierresCajaRepositorio : Base
    {
        public CierresCajaRepositorio()
            : base()
        {
        }

        public List<CierreCaja> GetAll()
        {
            return _applicationDbContext.CierresCaja.ToList();
        }

        public CierreCaja GetOne(int id)
        {
            return _applicationDbContext.CierresCaja.Find(id);
        }

        public bool Add(CierreCaja cierreCaja)
        {
            try
            {
                cierreCaja.Sucursal = null;
                cierreCaja.Turno = null;                
                _applicationDbContext.CierresCaja.Add(cierreCaja);
                Guardar();
                return true;
            }
            catch
            {
                return false;
            }
        }      

        public List<CierreCaja> GetSaldoTurnoMañana(DateTime desde, DateTime hasta, int sucID)
        {
            var fechaDesde = desde.Date;
            var fechaHasta = hasta.AddDays(1);
            var cierre = _applicationDbContext.CierresCaja.Where(a => a.SucursalID == sucID && a.FechaCierreCaja >= fechaDesde && a.FechaCierreCaja <= fechaHasta && a.Turno.Nombre == "Mañana").ToList();
            return cierre;
        }

        public CierreCaja GetByDate(DateTime fecha)
        {
            return _applicationDbContext.CierresCaja.Where(a => a.FechaCierreCaja == fecha && a.TurnoID == 1).First();
        }

        public bool Update(CierreCaja c, int id)
        {
            try
            {
                var cv = GetOne(id);
                cv.FechaCierreCaja = c.FechaCierreCaja;
                cv.FechaCierreEfectiva = c.FechaCierreEfectiva;
                cv.Saldo = c.Saldo;
                cv.SucursalID = c.SucursalID;
                cv.TotalProveedores = c.TotalProveedores;
                cv.TotalSueldos = c.TotalSueldos;
                cv.TotalVarios = c.TotalVarios;
                cv.TotalVentasEfectivo = c.TotalVentasEfectivo;
                cv.TotalVentasTarjetas = c.TotalVentasTarjetas;
                cv.TurnoID = c.TurnoID;
                cv.UsuarioID = c.UsuarioID;
                Guardar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
