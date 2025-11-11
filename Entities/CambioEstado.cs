using System;

namespace EventoSismicoApp.Entities
{
    public class CambioEstado
    {
        public int Id { get; set; }// Necesario para que el ORM no tire error por falta de PK
        public DateTime? FechaHoraFin { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public Estado Estado { get; set; }
        public Empleado EmpleadoResponsable { get; set; }
        
        public CambioEstado() { }

        public CambioEstado(Estado estado, DateTime fechaHoraInicio, DateTime? fechaHoraFin, Empleado empleado)
        {
            this.Estado = estado;
            this.FechaHoraInicio = fechaHoraInicio;
            this.FechaHoraFin = fechaHoraFin;

            // --- INICIO DE CAMBIO ---
            // 2. Asignamos el empleado al crearlo
            this.EmpleadoResponsable = empleado;
            // --- FIN DE CAMBIO ---
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