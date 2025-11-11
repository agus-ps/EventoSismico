using EventoSismicoApp.Entities;
using System.Collections.Generic;

namespace EventoSismicoApp.Iterador
{
    public class IteradorDatosSeriesTemporales : IIterador
    {
        private List<SerieTemporal> _series;
        private int _posicion;

        public IteradorDatosSeriesTemporales(List<SerieTemporal> series)
        {
            _series = series;
        }

        public void primero()
        {
            _posicion = 0;
        }

        public void siguiente()
        {
            _posicion++;
        }

        public bool haFinalizado()
        {
            return _posicion >= _series.Count;
        }

        public object elementoActual()
        {
            return _series[_posicion];
        }
    }
}