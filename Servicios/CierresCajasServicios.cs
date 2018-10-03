using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class CierresCajasServicios
    {
        private CierresCajaRepositorio _cierresCajaRepositorio;
        private VentasServicios _ventasServicios;
        private TipoMovimientosEfectivoServicios _tipoMovimientosEfectivoServicios;
        private MovimientosEfectivoServicios _movimientosEfectivosServicios;
        private SucursalesServicios _sucursalesServicios;
        private TurnosServicios _turnosServicios;
        private ConfiguracionServicios _configuracionServicios;
      

        public CierresCajasServicios()
        {
            _cierresCajaRepositorio = new CierresCajaRepositorio();
            _ventasServicios = new VentasServicios();
            _tipoMovimientosEfectivoServicios = new TipoMovimientosEfectivoServicios();
            _movimientosEfectivosServicios = new MovimientosEfectivoServicios();
            _sucursalesServicios = new SucursalesServicios();
            _turnosServicios = new TurnosServicios();
            _configuracionServicios = new ConfiguracionServicios();
        }

        public List<CierreCaja> GetAll()
        {
            return _cierresCajaRepositorio.GetAll();
        }

        public CierreCaja GetOne(int id)
        {
            return _cierresCajaRepositorio.GetOne(id);
        }

        public bool Add(CierreCaja cierreCaja)
        {

            return _cierresCajaRepositorio.Add(cierreCaja);
        }

        public List<CierreCaja> Calcular(DateTime fechaCierre, int sucID, DateTime fechaHoraActual)//, int? turnoID
        {
            var turnos = _turnosServicios.GetAll();
            //if (turnoID != null && turnoID != 0)
            //{
            //    var turnoQuitar = _turnosServicios.GetAll().Where(a => a.Id == turnoID).FirstOrDefault();
            //    turnos.Remove(turnoQuitar);
            //}
            var fecha = fechaCierre.Date;
            List<CierreCaja> cierre = new List<CierreCaja>();
            DateTime desde = new DateTime();
            DateTime hasta = new DateTime();
            foreach (var t in turnos)
            {
                //METODO NUEVO CIERRE DE CAJA POR EFECTIVA
                //hasta = DateTime.Now;
                hasta = fechaHoraActual;
                if (t.Nombre == "Tarde")
                {
                    try {
                        CierreCaja cierreTurnoAnterior = GetByDate(fecha);
                        desde = cierreTurnoAnterior.FechaCierreEfectiva;
                    }
                    catch (Exception ex){

                        //desde = DateTime.Now;//fechaCierre.AddHours(t.HoraDesde);
                        desde = fechaHoraActual;
                    }
                    
                }
                else
                {
                    desde = fechaCierre.AddHours(t.HoraDesde);
                }
                //METODO ANTERIOR, CIERRE DE CAJAS POR HORAS DE TURNO
                //hasta = fechaCierre.AddHours(t.HoraHasta);
                //desde = fechaCierre.AddHours(t.HoraDesde);

                CierreCaja c = new CierreCaja();
                c.FechaCierreCaja = fechaCierre;
                c.FechaCierreEfectiva = DateTime.Now;
                c.SucursalID = sucID;
                c.TurnoID = t.Id;
                c.Turno = t;
                c.Sucursal = _sucursalesServicios.GetOne(sucID);
                c.TotalProveedores = GetTotalProveedores(desde, hasta, sucID);
                c.TotalSueldos = GetTotalSueldos(desde, hasta, sucID);
                c.TotalVarios = GetTotalVarios(desde, hasta, sucID);
                var ventas = GetTotalVentas(desde, hasta, sucID);
                c.TotalVentasEfectivo = ventas.Item1;
                c.TotalVentasTarjetas = ventas.Item2;
                if (t.Nombre == "Tarde")
                {
                   var saldoMañana = GetSaldoTurnoMañana(desde, hasta, sucID);
                   c.Saldo = saldoMañana;
                }
                else
                {
                    c.Saldo = _configuracionServicios.GetSaldoInicial();
                }
                cierre.Add(c);
                
            }
            return cierre;
        }

        private CierreCaja GetByDate(DateTime fecha)
        {
            return _cierresCajaRepositorio.GetByDate(fecha);
        }

        private decimal GetSaldoTurnoMañana(DateTime desde, DateTime hasta, int sucID)
        {
            var turno = _turnosServicios.GetAll().Where(a => a.Nombre == "Mañana").First();
            var fechaDesde = desde.Date.AddHours(turno.HoraDesde);
            var fechaHasta = hasta.Date.AddHours(turno.HoraHasta);
            List<CierreCaja> cierres = _cierresCajaRepositorio.GetSaldoTurnoMañana(fechaDesde, fechaHasta, sucID);
            if (cierres.Count > 0)
            {
                return cierres.First().TotalCaja;
            }
            else
            {
                return 0M;
            }
        }

        private Tuple<decimal,decimal> GetTotalVentas(DateTime desde, DateTime hasta, int sucID)
        {
            var ventas = _ventasServicios.GetAllByDateAndSucursal(desde, hasta, sucID);
            
            decimal totalEfectivo = 0;
            decimal totalTarjeta = 0;
            foreach (var v in ventas)
            {
                var pagosEfectivo = v.Pagos.Where(a => a.FormaDePago.Nombre == "Efectivo").Sum(b => b.Monto);
                var pagosTarjetas = v.Pagos.Where(a => a.FormaDePago.Nombre.Contains("Tarjeta")).Sum(b => b.Monto);

                totalEfectivo = totalEfectivo + pagosEfectivo;
                totalTarjeta = totalTarjeta + pagosTarjetas;
            }
            Tuple<decimal, decimal> pagos = new Tuple<decimal, decimal>(totalEfectivo, totalTarjeta);         

            return pagos;
        }

        private decimal GetTotalSueldos(DateTime desde, DateTime hasta, int sucID)
        {  // TODO: Ver bien el tema de las categorias de los tipos de movimiento
            var mov = new List<MovimientoEfectivo>();
            mov = _movimientosEfectivosServicios.GetAllBySucursal(sucID).Where(a => a.TipoMovimiento.Categoria.Nombre == "Sueldos" && a.TipoMovimiento.Caja == true && (desde <= a.Fecha && hasta >= a.Fecha)).ToList();
            var movEgreso = mov.Where(a => a.TipoMovimiento.Suma == false).Sum(a => a.Monto);
            var movIngreso = mov.Where(a => a.TipoMovimiento.Suma == true).Sum(a => a.Monto);
            return movIngreso - movEgreso;
        }

        private decimal GetTotalProveedores(DateTime desde, DateTime hasta, int sucID)
        {
            // TODO: Ver bien el tema de las categorias de los tipos de movimiento
            var mov = new List<MovimientoEfectivo>();
            mov = _movimientosEfectivosServicios.GetAllBySucursal(sucID).Where(a => a.TipoMovimiento.Categoria.Nombre == "Proveedores" && a.TipoMovimiento.Caja == true && (desde <= a.Fecha && hasta >= a.Fecha)).ToList();
            var movEgreso = mov.Where(a => a.TipoMovimiento.Suma == false).Sum(a => a.Monto);
            var movIngreso = mov.Where(a => a.TipoMovimiento.Suma == true).Sum(a => a.Monto);
            return movIngreso - movEgreso;
        }

        private decimal GetTotalVarios(DateTime desde, DateTime hasta, int sucID)
        {
            // TODO: Ver bien el tema de las categorias de los tipos de movimiento
            var mov = new List<MovimientoEfectivo>();
            mov = _movimientosEfectivosServicios.GetAllBySucursal(sucID).Where(a => a.TipoMovimiento.Categoria.Nombre == "Varios" && a.TipoMovimiento.Caja == true && (desde <= a.Fecha && hasta >= a.Fecha)).ToList();
            var movEgreso = mov.Where(a => a.TipoMovimiento.Suma == false).Sum(a => a.Monto);
            var movIngreso = mov.Where(a => a.TipoMovimiento.Suma == true).Sum(a => a.Monto);
            return movIngreso - movEgreso;
        }

        public bool Update(CierreCaja cierre, int id)
        {
            return _cierresCajaRepositorio.Update(cierre, id);
        }
    }
}