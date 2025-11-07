namespace EventoSismicoApp.Entities
{
    public class OrigenDeGeneracion
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public OrigenDeGeneracion(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        // Método del diagrama
        public string getNombre() => Nombre;
    }
}