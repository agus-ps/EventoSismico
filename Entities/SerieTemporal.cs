using System.Collections.Generic;
using EventoSismicoApp.Iterador; // <-- 1. IMPORTAR ITERADOR

namespace EventoSismicoApp.Entities
{
    public class SerieTemporal : IAgregado
    {
        public bool CondicionAlarma { get; set; }
        public DateTime FechaHoraInicioRegistroMuestras { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public double FrecuenciaMuestreo { get; set; }
        public List<MuestraSismica> Muestras { get; set; }
        public Sismografo Sismografo { get; set; }


        public SerieTemporal(
            bool condicionAlarma,
            DateTime fechaHoraInicioRegistroMuestras,  // Nombre exacto que usaremos
            DateTime fechaHoraRegistro,
            double frecuenciaMuestreo,
            Sismografo sismografo)
        {
            CondicionAlarma = condicionAlarma;
            FechaHoraInicioRegistroMuestras = fechaHoraInicioRegistroMuestras;
            FechaHoraRegistro = fechaHoraRegistro;
            FrecuenciaMuestreo = frecuenciaMuestreo;
            Sismografo = sismografo;
            Muestras = new List<MuestraSismica>();
        }

        public IIterador crearIterador()
        {
            // Esto replica: 
            // serieTemporal -> IteradorMuestraSismica : new(...)
            return new IteradorMuestraSismica(this.Muestras);
        }

        // Método viejo, cuando esta clase era la que consultaba el nombre de la estacion
        public List<string[]> GetSerie()
        {
            var lista = new List<string[]>();
            string estacion = Sismografo.GetEstacion().GetNombre();

            foreach (var muestra in this.Muestras)
            {
                var datosMuestra = muestra.GetDatos(estacion);
                lista.AddRange(datosMuestra);
            }

            return lista;
        }


        /*
        public List<string[]> GetSerie(string estacion)
        {
            var lista = new List<string[]>();

            // 3. Ya no llamamos a Sismografo.GetEstacion()...
            //    Usamos directamente el parámetro 'estacion'

            foreach (var muestra in this.Muestras)
            {
                var datosMuestra = muestra.GetDatos(estacion);
                lista.AddRange(datosMuestra);
            }

            return lista;
        }*/

        public List<string[]> GetSerie(string estacion)
        {
            var lista = new List<string[]>();

            // Replicando: serieTemporal -> serieTemporal : crearIterador(...)
            IIterador iteradorMuestras = this.crearIterador();

            // Replicando: serieTemporal -> Iterador... : primero()
            iteradorMuestras.primero();

            // Replicando: loop mientras haya muestras
            while (!iteradorMuestras.haFinalizado())
            {
                // Replicando: serieTemporal -> Iterador... : haFinalizado() (ya está en el while)

                // Replicando: serieTemporal -> Iterador... : elementoActual(): Boolean
                // (Ignoramos el "Boolean" y lo casteamos a MuestraSismica)
                var muestraActual = (MuestraSismica)iteradorMuestras.elementoActual();

                // Replicando: serieTemporal -> muestra : getDatos()
                // (Que en nuestro código es muestraActual.GetDatos(estacion))
                var datosMuestra = muestraActual.GetDatos(estacion);
                lista.AddRange(datosMuestra);

                // Replicando: serieTemporal -> Iterador... : siguiente()
                iteradorMuestras.siguiente();
            }

            return lista;
        }
    }
}