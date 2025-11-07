
namespace EventoSismicoApp.Entities
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public Empleado Empleado { get; set; }

        public Usuario(string nombre, string contraseña, Empleado empleado)
        {
            Nombre = nombre;
            Contraseña = contraseña;
            Empleado = empleado;
        }

        // Método del diagrama
        public Empleado GetEmpleado() => this.Empleado;

    }
}