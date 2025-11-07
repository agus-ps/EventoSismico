using System;

namespace EventoSismicoApp.Entities
{
    public class CambioEstado
    {
        public DateTime? FechaHoraFin { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public Estado Estado { get; set; }


        public CambioEstado(Estado estado, DateTime fechaHoraInicio)
        {
            Estado = estado;
            FechaHoraInicio = fechaHoraInicio;
        }

        public CambioEstado(Estado estado, DateTime fechaHoraInicio, DateTime? fechaHoraFin)
        {
            Estado = estado;
            FechaHoraInicio = fechaHoraInicio;
            FechaHoraFin = fechaHoraFin;
        }

        public void SetFechaHoraFin(DateTime fechaHora)
        {
            FechaHoraFin = fechaHora;
        }

        public bool SosActual()
        {
            return FechaHoraFin == null;
        }

        public void Crear(Estado estado, DateTime fechaHoraInicio)
        {
            Estado = estado;
            FechaHoraInicio = fechaHoraInicio;
        }
    }
}