namespace EventoSismicoApp.Entities
{
    public class EstacionSismologica
    {
        public int Id { get; set; }
        public string CodigoEstacion { get; set; }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public EstacionSismologica() { }
        public EstacionSismologica(string codigoEstacion, string nombre, double latitud, double longitud)
        {
            CodigoEstacion = codigoEstacion;
            Nombre = nombre;
            Latitud = latitud;
            Longitud = longitud;
        }

        // Método del diagrama
        public string GetNombre() => Nombre;
    }
}