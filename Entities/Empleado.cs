namespace EventoSismicoApp.Entities
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public Usuario Usuario { get; set; }

        public Empleado(string nombre, string apellido, string mail, string telefono)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Telefono = telefono;
        }

        // Método del diagrama
        public bool EsTuUsuario(Usuario usuario) => this.Usuario.Nombre == usuario.Nombre;
    }
}