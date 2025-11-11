namespace EventoSismicoApp.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public Usuario Usuario { get; set; }

        public Empleado() { }
        public Empleado(string nombre, string apellido, string mail, string telefono)
        {
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Telefono = telefono;
        }

        // Método del diagrama. Viejo
        //public bool EsTuUsuario(Usuario usuario) => this.Usuario.Nombre == usuario.Nombre;
        public bool EsTuUsuario(Usuario usuario)
        {
            // --- INICIO DE CAMBIO ---
            // Antes: return this.Usuario.Nombre == usuario.Nombre;
            // Ahora:
            return this.Usuario == usuario;
            // --- FIN DE CAMBIO ---
        }
    }
}