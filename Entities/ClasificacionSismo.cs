namespace EventoSismicoApp.Entities
{
    public class ClasificacionSismo
    {
        public double KmProfundidadDesde { get; set; }
        public double KmProfundidadHasta { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }

        public ClasificacionSismo() { }

        public ClasificacionSismo(string nombre, double desde, double hasta)
        {
            Nombre = nombre;
            KmProfundidadDesde = desde;
            KmProfundidadHasta = hasta;
        }

        // Método del diagrama
        public string getNombre() => Nombre;
    }
}