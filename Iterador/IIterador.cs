using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoSismicoApp.Iterador
{
    public interface IIterador
    {
        void primero();
        void siguiente();
        bool haFinalizado();
        object elementoActual();
    }
}
