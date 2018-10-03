using DAL.Repositorio;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class ConfiguracionServicios
    {
        private ConfiguracionRepositorio _configuracionRepositorio;

        public ConfiguracionServicios()
        {
            _configuracionRepositorio = new ConfiguracionRepositorio();
        }

        public List<Configuracion> GetAll()
        {
            return _configuracionRepositorio.GetAll();
        }

        public bool IsPeriodoDePrueba()
        {
            var fechaPrueba = _configuracionRepositorio.GetAll().Select(a=>a.FechaFinPeriodoPrueba).FirstOrDefault();
            if (DateTime.Now >= fechaPrueba)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal decimal GetSaldoInicial()
        {
            return _configuracionRepositorio.GetAll().Select(a => a.SaldoInicialTurnoMañana).FirstOrDefault();
        }

        public int GetLongitudNombreEtiqueta()
        {
            return _configuracionRepositorio.GetAll().Select(a => a.LongitudNombreEtiqueta).FirstOrDefault();
        }
    }
}
