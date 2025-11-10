using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventoSismicoApp.Entities
{
    public class EventoSismico
    {
        // Propiedades (ajustadas a los constructores)
        public DateTime FechaHoraFin { get; set; }
        public DateTime FechaHoraOcurrencia { get; set; }
        public double LatitudEpicentro { get; set; }
        public double LongitudEpicentro { get; set; }
        public double LatitudHipocentro { get; set; }
        public double LongitudHipocentro { get; set; }
        public double ValorMagnitud { get; set; }
        public Estado EstadoActual { get; set; }
        public List<CambioEstado> CambiosEstado { get; set; }
        public AlcanceSismo Alcance { get; set; }
        public OrigenDeGeneracion Origen { get; set; }
        public ClasificacionSismo Clasificacion { get; set; }
        public List<SerieTemporal> SeriesTemporales { get; set; }
        public bool EsAutoDetectado { get; set; }

        // Constructor completo
        public EventoSismico(DateTime fechaHoraOcurrencia, double latEpicentro, double longEpicentro,
                           double latHipocentro, double longHipocentro, double magnitud, bool esAutoDetectado,
                           Estado estado, AlcanceSismo alcance, OrigenDeGeneracion origen,
                           ClasificacionSismo clasificacion)
        {
            FechaHoraOcurrencia = fechaHoraOcurrencia;
            LatitudEpicentro = latEpicentro;
            LongitudEpicentro = longEpicentro;
            LatitudHipocentro = latHipocentro;
            LongitudHipocentro = longHipocentro;
            ValorMagnitud = magnitud;
            EsAutoDetectado = esAutoDetectado;
            EstadoActual = estado;
            Alcance = alcance;
            Origen = origen;
            Clasificacion = clasificacion;
            CambiosEstado = new List<CambioEstado>();
            SeriesTemporales = new List<SerieTemporal>();
        }



        // Métodos actualizados:
        public bool esAutoDetectado() => this.EsAutoDetectado;
        public bool esPendienteRevisar() => EstadoActual != null && EstadoActual.esPendienteRevisar();

        public DateTime getHoraOcurrencia() => FechaHoraOcurrencia;

        public string getUbicacion() => $"Epicentro: {getCoordEpicentro()}, Hipocentro: {getCoordHipocentro()}";

        private string getCoordEpicentro() => $"{LatitudEpicentro:N4}, {LongitudEpicentro:N4}";

        private string getCoordHipocentro() => $"{LatitudHipocentro:N4}, {LongitudHipocentro:N4}";

        public double getMagnitud() => ValorMagnitud;

        public CambioEstado buscarCambioEstadoAbierto()
        {
            foreach (var cambio in CambiosEstado)
            {
                if (cambio.SosActual())
                {
                    return cambio;
                }
            }
            return null;
        }

        // Obsoleto, cambia por el de abajo
        public void CrearCambioEstado(Estado nuevoEstado, DateTime fechaHoraInicio)
        {
            var cambio = new CambioEstado(nuevoEstado, fechaHoraInicio, null);
            CambiosEstado.Add(cambio);
            EstadoActual = nuevoEstado; // Actualizar estado actual
        }

        // --- INICIO DE CAMBIO (Facu4) ---
        // 1. Creamos el método para revertir el estado
        public void liberarBloqueo(DateTime fechaHoraActual, Estado estadoPendiente, Empleado empleadoResponsable)
        {
            // 2. Buscamos el estado actual (que debería ser "BloqueadoEnRevision")
            CambioEstado actualCambioEstado = this.buscarCambioEstadoAbierto();

            if (actualCambioEstado != null && actualCambioEstado.Estado.esBloqueado())
            {
                // 3. Cerramos el estado "BloqueadoEnRevision"
                actualCambioEstado.SetFechaHoraFin(fechaHoraActual);

                // 4. Creamos un nuevo estado "PendienteRevisar"
                this.CrearCambioEstado(estadoPendiente, fechaHoraActual, empleadoResponsable);
            }
        }
        // --- FIN DE CAMBIO ---
        public void CrearCambioEstado(Estado nuevoEstado, DateTime fechaHoraInicio, Empleado empleado)
        {
            // 2. Llamamos al NUEVO constructor de CambioEstado (el del Archivo 1)
            var cambio = new CambioEstado(nuevoEstado, fechaHoraInicio, null, empleado);
            CambiosEstado.Add(cambio);
            EstadoActual = nuevoEstado; // Actualizar estado actual
        }



        
        // Obsoleto, cambia por el de abajo
        public void revisar(DateTime fechaHoraActual, Estado estadoBloqueadoEnRevision)
        {
            CambioEstado actualCambioEstado = this.buscarCambioEstadoAbierto();// Le pregunta a todos los cambios de estado si es actual

            if (actualCambioEstado != null)
            {
                actualCambioEstado.SetFechaHoraFin(fechaHoraActual);

                this.CrearCambioEstado(estadoBloqueadoEnRevision, fechaHoraActual);// TODO: para crear un cambio de estado
            }
        }
        public void revisar(DateTime fechaHoraActual, Estado estadoBloqueadoEnRevision, Empleado empleadoResponsable)
        {
            CambioEstado actualCambioEstado = this.buscarCambioEstadoAbierto();

            if (actualCambioEstado != null)
            {
                actualCambioEstado.SetFechaHoraFin(fechaHoraActual);

                // 4. Llamamos a la NUEVA versión de CrearCambioEstado
                this.CrearCambioEstado(estadoBloqueadoEnRevision, fechaHoraActual, empleadoResponsable);
            }
        }



        // Obsoleto, cambia por el de abajo
        public void rechazar(Estado estadoRechazado, DateTime fechaHora)
        {
            var cambioAbierto = this.buscarCambioEstadoAbierto();
            if (cambioAbierto != null)
            {
                cambioAbierto.SetFechaHoraFin(fechaHora);
                this.CrearCambioEstado(estadoRechazado, fechaHora);
            }
        }

        public void rechazar(Estado estadoRechazado, DateTime fechaHora, Empleado empleadoResponsable)
        {
            var cambioAbierto = this.buscarCambioEstadoAbierto();
            if (cambioAbierto != null)
            {
                cambioAbierto.SetFechaHoraFin(fechaHora);

                // 6. Llamamos a la NUEVA versión de CrearCambioEstado
                this.CrearCambioEstado(estadoRechazado, fechaHora, empleadoResponsable);
            }
        }




        public (string, string, string, List<string[]>) getDetalleEventoSismico()
        {
            string alcance = Alcance?.getNombre() ?? "N/A";
            string origen = Origen?.getNombre() ?? "N/A";
            string clasificacion = Clasificacion?.getNombre() ?? "N/A";

            //return $"Alcance: {alcance}, Origen: {origen}, Clasificación: {clasificacion}";
            var datos = this.obtenerDatosSeriesTemporales();
            return (alcance, origen, clasificacion, datos);
        }
        public List<string[]> obtenerDatosSeriesTemporales()
        {
            var datos = new List<string[]>();

            foreach (var serie in this.SeriesTemporales)
            {
                datos.AddRange(serie.GetSerie());
            }

            return datos;
        }
    }
}