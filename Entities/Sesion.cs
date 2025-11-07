namespace EventoSismicoApp.Entities
{
    public class Sesion
    {
        public DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }
        public Usuario Usuario { get; set; }

        public Sesion(DateTime fechaHoraInicio, DateTime? fechaHoraFin, Usuario usuario)
        {
            FechaHoraInicio = fechaHoraInicio;
            FechaHoraFin = fechaHoraFin;
            Usuario = usuario;
        }

        // Método del diagrama
        public Empleado GetEmpleadoEnSesion()
        {
            //return this.Usuario.GetEmpleado(); // Usuario ya tiene referencia a Empleado
            return this.Usuario.GetEmpleado();
        }
    }
}