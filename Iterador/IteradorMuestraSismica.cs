using EventoSismicoApp.Entities;
using System.Collections.Generic;

namespace EventoSismicoApp.Iterador
{
    public class IteradorMuestraSismica : IIterador
    {
        private List<MuestraSismica> _muestras;
        private int _posicion;

        public IteradorMuestraSismica(List<MuestraSismica> muestras)
        {
            _muestras = muestras;
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
            return _posicion >= _muestras.Count;
        }

        public object elementoActual()
        {
            return _muestras[_posicion];
        }
    }
}